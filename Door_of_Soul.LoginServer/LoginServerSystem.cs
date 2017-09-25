using Door_of_Soul.Communication.LoginServer;
using Door_of_Soul.Communication.LoginServer.System;
using Door_of_Soul.Communication.Protocol.External.System;
using Door_of_Soul.Core.LoginServer;
using Door_of_Soul.Core.Protocol;
using Door_of_Soul.Database.Repository;
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
                //returnCode = StartLoadProxyAnswerProcess(deviceId, answerId, out errorMessage);
            }
            return returnCode;
        }
    }
}
