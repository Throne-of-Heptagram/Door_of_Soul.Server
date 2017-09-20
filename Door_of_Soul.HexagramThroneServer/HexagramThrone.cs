using Door_of_Soul.Communication.HexagramNodeServer;
using Door_of_Soul.Communication.HexagramNodeServer.Throne;
using Door_of_Soul.Communication.Protocol.Hexagram.Throne;
using Door_of_Soul.Core.HexagramNodeServer;
using Door_of_Soul.Core.Protocol;
using Door_of_Soul.Database.DataStructure;
using Door_of_Soul.Database.Repository;
using System.Collections.Generic;

namespace Door_of_Soul.HexagramThroneServer
{
    class HexagramThrone : VirtualThrone
    {
        public override OperationReturnCode GetThroneAnswer(int hexagramEntranceId, int answerId, out string errorMessage)
        {
            lock (getThroneAnswerLock)
            {
                OperationReturnCode returnCode = OperationReturnCode.Successiful;
                errorMessage = "";
                ThroneAnswer answer;
                if (!AnswerFactory.Instance.Find(answerId, out answer))
                {
                    AnswerData answerData;
                    returnCode = AnswerRepository.Instance.Read(answerId, out errorMessage, out answerData);
                    if (returnCode == OperationReturnCode.Successiful)
                    {
                        if (!AnswerFactory.Instance.Create(answerData, out answer))
                        {
                            returnCode = OperationReturnCode.InstantiateFailed;
                            errorMessage = "GetThroneAnswer Instantiate Answer Failed";
                        }
                    }
                }
                ThroneHexagramEntrance hexagramEntrance;
                if (ThroneHexagramEntranceFactory.Instance.Find(hexagramEntranceId, out hexagramEntrance))
                {
                    if (returnCode == OperationReturnCode.Successiful)
                    {
                        ThroneOperationResponseApi.GetThroneAnswer(hexagramEntrance, OperationReturnCode.Successiful, errorMessage, answer);
                        return OperationReturnCode.Successiful;
                    }
                    else
                    {
                        ThroneOperationResponseApi.SendOperationResponse(hexagramEntrance, ThroneOperationCode.GetThroneAnswer, returnCode, errorMessage, new Dictionary<byte, object>());
                        return returnCode;
                    }
                }
                else
                {
                    errorMessage = $"GetThroneAnswer Failed HexagramEntranceId:{hexagramEntranceId} not existed";
                    return OperationReturnCode.NotExisted;
                }
            }
        }
    }
}
