using Door_of_Soul.Communication.TrinityServer;
using Door_of_Soul.Core.TrinityServer;

namespace Door_of_Soul.TrinityServer
{
    class TrinityServerResourceService : ResourceService
    {
        public override bool FindAnswer(int answerId, out TerminalAnswer answer)
        {
            TrinityAnswer proxyAnswer;
            if (AnswerFactory.Instance.Find(answerId, out proxyAnswer))
            {
                answer = proxyAnswer;
                return true;
            }
            else
            {
                answer = proxyAnswer;
                return false;
            }
        }

        public override bool FindAvatar(int avatarId, out VirtualAvatar avatar)
        {
            TrinityAvatar proxyAvatar;
            if (AvatarFactory.Instance.Find(avatarId, out proxyAvatar))
            {
                avatar = proxyAvatar;
                return true;
            }
            else
            {
                avatar = proxyAvatar;
                return false;
            }
        }

        public override bool FindSoul(int soulId, out VirtualSoul soul)
        {
            TrinitySoul proxySoul;
            if (SoulFactory.Instance.Find(soulId, out proxySoul))
            {
                soul = proxySoul;
                return true;
            }
            else
            {
                soul = proxySoul;
                return false;
            }
        }
    }
}
