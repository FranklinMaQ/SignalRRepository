using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRClient.ClientSignalRThings
{
    public class ChatHubConnection : SignalRClient.ClientSignalRThings.IChatHubConnection
    {
        private IHubProxy chatHub;
        private HubConnection connection;

    public IHubProxy ChatHub
    {
      get { return chatHub; }
      set { chatHub = value; }
    }        

    public HubConnection Connection
    {
      get { return connection; }
      set { connection = value; }
    }

        public void ConnectToChatHub(String address)
        {
            connection = new HubConnection("http://" + address + ":8080");
            chatHub = connection.CreateHubProxy("chatHub");
            connection.Start().Wait();
        }

        public bool CanUserAuthenticate(String username, String password)
        {
            return chatHub.Invoke<bool>("CanAuthenticate", username, password).Result;
        }
    }
}
