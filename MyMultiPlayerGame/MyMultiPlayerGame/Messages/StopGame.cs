using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMultiPlayerGame.Messages
{
    class StopGame : MessageBase
    {
        public override MessageTypes MessageType { get { return MessageTypes.StopGame; } }

        public bool isTie;
        public bool isReceiverWinner;

        public override void Send(BinaryWriter writer)
        {
            writer.Write(this.isTie);
            writer.Write(this.isReceiverWinner);
        }

        public override void Receive(BinaryReader reader)
        {
            this.isTie = reader.ReadBoolean();
            this.isReceiverWinner = reader.ReadBoolean();
        }
    }
}
