using Door_of_Soul.Communication.ProxyServer;
using Door_of_Soul.Communication.ProxyServer.Soul;
using Door_of_Soul.Core.Protocol;
using Door_of_Soul.Core.ProxyServer;

namespace Door_of_Soul.ProxyServer
{
    class ProxySoul : VirtualSoul
    {
        public ProxySoul(int soulId, string soulName, bool isActivated) : base(soulId, soulName, isActivated)
        {
        }

        public override OperationReturnCode GetHexagramEntranceAvatar(int deviceId, int avatarId, out string errorMessage)
        {
            lock(onGetHexagramEntranceAvatarResponseEventLock)
            {
                ProxyAvatar avatar;
                if (AvatarFactory.Instance.Find(avatarId, out avatar))
                {
                    TerminalDevice device;
                    if (DeviceFactory.Instance.Find(deviceId, out device))
                    {
                        SoulEventApi.LoadProxyAvatar(device, this, avatar);
                        errorMessage = "";
                        return OperationReturnCode.Successiful;
                    }
                    else
                    {
                        errorMessage = $"GetHexagramEntranceAvatar Failed DeviceId:{deviceId} not existed";
                        return OperationReturnCode.NotExisted;
                    }
                }
                else
                {
                    int callbackId = getHexagramEntranceAvatarResponseEventHandlerCounter++;
                    GetHexagramEntranceAvatarResponseEventHandler loadHexagramEntranceAvatarCallback = (callbackReturnCode, callbackMessage, callbackAvatar) =>
                    {
                        if (callbackReturnCode == OperationReturnCode.Successiful && callbackAvatar.AvatarId == avatarId)
                        {
                            TerminalDevice device;
                            if (DeviceFactory.Instance.Find(deviceId, out device))
                            {
                                SoulEventApi.LoadProxyAvatar(device, this, callbackAvatar);
                            }
                            OnGetHexagramEntranceAvatarResponse -= getHexagramEntranceAvatarResponseEventHandlerDictionary[callbackId];
                            getHexagramEntranceAvatarResponseEventHandlerDictionary.Remove(callbackId);
                        }
                    };
                    getHexagramEntranceAvatarResponseEventHandlerDictionary.Add(callbackId, loadHexagramEntranceAvatarCallback);
                    OnGetHexagramEntranceAvatarResponse += loadHexagramEntranceAvatarCallback;

                    SoulOperationRequestApi.GetHexagramEntranceAvatar(this, avatarId);
                    errorMessage = "";
                    return OperationReturnCode.Successiful;
                }
            }
        }

        protected override bool InstantiateAvatar(int avatarId, int entityId, string avatarName, int[] soulIds, out VirtualAvatar avatar)
        {
            ProxyAvatar proxyAvatar;
            if (AvatarFactory.Instance.Create(avatarId, entityId, avatarName, soulIds, out proxyAvatar))
            {
                avatar = proxyAvatar;
                return true;
            }
            else
            {
                avatar = null;
                return false;
            }
        }
    }
}
