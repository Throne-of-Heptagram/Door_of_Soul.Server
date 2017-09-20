using Door_of_Soul.Core;
using Door_of_Soul.Database.DataStructure;

namespace Door_of_Soul.HexagramThroneServer
{
    class AnswerFactory : GenericSubjectRepository<int, ThroneAnswer>
    {
        public static AnswerFactory Instance { get; private set; } = new AnswerFactory();

        private AnswerFactory()
        {

        }

        public bool Create(AnswerData answerData, out ThroneAnswer answer)
        {
            answer = new ThroneAnswer(answerData.answerId, answerData.answerName);
            for (int i = 0; i < answerData.soulIds.Length; i++)
            {
                answer.LinkSoul(answerData.soulIds[i]);
            }
            return Add(answerData.answerId, answer);
        }
    }
}
