using Door_of_Soul.Core.HexagramNodeServer;

namespace Door_of_Soul.HexagramLifeServer
{
    class LifeAvatar : VirtualAvatar
    {
        public LifeAvatar(int avatarId, int entityId, string avatarName) : base(avatarId, entityId, avatarName)
        {
        }
        public override string ToString()
        {
            return $"Life{base.ToString()}";
        }
    }
}
