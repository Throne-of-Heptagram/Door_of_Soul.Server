using Door_of_Soul.Core;
using Door_of_Soul.Database.DataStructure;

namespace Door_of_Soul.HexagramLifeServer
{
    class AvatarFactory : GenericSubjectRepository<int, LifeAvatar>
    {
        public static AvatarFactory Instance { get; private set; } = new AvatarFactory();

        private AvatarFactory()
        {

        }

        public bool Create(AvatarData avatarData, out LifeAvatar avatar)
        {
            avatar = new LifeAvatar(avatarData.avatarId, avatarData.entityId, avatarData.avatarName);
            for (int i = 0; i < avatarData.soulIds.Length; i++)
            {
                avatar.LinkSoul(avatarData.soulIds[i]);
            }
            return Add(avatarData.avatarId, avatar);
        }
    }
}
