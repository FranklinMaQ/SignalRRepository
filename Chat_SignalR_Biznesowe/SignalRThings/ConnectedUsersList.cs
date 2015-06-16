using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_SignalR_Biznesowe.SignalRThings
{
    public static class ConnectedUsersList
    {

        private static Dictionary<string, string> connectedUsers = new Dictionary<string, string>();

        public static Dictionary<string, string> ConnectedUsers
        {
            get { return ConnectedUsersList.connectedUsers; }
            set { ConnectedUsersList.connectedUsers = value; }
        }

    }
}
