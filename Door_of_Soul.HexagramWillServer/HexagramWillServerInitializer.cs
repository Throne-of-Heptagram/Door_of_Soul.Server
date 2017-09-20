using Door_of_Soul.Communication.HexagramNodeServer.Hexagram;
using Door_of_Soul.Communication.HexagramNodeServer.Hexagram.OperationRouter;
using Door_of_Soul.Communication.HexagramNodeServer.Will;
using Door_of_Soul.Communication.Protocol.Hexagram.Will;
using Door_of_Soul.Core.HexagramNodeServer;
using Door_of_Soul.Server;
using System;

namespace Door_of_Soul.HexagramWillServer
{
    public class HexagramWillServerInitializer : ServerInitializer
    {
        public override bool Initialize(out string errorMessage)
        {
            try
            {
                HexagramForwardOperationRouter<WillForwardOperationCode>.Initialize(new WillForwardOperationRouter());
                HexagramOperationRequestRouter<WillEventCode, WillOperationCode, VirtualWill>.Initialize(new WillOperationRequestRouter());
                VirtualWill.Initialize(new HexagramWill());

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
