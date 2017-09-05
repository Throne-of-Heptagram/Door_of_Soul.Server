using Door_of_Soul.Communication.ProxyServer;
using Door_of_Soul.Core.ProxyServer;

namespace Door_of_Soul.ProxyServer
{
    class ProxyServerResourceService : ResourceService
    {
        public override bool FindAnswer(int answerId, out TerminalAnswer answer)
        {
            ProxyAnswer proxyAnswer;
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
            ProxyAvatar proxyAvatar;
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
            ProxySoul proxySoul;
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
