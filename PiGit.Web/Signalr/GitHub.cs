using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using PiGit.Common;

namespace PiGit.Web.Signalr
{
    public class GitHub : Hub<IGitHub>
    {
        /// <summary>
        /// This logic allows us to send the notifications only to those clients
        /// who know the secret group name (Settings.RestrictedGroupName). With this solution I restrict
        /// the number of clients who can get the notification.
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public Task JoinGroup(string groupName)
        {
            return groupName == Settings.RestrictedGroupName
                ? Groups.Add(Context.ConnectionId, groupName)
                : Task.FromResult(false);
        }
    }
}