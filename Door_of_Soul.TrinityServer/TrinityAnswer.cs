using Door_of_Soul.Communication.TrinityServer;

namespace Door_of_Soul.TrinityServer
{
    class TrinityAnswer : TerminalAnswer
    {
        public TrinityAnswer(int answerId, string answerName) : base(answerId, answerName)
        {
        }
        public override string ToString()
        {
            return $"Trinity{base.ToString()}";
        }
    }
}
