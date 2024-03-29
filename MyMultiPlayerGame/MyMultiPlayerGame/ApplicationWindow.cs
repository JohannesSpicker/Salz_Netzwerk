﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using MyMultiPlayerGame.Messages;

namespace MyMultiPlayerGame
{
	public partial class ApplicationWindow : Form
	{
		private const int SERVERPORT = 1996;

		private TcpListener listener =
			new TcpListener(IPAddress.Parse("127.0.0.1"), SERVERPORT);

		private NetworkConnection connection;
		//private Game.Game myGame = new Game.Game();
		private Game.Game myGame;
		private Canvas canvas;
		public bool isServer { get; private set; }

		private ConcurrentQueue<MessageBase> IncomingNetworkMessages = new ConcurrentQueue<MessageBase>();

		public ApplicationWindow()
		{
			InitializeComponent();

			this.myGame = new Game.Game(this);
			this.canvas = new Canvas(this.myGame);
			this.canvas.Size = new Size(600, 300);
			this.canvas.Location = new Point(10, 100);
			this.Controls.Add(this.canvas);

			//this.buttonStartGame.Enabled = false;
		}

		private void buttonConnect_Click(object sender, EventArgs e)
		{
			var client = new TcpClient();
			client.Connect("127.0.0.1", SERVERPORT);
			MessageBox.Show("Connected to Server!!!");

			connection = new NetworkConnection(client);
			connection.MessageReceived += ConnectionOnMessageReceived;

			this.buttonOpenServer.Enabled = false;
			this.buttonConnect.Enabled = false;

			if (myGame.isMeReady)
				ReadyToggle();
		}

		private void buttonOpenServer_Click(object sender, EventArgs e)
		{
			isServer = true;
			listener.Start();
			var connectedClient = this.listener.AcceptTcpClient();

			MessageBox.Show("Client has connected!!!");
			//this.buttonStartGame.Enabled = true;

			connection = new NetworkConnection(connectedClient);
			connection.MessageReceived += ConnectionOnMessageReceived;

			this.buttonOpenServer.Enabled = false;
			this.buttonConnect.Enabled = false;

			if (myGame.isMeReady)
				ReadyToggle();
		}

		private void ConnectionOnMessageReceived(MessageBase message)
		{
			// executed by network thread!
			IncomingNetworkMessages.Enqueue(message);
		}

		private void buttonSend_Click(object sender, EventArgs e)
		{
			if (connection != null)
			{
				ChatMessage msg = new ChatMessage()
				{
					Sender = textBoxChatName.Text,
					Text = textBoxChatInput.Text
				};

				connection.Send(msg);                            //send chat message to other client
				DisplayChatmessage(msg.Sender + ": " + msg.Text);//display chat message on this client
			}
		}

		private void ProcessNetworkMessages()
		{
			while (IncomingNetworkMessages.TryDequeue(out var message))
			{
				if (message is ChatMessage)
				{
					var c = (ChatMessage)message;
					DisplayChatmessage(c.Sender + ": " + c.Text);
				}
				else if (message is StartGame)
				{
					StartGame m = (StartGame)message;
					// I am a client, StartGame comes from server
					this.myGame.Start(m.numPlayers, m.myPlayerNumber, new List<NetworkConnection>() { connection });
					DisplayChatmessage(">>> GAME HAS STARTED <<<");
					buttonReady.Enabled = false;
				}
				else if (message is GameInput)
				{
					this.myGame.ReceiveGameInput((GameInput)message);
				}
				else if (message is ReadyMessage)
				{
					this.myGame.ReceiveReadyInput((ReadyMessage)message);
				}
				else if (message is StopGame)
				{
					this.myGame.ReceiveStopInput((StopGame)message);
				}
			}
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			this.BeginInvoke(new Action(() =>
			{
				ProcessNetworkMessages();

				this.myGame.NextSimulationStep();

				this.canvas.Invalidate();
			}));
		}

		public void DisplayChatmessage(string text)
		{
			listBoxChat.Items.Add(text);
			listBoxChat.SelectedIndex = listBoxChat.Items.Count - 1;//scroll the chat down
		}

		private void ApplicationWindow_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (connection != null)
			{
				connection.Dispose();
			}
		}

		private void buttonStartGame_Click(object sender, EventArgs e) => StartTry();

		public void StartTry()
		{
			if (isServer)
			{
				if (myGame.isMeReady && myGame.isOppReady)
				{
					//this.buttonStartGame.Enabled = false;

					this.BeginInvoke(new Action(() =>
					{
						myGame.Start(2, 0, new List<NetworkConnection>() { connection });
					}));

					this.connection.Send(new StartGame()
					{
						numPlayers = 2,
						myPlayerNumber = 1
					});

					buttonReady.Enabled = false;
					DisplayChatmessage(">>> GAME HAS STARTED <<<");
				}
				else
					DisplayChatmessage("Both players need to be ready for the game to start.");
			}
		}

		public void SendStopMsg(bool isTie, bool isReceiverWinner)
		{
			this.connection.Send(new StopGame()
			{
				isTie = isTie,
				isReceiverWinner = isReceiverWinner
			});
		}

		private void buttonReady_Click(object sender, EventArgs e) => ReadyToggle();

		private void ReadyToggle()
		{
			myGame.isMeReady = !myGame.isMeReady;
			DisplayChatmessage("You are " + (myGame.isMeReady ? "" : "not ") + "ready.");//display chat message on this client

			buttonReady.Text = myGame.isMeReady ? "Ready" : "Not ready";
			buttonReady.BackColor = myGame.isMeReady ? Color.Green : Color.Red;

			if (connection != null)
			{
				ReadyMessage msg = new ReadyMessage()
				{
					Sender = textBoxChatName.Text,
					isReady = myGame.isMeReady
				};

				connection.Send(msg);//send ready message to other client
			}

			StartTry();
		}

		public void ChangeEnergyLabel() => labelMyEnergy.Text = "Energy: " + myGame.myEnergy.ToString("#0.0");
		public void ChangeMyHealthLabel() => labelHealthMe.Text = "My health: " + myGame.myHealth.ToString();
		public void ChangeOppHealthLabel() => labelHealthEnemy.Text = "Enemy health: " + myGame.oppHealth.ToString();

		private void label3_Click(object sender, EventArgs e) => buttonReady.BackColor = Color.White;

		private void buttonArcher_Click(object sender, EventArgs e) => ChangeQueuedSoldierType(Game.Soldier.SoldierType.AchyArcher);
		private void buttonBertha_Click(object sender, EventArgs e) => ChangeQueuedSoldierType(Game.Soldier.SoldierType.BlindBertha);
		private void buttonCommoner_Click(object sender, EventArgs e) => ChangeQueuedSoldierType(Game.Soldier.SoldierType.CrudeCommoner);

		private void ChangeQueuedSoldierType(Game.Soldier.SoldierType type)
		{
			switch (type)
			{
				case Game.Soldier.SoldierType.AchyArcher:
					buttonArcher.Enabled = false;
					buttonBertha.Enabled = true;
					buttonCommoner.Enabled = true;
					break;
				case Game.Soldier.SoldierType.BlindBertha:
					buttonArcher.Enabled = true;
					buttonBertha.Enabled = false;
					buttonCommoner.Enabled = true;
					break;
				case Game.Soldier.SoldierType.CrudeCommoner:
					buttonArcher.Enabled = true;
					buttonBertha.Enabled = true;
					buttonCommoner.Enabled = false;
					break;
				case Game.Soldier.SoldierType.NONE:
				default:
					buttonArcher.Enabled = true;
					buttonBertha.Enabled = true;
					buttonCommoner.Enabled = false;
					myGame.SetQueuedSoldierType(Game.Soldier.SoldierType.CrudeCommoner);
					return;
			}

			myGame.SetQueuedSoldierType(type);
		}
	}
}
