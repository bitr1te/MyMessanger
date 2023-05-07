namespace Messanger_Project
{
    partial class Messanger
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.мессенджерToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.signoutToolStripItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripItem = new System.Windows.Forms.ToolStripMenuItem();
            this.signinPanel = new System.Windows.Forms.Panel();
            this.linkRegister = new System.Windows.Forms.LinkLabel();
            this.labPhone = new System.Windows.Forms.Label();
            this.txtbPhone = new System.Windows.Forms.TextBox();
            this.txtbPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkRemember = new System.Windows.Forms.CheckBox();
            this.butSignIn = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.signinPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.мессенджерToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(323, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // мессенджерToolStripMenuItem
            // 
            this.мессенджерToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.signoutToolStripItem,
            this.closeToolStripItem});
            this.мессенджерToolStripMenuItem.Name = "мессенджерToolStripMenuItem";
            this.мессенджерToolStripMenuItem.Size = new System.Drawing.Size(89, 20);
            this.мессенджерToolStripMenuItem.Text = "Мессенджер";
            // 
            // signoutToolStripItem
            // 
            this.signoutToolStripItem.Name = "signoutToolStripItem";
            this.signoutToolStripItem.Size = new System.Drawing.Size(180, 22);
            this.signoutToolStripItem.Text = "Выйти из аккаунта";
            this.signoutToolStripItem.Click += new System.EventHandler(this.signoutToolStripItem_Click);
            // 
            // closeToolStripItem
            // 
            this.closeToolStripItem.Name = "closeToolStripItem";
            this.closeToolStripItem.Size = new System.Drawing.Size(180, 22);
            this.closeToolStripItem.Text = "Закрыть";
            this.closeToolStripItem.Click += new System.EventHandler(this.closeToolStripItem_Click);
            // 
            // signinPanel
            // 
            this.signinPanel.Controls.Add(this.butSignIn);
            this.signinPanel.Controls.Add(this.chkRemember);
            this.signinPanel.Controls.Add(this.txtbPassword);
            this.signinPanel.Controls.Add(this.label2);
            this.signinPanel.Controls.Add(this.txtbPhone);
            this.signinPanel.Controls.Add(this.labPhone);
            this.signinPanel.Controls.Add(this.linkRegister);
            this.signinPanel.Location = new System.Drawing.Point(12, 27);
            this.signinPanel.Name = "signinPanel";
            this.signinPanel.Size = new System.Drawing.Size(299, 445);
            this.signinPanel.TabIndex = 1;
            // 
            // linkRegister
            // 
            this.linkRegister.AutoSize = true;
            this.linkRegister.Location = new System.Drawing.Point(106, 368);
            this.linkRegister.Name = "linkRegister";
            this.linkRegister.Size = new System.Drawing.Size(79, 13);
            this.linkRegister.TabIndex = 0;
            this.linkRegister.TabStop = true;
            this.linkRegister.Text = "Новый аккант";
            this.linkRegister.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkRegister_LinkClicked);
            // 
            // labPhone
            // 
            this.labPhone.AutoSize = true;
            this.labPhone.Location = new System.Drawing.Point(62, 120);
            this.labPhone.Name = "labPhone";
            this.labPhone.Size = new System.Drawing.Size(96, 13);
            this.labPhone.TabIndex = 1;
            this.labPhone.Text = "Номер телефона:";
            // 
            // txtbPhone
            // 
            this.txtbPhone.Location = new System.Drawing.Point(65, 137);
            this.txtbPhone.Name = "txtbPhone";
            this.txtbPhone.Size = new System.Drawing.Size(152, 20);
            this.txtbPhone.TabIndex = 2;
            // 
            // txtbPassword
            // 
            this.txtbPassword.Location = new System.Drawing.Point(65, 182);
            this.txtbPassword.Name = "txtbPassword";
            this.txtbPassword.Size = new System.Drawing.Size(152, 20);
            this.txtbPassword.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(62, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Пароль:";
            // 
            // chkRemember
            // 
            this.chkRemember.AutoSize = true;
            this.chkRemember.Location = new System.Drawing.Point(65, 209);
            this.chkRemember.Name = "chkRemember";
            this.chkRemember.Size = new System.Drawing.Size(111, 17);
            this.chkRemember.TabIndex = 5;
            this.chkRemember.Text = "Запомнить меня";
            this.chkRemember.UseVisualStyleBackColor = true;
            this.chkRemember.CheckedChanged += new System.EventHandler(this.chkRemember_CheckedChanged);
            // 
            // butSignIn
            // 
            this.butSignIn.Location = new System.Drawing.Point(88, 253);
            this.butSignIn.Name = "butSignIn";
            this.butSignIn.Size = new System.Drawing.Size(111, 35);
            this.butSignIn.TabIndex = 6;
            this.butSignIn.Text = "Войти";
            this.butSignIn.UseVisualStyleBackColor = true;
            this.butSignIn.Click += new System.EventHandler(this.butSignIn_Click);
            // 
            // Messanger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 484);
            this.Controls.Add(this.signinPanel);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Messanger";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Мессенджер";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.signinPanel.ResumeLayout(false);
            this.signinPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem мессенджерToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem signoutToolStripItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripItem;
        private System.Windows.Forms.Panel signinPanel;
        private System.Windows.Forms.Button butSignIn;
        private System.Windows.Forms.CheckBox chkRemember;
        private System.Windows.Forms.TextBox txtbPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtbPhone;
        private System.Windows.Forms.Label labPhone;
        private System.Windows.Forms.LinkLabel linkRegister;
    }
}

