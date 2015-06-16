using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_SignalR_Biznesowe.Authentication
{
    public static class UserDictionary
    {
        private static Dictionary<string, User> users_dictionary = new Dictionary<string, User>();
        private static Dictionary<string, User> active = new Dictionary<string, User>();

        public static Dictionary<string, User> Active
        {
            get { return UserDictionary.active; }
            set { UserDictionary.active = value; }
        }
        public static Dictionary<string, User> Users_dictionary
        {
            get { return users_dictionary; }
            set { users_dictionary = value; }
        }

        static UserDictionary()
        {
            users_dictionary.Add("MaQ", new User("MaQ", "qwerty", Color.Green));
            users_dictionary.Add("VELO", new User("VELO", "qwerty", Color.Red));
            users_dictionary.Add("Pablo", new User("Pablo", "qwerty", Color.Red));
        }

        public static bool UserExist(String username)
        {
            return users_dictionary.ContainsKey(username);
        }
    }
}
