using Door_of_Soul.Core.HexagramEntranceServer;

namespace Door_of_Soul.HexagramEntranceServer
{
    class HexagramEntranceWorld : VirtualWorld
    {
        public override string ToString()
        {
            return $"Entrance{base.ToString()}";
        }
    }
}
