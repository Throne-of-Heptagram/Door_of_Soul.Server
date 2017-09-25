using Door_of_Soul.Core.HexagramNodeServer;

namespace Door_of_Soul.HexagramHistoryServer
{
    class HexagramHistory : VirtualHistory
    {
        public override string ToString()
        {
            return $"Hexagram{base.ToString()}";
        }
    }
}
