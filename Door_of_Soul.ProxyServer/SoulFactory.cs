using Door_of_Soul.Core;

namespace Door_of_Soul.ProxyServer
{
    public class SoulFactory : GenericSubjectRepository<int, ProxySoul>
    {
        public static SoulFactory Instance { get; private set; } = new SoulFactory();

        private SoulFactory()
        {

        }
    }
}
