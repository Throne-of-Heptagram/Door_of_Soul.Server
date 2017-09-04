using Door_of_Soul.Core;

namespace Door_of_Soul.HexagramEntranceServer
{
    class EntityFactory : GenericSubjectRepository<int, HexagramEntranceEntity>
    {
        public static EntityFactory Instance { get; private set; } = new EntityFactory();

        private EntityFactory()
        {

        }
    }
}
