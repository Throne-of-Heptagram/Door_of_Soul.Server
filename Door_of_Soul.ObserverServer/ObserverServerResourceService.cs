using Door_of_Soul.Communication.ObserverServer;
using Door_of_Soul.Core.ObserverServer;

namespace Door_of_Soul.ObserverServer
{
    class ObserverServerResourceService : ResourceService
    {
        public override bool FindWorld(int worldId, out VirtualWorld world)
        {
            ObserverWorld observerWorld;
            if (WorldFactory.Instance.Find(worldId, out observerWorld))
            {
                world = observerWorld;
                return true;
            }
            else
            {
                world = observerWorld;
                return false;
            }
        }

        public override bool FindScene(int sceneId, out TerminalScene scene)
        {
            ObserverScene observerScene;
            if (SceneFactory.Instance.Find(sceneId, out observerScene))
            {
                scene = observerScene;
                return true;
            }
            else
            {
                scene = observerScene;
                return false;
            }
        }
    }
}
