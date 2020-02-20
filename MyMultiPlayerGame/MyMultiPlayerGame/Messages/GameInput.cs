using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMultiPlayerGame.Messages
{
    class GameInput : MessageBase
    {
        public override MessageTypes MessageType { get { return MessageTypes.GameInput; } }

        public int simStep;
        public int myPlayerNumber;
        public int simulationStepNumber;
        public byte UnitPlaced;
        public float X;
        public float Y;

        public override void Send(BinaryWriter writer)
        {
            writer.Write(simStep);
            writer.Write(myPlayerNumber);
            writer.Write(simulationStepNumber);
            writer.Write(UnitPlaced);
            writer.Write(X);
            writer.Write(Y);
        }

        public override void Receive(BinaryReader reader)
        {
            this.simStep = reader.ReadInt32();
            this.myPlayerNumber = reader.ReadInt32();
            this.simulationStepNumber = reader.ReadInt32();
            this.UnitPlaced = reader.ReadByte();
            this.X = reader.ReadSingle();
            this.Y = reader.ReadSingle();
        }
    }
}

