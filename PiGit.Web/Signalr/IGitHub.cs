using PiGit.Common;

namespace PiGit.Web.Signalr
{
    public interface IGitHub
    {
        void Notify(GitMessage message);
    }
}
