using Door_of_Soul.Core.HexagramEntranceServer;

namespace Door_of_Soul.HexagramEntranceServer
{
    class HexagramEntranceEntity : VirtualEntity
    {
        public override string ToString()
        {
            return $"Entrance{base.ToString()}";
        }
    }
}
