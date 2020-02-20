using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMultiPlayerGame.Messages
{
    class ReadyMessage : MessageBase
    {
        public override MessageTypes MessageType { get { return MessageTypes.ReadyMessage; } }

        public string Sender;
        public bool isReady;

        public override void Send(BinaryWriter writer)
        {
            writer.Write(this.Sender);
            writer.Write(this.isReady);
        }

        public override void Receive(BinaryReader reader)
        {
            this.Sender = reader.ReadString();
            this.isReady = reader.ReadBoolean();
        }
    }
}
