﻿using Door_of_Soul.Core.HexagramNodeServer;

namespace Door_of_Soul.HexagramKnowledgeServer
{
    class HexagramKnowledge : VirtualKnowledge
    {
        public override string ToString()
        {
            return $"Hexagram{base.ToString()}";
        }
    }
}
