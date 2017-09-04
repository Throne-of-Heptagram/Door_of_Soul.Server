using Door_of_Soul.Communication.SceneServer;
using Door_of_Soul.Core.SceneServer;
using Door_of_Soul.Server;
using System;

namespace Door_of_Soul.SceneServer
{
    public class SceneServerInitializer : ServerInitializer
    {
        public override bool Initialize(out string errorMessage)
        {
            try
            {
                ResourceService.Initialize(new SceneServerResourceService());
                VirtualSystem.Initialize(new SceneServerSystem());

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
