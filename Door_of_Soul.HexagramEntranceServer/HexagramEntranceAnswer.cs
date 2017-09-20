using Door_of_Soul.Communication.HexagramEntranceServer;
using Door_of_Soul.Communication.HexagramEntranceServer.Answer;
using Door_of_Soul.Communication.HexagramEntranceServer.Will;
using Door_of_Soul.Core.HexagramEntranceServer;
using Door_of_Soul.Core.Protocol;

namespace Door_of_Soul.HexagramEntranceServer
{
    class HexagramEntranceAnswer : VirtualAnswer
    {
        public HexagramEntranceAnswer(int answerId, string answerName) : base(answerId, answerName)
        {
        }

        public override OperationReturnCode GetWillSoul(int endPointId, int soulId, out string errorMessage)
        {
            lock (onGetWillSoulResponseEventLock)
            {
                HexagramEntranceSoul soul;
                if (SoulFactory.Instance.Find(soulId, out soul))
                {
                    TerminalEndPoint endPoint;
                    if (EndPointFactory.Instance.Find(endPointId, out endPoint))
                    {
                        errorMessage = "";
                        AnswerOperationResponseApi.GetHexagramEntranceSoul(endPoint, this, OperationReturnCode.Successiful, errorMessage, soul);
                        return OperationReturnCode.Successiful;
                    }
                    else
                    {
                        errorMessage = $"GetWillSoul Failed EndPointId:{endPointId} not existed";
                        return OperationReturnCode.NotExisted;
                    }
                }
                else
                {
                    int callbackId = getWillSoulResponseEventHandlerCounter++;
                    GetWillSoulResponseEventHandler loadWillSoulCallback = (callbackReturnCode, callbackMessage, callbackSoul) =>
                    {
                        if (callbackReturnCode == OperationReturnCode.Successiful && callbackSoul.SoulId == soulId)
                        {
                            TerminalEndPoint endPoint;
                            if (EndPointFactory.Instance.Find(endPointId, out endPoint))
                            {
                                AnswerOperationResponseApi.GetHexagramEntranceSoul(endPoint, this, callbackReturnCode, callbackMessage, soul);
                            }
                            OnGetWillSoulResponse -= getWillSoulResponseEventHandlerDictionary[callbackId];
                            getWillSoulResponseEventHandlerDictionary.Remove(callbackId);
                        }
                    };
                    getWillSoulResponseEventHandlerDictionary.Add(callbackId, loadWillSoulCallback);
                    OnGetWillSoulResponse += loadWillSoulCallback;

                    WillOperationRequestApi.GetWillSoul(soulId);
                    errorMessage = "";
                    return OperationReturnCode.Successiful;
                }
            }
        }

        protected override bool InstantiateSoul(int soulId, string soulName, bool isActivated, int answerId, int[] avatarIds, out VirtualSoul soul)
        {
            HexagramEntranceSoul hexagramEntranceSoul;
            if (SoulFactory.Instance.Create(soulId, soulName, isActivated, answerId, avatarIds, out hexagramEntranceSoul))
            {
                soul = hexagramEntranceSoul;
                return true;
            }
            else
            {
                soul = null;
                return false;
            }
        }
    }
}
