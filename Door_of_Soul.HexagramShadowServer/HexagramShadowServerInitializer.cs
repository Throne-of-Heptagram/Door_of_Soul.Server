using Door_of_Soul.Communication.HexagramNodeServer;
using Door_of_Soul.Communication.HexagramNodeServer.HexagramCentral;
using Door_of_Soul.Communication.HexagramNodeServer.HexagramCentral.OperationRouter;
using Door_of_Soul.Communication.HexagramNodeServer.Shadow;
using Door_of_Soul.Communication.Protocol.Hexagram.Shadow;
using Door_of_Soul.Core.HexagramNodeServer;
using Door_of_Soul.Server;
using System;

namespace Door_of_Soul.HexagramShadowServer
{
    public class HexagramShadowServerInitializer : ServerInitializer
    {
        public override bool Initialize(out string errorMessage)
        {
            try
            {
                HexagramForwardOperationRouter<ShadowForwardOperationCode>.Initialize(new ShadowForwardOperationRouter());
                HexagramOperationRequestRouter<ShadowHexagramEntrance, VirtualShadow, ShadowOperationCode >.Initialize(new ShadowOperationRequestRouter());
                VirtualShadow.Initialize(new HexagramShadow());

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
