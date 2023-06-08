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
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using System.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Windows.Markup;
using System.Windows.Controls.Primitives;
using System.Threading.Channels;

namespace Client
{
    /// <summary>
    /// Логика взаимодействия для LogInPage.xaml
    /// </summary>
    public partial class LogInPage : Window
    {

        public LogInPage()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(Properties.Settings.Default.Remember)
            {
                this.Login.Text = Properties.Settings.Default.Login;
                this.Password.Text = Properties.Settings.Default.Password;
                this.Remember.IsChecked = Properties.Settings.Default.Remember;
            }
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            Registration registrationWindow = new Registration();
            registrationWindow.Show();
            Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if((bool)this.Remember.IsChecked)
            {
                Properties.Settings.Default.Login = Login.Text;
                Properties.Settings.Default.Password = Password.Text;
                Properties.Settings.Default.Remember = (bool)Remember.IsChecked;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.Login = "";
                Properties.Settings.Default.Password = "";
                Properties.Settings.Default.Remember = false;
                Properties.Settings.Default.Save();
            }
            string connectionString = "Data Source=DESKTOP-OCDVJBU\\SQLEXPRESS02;Initial Catalog=OurMessangerDB;Integrated Security=True;TrustServerCertificate=true;";

            string login = Login.Text;
            string password = Password.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Users WHERE Login = @Login AND Password = @Password";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Login", login);
                    command.Parameters.AddWithValue("@Password", password);

                    int count = (int)command.ExecuteScalar();

                    if (count > 0)
                    {
                        string Nick = "";
                        string query1 = "SELECT Name FROM Users WHERE Login = @Login AND Password = @Password";
                        using (SqlCommand command1 = new SqlCommand(query1, connection))
                        {
                            command1.Parameters.AddWithValue("@Login", login);
                            command1.Parameters.AddWithValue("@Password", password);
                            object result = command1.ExecuteScalar();
                            if (result != null)
                            {
                                Nick = result.ToString();
                            }
                        }
                        Properties.Settings.Default.Nick = Nick;
                        MainWindow mainWindow = new MainWindow(Nick);
                        mainWindow.Show();
                        Hide();
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль!");
                    }
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Properties.Settings.Default.Login = "";
            Properties.Settings.Default.Password = "";
            Properties.Settings.Default.Remember = false;
            Properties.Settings.Default.Save();
        }
    }
}
