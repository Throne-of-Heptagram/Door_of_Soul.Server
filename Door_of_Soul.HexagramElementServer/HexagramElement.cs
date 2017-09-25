using Door_of_Soul.Core.HexagramNodeServer;

namespace Door_of_Soul.HexagramElementServer
{
    class HexagramElement : VirtualElement
    {
        public override string ToString()
        {
            return $"Hexagram{base.ToString()}";
        }
    }
}
