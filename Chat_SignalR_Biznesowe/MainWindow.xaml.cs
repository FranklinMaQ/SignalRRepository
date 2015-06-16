﻿using System;
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
    public partial class MainWindow : Window
    {
        bool isStarted = false;
        public static MainWindow referencja;
        private IDisposable SignalR { get; set; }
        const string ServerURI = "http://localhost:8080";
        public MainWindow()
        {
            InitializeComponent();
            if (referencja == null)
                referencja = this;
        }
        public void AddClientInfo(String text)
        {
            Dispatcher.BeginInvoke(new Action(delegate() //Bo watek niema dostępu do kontrolek
            {
                ListBoxClients.Items.Add(text);
            }));
            //ListBoxClients.Items.Add(text);
        }
        private void run_server_Click(object sender, RoutedEventArgs e)
        {
            if (!isStarted)
            {
                isStarted = true;
                btnStart.Content = "Server Stop";
                Task.Run(() => StartServer());
            }
            else
            {
                isStarted = false;
                btnStart.Content = "Server Start";
                SignalR.Dispose();
            }
        }
        private void StartServer()
        {
            try
            {
                SignalR = WebApp.Start(ServerURI);
            }
            catch (TargetInvocationException ex)
            {
                Debug.WriteLine(ex.Message.ToString());
                return;
            }
        }
    }
}
