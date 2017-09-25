using Door_of_Soul.Core.HexagramNodeServer;

namespace Door_of_Soul.HexagramEternityServer
{
    class HexagramEternity : VirtualEternity
    {
        public override string ToString()
        {
            return $"Hexagram{base.ToString()}";
        }
    }
}
