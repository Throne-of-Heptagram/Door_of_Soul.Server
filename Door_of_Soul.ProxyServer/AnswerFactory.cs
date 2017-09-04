﻿using Door_of_Soul.Core;

namespace Door_of_Soul.ProxyServer
{
    class AnswerFactory : GenericSubjectRepository<int, ProxyAnswer>
    {
        public static AnswerFactory Instance { get; private set; } = new AnswerFactory();

        private AnswerFactory()
        {

        }
    }
}
