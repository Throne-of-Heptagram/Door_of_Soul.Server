using Door_of_Soul.Core;

namespace Door_of_Soul.HexagramEntranceServer
{
    class SoulFactory : GenericSubjectRepository<int, HexagramEntranceSoul>
    {
        public static SoulFactory Instance { get; private set; } = new SoulFactory();

        private SoulFactory()
        {

        }
    }
}
