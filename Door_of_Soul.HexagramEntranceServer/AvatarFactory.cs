using Door_of_Soul.Core;
namespace Door_of_Soul.HexagramEntranceServer
{
    public class AvatarFactory : GenericSubjectRepository<int, HexagramEntranceAvatar>
    {
        public static AvatarFactory Instance { get; private set; } = new AvatarFactory();

        private AvatarFactory()
        {

        }
    }
}
