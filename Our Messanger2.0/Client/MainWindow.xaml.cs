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

        public MainWindow(string Nick)
        {
            InitializeComponent();
            this.Nick = Nick;


            //ChatList.ItemsSource = ;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Connect.Height = 0;
        }
    }
}
