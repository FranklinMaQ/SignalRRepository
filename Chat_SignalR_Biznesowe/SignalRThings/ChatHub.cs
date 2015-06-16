﻿using System;
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
            
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, message);
            Debug.WriteLine("Send");
        }     

        public void MarkUserByID(String username)       // dodaj uzytkownika do haszmapy po id
        {
            MainWindow.referencja.AddClientInfo(username);
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