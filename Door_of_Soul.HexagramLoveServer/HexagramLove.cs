using Door_of_Soul.Core.HexagramNodeServer;

namespace Door_of_Soul.HexagramLoveServer
{
    class HexagramLove : VirtualLove
    {
        public override string ToString()
        {
            return $"Hexagram{base.ToString()}";
        }
    }
}
