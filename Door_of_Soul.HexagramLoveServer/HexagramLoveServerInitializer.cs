using Door_of_Soul.Communication.HexagramNodeServer;
using Door_of_Soul.Communication.HexagramNodeServer.HexagramCentral;
using Door_of_Soul.Communication.HexagramNodeServer.HexagramCentral.OperationRouter;
using Door_of_Soul.Communication.HexagramNodeServer.Love;
using Door_of_Soul.Communication.Protocol.Hexagram.Love;
using Door_of_Soul.Core.HexagramNodeServer;
using Door_of_Soul.Server;
using System;

namespace Door_of_Soul.HexagramLoveServer
{
    public class HexagramLoveServerInitializer : ServerInitializer
    {
        public override bool Initialize(out string errorMessage)
        {
            try
            {
                HexagramForwardOperationRouter<LoveForwardOperationCode>.Initialize(new LoveForwardOperationRouter());
                HexagramOperationRequestRouter<LoveHexagramEntrance, VirtualLove, LoveOperationCode>.Initialize(new LoveOperationRequestRouter());
                VirtualLove.Initialize(new HexagramLove());

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
