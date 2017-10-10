using Door_of_Soul.Core;

namespace Door_of_Soul.HexagramThroneServer
{
    class AnswerFactory : GenericSubjectRepository<int, ThroneAnswer>
    {
        public static AnswerFactory Instance { get; private set; } = new AnswerFactory();

        private AnswerFactory()
        {

        }
    }
}
