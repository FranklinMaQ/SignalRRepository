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
        }

        public void Send(string name, string message)
        {
            Clients.All.broadcastMessage(name, message);
        }

        public void SendPrivate(string from, string to, string message)
        {
            
            string sender_id = getId(from);
            string receiver_id = getId(to);
            Clients.Client(sender_id).SendPriv("@" + to, message);
            Clients.Client(receiver_id).SendPriv("@" + from, message);
        }

        private string getId(string login)
        {
            foreach (KeyValuePair<string, string> entry in SignalRThings.ConnectedUsersList.ConnectedUsers)
            {
                if (entry.Value == login)
                {
                    return entry.Key;
                }
            }
            return "";
        }
     

        public void MarkUserByID(String username)       // dodaj uzytkownika do haszmapy po id
        {
            MainWindow.referencja.AddClientInfo(username);//dodaje do listy na okienku
            SignalRThings.ConnectedUsersList.ConnectedUsers.Add(Context.ConnectionId.ToString(), username);
            Debug.WriteLine(Context.ConnectionId.ToString() + " " + username);
            Debug.WriteLine(SignalRThings.ConnectedUsersList.ConnectedUsers.Count.ToString() + " : items");
            Clients.All.broadcastMessage("Server", username+" has connected");
        }

        public void UnMarkUserByID(string login)
        {
            MainWindow.referencja.deleteUserFromList(login);
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

        public void disconnect(String id)
        {
            string login= SignalRThings.ConnectedUsersList.ConnectedUsers[id];
            SignalRThings.ConnectedUsersList.ConnectedUsers.Remove(id);
            Clients.All.broadcastMessage("Server", login+" has disconnected! ");
            Clients.All.SendDict(SignalRThings.ConnectedUsersList.ConnectedUsers);
        }
    }
}