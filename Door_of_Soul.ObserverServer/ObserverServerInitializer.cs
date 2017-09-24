using Door_of_Soul.Communication.ObserverServer;
using Door_of_Soul.Core.ObserverServer;
using Door_of_Soul.Server;
using System;

namespace Door_of_Soul.ObserverServer
{
    public class ObserverServerInitializer : ServerInitializer
    {
        public override bool Initialize(out string errorMessage)
        {
            try
            {
                ResourceService.Initialize(new ObserverServerResourceService());
                VirtualSystem.Initialize(new ObserverServerSystem());

                errorMessage = "";
                return true;
            }
            catch (Exception exception)
            {
                errorMessage = $"{exception}:{exception.StackTrace}";
                return false;
            }
        }
    }
}
