﻿using Door_of_Soul.Core.HexagramEntranceServer;

namespace Door_of_Soul.HexagramEntranceServer
{
    class HexagramEntranceAnswer : VirtualAnswer
    {
        public HexagramEntranceAnswer(int answerId, string answerName, int accessEndPointId, string answerAccessToken) : base(answerId, answerName, accessEndPointId, answerAccessToken)
        {
        }

        public override string ToString()
        {
            return $"Entrance{base.ToString()}";
        }
    }
}
