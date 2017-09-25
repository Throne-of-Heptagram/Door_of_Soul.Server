using Door_of_Soul.Core.ObserverServer;

namespace Door_of_Soul.ObserverServer
{
    class ObserverServerSystem : VirtualSystem
    {
        public override string ToString()
        {
            return $"Observer{base.ToString()}";
        }
    }
}
