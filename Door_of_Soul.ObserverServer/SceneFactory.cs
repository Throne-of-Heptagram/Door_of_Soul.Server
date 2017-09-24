using Door_of_Soul.Core;

namespace Door_of_Soul.ObserverServer
{
    class SceneFactory : GenericSubjectRepository<int, ObserverScene>
    {
        public static SceneFactory Instance { get; private set; } = new SceneFactory();

        private SceneFactory()
        {

        }
    }
}
