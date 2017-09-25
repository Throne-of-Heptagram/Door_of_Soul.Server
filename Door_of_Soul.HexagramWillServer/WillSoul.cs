using Door_of_Soul.Core.HexagramNodeServer;

namespace Door_of_Soul.HexagramWillServer
{
    class WillSoul : VirtualSoul
    {
        public WillSoul(int soulId, string soulName, bool isActivated) : base(soulId, soulName, isActivated)
        {
        }
        public override string ToString()
        {
            return $"Will{base.ToString()}";
        }
    }
}
