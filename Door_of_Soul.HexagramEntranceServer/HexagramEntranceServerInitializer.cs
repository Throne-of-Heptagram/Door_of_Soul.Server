using Door_of_Soul.Core.HexagramEntranceServer;
using Door_of_Soul.Server;
using System;

namespace Door_of_Soul.HexagramEntranceServer
{
    public class HexagramEntranceServerInitializer : ServerInitializer
    {
        public override bool Initialize(out string errorMessage)
        {
            try
            {
                VirtualSystem.Initialize(new HexagramEntranceServerSystem());

                errorMessage = "";
                return true;
            }
            catch(Exception exception)
            {
                errorMessage = $"{exception}:{exception.StackTrace}";
                return false;
            }
        }
    }
}
