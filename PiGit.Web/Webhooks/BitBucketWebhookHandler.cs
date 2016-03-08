using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.WebHooks;
using PiGit.Common;
using PiGit.Web.Signalr;

namespace PiGit.Web.Webhooks
{
    public class BitbucketWebHookHandler : WebHookHandler
    {
        public BitbucketWebHookHandler()
        {
            Receiver = BitbucketWebHookReceiver.ReceiverName;
        }

        public async override Task ExecuteAsync(string generator, WebHookHandlerContext context)
        {
            var action = context.Actions.First();

            var bitBucketAction = BitBucketActions.Actions.Where(a => a.Key == action).ToList();
            if (!bitBucketAction.Any())
                throw new Exception($"No such action exists, {action}");

            var message = new GitMessage { ActionType = bitBucketAction.First().Key };
            SendSignalrMessage(message);

            await Task.FromResult(true);
        }

        private static void SendSignalrMessage(GitMessage message)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<GitHub>();
            hubContext.Clients.Group(Settings.RestrictedGroupName).Notify(message);
        }
    }
}