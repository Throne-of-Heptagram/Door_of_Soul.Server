using Door_of_Soul.Core;

namespace Door_of_Soul.TrinityServer
{
    class AvatarFactory : GenericSubjectRepository<int, TrinityAvatar>
    {
        public static AvatarFactory Instance { get; private set; } = new AvatarFactory();

        private AvatarFactory()
        {

        }

        public bool Create(int avatarId, int entityId, string avatarName, int[] soulIds, out TrinityAvatar avatar)
        {
            avatar = new TrinityAvatar(avatarId, entityId, avatarName);
            for (int i = 0; i < soulIds.Length; i++)
            {
                avatar.LinkSoul(soulIds[i]);
            }
            return Add(avatarId, avatar);
        }
    }
}
