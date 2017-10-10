using Door_of_Soul.Communication.HexagramEntranceServer;
using Door_of_Soul.Communication.HexagramEntranceServer.System;
using Door_of_Soul.Communication.HexagramEntranceServer.Throne;
using Door_of_Soul.Communication.HexagramEntranceServer.Throne.Device;
using Door_of_Soul.Communication.Protocol.Internal.System;
using Door_of_Soul.Core.HexagramEntranceServer;
using Door_of_Soul.Core.Protocol;
using Door_of_Soul.Database.Repository.Throne;
using System.Collections.Generic;

namespace Door_of_Soul.HexagramEntranceServer
{
    class HexagramEntranceServerSystem : VirtualSystem
    {
        public override event GetAnswerTrinityServerResponseEventHandler OnGetAnswerTrinityServer;
        private int onGetAnswerTrinityServerEventIdCounter = 0;
        private object onGetAnswerTrinityServerEventLock = new object();
        private Dictionary<int, GetAnswerTrinityServerResponseEventHandler> onGetAnswerTrinityServerEventDictionary = new Dictionary<int, GetAnswerTrinityServerResponseEventHandler>();

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
            lock (onGetAnswerTrinityServerEventLock)
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
                    int callbackId = onGetAnswerTrinityServerEventIdCounter++;
                    GetAnswerTrinityServerResponseEventHandler callbackFunction = (callbackReturnCode, callbackOperationMessage, callbackTrinityServerEndPointId, callbackAnswerId, callbackAnswerAccessToken) =>
                    {
                        if (callbackAnswerId != answerId)
                            return;
                        lock (onGetAnswerTrinityServerEventLock)
                        {
                            TerminalEndPoint endPoint;
                            if (EndPointFactory.Instance.Find(endPointId, out endPoint))
                            {
                                SystemOperationResponseApi.GetAnswerTrinityServer(
                                    terminal: endPoint,
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
