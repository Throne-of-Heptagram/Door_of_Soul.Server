using Door_of_Soul.Core.LoginServer;
using Door_of_Soul.Server;
using System;

namespace Door_of_Soul.LoginServer
{
    public class LoginServerInitializer : ServerInitializer
    {
        public override bool Initialize(out string errorMessage)
        {
            try
            {
                VirtualSystem.Initialize(new LoginServerSystem());

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
