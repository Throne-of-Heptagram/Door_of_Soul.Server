using Door_of_Soul.Core.HexagramNodeServer;

namespace Door_of_Soul.HexagramThroneServer
{
    class ThroneAnswer : VirtualAnswer
    {
        public ThroneAnswer(int answerId, string answerName) : base(answerId, answerName)
        {
        }
        public override string ToString()
        {
            return $"Throne{base.ToString()}";
        }
    }
}
