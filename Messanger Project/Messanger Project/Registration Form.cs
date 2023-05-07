using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Messanger_Project
{
    public partial class Registration_Form : Form
    {
        string password;
        public Registration_Form()
        {
            InitializeComponent();
        }

        private void butReg_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtbPhone.Text))
            {
                MessageBox.Show("Пожалуйста, введите номер телефона!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtbPhone.Focus();
                return;
            }
            if(string.IsNullOrEmpty(txtbName.Text))
            {
                MessageBox.Show("Пожалуйста, введите имя!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtbName.Focus(); 
                return;
            } 
            if(WhatsAppApi.Register.WhatsRegisterV2.RequestCode(txtbPhone.Text, out password, "sms"))
            {
                if (!string.IsNullOrEmpty(password))
                    Save();
                else
                {
                    grbRegister.Enabled = false;
                    grbConfirm.Enabled = true;
                }
            }
            else
                MessageBox.Show("Невозможно сгенерировать пароль!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Save()
        {
            grbRegister.Enabled = false;
            grbConfirm.Enabled = false;
            Properties.Settings.Default.PhoneNumber = txtbPhone.Text;
            Properties.Settings.Default.FullName = txtbName.Text;
            Properties.Settings.Default.Password = password;
            Properties.Settings.Default.Save();

            if (Globals.DB.Accaunts.FindByAccountId(txtbPhone.Text) == null)
            {
                DataSet1.AccauntsRow row = Globals.DB.Accaunts.NewAccauntsRow();
                row.AccountId = txtbPhone.Text;
                row.Password = password;
                Globals.DB.Accaunts.AddAccauntsRow(row);
                Globals.DB.AcceptChanges();
                Globals.DB.Accaunts.WriteXml(string.Format("{0}\\accounts.dat", Application.StartupPath));
            }
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void butConfirm_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtvSMS.Text))
            {
                MessageBox.Show("Пожалуйста, введите СМС-код!","Сообщение",MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtvSMS.Focus();
                return;
            }
            password = WhatsAppApi.Register.WhatsRegisterV2.RegisterCode(txtbPhone.Text, txtvSMS.Text);
        }
    }
}
