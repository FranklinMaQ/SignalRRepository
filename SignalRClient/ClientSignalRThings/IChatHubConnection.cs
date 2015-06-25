using System;
namespace SignalRClient.ClientSignalRThings
{
    public interface IChatHubConnection
    {
        bool CanUserAuthenticate(string username, string password);
        Microsoft.AspNet.SignalR.Client.IHubProxy ChatHub { get; set; }
        Microsoft.AspNet.SignalR.Client.HubConnection Connection { get; set; }
        void ConnectToChatHub(string address);
    }
}
