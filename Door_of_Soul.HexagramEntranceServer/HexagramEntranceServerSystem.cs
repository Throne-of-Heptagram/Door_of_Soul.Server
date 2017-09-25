using Door_of_Soul.Communication.HexagramEntranceServer;
using Door_of_Soul.Communication.HexagramEntranceServer.System;
using Door_of_Soul.Communication.HexagramEntranceServer.Throne.Device;
using Door_of_Soul.Communication.Protocol.Internal.System;
using Door_of_Soul.Core.HexagramEntranceServer;
using Door_of_Soul.Core.Protocol;
using Door_of_Soul.Database.Repository;

namespace Door_of_Soul.HexagramEntranceServer
{
    class HexagramEntranceServerSystem : VirtualSystem
    {
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
    }
}
