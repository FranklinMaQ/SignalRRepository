using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Chat_SignalR_Biznesowe.SignalRThings
{
    public class Server : Chat_SignalR_Biznesowe.SignalRThings.IServer
    {
        public IDisposable SignalR { get; set; }
     //   const string ServerURI = "http://localhost:8080";

        public void StartServer(String serverURI)
        {
            try
            {
                SignalR = WebApp.Start(serverURI);

                
            }
            catch (TargetInvocationException ex)
            {
                Debug.WriteLine(ex.Message.ToString());
                return;
            }
        }

        public void DisposeServer()
        {
            SignalR.Dispose();
        }

    }
}
