using Door_of_Soul.Communication.HexagramNodeServer;
using Door_of_Soul.Communication.HexagramNodeServer.HexagramCentral;
using Door_of_Soul.Communication.HexagramNodeServer.HexagramCentral.OperationRouter;
using Door_of_Soul.Communication.HexagramNodeServer.Space;
using Door_of_Soul.Communication.Protocol.Hexagram.Space;
using Door_of_Soul.Core.HexagramNodeServer;
using Door_of_Soul.Server;
using System;

namespace Door_of_Soul.HexagramSpaceServer
{
    public class HexagramSpaceServerInitializer : ServerInitializer
    {
        public override bool Initialize(out string errorMessage)
        {
            try
            {
                HexagramForwardOperationRouter<SpaceForwardOperationCode>.Initialize(new SpaceForwardOperationRouter());
                HexagramOperationRequestRouter<SpaceHexagramEntrance, VirtualSpace, SpaceOperationCode>.Initialize(new SpaceOperationRequestRouter());
                VirtualSpace.Initialize(new HexagramSpace());

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
