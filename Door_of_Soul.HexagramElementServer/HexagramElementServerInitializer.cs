using Door_of_Soul.Communication.HexagramNodeServer;
using Door_of_Soul.Communication.HexagramNodeServer.Element;
using Door_of_Soul.Communication.HexagramNodeServer.HexagramCentral;
using Door_of_Soul.Communication.HexagramNodeServer.HexagramCentral.OperationRouter;
using Door_of_Soul.Communication.Protocol.Hexagram.Element;
using Door_of_Soul.Core.HexagramNodeServer;
using Door_of_Soul.Server;
using System;

namespace Door_of_Soul.HexagramElementServer
{
    public class HexagramElementServerInitializer : ServerInitializer
    {
        public override bool Initialize(out string errorMessage)
        {
            try
            {
                HexagramForwardOperationRouter<ElementForwardOperationCode>.Initialize(new ElementForwardOperationRouter());
                HexagramOperationRequestRouter<ElementHexagramEntrance, VirtualElement, ElementOperationCode>.Initialize(new ElementOperationRequestRouter());
                VirtualElement.Initialize(new HexagramElement());

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
