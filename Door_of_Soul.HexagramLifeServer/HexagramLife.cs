using Door_of_Soul.Communication.HexagramNodeServer;
using Door_of_Soul.Communication.HexagramNodeServer.Life;
using Door_of_Soul.Communication.Protocol.Hexagram.Life;
using Door_of_Soul.Core.HexagramNodeServer;
using Door_of_Soul.Core.Protocol;
using Door_of_Soul.Database.DataStructure;
using Door_of_Soul.Database.Repository;
using System.Collections.Generic;

namespace Door_of_Soul.HexagramLifeServer
{
    class HexagramLife : VirtualLife
    {
        public override OperationReturnCode GetLifeAvatar(int hexagramEntranceId, int avatarId, out string errorMessage)
        {
            lock (getLifeAvatarLock)
            {
                OperationReturnCode returnCode = OperationReturnCode.Successiful;
                errorMessage = "";
                LifeAvatar avatar;
                if (!AvatarFactory.Instance.Find(avatarId, out avatar))
                {
                    AvatarData avatarData;
                    returnCode = AvatarRepository.Instance.Read(avatarId, out errorMessage, out avatarData);
                    if (returnCode == OperationReturnCode.Successiful)
                    {
                        if (!AvatarFactory.Instance.Create(avatarData, out avatar))
                        {
                            returnCode = OperationReturnCode.InstantiateFailed;
                            errorMessage = "GetLifeAvatar Instantiate Avatar Failed";
                        }
                    }
                }
                LifeHexagramEntrance hexagramEntrance;
                if (LifeHexagramEntranceFactory.Instance.Find(hexagramEntranceId, out hexagramEntrance))
                {
                    if (returnCode == OperationReturnCode.Successiful)
                    {
                        LifeOperationResponseApi.GetLifeAvatar(hexagramEntrance, OperationReturnCode.Successiful, errorMessage, avatar);
                        return OperationReturnCode.Successiful;
                    }
                    else
                    {
                        LifeOperationResponseApi.SendOperationResponse(hexagramEntrance, LifeOperationCode.GetLifeAvatar, returnCode, errorMessage, new Dictionary<byte, object>());
                        return returnCode;
                    }
                }
                else
                {
                    errorMessage = $"GetLifeAvatar Failed HexagramEntranceId:{hexagramEntranceId} not existed";
                    return OperationReturnCode.NotExisted;
                }
            }
        }
    }
}
