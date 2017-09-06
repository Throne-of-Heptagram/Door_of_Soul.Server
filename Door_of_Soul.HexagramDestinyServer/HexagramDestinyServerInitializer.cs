using Door_of_Soul.Communication.HexagramNodeServer.Hexagram;
using Door_of_Soul.Communication.HexagramNodeServer.Hexagram.OperationRouter;
using Door_of_Soul.Communication.HexagramNodeServer.Destiny;
using Door_of_Soul.Communication.Protocol.Hexagram.Destiny;
using Door_of_Soul.Core.HexagramNodeServer;
using Door_of_Soul.Server;
using System;

namespace Door_of_Soul.HexagramDestinyServer
{
    public class HexagramDestinyServerInitializer : ServerInitializer
    {
        public override bool Initialize(out string errorMessage)
        {
            try
            {
                HexagramForwardOperationRouter<DestinyForwardOperationCode>.Initialize(new DestinyForwardOperationRouter());
                HexagramOperationRequestRouter<DestinyEventCode, DestinyOperationCode>.Initialize(new DestinyOperationRequestRouter());
                VirtualDestiny.Initialize(new HexagramDestiny());

                errorMessage = "";
                return true;
            }
            catch (Exception exception)
            {
                errorMessage = $"{exception}:{exception.StackTrace}";
                return false;
            }
        }
    }
}
