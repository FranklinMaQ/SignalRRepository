using Chat_SignalR_Biznesowe.Authentication;
using Microsoft.AspNet.SignalR.Client;
using SignalRClient.ClientSignalRThings;
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
        IChatHubConnection connection;

        public MainWindow()
        {
            InitializeComponent();
            connection = new ChatHubConnection();
        }

        private void connect_to_hub_Click(object sender, RoutedEventArgs e)
        { 
            try
            {
                connection.ConnectToChatHub(ip_address.Text);

                bool canUserAuthenticate = connection.CanUserAuthenticate(login.Text, password.Password);

                if(canUserAuthenticate == true)
                {
                    ChatWindow chat_window = new ChatWindow(connection, login.Text);
                    chat_window.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Użytkownik nie istnieje lub podane złe hasło! Spróbuj ponownie!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }

           
            }
            catch(Exception x)
            {
                MessageBox.Show("Serwer nie znaleziony! Czy został uruchomiony?", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                Debug.WriteLine(x.ToString());
            }
   }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
          
            connection.Connection.Dispose();
            
        }

      
    }
}
