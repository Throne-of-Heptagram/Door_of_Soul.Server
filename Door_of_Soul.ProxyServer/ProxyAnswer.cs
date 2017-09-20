using Door_of_Soul.Communication.ProxyServer;
using Door_of_Soul.Core.Protocol;
using Door_of_Soul.Core.ProxyServer;
using Door_of_Soul.Communication.ProxyServer.Answer;

namespace Door_of_Soul.ProxyServer
{
    class ProxyAnswer : TerminalAnswer
    {
        public ProxyAnswer(int answerId, string answerName) : base(answerId, answerName)
        {
        }

        public override OperationReturnCode GetHexagramEntranceSoul(int deviceId, int soulId, out string errorMessage)
        {
            lock(onGetHexagramEntranceSoulResponseEventLock)
            {
                ProxySoul soul;
                if (SoulFactory.Instance.Find(soulId, out soul))
                {
                    TerminalDevice device;
                    if (DeviceFactory.Instance.Find(deviceId, out device))
                    {
                        AnswerEventApi.LoadProxySoul(device, this, soul);
                        errorMessage = "";
                        return OperationReturnCode.Successiful;
                    }
                    else
                    {
                        errorMessage = $"GetHexagramEntranceSoul Failed DeviceId:{deviceId} not existed";
                        return OperationReturnCode.NotExisted;
                    }
                }
                else
                {
                    int callbackId = getHexagramEntranceSoulResponseEventHandlerCounter++;
                    GetHexagramEntranceSoulResponseEventHandler loadHexagramEntranceSoulCallback = (callbackReturnCode, callbackMessage, callbackSoul) =>
                    {
                        if (callbackReturnCode == OperationReturnCode.Successiful && callbackSoul.SoulId == soulId)
                        {
                            TerminalDevice device;
                            if (DeviceFactory.Instance.Find(deviceId, out device))
                            {
                                AnswerEventApi.LoadProxySoul(device, this, callbackSoul);
                            }
                            OnGetHexagramEntranceSoulResponse -= getHexagramEntranceSoulResponseEventHandlerDictionary[callbackId];
                            getHexagramEntranceSoulResponseEventHandlerDictionary.Remove(callbackId);
                        }
                    };
                    getHexagramEntranceSoulResponseEventHandlerDictionary.Add(callbackId, loadHexagramEntranceSoulCallback);
                    OnGetHexagramEntranceSoulResponse += loadHexagramEntranceSoulCallback;

                    AnswerOperationRequestApi.GetHexagramEntranceSoul(this, soulId);
                    errorMessage = "";
                    return OperationReturnCode.Successiful;
                }
            }
        }

        protected override bool InstantiateSoul(int soulId, string soulName, bool isActivated, int answerId, int[] avatarIds, out VirtualSoul soul)
        {
            ProxySoul proxySoul;
            if (SoulFactory.Instance.Create(soulId, soulName, isActivated, answerId, avatarIds, out proxySoul))
            {
                soul = proxySoul;
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
