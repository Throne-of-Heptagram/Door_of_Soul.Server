using Door_of_Soul.Core.HexagramNodeServer;

namespace Door_of_Soul.HexagramShadowServer
{
    class HexagramShadow : VirtualShadow
    {
        public override string ToString()
        {
            return $"Hexagram{base.ToString()}";
        }
    }
}
