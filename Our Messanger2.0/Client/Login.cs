using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client
{
    public class Login
    {
        public Login()
        {

        }
        public void Log(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data Source=MIKHAILPC1;Initial Catalog=OurMessandgerDB;Integrated Security=True;TrustServerCertificate=true;";

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
                        MessageBox.Show("Login and password exist in the database.");
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        Window.GetWindow(this).Close();
                    }
                    else
                    {
                        MessageBox.Show("Login and password do not exist in the database. Register before work!");
                        Registration registrationWindow = new Registration();
                        registrationWindow.Show();
                        Window.GetWindow(this).Close();
                    }
                }
            }
        }
    }
}
