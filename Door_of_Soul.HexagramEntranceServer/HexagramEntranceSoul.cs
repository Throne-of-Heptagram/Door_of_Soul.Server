using Door_of_Soul.Communication.HexagramEntranceServer;
using Door_of_Soul.Communication.HexagramEntranceServer.Life;
using Door_of_Soul.Communication.HexagramEntranceServer.Soul;
using Door_of_Soul.Core.HexagramEntranceServer;
using Door_of_Soul.Core.Protocol;

namespace Door_of_Soul.HexagramEntranceServer
{
    class HexagramEntranceSoul : VirtualSoul
    {
        public HexagramEntranceSoul(int soulId, string soulName, bool isActivated) : base(soulId, soulName, isActivated)
        {
        }

        public override OperationReturnCode GetLifeAvatar(int endPointId, int avatarId, out string errorMessage)
        {
            lock (onGetLifeAvatarResponseEventLock)
            {
                HexagramEntranceAvatar avatar;
                if (AvatarFactory.Instance.Find(avatarId, out avatar))
                {
                    TerminalEndPoint endPoint;
                    if (EndPointFactory.Instance.Find(endPointId, out endPoint))
                    {
                        errorMessage = "";
                        SoulOperationResponseApi.GetHexagramEntranceAvatar(endPoint, this, OperationReturnCode.Successiful, errorMessage, avatar);
                        return OperationReturnCode.Successiful;
                    }
                    else
                    {
                        errorMessage = $"GetLifeAvatar Failed EndPoint:{endPoint} not existed";
                        return OperationReturnCode.NotExisted;
                    }
                }
                else
                {
                    int callbackId = getLifeAvatarResponseEventHandlerCounter++;
                    GetLifeAvatarResponseEventHandler loadLifeAvatarCallback = (callbackReturnCode, callbackMessage, callbackAvatar) =>
                    {
                        if (callbackReturnCode == OperationReturnCode.Successiful && callbackAvatar.AvatarId == avatarId)
                        {
                            TerminalEndPoint endPoint;
                            if (EndPointFactory.Instance.Find(endPointId, out endPoint))
                            {
                                SoulOperationResponseApi.GetHexagramEntranceAvatar(endPoint, this, callbackReturnCode, callbackMessage, avatar);
                            }
                            OnGetLifeAvatarResponse -= getLifeAvatarResponseEventHandlerDictionary[callbackId];
                            getLifeAvatarResponseEventHandlerDictionary.Remove(callbackId);
                        }
                    };
                    getLifeAvatarResponseEventHandlerDictionary.Add(callbackId, loadLifeAvatarCallback);
                    OnGetLifeAvatarResponse += loadLifeAvatarCallback;

                    LifeOperationRequestApi.GetLifeAvatar(avatarId);
                    errorMessage = "";
                    return OperationReturnCode.Successiful;
                }
            }
        }

        protected override bool InstantiateAvatar(int avatarId, int entityId, string avatarName, int[] soulIds, out VirtualAvatar avatar)
        {
            HexagramEntranceAvatar hexagramEntranceAvatar;
            if (AvatarFactory.Instance.Create(avatarId, entityId, avatarName, soulIds, out hexagramEntranceAvatar))
            {
                avatar = hexagramEntranceAvatar;
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
