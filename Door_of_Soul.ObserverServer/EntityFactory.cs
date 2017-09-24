using Door_of_Soul.Core;

namespace Door_of_Soul.ObserverServer
{
    class EntityFactory : GenericSubjectRepository<int, ObserverEntity>
    {
        public static EntityFactory Instance { get; private set; } = new EntityFactory();

        private EntityFactory()
        {

        }
    }
}
