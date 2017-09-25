using Door_of_Soul.Core.HexagramNodeServer;

namespace Door_of_Soul.HexagramSpaceServer
{
    class HexagramSpace : VirtualSpace
    {
        public override string ToString()
        {
            return $"Hexagram{base.ToString()}";
        }

        public override bool FindScene(int sceneId, out VirtualScene scene)
        {
            throw new System.NotImplementedException();
        }

        public override bool FindWorld(int worldId, out VirtualWorld world)
        {
            throw new System.NotImplementedException();
        }
    }
}
