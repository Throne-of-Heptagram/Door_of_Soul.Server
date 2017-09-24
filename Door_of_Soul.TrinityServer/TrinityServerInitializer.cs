using Door_of_Soul.Communication.TrinityServer;
using Door_of_Soul.Core.TrinityServer;
using Door_of_Soul.Server;
using System;

namespace Door_of_Soul.TrinityServer
{
    public class TrinityServerInitializer : ServerInitializer
    {
        public override bool Initialize(out string errorMessage)
        {
            try
            {
                ResourceService.Initialize(new TrinityServerResourceService());
                VirtualSystem.Initialize(new TrinityServerSystem());

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
