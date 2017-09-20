using Door_of_Soul.Communication.HexagramNodeServer.Hexagram;
using Door_of_Soul.Communication.HexagramNodeServer.Hexagram.OperationRouter;
using Door_of_Soul.Communication.HexagramNodeServer.Life;
using Door_of_Soul.Communication.Protocol.Hexagram.Life;
using Door_of_Soul.Core.HexagramNodeServer;
using Door_of_Soul.Server;
using System;

namespace Door_of_Soul.HexagramLifeServer
{
    public class HexagramLifeServerInitializer : ServerInitializer
    {
        public override bool Initialize(out string errorMessage)
        {
            try
            {
                HexagramForwardOperationRouter<LifeForwardOperationCode>.Initialize(new LifeForwardOperationRouter());
                HexagramOperationRequestRouter<LifeEventCode, LifeOperationCode, VirtualLife>.Initialize(new LifeOperationRequestRouter());
                VirtualLife.Initialize(new HexagramLife());

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
