using Door_of_Soul.Core;

namespace Door_of_Soul.SceneServer
{
    class WorldFactory : GenericSubjectRepository<int, ObservableWorld>
    {
        public static WorldFactory Instance { get; private set; } = new WorldFactory();

        private WorldFactory()
        {

        }
    }
}
