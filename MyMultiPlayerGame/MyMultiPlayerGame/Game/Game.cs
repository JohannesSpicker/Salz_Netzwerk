using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using MyMultiPlayerGame.Messages;

namespace MyMultiPlayerGame.Game
{
    class Game
    {
        bool running;
        int simStepCount;
        List<GameObject> allGameObjects = new List<GameObject>();

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

        InputEvent[] collectedInputEvents;                              // events needed for next simulation
        List<GameInput> futureEvents = new List<GameInput>();           // events needed after next simulation

        InputEvent myInput = null;
        List<NetworkConnection> peers;

        public bool isMeReady = false;
        public bool isOppReady = false;
        
        public void ReceiveReadyInput(ApplicationWindow appWindow, ReadyMessage message)
        {
            this.isOppReady = message.isReady;
            appWindow.DisplayChatmessage(message.Sender + " is " + (this.isOppReady ? "" : "not ") + "ready."); 
        }

        public void ReadyCheck()
        {
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

            SendInput();

            System.Diagnostics.Debug.WriteLine("Start() end");
        }

        public void Stop()
        {
            this.running = false;
        }

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
                    this.allGameObjects.Add(new Soldier(this, i)
                    {
                        X = input.X,
                        Y = input.Y
                    });
                }

                this.collectedInputEvents[i] = null;
            }

            // next simulation step
            this.simStepCount++;

            foreach (var g in allGameObjects)
            {
                g.NextSimulationStep();
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
        }

        public void Render(Graphics g)
        {
            foreach (var o in allGameObjects)
            {
                o.Render(g);
            }
        }

        public void SpawnSoldier(float x, float y)
        {
            if (!running)
                return;

            this.myInput = new InputEvent()
            {
                UnitTypePlaced = 1,
                X = x,
                Y = y
            };
        }

    }
}
