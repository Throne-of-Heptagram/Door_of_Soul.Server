using Door_of_Soul.Communication.HexagramNodeServer;
using Door_of_Soul.Communication.HexagramNodeServer.Will;
using Door_of_Soul.Communication.Protocol.Hexagram.Will;
using Door_of_Soul.Core.HexagramNodeServer;
using Door_of_Soul.Core.Protocol;
using Door_of_Soul.Database.DataStructure;
using Door_of_Soul.Database.Repository;
using System.Collections.Generic;

namespace Door_of_Soul.HexagramWillServer
{
    class HexagramWill : VirtualWill
    {
        public override OperationReturnCode GetWillSoul(int hexagramEntranceId, int soulId, out string errorMessage)
        {
            lock (getWillSoulLock)
            {
                OperationReturnCode returnCode = OperationReturnCode.Successiful;
                errorMessage = "";
                WillSoul soul;
                if (!SoulFactory.Instance.Find(soulId, out soul))
                {
                    SoulData soulData;
                    returnCode = SoulRepository.Instance.Read(soulId, out errorMessage, out soulData);
                    if (returnCode == OperationReturnCode.Successiful)
                    {
                        if (!SoulFactory.Instance.Create(soulData, out soul))
                        {
                            returnCode = OperationReturnCode.InstantiateFailed;
                            errorMessage = "GetWillSoul Instantiate Soul Failed";
                        }
                    }
                }
                WillHexagramEntrance hexagramEntrance;
                if (WillHexagramEntranceFactory.Instance.Find(hexagramEntranceId, out hexagramEntrance))
                {
                    if (returnCode == OperationReturnCode.Successiful)
                    {
                        WillOperationResponseApi.GetWillSoul(hexagramEntrance, OperationReturnCode.Successiful, errorMessage, soul);
                        return OperationReturnCode.Successiful;
                    }
                    else
                    {
                        WillOperationResponseApi.SendOperationResponse(hexagramEntrance, WillOperationCode.GetWillSoul, returnCode, errorMessage, new Dictionary<byte, object>());
                        return returnCode;
                    }
                }
                else
                {
                    errorMessage = $"GetWillSoul Failed HexagramEntranceId:{hexagramEntranceId} not existed";
                    return OperationReturnCode.NotExisted;
                }
            }
        }
    }
}
