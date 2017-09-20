using Door_of_Soul.Core;
namespace Door_of_Soul.HexagramEntranceServer
{
    class AvatarFactory : GenericSubjectRepository<int, HexagramEntranceAvatar>
    {
        public static AvatarFactory Instance { get; private set; } = new AvatarFactory();

        private AvatarFactory()
        {

        }

        public bool Create(int avatarId, int entityId, string avatarName, int[] soulIds, out HexagramEntranceAvatar avatar)
        {
            avatar = new HexagramEntranceAvatar(avatarId, entityId, avatarName);
            for (int i = 0; i < soulIds.Length; i++)
            {
                avatar.LinkSoul(soulIds[i]);
            }
            return Add(avatarId, avatar);
        }
    }
}
