using Door_of_Soul.Core;

namespace Door_of_Soul.TrinityServer
{
    class AnswerFactory : GenericSubjectRepository<int, TrinityAnswer>
    {
        public static AnswerFactory Instance { get; private set; } = new AnswerFactory();

        private AnswerFactory()
        {

        }

        public bool Create(int answerId, string answerName, int[] soulIds, out TrinityAnswer answer)
        {
            answer = new TrinityAnswer(answerId, answerName);
            for(int i = 0; i < soulIds.Length; i++)
            {
                answer.LinkSoul(soulIds[i]);
            }
            return Add(answerId, answer);
        }
    }
}
