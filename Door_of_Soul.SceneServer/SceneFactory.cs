﻿using Door_of_Soul.Core;

namespace Door_of_Soul.SceneServer
{
    class SceneFactory : GenericSubjectRepository<int, ObservableScene>
    {
        public static SceneFactory Instance { get; private set; } = new SceneFactory();

        private SceneFactory()
        {

        }
    }
}
