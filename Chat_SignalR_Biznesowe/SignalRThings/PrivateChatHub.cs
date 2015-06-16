using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace Chat_SignalR_Biznesowe.SignalRThings
{
   public class PrivateChatHub : Hub
    {
       public void Send(string name, string message)
       {
          
           // Call the broadcastMessage method to update clients.
           Clients.All.broadcastMessage(name, message);
         //  Debug.WriteLine("Send");
       }
    }
}
