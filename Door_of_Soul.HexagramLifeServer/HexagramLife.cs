using Door_of_Soul.Core.HexagramNodeServer;

namespace Door_of_Soul.HexagramLifeServer
{
    class HexagramLife : VirtualLife
    {
        public override string ToString()
        {
            return $"Hexagram{base.ToString()}";
        }
    }
}
