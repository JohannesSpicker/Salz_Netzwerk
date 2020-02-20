namespace MyMultiPlayerGame
{
    partial class ApplicationWindow
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
            this.components = new System.ComponentModel.Container();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.textBoxServerAdress = new System.Windows.Forms.TextBox();
            this.buttonOpenServer = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxChatInput = new System.Windows.Forms.TextBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.listBoxChat = new System.Windows.Forms.ListBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.buttonStartGame = new System.Windows.Forms.Button();
            this.textBoxChatName = new System.Windows.Forms.TextBox();
            this.buttonReady = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(177, 14);
            this.buttonConnect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(112, 35);
            this.buttonConnect.TabIndex = 0;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // textBoxServerAdress
            // 
            this.textBoxServerAdress.Location = new System.Drawing.Point(18, 18);
            this.textBoxServerAdress.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxServerAdress.Name = "textBoxServerAdress";
            this.textBoxServerAdress.Size = new System.Drawing.Size(148, 26);
            this.textBoxServerAdress.TabIndex = 1;
            this.textBoxServerAdress.Text = "127.0.0.1";
            // 
            // buttonOpenServer
            // 
            this.buttonOpenServer.Location = new System.Drawing.Point(414, 14);
            this.buttonOpenServer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonOpenServer.Name = "buttonOpenServer";
            this.buttonOpenServer.Size = new System.Drawing.Size(112, 35);
            this.buttonOpenServer.TabIndex = 2;
            this.buttonOpenServer.Text = "Open Server";
            this.buttonOpenServer.UseVisualStyleBackColor = true;
            this.buttonOpenServer.Click += new System.EventHandler(this.buttonOpenServer_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(326, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "... or ...";
            // 
            // textBoxChatInput
            // 
            this.textBoxChatInput.Location = new System.Drawing.Point(147, 917);
            this.textBoxChatInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxChatInput.Name = "textBoxChatInput";
            this.textBoxChatInput.Size = new System.Drawing.Size(561, 26);
            this.textBoxChatInput.TabIndex = 4;
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(718, 912);
            this.buttonSend.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(112, 35);
            this.buttonSend.TabIndex = 5;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // listBoxChat
            // 
            this.listBoxChat.FormattingEnabled = true;
            this.listBoxChat.ItemHeight = 20;
            this.listBoxChat.Location = new System.Drawing.Point(18, 742);
            this.listBoxChat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listBoxChat.Name = "listBoxChat";
            this.listBoxChat.Size = new System.Drawing.Size(811, 164);
            this.listBoxChat.TabIndex = 6;
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // buttonStartGame
            // 
            this.buttonStartGame.Location = new System.Drawing.Point(597, 15);
            this.buttonStartGame.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonStartGame.Name = "buttonStartGame";
            this.buttonStartGame.Size = new System.Drawing.Size(112, 35);
            this.buttonStartGame.TabIndex = 7;
            this.buttonStartGame.Text = "Start Game";
            this.buttonStartGame.UseVisualStyleBackColor = true;
            this.buttonStartGame.Click += new System.EventHandler(this.buttonStartGame_Click);
            // 
            // textBoxChatName
            // 
            this.textBoxChatName.Location = new System.Drawing.Point(18, 917);
            this.textBoxChatName.Name = "textBoxChatName";
            this.textBoxChatName.Size = new System.Drawing.Size(122, 26);
            this.textBoxChatName.TabIndex = 8;
            this.textBoxChatName.Text = "YourName";
            // 
            // buttonReady
            // 
            this.buttonReady.Location = new System.Drawing.Point(789, 15);
            this.buttonReady.Name = "buttonReady";
            this.buttonReady.Size = new System.Drawing.Size(105, 34);
            this.buttonReady.TabIndex = 9;
            this.buttonReady.Text = "Not ready";
            this.buttonReady.UseVisualStyleBackColor = true;
            this.buttonReady.Click += new System.EventHandler(this.buttonReady_Click);
            // 
            // ApplicationWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1244, 966);
            this.Controls.Add(this.buttonReady);
            this.Controls.Add(this.textBoxChatName);
            this.Controls.Add(this.buttonStartGame);
            this.Controls.Add(this.listBoxChat);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.textBoxChatInput);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonOpenServer);
            this.Controls.Add(this.textBoxServerAdress);
            this.Controls.Add(this.buttonConnect);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ApplicationWindow";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ApplicationWindow_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.TextBox textBoxServerAdress;
        private System.Windows.Forms.Button buttonOpenServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxChatInput;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.ListBox listBoxChat;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button buttonStartGame;
        private System.Windows.Forms.TextBox textBoxChatName;
        private System.Windows.Forms.Button buttonReady;
    }
}

