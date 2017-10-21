using Door_of_Soul.Communication.LoginServer;
using Door_of_Soul.Communication.LoginServer.System;
using Door_of_Soul.Communication.Protocol.External.System;
using Door_of_Soul.Core;
using Door_of_Soul.Core.LoginServer;
using Door_of_Soul.Core.Protocol;
using Door_of_Soul.Database.DataStructure;
using Door_of_Soul.Database.Repository.Eternity;
using Door_of_Soul.Database.Repository.Throne;
using System.Collections.Generic;

namespace Door_of_Soul.LoginServer
{
    class LoginServerSystem : VirtualSystem
    {
        public override string ToString()
        {
            return $"Login{base.ToString()}";
        }

        public override OperationReturnCode Register(int deviceId, string answerName, string basicPassword, out string errorMessage)
        {
            OperationReturnCode returnCode = AnswerRepository.Instance.IsAnswerNameValid(answerName, out errorMessage);
            if (returnCode == OperationReturnCode.Successiful)
            {
                SystemOperationRequestApi.DeviceRegister(deviceId, answerName, basicPassword);
            }
            else
            {
                TerminalDevice device;
                if (DeviceFactory.Instance.Find(deviceId, out device))
                {
                    SystemOperationResponseApi.SendOperationResponse(device, SystemOperationCode.Register, returnCode, errorMessage, new Dictionary<byte, object>());
                }
                else
                {
                    errorMessage = $"Register Failed DeviceId:{deviceId} not existed";
                    returnCode = OperationReturnCode.NotExisted;
                }
            }
            return returnCode;
        }

        public override OperationReturnCode Login(int deviceId, string answerName, string basicPassword, out string errorMessage)
        {
            int answerId;
            OperationReturnCode returnCode = AnswerRepository.Instance.Login(answerName, basicPassword, out errorMessage, out answerId);
            if (returnCode == OperationReturnCode.Successiful)
            {
                TerminalDevice dependentDevice;
                List<IEventDependencyReleasable> eventDependentTargets = new List<IEventDependencyReleasable>();
                if(DeviceFactory.Instance.Find(deviceId, out dependentDevice))
                {
                    eventDependentTargets.Add(dependentDevice);
                }
                OnGetAnswerTrinityServer.RegisterEvent(
                    eventHandler: (callbackSubject, eventParameter) =>
                    {
                        if (eventParameter.answerId != answerId)
                            return false;
                        TerminalDevice device;
                        EndPointData trinityServerEndPointData;
                        string message;
                        returnCode = EndPointRepository.Instance.Read(eventParameter.trinityServerEndPointId, out message, out trinityServerEndPointData);
                        if (returnCode == OperationReturnCode.Successiful && DeviceFactory.Instance.Find(deviceId, out device))
                        {
                            SystemOperationResponseApi.Login(
                                terminal: device,
                                returnCode: eventParameter.returnCode,
                                operationMessage: eventParameter.operationMessage,
                                trinityServerAddress: trinityServerEndPointData.serverAddresses,
                                trinityServerPort: trinityServerEndPointData.serverPort,
                                trinityServerApplicationName: trinityServerEndPointData.serverApplicationName,
                                answerId: eventParameter.answerId,
                                answerAccessToken: eventParameter.answerAccessToken);
                        }
                        return true;
                    },
                    dependentTargets: eventDependentTargets);
                returnCode = GetAnswerTrinityServer(answerId, out errorMessage);
            }
            return returnCode;
        }

        public override OperationReturnCode GetAnswerTrinityServer(int answerId, out string errorMessage)
        {
            SystemOperationRequestApi.GetAnswerTrinityServer(answerId);
            errorMessage = "";
            return OperationReturnCode.Successiful;
        }

        public override void GetAnswerTrinityServerResponse(GetAnswerTrinityServerResponseParameter responseParameter)
        {
            OnGetAnswerTrinityServer.InvokeEvent(responseParameter);
        }
    }
}
