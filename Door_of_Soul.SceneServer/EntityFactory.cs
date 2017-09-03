using Door_of_Soul.Core;

namespace Door_of_Soul.SceneServer
{
    public class EntityFactory : GenericSubjectRepository<int, ObservableEntity>
    {
        public static EntityFactory Instance { get; private set; } = new EntityFactory();

        private EntityFactory()
        {

        }
    }
}
