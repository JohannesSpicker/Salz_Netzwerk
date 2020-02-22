using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMultiPlayerGame.Messages
{
    abstract class MessageBase
    {
        public enum MessageTypes
        {
            ChatMessage,
            StartGame,
            StopGame,
            GameInput,
            ReadyMessage,
        }

        public abstract MessageTypes MessageType { get; }

        public byte[] Send()
        {
            MemoryStream memStream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(memStream);

            writer.Write((int)this.MessageType);
            writer.Write(0);//size placeholder
            Send(writer);

            long size = memStream.Length;
            memStream.Seek(4, SeekOrigin.Begin);
            writer.Write((int) size);

            return memStream.ToArray();
        }

        public abstract void Send(BinaryWriter writer);

        public static MessageBase Receive(byte[] bytes, int size)
        {
            MemoryStream memStream = new MemoryStream(bytes);
            BinaryReader reader = new BinaryReader(memStream);

            var type = reader.ReadInt32();
            var messageSize = reader.ReadInt32();

            System.Diagnostics.Debug.Assert(messageSize == size);

            MessageBase m = null;
            switch ((MessageTypes)type)
            {
                case MessageTypes.ChatMessage:
                    m = new ChatMessage();
                    break;
                case MessageTypes.StartGame:
                    m = new StartGame();
                    break;
                case MessageTypes.StopGame:
                    m = new StopGame();
                    break;
                case MessageTypes.GameInput:
                    m = new GameInput();
                    break;
                case MessageTypes.ReadyMessage:
                    m = new ReadyMessage();
                    break;
            }

            System.Diagnostics.Debug.Assert(m != null);

            m.Receive(reader);

            return m;
        }

        public abstract void Receive(BinaryReader reader);

    }
}
