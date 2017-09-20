using Door_of_Soul.Communication.HexagramNodeServer.Hexagram;
using Door_of_Soul.Communication.HexagramNodeServer.Hexagram.OperationRouter;
using Door_of_Soul.Communication.HexagramNodeServer.Infinite;
using Door_of_Soul.Communication.Protocol.Hexagram.Infinite;
using Door_of_Soul.Core.HexagramNodeServer;
using Door_of_Soul.Server;
using System;

namespace Door_of_Soul.HexagramInfiniteServer
{
    public class HexagramInfiniteServerInitializer : ServerInitializer
    {
        public override bool Initialize(out string errorMessage)
        {
            try
            {
                HexagramForwardOperationRouter<InfiniteForwardOperationCode>.Initialize(new InfiniteForwardOperationRouter());
                HexagramOperationRequestRouter<InfiniteEventCode, InfiniteOperationCode, VirtualInfinite>.Initialize(new InfiniteOperationRequestRouter());
                VirtualInfinite.Initialize(new HexagramInfinite());

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
