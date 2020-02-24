using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using MyMultiPlayerGame.Messages;

namespace MyMultiPlayerGame.Game
{
	class Game
	{
		public Game(ApplicationWindow appWindow)
		{
			this.appWindow = appWindow;
		}

		ApplicationWindow appWindow;

		bool running;
		int simStepCount;
		List<GameObject> allGameObjects = new List<GameObject>();
		List<Soldier> allSoldiers = new List<Soldier>();
		List<GameObject> allGarbage = new List<GameObject>();

		public class InputEvent
		{
			public byte UnitTypePlaced;         // 0 for none
			public float X;
			public float Y;
		}

		public int numPlayers { get; private set; }
		public int myPlayerNumber { get; private set; }
		public int boardWidth { get; private set; }
		public int boardHeight { get; private set; }

		Soldier.SoldierType queuedSoldierType = Soldier.SoldierType.NONE;
		InputEvent[] collectedInputEvents;                              // events needed for next simulation
		List<GameInput> futureEvents = new List<GameInput>();           // events needed after next simulation

		InputEvent myInput = null;
		List<NetworkConnection> peers;

		public bool isMeReady = false;
		public bool isOppReady = false;

		public int myHealth
		{
			get => myHealthBackingField;
			set
			{
				myHealthBackingField = Math.Max(Math.Min(value, 100), 0);
				appWindow.ChangeMyHealthLabel();
			}
		}
		private int myHealthBackingField = 100;
		public int oppHealth
		{
			get => oppHealthbackingField;
			set
			{
				oppHealthbackingField = Math.Max(Math.Min(value, 100), 0);
				appWindow.ChangeOppHealthLabel();
			}
		}
		private int oppHealthbackingField = 100;
		public float myEnergy
		{
			get => myEnergyBackingField;
			set
			{
				myEnergyBackingField = Math.Max(Math.Min(value, 10f), 0f);
				appWindow.ChangeEnergyLabel();
			}
		}
		private float myEnergyBackingField = 10f;

		#region Setup + End
		public void ReceiveReadyInput(ReadyMessage message)
		{
			this.isOppReady = message.isReady;
			appWindow.DisplayChatmessage(message.Sender + " is " + (this.isOppReady ? "" : "not ") + "ready.");
			appWindow.StartTry();
		}

		public void Start(int numPlayers, int myPlayerNumber, List<NetworkConnection> peers)
		{
			System.Diagnostics.Debug.WriteLine("Start()");

			this.boardHeight = 300;
			this.boardWidth = 600;
			this.running = true;
			this.simStepCount = 0;
			this.numPlayers = numPlayers;
			this.myPlayerNumber = myPlayerNumber;
			this.collectedInputEvents = new InputEvent[numPlayers];
			this.peers = peers;

			this.allGameObjects.Clear();
			this.allSoldiers.Clear();
			this.allGarbage.Clear();

			SendInput();

			System.Diagnostics.Debug.WriteLine("Start() end");
		}

		/// <summary>
		/// Checks for game end and sends StopGame messages appropriately.
		/// </summary>
		private void CheckGameEnd()
		{
			if (!appWindow.isServer || (0 < myHealth && 0 < oppHealth))//early skip on client side, or both players alive
				return;

			if (myHealth <= 0 && oppHealth <= 0)
			{
				//TIE
				appWindow.DisplayChatmessage(">>> GAME IS A TIE <<<");
				appWindow.SendStopMsg(true, false);

			}
			else if (myHealth <= 0)
			{
				//SERVER LOSS
				appWindow.DisplayChatmessage(">>> YOU LOST <<<");
				appWindow.SendStopMsg(false, true);
			}
			else
			{
				//CLIENT LOSS
				appWindow.DisplayChatmessage(">>> YOU WON <<<");
				appWindow.SendStopMsg(false, false);
			}

			Stop();
		}

		public void ReceiveStopInput(StopGame message)
		{
			if (message.isTie)
				appWindow.DisplayChatmessage(">>> GAME IS A TIE <<<");
			else if (message.isReceiverWinner)
				appWindow.DisplayChatmessage(">>> YOU WON <<<");
			else
				appWindow.DisplayChatmessage(">>> YOU LOST <<<");

			Stop();
		}

		public void Stop() => this.running = false;
		#endregion

		#region GameLoop
		protected void SendInput()
		{
			System.Diagnostics.Debug.WriteLine("SendInput()");

			// no input yet? to late, my input is "do nothing"
			if (this.myInput == null)
			{
				this.myInput = new InputEvent() { UnitTypePlaced = 0 };
			}

			// collect my own input (we are not going to send it to ourselves over the network!)
			this.collectedInputEvents[this.myPlayerNumber] = this.myInput;

			var message = new GameInput()
			{
				simStep = this.simStepCount + 1,
				myPlayerNumber = this.myPlayerNumber,
				simulationStepNumber = this.simStepCount,
				UnitPlaced = this.myInput.UnitTypePlaced,
				X = this.myInput.X,
				Y = this.myInput.Y
			};

			// client will only send to server (and server will distribute to all clients) 
			// server will send to all clients
			foreach (var peer in this.peers)
			{
				peer.Send(message);
			}

			this.myInput = null;
		}

		public void ReceiveGameInput(GameInput message)
		{
			System.Diagnostics.Debug.WriteLine("ReceiveGameInput()");

			if (!running)
			{
				System.Diagnostics.Debug.WriteLine("error called ReceiveGameInput() but game is not running");
			}

			if (message.simStep == simStepCount + 1)
			{
				// this is needed to do the next simulation step
				this.collectedInputEvents[message.myPlayerNumber] = new InputEvent
				{
					UnitTypePlaced = message.UnitPlaced,
					X = message.X,
					Y = message.Y
				};
			}
			else if (message.simStep > simStepCount + 1)
			{
				// not needed yet, delay
				this.futureEvents.Add(message);
			}
			else
			{
				// this should never happen - received input for a sim step that is already over
				throw new ApplicationException("received message from the past :(");
			}
		}

		public void NextSimulationStep()
		{
			System.Diagnostics.Debug.WriteLine("NextSimulationStep()");

			if (!this.running)
				return;

			// proceed to next step ONLY if all inputs have arrived!
			foreach (var input in this.collectedInputEvents)
			{
				if (input == null)
				{
					// at least one input is missing
					return;
				}
			}

			// evaluate inputs
			for (int i = 0; i < this.collectedInputEvents.Length; ++i)
			{
				var input = this.collectedInputEvents[i];
				if (input.UnitTypePlaced > 0)
				{
					Soldier soldier = new Soldier(this, i, (Soldier.SoldierType)input.UnitTypePlaced)
					{
						X = input.X,
						Y = input.Y
					};

					this.allGameObjects.Add(soldier);
					this.allSoldiers.Add(soldier);
				}

				this.collectedInputEvents[i] = null;
			}

			// next simulation step
			this.simStepCount++;

			foreach (var g in allGameObjects)
			{
				g.NextSimulationStep(this);
			}

			SendInput();

			// if we already have inputs for the new simulation step, use them now
			if (this.futureEvents.Count > 0)
			{
				var oldList = this.futureEvents;
				this.futureEvents = new List<GameInput>();
				foreach (var message in oldList)
				{
					ReceiveGameInput(message);
				}
			}

			myEnergy += 0.1f;//recover energy

			CollectGarbage();
			CheckGameEnd();
		}

		public void Render(Graphics g)
		{
			foreach (var o in allGameObjects)
			{
				o.Render(g);
			}
		}
		#endregion

		#region Soldier Helpers
		public void SpawnSoldier(float x, float y)
		{
			if (!running)//skip if game not running
				return;

			if (myPlayerNumber != (x < (float)boardWidth / 2f ? 0 : 1))//skip if input out of bounds
				return;

			float cost = SoldierCost(queuedSoldierType);

			if (myEnergy < cost)//skip if not enough energy
				return;

			myEnergy -= cost;//pay energy

			this.myInput = new InputEvent()
			{
				UnitTypePlaced = (byte)queuedSoldierType,
				X = x,
				Y = y
			};
		}

		public void SetQueuedSoldierType(Soldier.SoldierType type) => queuedSoldierType = type;

		/// <summary>
		/// Returns the best target and whether it is in shooting range.
		/// If no fitting targets are available returns shooter.
		/// </summary>
		public Tuple<Soldier, bool> FindBestTarget(Soldier shooter)
		{
			Soldier bestCandidate = shooter;//best target found so far
			bool isInAttackRange = false;//found a target in attack range yet?

			foreach (Soldier target in allSoldiers.FindAll(x => x.Player != shooter.Player))
			{
				float sqDist = GameObject.SquareDistance(shooter.X, shooter.Y, target.X, target.Y);

				if (sqDist < shooter.FireRange * shooter.FireRange)//enemy in fire range
				{
					if (!isInAttackRange || target.HP < bestCandidate.HP || bestCandidate == shooter)
					{
						bestCandidate = target;
						isInAttackRange = true;
					}
				}
				else if (!isInAttackRange && sqDist < shooter.ViewRange * shooter.ViewRange)//enemy in view range
				{
					if (target.HP < bestCandidate.HP || bestCandidate == shooter)
					{
						bestCandidate = target;
					}
				}
			}

			return new Tuple<Soldier, bool>(bestCandidate, isInAttackRange);
		}

		private float SoldierCost(Soldier.SoldierType type)
		{
			switch (type)
			{
				case Soldier.SoldierType.AchyArcher:
					return 2f;
				case Soldier.SoldierType.BlindBertha:
					return 6f;
				case Soldier.SoldierType.CrudeCommoner:
					return 3f;
				case Soldier.SoldierType.NONE:
				default:
					return 2f;
			}
		}
		#endregion

		#region Garbage
		public void PutIntoGarbage(Soldier soldier) => allGarbage.Add(soldier);

		private void CollectGarbage()
		{
			foreach (GameObject o in allGarbage)
			{
				if (allGameObjects.Contains(o))
					allGameObjects.Remove(o);
				if (o is Soldier && allSoldiers.Contains((Soldier)o))
					allSoldiers.Remove((Soldier)o);
			}

			allGarbage.Clear();
		}
		#endregion
	}
}
