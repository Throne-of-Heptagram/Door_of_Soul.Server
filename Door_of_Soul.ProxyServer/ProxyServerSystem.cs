using Door_of_Soul.Communication.ProxyServer;
using Door_of_Soul.Communication.ProxyServer.System;
using Door_of_Soul.Core.Protocol;
using Door_of_Soul.Core.ProxyServer;
using Door_of_Soul.Database.Repository;

namespace Door_of_Soul.ProxyServer
{
    class ProxyServerSystem : VirtualSystem
    {
        public override OperationReturnCode Register(int deviceId, string answerName, string basicPassword, out string errorMessage)
        {
            int answerId;
            OperationReturnCode returnCode = AnswerRepository.Instance.Register(answerName, basicPassword, out errorMessage, out answerId);
            if(returnCode == OperationReturnCode.Successiful)
            {
                returnCode = StartLoadProxyAnswerProcess(deviceId, answerId, out errorMessage);
            }
            return returnCode;
        }

        public override OperationReturnCode Login(int deviceId, string answerName, string basicPassword, out string errorMessage)
        {
            int answerId;
            OperationReturnCode returnCode = AnswerRepository.Instance.Login(answerName, basicPassword, out errorMessage, out answerId);
            if (returnCode == OperationReturnCode.Successiful)
            {
                returnCode = StartLoadProxyAnswerProcess(deviceId, answerId, out errorMessage);
            }
            return returnCode;
        }

        protected override bool InstantiateAnswer(int answerId, string answerName, int[] soulIds, out VirtualAnswer answer)
        {
            ProxyAnswer proxyAnswer;
            if(AnswerFactory.Instance.Create(answerId, answerName, soulIds, out proxyAnswer))
            {
                answer = proxyAnswer;
                return true;
            }
            else
            {
                answer = null;
                return false;
            }
        }

        private OperationReturnCode StartLoadProxyAnswerProcess(int deviceId, int answerId, out string errorMessage)
        {
            ProxyAnswer answer;
            if (AnswerFactory.Instance.Find(answerId, out answer))
            {
                TerminalDevice device;
                if (DeviceFactory.Instance.Find(deviceId, out device))
                {
                    SystemEventApi.LoadProxyAnswer(device, answer);
                    errorMessage = "";
                    return OperationReturnCode.Successiful;
                }
                else
                {
                    errorMessage = $"StartLoadProxyAnswerProcess Failed DeviceId:{deviceId} not existed";
                    return OperationReturnCode.NotExisted;
                }
            }
            else
            {
                return GetHexagramEntranceAnswer(deviceId, answerId, out errorMessage);
            }
        }

        public override OperationReturnCode GetHexagramEntranceAnswer(int deviceId, int answerId, out string errorMessage)
        {
            lock (onGetHexagramEntranceAnswerResponseEventLock)
            {
                ProxyAnswer answer;
                if (AnswerFactory.Instance.Find(answerId, out answer))
                {
                    TerminalDevice device;
                    if (DeviceFactory.Instance.Find(deviceId, out device))
                    {
                        SystemEventApi.LoadProxyAnswer(device, answer);
                        errorMessage = "";
                        return OperationReturnCode.Successiful;
                    }
                    else
                    {
                        errorMessage = $"GetHexagramEntranceAnswer Failed DeviceId:{deviceId} not existed";
                        return OperationReturnCode.NotExisted;
                    }
                }
                else
                {
                    int callbackId = getHexagramEntranceAnswerResponseEventHandlerCounter++;
                    GetHexagramEntranceAnswerResponseEventHandler loadHexagramEntranceAnswerCallback = (callbackReturnCode, callbackMessage, callbackAnswer) =>
                    {
                        if (callbackReturnCode == OperationReturnCode.Successiful && callbackAnswer.AnswerId == answerId)
                        {
                            TerminalDevice device;
                            if (DeviceFactory.Instance.Find(deviceId, out device))
                            {
                                SystemEventApi.LoadProxyAnswer(device, callbackAnswer as TerminalAnswer);
                            }
                            OnGetHexagramEntranceAnswerResponse -= getHexagramEntranceAnswerResponseEventHandlerDictionary[callbackId];
                            getHexagramEntranceAnswerResponseEventHandlerDictionary.Remove(callbackId);
                        }
                    };
                    getHexagramEntranceAnswerResponseEventHandlerDictionary.Add(callbackId, loadHexagramEntranceAnswerCallback);
                    OnGetHexagramEntranceAnswerResponse += loadHexagramEntranceAnswerCallback;

                    SystemOperationRequestApi.GetHexagramEntranceAnswer(answerId);
                    errorMessage = "";
                    return OperationReturnCode.Successiful;
                }
            }
        }
    }
}
