using Door_of_Soul.Communication.HexagramNodeServer;
using Door_of_Soul.Communication.HexagramNodeServer.Throne.Device;
using Door_of_Soul.Communication.Protocol.Hexagram.Throne.Device;
using Door_of_Soul.Core.HexagramNodeServer;
using Door_of_Soul.Core.Protocol;
using Door_of_Soul.Database.Repository;

namespace Door_of_Soul.HexagramThroneServer
{
    class HexagramThrone : VirtualThrone
    {
        public override string ToString()
        {
            return $"Hexagram{base.ToString()}";
        }

        public override OperationReturnCode DeviceRegister(int entranceId, int endPointId, int deviceId, string answerName, string basicPassword, out string errorMessage)
        {
            int answerId;
            OperationReturnCode returnCode = AnswerRepository.Instance.Register(answerName, basicPassword, out errorMessage, out answerId);
            ThroneHexagramEntrance entrance;
            if (ThroneHexagramEntranceFactory.Instance.Find(entranceId, out entrance))
            {
                DeviceThroneOperationResponseApi.SendOperationResponse(entrance, endPointId, deviceId, DeviceThroneOperationCode.Register, returnCode, errorMessage, new System.Collections.Generic.Dictionary<byte, object>());
            }
            else
            {
                errorMessage = $"DeviceRegister Failed ThroneHexagramEntranceId:{entranceId} not existed";
                returnCode = OperationReturnCode.NotExisted;
            }
            return returnCode;
        }
    }
}
