using Chat_SignalR_Biznesowe.Authentication;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SignalRClient
{
    public partial class ChatWindow : Window
    {
        private IHubProxy chatHub;
        private Dictionary<string, string> user_dict;
        private String login;
        private readonly BackgroundWorker worker = new BackgroundWorker();


        public ChatWindow(IHubProxy chatHub, String login)
        {
            InitializeComponent();
            this.chatHub = chatHub;
       //     this.user_dict = user_dict;
            this.login = login;
            users_list.SelectedIndex = 0;
            chatHub.Invoke("MarkUserByID", login);

            lblLog.Content = "Zalogowany jako: " + login;

            chatHub.On<string, string>("BroadcastMessage", (name, message) =>
            this.Dispatcher.Invoke((Action)delegate
            {
               
                    klienci.Items.Add(String.Format("{0}: {1}\r", name, message));
              
            }));

            chatHub.On<Dictionary<string, string>>("SendDict", (dict) =>
            this.Dispatcher.Invoke((Action)delegate
            {
                Debug.WriteLine("sadas");
                user_dict = dict;
                users_list.Items.Clear();
                users_list.Items.Add("WSZYSCY");
                users_list.SelectedIndex = 0;
                foreach (KeyValuePair<string, string> entry in dict)
                {
                    users_list.Items.Add( entry.Value); 
                }
            }));

            chatHub.Invoke("SendLoggedUsersDictionary");
        }

      public bool AmILogged(String login)
        {
          if(users_list.Items.Contains(login))
          {
              return true;
              
              
          }
          return false;
        }

        private void send_Click(object sender, RoutedEventArgs e)
        {
            if(users_list.SelectedIndex == 0)
            {
                chatHub.Invoke("Send", login, tekst.Text);
            }
          
        }

        private void PlaceholdersListBox_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var item = ItemsControl.ContainerFromElement(sender as ListBox, e.OriginalSource as DependencyObject) as ListBoxItem;
            if (item != null)
            {

                chatHub.Invoke("WhoIsSendingPWToMe", login, item.Content.ToString());
                // PW_Window pw = new PW_Window(login, item.Content.ToString());
            }
        }

        public static void DoEvents()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background,
                                                  new Action(delegate { }));
        }

        private void tekst_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tekst.Text = "";

           
        }

      

     

        private void tekst_KeyDown(object sender, KeyEventArgs e)
        {            

            if (e.Key == Key.Enter)
            {
                chatHub.Invoke("Send", login, tekst.Text);
                tekst.Text = "";
            }
        }
        private string getId()
        {
            foreach(KeyValuePair<string, string> entry in user_dict)
            {
                if(entry.Value==login)
                {
                    return entry.Key;
                }
            }
            return "";
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            chatHub.Invoke("disconnect",getId());
            chatHub.Invoke("UnMarkUserByID", login);
        }

      

      
    }
}
