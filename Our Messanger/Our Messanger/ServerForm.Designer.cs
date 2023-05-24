namespace Our_Messanger
{
    partial class ServerForm
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
            this.btnStart = new System.Windows.Forms.Button();
            this.txtboxPort = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.startstopPanel = new System.Windows.Forms.Panel();
            this.txtConsole = new System.Windows.Forms.RichTextBox();
            this.startstopPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(111, 38);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtboxPort
            // 
            this.txtboxPort.Location = new System.Drawing.Point(77, 12);
            this.txtboxPort.MaxLength = 4;
            this.txtboxPort.Name = "txtboxPort";
            this.txtboxPort.Size = new System.Drawing.Size(100, 20);
            this.txtboxPort.TabIndex = 4;
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(36, 15);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(29, 13);
            this.lblPort.TabIndex = 3;
            this.lblPort.Text = "Port:";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(30, 38);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // startstopPanel
            // 
            this.startstopPanel.Controls.Add(this.btnStop);
            this.startstopPanel.Controls.Add(this.txtboxPort);
            this.startstopPanel.Controls.Add(this.lblPort);
            this.startstopPanel.Controls.Add(this.btnStart);
            this.startstopPanel.Location = new System.Drawing.Point(104, 25);
            this.startstopPanel.Name = "startstopPanel";
            this.startstopPanel.Size = new System.Drawing.Size(200, 74);
            this.startstopPanel.TabIndex = 6;
            // 
            // txtConsole
            // 
            this.txtConsole.Location = new System.Drawing.Point(12, 118);
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.Size = new System.Drawing.Size(391, 178);
            this.txtConsole.TabIndex = 7;
            this.txtConsole.Text = "";
            this.txtConsole.TextChanged += new System.EventHandler(this.txtConsole_TextChanged);
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 308);
            this.Controls.Add(this.txtConsole);
            this.Controls.Add(this.startstopPanel);
            this.Name = "ServerForm";
            this.Text = "Our Messanger";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.startstopPanel.ResumeLayout(false);
            this.startstopPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox txtboxPort;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Panel startstopPanel;
        private System.Windows.Forms.RichTextBox txtConsole;
    }
}

