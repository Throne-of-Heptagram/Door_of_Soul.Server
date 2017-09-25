using Door_of_Soul.Core.TrinityServer;

namespace Door_of_Soul.TrinityServer
{
    class TrinityServerSystem : VirtualSystem
    {
        public override string ToString()
        {
            return $"Trinity{base.ToString()}";
        }
    }
}
