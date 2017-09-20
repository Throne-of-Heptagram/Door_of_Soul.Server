using Door_of_Soul.Core;

namespace Door_of_Soul.ProxyServer
{
    class SoulFactory : GenericSubjectRepository<int, ProxySoul>
    {
        public static SoulFactory Instance { get; private set; } = new SoulFactory();

        private SoulFactory()
        {

        }

        public bool Create(int soulId, string soulName, bool isActivated, int answerId, int[] avatarIds, out ProxySoul soul)
        {
            soul = new ProxySoul(soulId, soulName, isActivated);
            soul.AnswerId = answerId;
            for (int i = 0; i < avatarIds.Length; i++)
            {
                soul.LinkAvatar(avatarIds[i]);
            }
            return Add(soulId, soul);
        }
    }
}
