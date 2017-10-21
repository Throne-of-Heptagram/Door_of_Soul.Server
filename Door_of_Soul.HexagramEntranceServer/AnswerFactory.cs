using Door_of_Soul.Core;

namespace Door_of_Soul.HexagramEntranceServer
{
    class AnswerFactory : GenericSubjectRepository<int, HexagramEntranceAnswer>
    {
        public static AnswerFactory Instance { get; private set; } = new AnswerFactory();

        private AnswerFactory()
        {

        }

        public bool Create(int answerId, string answerName, int accessEndPointId, string answerAccessToken, int[] soulIds, out HexagramEntranceAnswer answer)
        {
            answer = new HexagramEntranceAnswer(answerId, answerName, accessEndPointId, answerAccessToken);
            for (int i = 0; i < soulIds.Length; i++)
            {
                answer.LinkSoul(soulIds[i]);
            }
            return Add(answerId, answer);
        }
    }
}
