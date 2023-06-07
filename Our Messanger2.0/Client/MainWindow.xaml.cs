using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string Nick;
        MessClient client;

        public MainWindow(string Nick)
        {
            InitializeComponent();
            this.Nick = Nick;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            client = new MessClient(Nick);
            _ = client.ConnectCommand;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Send_Click(object sender, RoutedEventArgs e)
        {
            client.Target = Target.Text;
            client.Message = Message.Text;
            _ = client.SendCommand;

            Message.Text = "";
        }
    }
}
