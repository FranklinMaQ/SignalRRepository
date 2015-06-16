using Chat_SignalR_Biznesowe.Authentication;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace SignalRClient
{
    public partial class MainWindow : Window
    {
        IHubProxy chatHub;
        HubConnection connection;

        public MainWindow()
        {
            InitializeComponent(); 
        }

        private void connect_to_hub_Click(object sender, RoutedEventArgs e)
        { 
            try
            {
            connection = new HubConnection("http://" + ip_address.Text + ":8080");
            chatHub = connection.CreateHubProxy("chatHub");
            connection.Start().Wait();

            bool canUserAuthenticate = chatHub.Invoke<bool>("CanAuthenticate", login.Text, password.Password).Result;

                if(canUserAuthenticate == true)
                {
                    ChatWindow chat_window = new ChatWindow(chatHub, login.Text);
                    chat_window.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Użytkownik nie istnieje lub podane złe hasło! Spróbuj ponownie!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }

           
            }
            catch(Exception he)
            {
                MessageBox.Show("Serwer nie znaleziony! Czy został uruchomiony?", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
   }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
            connection.Dispose();
            
        }

      
    }
}
