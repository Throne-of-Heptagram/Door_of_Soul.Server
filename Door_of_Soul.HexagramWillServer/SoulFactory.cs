using Door_of_Soul.Core;
using Door_of_Soul.Database.DataStructure;

namespace Door_of_Soul.HexagramWillServer
{
    class SoulFactory : GenericSubjectRepository<int, WillSoul>
    {
        public static SoulFactory Instance { get; private set; } = new SoulFactory();

        private SoulFactory()
        {

        }

        public bool Create(SoulData soulData, out WillSoul soul)
        {
            soul = new WillSoul(soulData.soulId, soulData.soulName, false);
            soul.AnswerId = soulData.answerId;
            for (int i = 0; i < soulData.avatarIds.Length; i++)
            {
                soul.LinkAvatar(soulData.avatarIds[i]);
            }
            return Add(soulData.soulId, soul);
        }
    }
}
