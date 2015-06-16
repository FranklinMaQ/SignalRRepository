using Microsoft.Owin;
using Owin;
using Chat_SignalR_Biznesowe;

[assembly: OwinStartup(typeof(Chat_SignalR_Biznesowe.Startup))]
namespace Chat_SignalR_Biznesowe
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
} 