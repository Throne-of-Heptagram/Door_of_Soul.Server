﻿using Door_of_Soul.Core.HexagramNodeServer;

namespace Door_of_Soul.HexagramWillServer
{
    class HexagramWill : VirtualWill
    {
        public override string ToString()
        {
            return $"Hexagram{base.ToString()}";
        }
    }
}
