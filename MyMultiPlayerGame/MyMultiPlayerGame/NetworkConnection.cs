using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MyMultiPlayerGame.Messages;

namespace MyMultiPlayerGame
{
    class NetworkConnection : IDisposable
    {
        private TcpClient client;
        private Thread thread;
        private volatile bool stopped;
        private MessageProtocol messageProtocol;

        public event Action<MessageBase> MessageReceived;

        public NetworkConnection(TcpClient c)
        {
            client = c;

            messageProtocol = new MessageProtocol();
            messageProtocol.MessageComplete += MessageProtocol_MessageComplete;

            thread = new Thread(ThreadProc);
            thread.Start();
        }

        public void Dispose()
        {
            stopped = true;
            thread.Join();   // wait for thread end
        }

        private void MessageProtocol_MessageComplete(byte[] bytes, int size)
        {
            var message = MessageBase.Receive(bytes, size);
            if (MessageReceived != null)
                MessageReceived(message);
        }

        public void Send(MessageBase m)
        {
            var bytes = m.Send();
            client.GetStream().Write(bytes, 0, bytes.Length);
        }

        public void ThreadProc()
        {
            byte[] receiveBuffer = new byte[1024];

            while (!stopped)
            {
                while (client.GetStream().DataAvailable)
                {
                    int bytesRead = client.GetStream().Read(receiveBuffer, 0, receiveBuffer.Length);
                    messageProtocol.ReceiveData(receiveBuffer, 0, bytesRead);
                }

                Thread.Sleep(1);
            }
        }
    }
}
