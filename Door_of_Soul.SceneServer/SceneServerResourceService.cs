using Door_of_Soul.Communication.SceneServer;
using Door_of_Soul.Core.SceneServer;

namespace Door_of_Soul.SceneServer
{
    class SceneServerResourceService : ResourceService
    {
        public override bool FindWorld(int worldId, out VirtualWorld world)
        {
            ObservableWorld observableWorld;
            if (WorldFactory.Instance.Find(worldId, out observableWorld))
            {
                world = observableWorld;
                return true;
            }
            else
            {
                world = observableWorld;
                return false;
            }
        }

        public override bool FindScene(int sceneId, out TerminalScene scene)
        {
            ObservableScene observableScene;
            if (SceneFactory.Instance.Find(sceneId, out observableScene))
            {
                scene = observableScene;
                return true;
            }
            else
            {
                scene = observableScene;
                return false;
            }
        }
    }
}
