using Door_of_Soul.Core;

namespace Door_of_Soul.ObserverServer
{
    class WorldFactory : GenericSubjectRepository<int, ObserverWorld>
    {
        public static WorldFactory Instance { get; private set; } = new WorldFactory();

        private WorldFactory()
        {

        }
    }
}
