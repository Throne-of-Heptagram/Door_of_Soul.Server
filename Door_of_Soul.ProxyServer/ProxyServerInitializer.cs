using Door_of_Soul.Core.ProxyServer;
using Door_of_Soul.Server;
using System;

namespace Door_of_Soul.ProxyServer
{
    public class ProxyServerInitializer : ServerInitializer
    {
        public override bool Initialize(out string errorMessage)
        {
            try
            {
                VirtualSystem.Initialize(new ProxyServerSystem());

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
