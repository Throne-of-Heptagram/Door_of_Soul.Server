using Door_of_Soul.Core.HexagramEntranceServer;

namespace Door_of_Soul.HexagramEntranceServer
{
    class HexagramEntranceSoul : VirtualSoul
    {
        public HexagramEntranceSoul(int soulId, string soulName, bool isActivated) : base(soulId, soulName, isActivated)
        {
        }
        public override string ToString()
        {
            return $"Entrance{base.ToString()}";
        }
    }
}
