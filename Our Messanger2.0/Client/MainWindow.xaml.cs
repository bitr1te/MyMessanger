using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
        public string Nick = Properties.Settings.Default.Nick;
        public int ID = Properties.Settings.Default.ID;
        List<int> targetsId = new List<int>();
        List<string> targetsName = new List<string>();
        List<bool> sayHellow = new List<bool>();

        string con = "Data Source=DESKTOP-OCDVJBU\\SQLEXPRESS02;Initial Catalog=OurMessangerDB;Integrated Security=True;TrustServerCertificate=true;";

        public MainWindow()
        {
            InitializeComponent();

            ConfirmButton.Height = 0;
            NewChatBox.Height = 0;
            NewChatButton.Height = 0;
            RefreshChatButton.Height = 0;

            OkNickButton.Width = 0;
            NewNickBox.Width = 0;

            if (ChatList.SelectedIndex == -1)
            {
                Message.Height = 0;
                SendButton.Width = 0;
            }
            else
            {
                Message.Height = 20;
                SendButton.Height = 20;
            }


            Connect.Command.Execute(null);
            Button_Click(Connect, null);
        }

        public void RefreshChats()
        {
            targetsId.Clear();
            targetsName.Clear();

            SqlConnection connection = new SqlConnection(con);

            connection.Open();

            var cmd = new SqlCommand("SELECT Member2 FROM Chats JOIN Users ON ID_user = Member1 WHERE ID_user = @id", connection);
            cmd.Parameters.AddWithValue("@id", ID);
            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    int chat = (int)rd["Member2"];
                    targetsId.Add(chat);
                }
            }

            foreach (var id in targetsId)
            {
                var getName = new SqlCommand("SELECT Name FROM Users WHERE ID_user = @Id", connection);
                getName.Parameters.AddWithValue("@Id", id);

                using (getName)
                {
                    targetsName.Add((string)getName.ExecuteScalar());
                }
            }
            ChatList.ItemsSource = targetsName;
            ChatList.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            targetsId.Clear();
            targetsName.Clear();

            SqlConnection connection = new SqlConnection(con);

            connection.Open();

            var cmd = new SqlCommand("SELECT Member2 FROM Chats JOIN Users ON ID_user = Member1 WHERE ID_user = @id", connection);
            cmd.Parameters.AddWithValue("@id", ID);
            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    int chat = (int)rd["Member2"];
                    targetsId.Add(chat);
                }
            }

            foreach (var id in targetsId)
            {
                var getName = new SqlCommand("SELECT Name FROM Users WHERE ID_user = @Id", connection);
                getName.Parameters.AddWithValue("@Id", id);

                using (getName)
                {
                    targetsName.Add((string)getName.ExecuteScalar());
                    sayHellow.Add(false);
                }
            }

            ChatList.ItemsSource = targetsName;
            Connect.Height = 0;

            NewChatButton.Height = 20;
            RefreshChatButton.Height = 20;

            if (ChatList.SelectedIndex == -1)
            {
                SendButton.Height = 0;
                SendButton.Width = 98;
                Message.Height = 0;
                return;
            }
            else
            {
                SendButton.Height = 20;
                SendButton.Width = 98;
                Message.Height = 20;
            }

            int targetId = targetsId[ChatList.SelectedIndex];
            Properties.Settings.Default.TargetId = targetId;
        }

        private void ChatList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshChatBox();
            RefreshChats();

            SendButton.IsEnabled = true;

            SendButton.Height = 20;
            Message.Height = 20;

            int targetId = targetsId[ChatList.SelectedIndex];
            Properties.Settings.Default.TargetId = targetId;

            if (ChatList.SelectedIndex == -1)
                return;

            if (sayHellow.Count > 0)
            {
                try
                {
                    if (!sayHellow[ChatList.SelectedIndex])
                    {
                        SendButton.Command.Execute(null);
                        sayHellow[ChatList.SelectedIndex] = true;
                    }
                }
                catch
                {
                    sayHellow.Add(true);
                    SendButton.Command.Execute(null);
                }
            }
        }

        private void RefreshChatBox()
        {
            ChatBox.Clear();
            int targetId = targetsId[ChatList.SelectedIndex];
            Properties.Settings.Default.TargetId = targetId;

            SqlConnection connection = new SqlConnection(con);
            connection.Open();

            int id = -1;
            var cmd1 = new SqlCommand("SELECT ID_chat FROM Chats WHERE Member1 = @ID AND Member2 = @targetId", connection);
            cmd1.Parameters.AddWithValue("ID", ID);
            cmd1.Parameters.AddWithValue("targetId", targetId);

            using (cmd1)
            {
                id = ((int)cmd1.ExecuteScalar());
            }


            var cmd2 = new SqlCommand("SELECT Message FROM Messages WHERE ID_chat = @Id ORDER BY Date", connection);
            cmd2.Parameters.AddWithValue("@Id", id);
            using (var rd = cmd2.ExecuteReader())
            {
                while (rd.Read())
                {
                    ChatBox.Text += $"{rd["Message"]}\n";
                }
            }
        }

        private void Message_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshChats();
        }

        private void ChatList_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            ChatList.SelectedIndex = Properties.Settings.Default.TargetId;
        }

        private void NewChatButton_Click(object sender, RoutedEventArgs e)
        {
            NewChatButton.Height = 0;

            NewChatBox.Height = 20;
            ConfirmButton.Height = 20;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(con);
            connection.Open();

            var getId = new SqlCommand("SELECT ID_user FROM Users WHERE Name = @Name", connection);
            getId.Parameters.AddWithValue("@Name", NewChatBox.Text);

            int newMemberId = -1;
            using (getId)
            {
                if (getId.ExecuteScalar() is null)
                {
                    MessageBox.Show("Пользователя не существует!");
                    return;
                }
                newMemberId = (int)getId.ExecuteScalar();
            }

            var getChat = new SqlCommand("SELECT ID_chat FROM Chats WHERE Member1 = @Member1 AND Member2 = @Member2", connection);
            getChat.Parameters.AddWithValue("@Member1", newMemberId);
            getChat.Parameters.AddWithValue("@Member2", ID);

            using (getChat)
            {
                if (getChat.ExecuteScalar() is not null)
                {
                    MessageBox.Show("Чат уже существует!");
                    return;
                }
            }

            var insert1 = new SqlCommand("INSERT INTO Chats (Member1, Member2) VALUES (@Member1, @Member2)", connection);
            insert1.Parameters.AddWithValue("@Member1", ID);
            insert1.Parameters.AddWithValue("@Member2", newMemberId);
            using (insert1)
            {
                int rowsAffected = insert1.ExecuteNonQuery();
            }

            var insert2 = new SqlCommand("INSERT INTO Chats (Member1, Member2) VALUES (@Member1, @Member2)", connection);
            insert2.Parameters.AddWithValue("@Member1", newMemberId);
            insert2.Parameters.AddWithValue("@Member2", ID);
            using (insert2)
            {
                int rowsAffected = insert2.ExecuteNonQuery();
            }

            RefreshChats();

            NewChatButton.Height = 20;

            NewChatBox.Height = 0;
            ConfirmButton.Height = 0;
        }

        private void RefreshChatButton_Click(object sender, RoutedEventArgs e)
        {
            ChatList.SelectedIndex = 0;
            RefreshChats();
        }

        private void ChangeNickButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeNickButton.Width = 0;
            OkNickButton.Width = 74;
            NewNickBox.Width = 137;
        }

        private void OkNickButton_Click(object sender, RoutedEventArgs e)
        {
            string username = NewNickBox.Text;

            if (username.Contains("#"))
            {
                MessageBox.Show("Имя содержит недопуитимый символ (#)!");
                return;
            }

            Properties.Settings.Default.Nick = username;

            SqlConnection connection = new SqlConnection(con);
            connection.Open();

            var checkNick = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Name = @Name", connection);
            checkNick.Parameters.AddWithValue("@Name", username);
            using (checkNick)
            {
                if ((int)checkNick.ExecuteScalar() > 0)
                {
                    MessageBox.Show("Человек с таким ником уже существует!");
                    return;
                }
            }


            var updateCom = new SqlCommand("UPDATE Users SET Name = @NewName WHERE ID_user = @ID", connection);
            updateCom.Parameters.AddWithValue("@ID", ID);
            updateCom.Parameters.AddWithValue("@NewName", username);
            using (updateCom)
            {
                updateCom.ExecuteNonQuery();
            }

            ChangeNickButton.Width = 74;
            OkNickButton.Width = 0;
            NewNickBox.Width = 0;
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (ChatList.SelectedIndex == -1)
                SendButton.IsEnabled = false;
        }
    }
}
