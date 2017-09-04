using Door_of_Soul.Core;

namespace Door_of_Soul.HexagramEntranceServer
{
    class WorldFactory : GenericSubjectRepository<int, HexagramEntranceWorld>
    {
        public static WorldFactory Instance { get; private set; } = new WorldFactory();

        private WorldFactory()
        {

        }
    }
}
