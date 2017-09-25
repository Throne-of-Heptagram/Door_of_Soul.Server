using Door_of_Soul.Core.HexagramNodeServer;

namespace Door_of_Soul.HexagramInfiniteServer
{
    class HexagramInfinite : VirtualInfinite
    {
        public override string ToString()
        {
            return $"Hexagram{base.ToString()}";
        }
    }
}
