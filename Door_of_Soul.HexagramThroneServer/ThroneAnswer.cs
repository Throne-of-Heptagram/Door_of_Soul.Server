using Door_of_Soul.Core.HexagramNodeServer;

namespace Door_of_Soul.HexagramThroneServer
{
    class ThroneAnswer : VirtualAnswer
    {
        public ThroneAnswer(int answerId, string answerName, int accessHexagramEntranceId, int accessEndPointId, string answerAccessToken) : base(answerId, answerName, accessHexagramEntranceId, accessEndPointId, answerAccessToken)
        {
        }

        public override string ToString()
        {
            return $"Throne{base.ToString()}";
        }
    }
}
