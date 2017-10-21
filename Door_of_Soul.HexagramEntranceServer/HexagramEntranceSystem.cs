using Door_of_Soul.Communication.HexagramEntranceServer;
using Door_of_Soul.Communication.HexagramEntranceServer.System;
using Door_of_Soul.Communication.HexagramEntranceServer.Throne;
using Door_of_Soul.Communication.HexagramEntranceServer.Throne.Device;
using Door_of_Soul.Communication.Protocol.Internal.System;
using Door_of_Soul.Core;
using Door_of_Soul.Core.HexagramEntranceServer;
using Door_of_Soul.Core.Protocol;
using Door_of_Soul.Database.Repository.Throne;
using System.Collections.Generic;

namespace Door_of_Soul.HexagramEntranceServer
{
    class HexagramEntranceSystem : VirtualSystem
    {
        private object getAnswerTrinityServerLock = new object();

        public override string ToString()
        {
            return $"Entrance{base.ToString()}";
        }

        public override OperationReturnCode DeviceRegister(int endPointId, int deviceId, string answerName, string basicPassword, out string errorMessage)
        {
            OperationReturnCode returnCode = AnswerRepository.Instance.IsAnswerNameValid(answerName, out errorMessage);
            if(returnCode == OperationReturnCode.Successiful)
            {
                DeviceThroneOperationRequestApi.Register(endPointId, deviceId, answerName, basicPassword);
            }
            else
            {
                TerminalEndPoint endPoint;
                if (EndPointFactory.Instance.Find(endPointId, out endPoint))
                {
                    SystemOperationResponseApi.SendDeviceOperationResponse(endPoint, deviceId, SystemOperationCode.DeviceRegister, returnCode, errorMessage, new System.Collections.Generic.Dictionary<byte, object>());
                }
                else
                {
                    errorMessage = $"DeviceRegister Failed EndPointId:{endPointId} not existed";
                    returnCode = OperationReturnCode.NotExisted;
                }
            }
            return returnCode;
        }

        public override OperationReturnCode GetAnswerTrinityServer(int endPointId, int answerId, out string errorMessage)
        {
            errorMessage = "";
            OperationReturnCode returnCode = OperationReturnCode.Successiful;
            lock (getAnswerTrinityServerLock)
            {
                HexagramEntranceAnswer answer;
                if(AnswerFactory.Instance.Find(answerId, out answer))
                {
                    TerminalEndPoint endPoint;
                    if (EndPointFactory.Instance.Find(endPointId, out endPoint))
                    {
                        SystemOperationResponseApi.GetAnswerTrinityServer(
                            terminal: endPoint,
                            returnCode: returnCode,
                            operationMessage: errorMessage,
                            trinityServerEndPointId: answer.AccessEndPointId,
                            answerId: answerId,
                            answerAccessToken: answer.AnswerAccessToken);
                    }
                    else
                    {
                        errorMessage = $"GetAnswerTrinityServer Failed EndPointId:{endPointId} not existed";
                        returnCode = OperationReturnCode.NotExisted;
                    }
                }
                else
                {
                    TerminalEndPoint dependentEndPoint;
                    List<IEventDependencyReleasable> eventDependentTargets = new List<IEventDependencyReleasable>();
                    if (EndPointFactory.Instance.Find(endPointId, out dependentEndPoint))
                    {
                        eventDependentTargets.Add(dependentEndPoint);
                    }
                    OnGetAnswerTrinityServer.RegisterEvent(
                        eventHandler: (callbackSubject, eventParameter) =>
                        {
                            if (eventParameter.answerId != answerId)
                                return false;
                            TerminalEndPoint endPoint;
                            if (EndPointFactory.Instance.Find(endPointId, out endPoint))
                            {
                                SystemOperationResponseApi.GetAnswerTrinityServer(
                                    terminal: endPoint,
                                    returnCode: eventParameter.returnCode,
                                    operationMessage: eventParameter.operationMessage,
                                    trinityServerEndPointId: eventParameter.trinityServerEndPointId,
                                    answerId: eventParameter.answerId,
                                    answerAccessToken: eventParameter.answerAccessToken);
                            }
                            return true;
                        },
                        dependentTargets: eventDependentTargets);
                    ThroneOperationRequestApi.GetAnswerTrinityServer(answerId);
                }
            }
            return returnCode;
        }

        public override void GetAnswerTrinityServerResponse(GetAnswerTrinityServerResponseParameter responseParameter)
        {
            lock (getAnswerTrinityServerLock)
            {
                OnGetAnswerTrinityServer.InvokeEvent(responseParameter);
            }
        }

        public override OperationReturnCode AssignAnswer(int answerId, out string errorMessage)
        {
            throw new System.NotImplementedException();
        }

        public override void AssignAnswerResponse(AssignAnswerResponseParameter responseParameter)
        {
            throw new System.NotImplementedException();
        }
    }
}
