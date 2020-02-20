using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMultiPlayerGame.Messages
{
    class ChatMessage : MessageBase
    {
        public override MessageTypes MessageType { get { return MessageTypes.ChatMessage; } }

        public string Sender;
        public string Text;

        public override void Send(BinaryWriter writer)
        {
            writer.Write(this.Sender);
            writer.Write(this.Text);
        }

        public override void Receive(BinaryReader reader)
        {
            this.Sender = reader.ReadString();
            this.Text = reader.ReadString();
        }

    }
}
