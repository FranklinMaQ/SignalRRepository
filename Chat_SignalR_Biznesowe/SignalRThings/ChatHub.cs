using System;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;
using System.Collections.Generic;
using Chat_SignalR_Biznesowe.Authentication;

namespace Chat_SignalR_Biznesowe
{
    public class ChatHub : Hub
    {

        public bool CanAuthenticate(String username, String password)
        {
            return Authentication.Authentication.CanAuthenticate(username, password, Context.ConnectionId.ToString());   
        }

        public void SendLoggedUsersDictionary()
        {
            Clients.All.SendDict(SignalRThings.ConnectedUsersList.ConnectedUsers);
            Debug.WriteLine("test");
        }

        public void Send(string name, string message)
        {
            Clients.All.broadcastMessage(name, message);
        }     

        public void MarkUserByID(String username)       // dodaj uzytkownika do haszmapy po id
        {
            MainWindow.referencja.AddClientInfo(username);//dodaje do listy na okienku
            SignalRThings.ConnectedUsersList.ConnectedUsers.Add(Context.ConnectionId.ToString(), username);
            Debug.WriteLine(Context.ConnectionId.ToString() + " " + username);
            Debug.WriteLine(SignalRThings.ConnectedUsersList.ConnectedUsers.Count.ToString() + " : items");
        }
    
        public override Task OnConnected()
        {
            Debug.WriteLine("Connected: " + Context.ConnectionId.ToString());
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }
    }
}