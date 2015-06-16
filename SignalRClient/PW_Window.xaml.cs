using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SignalRClient
{
    /// <summary>
    /// Interaction logic for PW_Window.xaml
    /// </summary>
    public partial class PW_Window : Window
    {
        private string login;
        private string p;

      

        public PW_Window(string login, string p)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.login = login;
            this.p = p;

            MessageBox.Show(login + " " + p); 
        }
    }
}
