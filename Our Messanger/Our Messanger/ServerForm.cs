using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Our_Messanger
{
    public partial class ServerForm : Form
    {
        MessServer server;

        public ServerForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtConsole.Text += "Welcome";
            btnStop.Enabled = false;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            server = new MessServer(true, Convert.ToInt32(txtboxPort.Text));
            server.Start();
            txtConsole.Text += "\nСервер запущен";

            btnStart.Enabled = false;
            btnStop.Enabled = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            server.Stop();
            txtConsole.Text += "\nСервер остановлен";

            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }
    }
}
