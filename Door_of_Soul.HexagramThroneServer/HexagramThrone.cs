using Door_of_Soul.Communication.HexagramNodeServer;
using Door_of_Soul.Communication.HexagramNodeServer.Throne.Device;
using Door_of_Soul.Communication.HexagramNodeServer.Throne;
using Door_of_Soul.Communication.Protocol.Hexagram.Throne.Device;
using Door_of_Soul.Core.HexagramNodeServer;
using Door_of_Soul.Core.Protocol;
using Door_of_Soul.Database.Repository.Throne;
using System.Collections.Generic;
using System.Linq;

namespace Door_of_Soul.HexagramThroneServer
{
    class HexagramThrone : VirtualThrone
    {
        public override event GetAnswerTrinityServerResponseEventHandler OnGetAnswerTrinityServer;
        private int onGetAnswerTrinityServerEventIdCounter = 0;
        private object onGetAnswerTrinityServerEventLock = new object();
        private Dictionary<int, GetAnswerTrinityServerResponseEventHandler> onGetAnswerTrinityServerEventDictionary = new Dictionary<int, GetAnswerTrinityServerResponseEventHandler>();

        private int HexagramEntranceIdForAssignAnswer
        {
            get
            {
                int minAccessAnswerCount = -1;
                int minAccessAnswerEntranceId = 0;
                foreach (var entrance in ThroneHexagramEntranceFactory.Instance.Subjects)
                {
                    if(entrance.AccessAnswerCount > minAccessAnswerCount)
                    {
                        minAccessAnswerCount = entrance.AccessAnswerCount;
                        minAccessAnswerEntranceId = entrance.HexagramEntranceId;
                    }
                }
                return minAccessAnswerEntranceId;
            }
        }

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

        public override OperationReturnCode GetAnswerTrinityServer(int entranceId, int answerId, out string errorMessage)
        {
            errorMessage = "";
            OperationReturnCode returnCode = OperationReturnCode.Successiful;
            lock (onGetAnswerTrinityServerEventLock)
            {
                ThroneAnswer answer;
                if (AnswerFactory.Instance.Find(answerId, out answer))
                {
                    ThroneHexagramEntrance entrance;
                    if (ThroneHexagramEntranceFactory.Instance.Find(entranceId, out entrance))
                    {
                        ThroneOperationResponseApi.GetAnswerTrinityServer(
                            terminal: entrance,
                            returnCode: returnCode,
                            operationMessage: errorMessage,
                            trinityServerEndPointId: answer.AccessEndPointId,
                            answerId: answerId,
                            answerAccessToken: answer.AnswerAccessToken);
                    }
                    else
                    {
                        errorMessage = $"GetAnswerTrinityServer Failed HexagramEntranceId:{entranceId} not existed";
                        returnCode = OperationReturnCode.NotExisted;
                    }
                }
                else
                {
                    int callbackId = onGetAnswerTrinityServerEventIdCounter++;
                    GetAnswerTrinityServerResponseEventHandler callbackFunction = (callbackReturnCode, callbackOperationMessage, callbackTrinityServerEndPointId, callbackAnswerId, callbackAnswerAccessToken) =>
                    {
                        if (callbackAnswerId != answerId)
                            return;
                        lock (onGetAnswerTrinityServerEventLock)
                        {
                            ThroneHexagramEntrance entrance;
                            if (ThroneHexagramEntranceFactory.Instance.Find(entranceId, out entrance))
                            {
                                ThroneOperationResponseApi.GetAnswerTrinityServer(
                                    terminal: entrance,
                                    returnCode: callbackReturnCode,
                                    operationMessage: callbackOperationMessage,
                                    trinityServerEndPointId: callbackTrinityServerEndPointId,
                                    answerId: callbackAnswerId,
                                    answerAccessToken: callbackAnswerAccessToken);
                            }
                            if (onGetAnswerTrinityServerEventDictionary.ContainsKey(callbackId))
                            {
                                var self = onGetAnswerTrinityServerEventDictionary[callbackId];
                                OnGetAnswerTrinityServer -= self;
                                onGetAnswerTrinityServerEventDictionary.Remove(callbackId);
                            }
                        }
                    };
                    onGetAnswerTrinityServerEventDictionary.Add(callbackId, callbackFunction);
                    OnGetAnswerTrinityServer += callbackFunction;

                    //Assign Answer
                    ThroneOperationRequestApi.GetAnswerTrinityServer(answerId);
                }
            }
            return returnCode;
        }

        public override void GetAnswerTrinityServerResponse(OperationReturnCode returnCode, string operationMessage, int trinityServerEndPointId, int answerId, string answerAccessToken)
        {
            OnGetAnswerTrinityServer?.Invoke(returnCode, operationMessage, trinityServerEndPointId, answerId, answerAccessToken);
        }
    }
}
