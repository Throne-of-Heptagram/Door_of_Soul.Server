using Door_of_Soul.Core.HexagramNodeServer;

namespace Door_of_Soul.HexagramDestinyServer
{
    class HexagramDestiny : VirtualDestiny
    {
        public override string ToString()
        {
            return $"Hexagram{base.ToString()}";
        }
    }
}
