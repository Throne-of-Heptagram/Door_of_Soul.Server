namespace Door_of_Soul.Server
{
    public abstract class ServerInitializer
    {
        public static ServerInitializer Instance { get; private set; }
        public static void Initialize(ServerInitializer instance)
        {
            Instance = instance;
        }

        public abstract bool Initialize(out string errorMessage);
    }
}
