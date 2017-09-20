using Door_of_Soul.Communication.HexagramNodeServer.Hexagram;
using Door_of_Soul.Communication.HexagramNodeServer.Hexagram.OperationRouter;
using Door_of_Soul.Communication.HexagramNodeServer.Throne;
using Door_of_Soul.Communication.Protocol.Hexagram.Throne;
using Door_of_Soul.Core.HexagramNodeServer;
using Door_of_Soul.Server;
using System;

namespace Door_of_Soul.HexagramThroneServer
{
    public class HexagramThroneServerInitializer : ServerInitializer
    {
        public override bool Initialize(out string errorMessage)
        {
            try
            {
                HexagramForwardOperationRouter<ThroneForwardOperationCode>.Initialize(new ThroneForwardOperationRouter());
                HexagramOperationRequestRouter<ThroneEventCode, ThroneOperationCode, VirtualThrone>.Initialize(new ThroneOperationRequestRouter());
                VirtualThrone.Initialize(new HexagramThrone());

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
