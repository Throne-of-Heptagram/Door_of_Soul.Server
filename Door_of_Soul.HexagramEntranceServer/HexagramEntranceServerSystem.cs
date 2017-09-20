using Door_of_Soul.Communication.HexagramEntranceServer;
using Door_of_Soul.Communication.HexagramEntranceServer.System;
using Door_of_Soul.Communication.HexagramEntranceServer.Throne;
using Door_of_Soul.Core.HexagramEntranceServer;
using Door_of_Soul.Core.Protocol;

namespace Door_of_Soul.HexagramEntranceServer
{
    class HexagramEntranceServerSystem : VirtualSystem
    {
        public override OperationReturnCode GetThroneAnswer(int endPointId, int answerId, out string errorMessage)
        {
            lock (onGetThroneAnswerResponseEventLock)
            {
                HexagramEntranceAnswer answer;
                if (AnswerFactory.Instance.Find(answerId, out answer))
                {
                    TerminalEndPoint endPoint;
                    if (EndPointFactory.Instance.Find(endPointId, out endPoint))
                    {
                        errorMessage = "";
                        SystemOperationResponseApi.GetHexagramEntranceAnswer(endPoint, OperationReturnCode.Successiful, errorMessage, answer);
                        return OperationReturnCode.Successiful;
                    }
                    else
                    {
                        errorMessage = $"GetThroneAnswer Failed EndPointId:{endPointId} not existed";
                        return OperationReturnCode.NotExisted;
                    }
                }
                else
                {
                    int callbackId = getThroneAnswerResponseEventHandlerCounter++;
                    GetThroneAnswerResponseEventHandler loadThroneAnswerCallback = (callbackReturnCode, callbackMessage, callbackAnswer) =>
                    {
                        if (callbackReturnCode == OperationReturnCode.Successiful && callbackAnswer.AnswerId == answerId)
                        {
                            TerminalEndPoint endPoint;
                            if (EndPointFactory.Instance.Find(endPointId, out endPoint))
                            {
                                SystemOperationResponseApi.GetHexagramEntranceAnswer(endPoint, callbackReturnCode, callbackMessage, answer);
                            }
                            OnGetThroneAnswerResponse -= getThroneAnswerResponseEventHandlerDictionary[callbackId];
                            getThroneAnswerResponseEventHandlerDictionary.Remove(callbackId);
                        }
                    };
                    getThroneAnswerResponseEventHandlerDictionary.Add(callbackId, loadThroneAnswerCallback);
                    OnGetThroneAnswerResponse += loadThroneAnswerCallback;

                    ThroneOperationRequestApi.GetThroneAnswer(answerId);
                    errorMessage = "";
                    return OperationReturnCode.Successiful;
                }
            }
        }

        protected override bool InstantiateAnswer(int answerId, string answerName, int[] soulIds, out VirtualAnswer answer)
        {
            HexagramEntranceAnswer hexagramEntranceAnswer;
            if (AnswerFactory.Instance.Create(answerId, answerName, soulIds, out hexagramEntranceAnswer))
            {
                answer = hexagramEntranceAnswer;
                return true;
            }
            else
            {
                answer = null;
                return false;
            }
        }
    }
}
