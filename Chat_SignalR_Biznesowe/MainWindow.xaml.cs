using System;
using System.Collections.Generic;
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
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Owin;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;
using System.Drawing;
using Chat_SignalR_Biznesowe.Authentication;

namespace Chat_SignalR_Biznesowe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IDisposable SignalR { get; set; }
        const string ServerURI = "http://localhost:8080";
       


        public MainWindow()
        {
            InitializeComponent();
            
            
        }
        
        public void AddClientInfo(String text)
        {
            clients_info.Items.Add(text);
        }

        private void run_server_Click(object sender, RoutedEventArgs e)
        {
           
            Task.Run(() => StartServer());
            
        }

    

        private void StartServer()
        {
            try
            {
                SignalR = WebApp.Start(ServerURI);
            
                MessageBox.Show("Server started. Connect me at localhost.");
            }
            catch (TargetInvocationException ex)
            {
             
                Debug.WriteLine(ex.Message.ToString());
            
                return;
            }
         

        }

     


     
        }
}
