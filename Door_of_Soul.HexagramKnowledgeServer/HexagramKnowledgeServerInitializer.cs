using Door_of_Soul.Communication.HexagramNodeServer.HexagramCentral;
using Door_of_Soul.Communication.HexagramNodeServer.HexagramCentral.OperationRouter;
using Door_of_Soul.Communication.HexagramNodeServer;
using Door_of_Soul.Communication.HexagramNodeServer.Knowledge;
using Door_of_Soul.Communication.Protocol.Hexagram.Knowledge;
using Door_of_Soul.Core.HexagramNodeServer;
using Door_of_Soul.Server;
using System;

namespace Door_of_Soul.HexagramKnowledgeServer
{
    public class HexagramKnowledgeServerInitializer : ServerInitializer
    {
        public override bool Initialize(out string errorMessage)
        {
            try
            {
                HexagramForwardOperationRouter<KnowledgeForwardOperationCode>.Initialize(new KnowledgeForwardOperationRouter());
                HexagramOperationRequestRouter<KnowledgeHexagramEntrance, VirtualKnowledge, KnowledgeOperationCode>.Initialize(new KnowledgeOperationRequestRouter());
                VirtualKnowledge.Initialize(new HexagramKnowledge());

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
