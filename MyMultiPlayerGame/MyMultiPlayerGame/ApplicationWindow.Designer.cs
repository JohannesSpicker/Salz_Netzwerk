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
			this.textBoxChatName = new System.Windows.Forms.TextBox();
			this.buttonReady = new System.Windows.Forms.Button();
			this.labelHealthMe = new System.Windows.Forms.Label();
			this.labelHealthEnemy = new System.Windows.Forms.Label();
			this.labelMyEnergy = new System.Windows.Forms.Label();
			this.buttonArcher = new System.Windows.Forms.Button();
			this.buttonBertha = new System.Windows.Forms.Button();
			this.buttonCommoner = new System.Windows.Forms.Button();
			this.labelCost = new System.Windows.Forms.Label();
			this.labelCostA = new System.Windows.Forms.Label();
			this.labelCostB = new System.Windows.Forms.Label();
			this.labelCostC = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// buttonConnect
			// 
			this.buttonConnect.Location = new System.Drawing.Point(118, 9);
			this.buttonConnect.Name = "buttonConnect";
			this.buttonConnect.Size = new System.Drawing.Size(75, 23);
			this.buttonConnect.TabIndex = 0;
			this.buttonConnect.Text = "Connect";
			this.buttonConnect.UseVisualStyleBackColor = true;
			this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
			// 
			// textBoxServerAdress
			// 
			this.textBoxServerAdress.Location = new System.Drawing.Point(12, 12);
			this.textBoxServerAdress.Name = "textBoxServerAdress";
			this.textBoxServerAdress.Size = new System.Drawing.Size(100, 20);
			this.textBoxServerAdress.TabIndex = 1;
			this.textBoxServerAdress.Text = "127.0.0.1";
			// 
			// buttonOpenServer
			// 
			this.buttonOpenServer.Location = new System.Drawing.Point(276, 9);
			this.buttonOpenServer.Name = "buttonOpenServer";
			this.buttonOpenServer.Size = new System.Drawing.Size(75, 23);
			this.buttonOpenServer.TabIndex = 2;
			this.buttonOpenServer.Text = "Open Server";
			this.buttonOpenServer.UseVisualStyleBackColor = true;
			this.buttonOpenServer.Click += new System.EventHandler(this.buttonOpenServer_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(217, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "... or ...";
			// 
			// textBoxChatInput
			// 
			this.textBoxChatInput.Location = new System.Drawing.Point(98, 596);
			this.textBoxChatInput.Name = "textBoxChatInput";
			this.textBoxChatInput.Size = new System.Drawing.Size(375, 20);
			this.textBoxChatInput.TabIndex = 4;
			// 
			// buttonSend
			// 
			this.buttonSend.Location = new System.Drawing.Point(479, 593);
			this.buttonSend.Name = "buttonSend";
			this.buttonSend.Size = new System.Drawing.Size(75, 23);
			this.buttonSend.TabIndex = 5;
			this.buttonSend.Text = "Send";
			this.buttonSend.UseVisualStyleBackColor = true;
			this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
			// 
			// listBoxChat
			// 
			this.listBoxChat.FormattingEnabled = true;
			this.listBoxChat.Location = new System.Drawing.Point(12, 482);
			this.listBoxChat.Name = "listBoxChat";
			this.listBoxChat.Size = new System.Drawing.Size(542, 108);
			this.listBoxChat.TabIndex = 6;
			// 
			// timer
			// 
			this.timer.Enabled = true;
			this.timer.Tick += new System.EventHandler(this.timer_Tick);
			// 
			// textBoxChatName
			// 
			this.textBoxChatName.Location = new System.Drawing.Point(12, 596);
			this.textBoxChatName.Margin = new System.Windows.Forms.Padding(2);
			this.textBoxChatName.Name = "textBoxChatName";
			this.textBoxChatName.Size = new System.Drawing.Size(83, 20);
			this.textBoxChatName.TabIndex = 8;
			this.textBoxChatName.Text = "YourName";
			// 
			// buttonReady
			// 
			this.buttonReady.Location = new System.Drawing.Point(400, 9);
			this.buttonReady.Margin = new System.Windows.Forms.Padding(2);
			this.buttonReady.Name = "buttonReady";
			this.buttonReady.Size = new System.Drawing.Size(73, 23);
			this.buttonReady.TabIndex = 9;
			this.buttonReady.Text = "Not ready";
			this.buttonReady.UseVisualStyleBackColor = true;
			this.buttonReady.Click += new System.EventHandler(this.buttonReady_Click);
			// 
			// labelHealthMe
			// 
			this.labelHealthMe.AutoSize = true;
			this.labelHealthMe.Location = new System.Drawing.Point(632, 114);
			this.labelHealthMe.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.labelHealthMe.Name = "labelHealthMe";
			this.labelHealthMe.Size = new System.Drawing.Size(77, 13);
			this.labelHealthMe.TabIndex = 10;
			this.labelHealthMe.Text = "My health: 100";
			// 
			// labelHealthEnemy
			// 
			this.labelHealthEnemy.AutoSize = true;
			this.labelHealthEnemy.ForeColor = System.Drawing.Color.Black;
			this.labelHealthEnemy.Location = new System.Drawing.Point(632, 149);
			this.labelHealthEnemy.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.labelHealthEnemy.Name = "labelHealthEnemy";
			this.labelHealthEnemy.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.labelHealthEnemy.Size = new System.Drawing.Size(97, 13);
			this.labelHealthEnemy.TabIndex = 11;
			this.labelHealthEnemy.Text = "Enemy Health: 100";
			this.labelHealthEnemy.Click += new System.EventHandler(this.label3_Click);
			// 
			// labelMyEnergy
			// 
			this.labelMyEnergy.AutoSize = true;
			this.labelMyEnergy.ForeColor = System.Drawing.SystemColors.ControlText;
			this.labelMyEnergy.Location = new System.Drawing.Point(632, 198);
			this.labelMyEnergy.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.labelMyEnergy.Name = "labelMyEnergy";
			this.labelMyEnergy.Size = new System.Drawing.Size(58, 13);
			this.labelMyEnergy.TabIndex = 12;
			this.labelMyEnergy.Text = "Energy: 10";
			// 
			// buttonArcher
			// 
			this.buttonArcher.Location = new System.Drawing.Point(654, 252);
			this.buttonArcher.Name = "buttonArcher";
			this.buttonArcher.Size = new System.Drawing.Size(75, 23);
			this.buttonArcher.TabIndex = 13;
			this.buttonArcher.Text = "Archer";
			this.buttonArcher.UseVisualStyleBackColor = true;
			this.buttonArcher.Click += new System.EventHandler(this.buttonArcher_Click);
			// 
			// buttonBertha
			// 
			this.buttonBertha.Location = new System.Drawing.Point(654, 281);
			this.buttonBertha.Name = "buttonBertha";
			this.buttonBertha.Size = new System.Drawing.Size(75, 23);
			this.buttonBertha.TabIndex = 14;
			this.buttonBertha.Text = "Bertha";
			this.buttonBertha.UseVisualStyleBackColor = true;
			this.buttonBertha.Click += new System.EventHandler(this.buttonBertha_Click);
			// 
			// buttonCommoner
			// 
			this.buttonCommoner.Location = new System.Drawing.Point(654, 310);
			this.buttonCommoner.Name = "buttonCommoner";
			this.buttonCommoner.Size = new System.Drawing.Size(75, 23);
			this.buttonCommoner.TabIndex = 15;
			this.buttonCommoner.Text = "Commoner";
			this.buttonCommoner.UseVisualStyleBackColor = true;
			this.buttonCommoner.Click += new System.EventHandler(this.buttonCommoner_Click);
			// 
			// labelCost
			// 
			this.labelCost.AutoSize = true;
			this.labelCost.Location = new System.Drawing.Point(750, 233);
			this.labelCost.Name = "labelCost";
			this.labelCost.Size = new System.Drawing.Size(28, 13);
			this.labelCost.TabIndex = 16;
			this.labelCost.Text = "Cost";
			// 
			// labelCostA
			// 
			this.labelCostA.AutoSize = true;
			this.labelCostA.Location = new System.Drawing.Point(750, 257);
			this.labelCostA.Name = "labelCostA";
			this.labelCostA.Size = new System.Drawing.Size(13, 13);
			this.labelCostA.TabIndex = 17;
			this.labelCostA.Text = "2";
			// 
			// labelCostB
			// 
			this.labelCostB.AutoSize = true;
			this.labelCostB.Location = new System.Drawing.Point(750, 286);
			this.labelCostB.Name = "labelCostB";
			this.labelCostB.Size = new System.Drawing.Size(13, 13);
			this.labelCostB.TabIndex = 18;
			this.labelCostB.Text = "6";
			// 
			// labelCostC
			// 
			this.labelCostC.AutoSize = true;
			this.labelCostC.Location = new System.Drawing.Point(750, 315);
			this.labelCostC.Name = "labelCostC";
			this.labelCostC.Size = new System.Drawing.Size(13, 13);
			this.labelCostC.TabIndex = 19;
			this.labelCostC.Text = "3";
			// 
			// ApplicationWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(829, 628);
			this.Controls.Add(this.labelCostC);
			this.Controls.Add(this.labelCostB);
			this.Controls.Add(this.labelCostA);
			this.Controls.Add(this.labelCost);
			this.Controls.Add(this.buttonCommoner);
			this.Controls.Add(this.buttonBertha);
			this.Controls.Add(this.buttonArcher);
			this.Controls.Add(this.labelMyEnergy);
			this.Controls.Add(this.labelHealthEnemy);
			this.Controls.Add(this.labelHealthMe);
			this.Controls.Add(this.buttonReady);
			this.Controls.Add(this.textBoxChatName);
			this.Controls.Add(this.listBoxChat);
			this.Controls.Add(this.buttonSend);
			this.Controls.Add(this.textBoxChatInput);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonOpenServer);
			this.Controls.Add(this.textBoxServerAdress);
			this.Controls.Add(this.buttonConnect);
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
        private System.Windows.Forms.TextBox textBoxChatName;
        private System.Windows.Forms.Button buttonReady;
        private System.Windows.Forms.Label labelHealthMe;
        private System.Windows.Forms.Label labelHealthEnemy;
        private System.Windows.Forms.Label labelMyEnergy;
		private System.Windows.Forms.Button buttonArcher;
		private System.Windows.Forms.Button buttonBertha;
		private System.Windows.Forms.Button buttonCommoner;
		private System.Windows.Forms.Label labelCost;
		private System.Windows.Forms.Label labelCostA;
		private System.Windows.Forms.Label labelCostB;
		private System.Windows.Forms.Label labelCostC;
	}
}

