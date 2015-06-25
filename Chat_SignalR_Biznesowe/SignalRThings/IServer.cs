using System;
namespace Chat_SignalR_Biznesowe.SignalRThings
{
    public interface IServer
    {
        void StartServer(string serverURI);
        void DisposeServer();
    }
}
