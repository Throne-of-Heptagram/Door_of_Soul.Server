using Door_of_Soul.Core.Protocol;
using Door_of_Soul.Core.TrinityServer;

namespace Door_of_Soul.TrinityServer
{
    class TrinityServerSystem : VirtualSystem
    {
        public override OperationReturnCode AssignAnswer(int answerId, out string errorMessage)
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return $"Trinity{base.ToString()}";
        }
    }
}
