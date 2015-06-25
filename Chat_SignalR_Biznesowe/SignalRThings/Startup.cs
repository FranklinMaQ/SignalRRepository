using Microsoft.Owin;
using Owin;
using Chat_SignalR_Biznesowe;
using Microsoft.Owin.Cors;

[assembly: OwinStartup(typeof(Chat_SignalR_Biznesowe.Startup))]
namespace Chat_SignalR_Biznesowe
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
           
        }
    }
} 