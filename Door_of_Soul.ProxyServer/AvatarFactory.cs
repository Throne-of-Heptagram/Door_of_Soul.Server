using Door_of_Soul.Core;

namespace Door_of_Soul.ProxyServer
{
    class AvatarFactory : GenericSubjectRepository<int, ProxyAvatar>
    {
        public static AvatarFactory Instance { get; private set; } = new AvatarFactory();

        private AvatarFactory()
        {

        }
    }
}
