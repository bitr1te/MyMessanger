namespace Messanger_Project
{
    partial class Registration_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtbPhone = new System.Windows.Forms.TextBox();
            this.txtbName = new System.Windows.Forms.TextBox();
            this.butReg = new System.Windows.Forms.Button();
            this.grbRegister = new System.Windows.Forms.GroupBox();
            this.grbConfirm = new System.Windows.Forms.GroupBox();
            this.txtvSMS = new System.Windows.Forms.TextBox();
            this.butConfirm = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.grbRegister.SuspendLayout();
            this.grbConfirm.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Номер телефона:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Полное имя:";
            // 
            // txtbPhone
            // 
            this.txtbPhone.Location = new System.Drawing.Point(127, 19);
            this.txtbPhone.Name = "txtbPhone";
            this.txtbPhone.Size = new System.Drawing.Size(208, 20);
            this.txtbPhone.TabIndex = 2;
            // 
            // txtbName
            // 
            this.txtbName.Location = new System.Drawing.Point(128, 48);
            this.txtbName.Name = "txtbName";
            this.txtbName.Size = new System.Drawing.Size(207, 20);
            this.txtbName.TabIndex = 3;
            // 
            // butReg
            // 
            this.butReg.Location = new System.Drawing.Point(209, 74);
            this.butReg.Name = "butReg";
            this.butReg.Size = new System.Drawing.Size(126, 23);
            this.butReg.TabIndex = 4;
            this.butReg.Text = "Зарегистрироваться";
            this.butReg.UseVisualStyleBackColor = true;
            // 
            // grbRegister
            // 
            this.grbRegister.Controls.Add(this.txtbPhone);
            this.grbRegister.Controls.Add(this.butReg);
            this.grbRegister.Controls.Add(this.label1);
            this.grbRegister.Controls.Add(this.txtbName);
            this.grbRegister.Controls.Add(this.label2);
            this.grbRegister.Location = new System.Drawing.Point(12, 12);
            this.grbRegister.Name = "grbRegister";
            this.grbRegister.Size = new System.Drawing.Size(348, 113);
            this.grbRegister.TabIndex = 5;
            this.grbRegister.TabStop = false;
            this.grbRegister.Text = "Шаг 1: Регистрация";
            // 
            // grbConfirm
            // 
            this.grbConfirm.Controls.Add(this.txtvSMS);
            this.grbConfirm.Controls.Add(this.butConfirm);
            this.grbConfirm.Controls.Add(this.label3);
            this.grbConfirm.Location = new System.Drawing.Point(12, 131);
            this.grbConfirm.Name = "grbConfirm";
            this.grbConfirm.Size = new System.Drawing.Size(348, 77);
            this.grbConfirm.TabIndex = 6;
            this.grbConfirm.TabStop = false;
            this.grbConfirm.Text = "Шаг 2: Подтверждение телефона";
            // 
            // txtvSMS
            // 
            this.txtvSMS.Location = new System.Drawing.Point(127, 19);
            this.txtvSMS.MaxLength = 6;
            this.txtvSMS.Name = "txtvSMS";
            this.txtvSMS.Size = new System.Drawing.Size(208, 20);
            this.txtvSMS.TabIndex = 2;
            // 
            // butConfirm
            // 
            this.butConfirm.Location = new System.Drawing.Point(209, 45);
            this.butConfirm.Name = "butConfirm";
            this.butConfirm.Size = new System.Drawing.Size(126, 23);
            this.butConfirm.TabIndex = 4;
            this.butConfirm.Text = "Подтвердить";
            this.butConfirm.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "SMS-код:";
            // 
            // Registration_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 217);
            this.Controls.Add(this.grbConfirm);
            this.Controls.Add(this.grbRegister);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Registration_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Регистрация";
            this.grbRegister.ResumeLayout(false);
            this.grbRegister.PerformLayout();
            this.grbConfirm.ResumeLayout(false);
            this.grbConfirm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtbPhone;
        private System.Windows.Forms.TextBox txtbName;
        private System.Windows.Forms.Button butReg;
        private System.Windows.Forms.GroupBox grbRegister;
        private System.Windows.Forms.GroupBox grbConfirm;
        private System.Windows.Forms.TextBox txtvSMS;
        private System.Windows.Forms.Button butConfirm;
        private System.Windows.Forms.Label label3;
    }
}