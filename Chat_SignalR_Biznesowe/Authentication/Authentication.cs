using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace Chat_SignalR_Biznesowe.Authentication
{
    public static class Authentication
    {
        public static bool CanAuthenticate(String login, String password, String id)
        {
            if(UserDictionary.UserExist(login))
            {
                if(UserDictionary.Users_dictionary[login].Password == password)
                {
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
