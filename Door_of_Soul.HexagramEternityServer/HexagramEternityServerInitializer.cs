using Door_of_Soul.Communication.HexagramNodeServer.Hexagram;
using Door_of_Soul.Communication.HexagramNodeServer.Hexagram.OperationRouter;
using Door_of_Soul.Communication.HexagramNodeServer.Eternity;
using Door_of_Soul.Communication.Protocol.Hexagram.Eternity;
using Door_of_Soul.Core.HexagramNodeServer;
using Door_of_Soul.Server;
using System;

namespace Door_of_Soul.HexagramEternityServer
{
    public class HexagramEternityServerInitializer : ServerInitializer
    {
        public override bool Initialize(out string errorMessage)
        {
            try
            {
                HexagramForwardOperationRouter<EternityForwardOperationCode>.Initialize(new EternityForwardOperationRouter());
                HexagramOperationRequestRouter<EternityEventCode, EternityOperationCode, VirtualEternity>.Initialize(new EternityOperationRequestRouter());
                VirtualEternity.Initialize(new HexagramEternity());

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
