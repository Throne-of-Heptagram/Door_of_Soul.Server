﻿using Door_of_Soul.Core;

namespace Door_of_Soul.HexagramEntranceServer
{
    public class AnswerFactory : GenericSubjectRepository<int, HexagramEntranceAnswer>
    {
        public static AnswerFactory Instance { get; private set; } = new AnswerFactory();

        private AnswerFactory()
        {

        }
    }
}