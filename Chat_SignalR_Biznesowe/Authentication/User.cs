using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_SignalR_Biznesowe.Authentication
{
    public class User
    {
        private String username;
        private String password;
        
        private String id;

        public String Id
        {
            get { return id; }
            set { id = value; }
        }

        public User(String username, String password, Color color)
        {
            this.username = username;
            this.password = password;
           
        }
        public String Username
        {
            get { return username; }
            set { username = value; }
        }
 
        public String Password
        {
            get { return password; }
            set { password = value; }
        }
       
      
    }
}
