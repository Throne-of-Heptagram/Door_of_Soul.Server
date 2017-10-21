﻿using Door_of_Soul.Communication.HexagramNodeServer;
using Door_of_Soul.Communication.HexagramNodeServer.HexagramCentral;
using Door_of_Soul.Communication.HexagramNodeServer.HexagramCentral.OperationRouter;
using Door_of_Soul.Communication.HexagramNodeServer.History;
using Door_of_Soul.Communication.Protocol.Hexagram.History;
using Door_of_Soul.Core.HexagramNodeServer;
using Door_of_Soul.Server;
using System;

namespace Door_of_Soul.HexagramHistoryServer
{
    public class HexagramHistoryServerInitializer : ServerInitializer
    {
        public override bool Initialize(out string errorMessage)
        {
            try
            {
                HexagramForwardOperationRouter<HistoryForwardOperationCode>.Initialize(new HistoryForwardOperationRouter());
                HexagramOperationRequestRouter<HistoryHexagramEntrance, VirtualHistory, HistoryOperationCode>.Initialize(new HistoryOperationRequestRouter());
                VirtualHistory.Initialize(new HexagramHistory());

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
