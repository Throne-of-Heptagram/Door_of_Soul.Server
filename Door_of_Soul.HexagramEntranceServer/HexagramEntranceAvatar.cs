using Door_of_Soul.Core.HexagramEntranceServer;

namespace Door_of_Soul.HexagramEntranceServer
{
    class HexagramEntranceAvatar : VirtualAvatar
    {
        public HexagramEntranceAvatar(int avatarId, int entityId, string avatarName) : base(avatarId, entityId, avatarName)
        {
        }
        public override string ToString()
        {
            return $"Entrance{base.ToString()}";
        }
    }
}
