using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using PiGit.Common;

namespace PyGit.Universal.Signalr
{
    class GitListener
    {
        public async Task Init()
        {
            var hubConnection = new HubConnection(Settings.HubUrl);
            var hubProxy = hubConnection.CreateHubProxy(Settings.HubName);
            hubProxy.On<GitMessage>(Settings.ClientMethod, gitMessage =>
            {
                MessageReceived?.Invoke(this, gitMessage);
            }
            );
            await hubConnection.Start();
            await hubProxy.Invoke("JoinGroup", Settings.RestrictedGroupName);
        }

        public event EventHandler<GitMessage> MessageReceived;
    }
}
