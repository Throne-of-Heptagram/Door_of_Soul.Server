﻿using Door_of_Soul.Core;

namespace Door_of_Soul.HexagramEntranceServer
{
    class SceneFactory : GenericSubjectRepository<int, HexagramEntranceScene>
    {
        public static SceneFactory Instance { get; private set; } = new SceneFactory();

        private SceneFactory()
        {

        }
    }
}
