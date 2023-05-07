using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WhatsAppApi;

namespace Messanger_Project
{
    public partial class Messanger : Form
    {
        public Messanger()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            signoutToolStripItem.Visible = false;
            signinPanel.BringToFront();
            if(Properties.Settings.Default.Remember)
            {
                txtbPhone.Text = Properties.Settings.Default.PhoneNumber;
                txtbPassword.Text = Properties.Settings.Default.Password;
                chkRemember.Checked = true;
            }
        }

        private void butSignIn_Click(object sender, EventArgs e)
        {

        }

        private void chkRemember_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void linkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void closeToolStripItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void signoutToolStripItem_Click(object sender, EventArgs e)
        {

        }
    }
}
