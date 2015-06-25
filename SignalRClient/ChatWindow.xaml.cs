using Chat_SignalR_Biznesowe.Authentication;
using Microsoft.AspNet.SignalR.Client;
using SignalRClient.ClientSignalRThings;
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
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SignalRClient
{
    public partial class ChatWindow : Window
    {
      //  private IHubProxy chatHub;
        private IChatHubConnection connection;
        private Dictionary<string, string> user_dict;
        private String login;
        private readonly BackgroundWorker worker = new BackgroundWorker();


        public ChatWindow(IChatHubConnection connection, String login)
        {
            InitializeComponent();
            AddEmoticons();
            this.connection = connection;
       //     this.user_dict = user_dict;
            this.login = login;
            users_list.SelectedIndex = 0;
            connection.ChatHub.Invoke("MarkUserByID", login);

            lblLog.Content = "Zalogowany jako: " + login;

            connection.ChatHub.On<string, string>("BroadcastMessage", (name, message) =>
            this.Dispatcher.Invoke((Action)delegate
            {
               
                    chat_messages.Items.Add(String.Format("{0}: {1}\r", name, message));
                    UpdateScrollBar(chat_messages);

              
            }));

            connection.ChatHub.On<string, string>("SendPriv", (to, message) =>
            this.Dispatcher.Invoke((Action)delegate
            {
               
                    chat_messages.Items.Add(String.Format("PRIV: {0}: {1}\r", to, message));
              
            }));

            connection.ChatHub.On<Dictionary<string, string>>("SendDict", (dict) =>
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

            connection.ChatHub.Invoke("SendLoggedUsersDictionary");
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
                connection.ChatHub.Invoke("Send", login, tekst.Text);
            }
            else if(users_list.SelectedValue.ToString() == login)
            {
                MessageBox.Show("Nie możesz rozmawiać z samym sobą :)", "Krytyczny wyjątek", MessageBoxButton.OK, MessageBoxImage.Asterisk);
               
            }
            else
            {
                connection.ChatHub.Invoke("SendPrivate", login, users_list.SelectedValue, tekst.Text);
            }
          
        }

      

        private void tekst_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tekst.Text = "";

           
        }      

     

        private void tekst_KeyDown(object sender, KeyEventArgs e)
        {            

            if (e.Key == Key.Enter)
            {
                if (users_list.SelectedIndex == 0)
                {
                    connection.ChatHub.Invoke("Send", login, tekst.Text);
                }
                else if (users_list.SelectedValue.ToString() == login)
                {
                    MessageBox.Show("Nie możesz rozmawiać z samym sobą :)", "Krytyczny wyjątek", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                }
                else
                {
                    connection.ChatHub.Invoke("SendPrivate", login, users_list.SelectedValue, tekst.Text);
                }
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
            connection.ChatHub.Invoke("disconnect", getId());
            connection.ChatHub.Invoke("UnMarkUserByID", login);
        }

        private void UpdateScrollBar(ListBox listBox)
        {
            if (listBox != null)
            {
                var border = (Border)VisualTreeHelper.GetChild(listBox, 0);
                var scrollViewer = (ScrollViewer)VisualTreeHelper.GetChild(border, 0);
                scrollViewer.ScrollToBottom();
            }

        }

        private void AddEmoticons()
        {
            List<String> emoticons = new List<string>() { ":-)", ";-)", ":-(", ";-(", ":-P", ":-D", ":-X", ":-*", ":-C", ":-/", ":-O", ":-8" };

           foreach(String s in emoticons)
           {
                System.Windows.Controls.Button newBtn = new Button();

                newBtn.Content = s;
                newBtn.Name = "ButtonEmot";
                newBtn.Width = 50;
                newBtn.Click += (_sender, _args) =>
                {
                    tekst.Text = tekst.Text + " " + s;
                   
                    tekst.SelectionStart = tekst.Text.Length;
                    tekst.SelectionLength = 0;
                    tekst.Focus();
                    
                }; 

                emotes_panel.Children.Add(newBtn);
            }
        }

    }
}
