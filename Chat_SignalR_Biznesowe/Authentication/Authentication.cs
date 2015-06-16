using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace Chat_SignalR_Biznesowe.Authentication
{
    public class Authentication
    {
        public static bool CanAuthenticate(String login, String password, String id)
        {
            if(UserDictionary.UserExist(login))
            {
                if(UserDictionary.Users_dictionary[login].Password == password)
                {
                    var x = GlobalHost.ConnectionManager.GetHubContext("chatHub");
                    User u = new User(login, password, Color.Red);
                    x.Clients.Client(id).SendOnlyToMe(UserDictionary.Active);
                    UserDictionary.Active.Add(login, u);
                    x.Clients.All.UpdateActiveList(u);
                    return true;    // password ok
                }
                else
                {
                    return false;   // bad password
                }
            }
            else
            {
                return false;       // user doesnt exist
            }
        }
    }
}
