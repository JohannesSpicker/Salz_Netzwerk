using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMultiPlayerGame
{
    class MessageProtocol
    {
        public event Action<byte[], int> MessageComplete;

        public byte[] collectBuffer = new byte[1024*64];
        private int bytesCollected;
        private int bytesNeeded;


        public void ReceiveData(byte[] receiveBuffer, int offset, int newBytes)
        {
            for (int i = offset; i < offset + newBytes; ++i)
            {
                collectBuffer[bytesCollected] = receiveBuffer[i];
                bytesCollected++;

                if (bytesCollected == 8)
                {
                    var reader = new BinaryReader(new MemoryStream(collectBuffer));
                    var type = reader.ReadInt32();
                    bytesNeeded = reader.ReadInt32();
                }

                if (bytesNeeded > 0 && bytesCollected == bytesNeeded)
                {
                    MessageComplete(collectBuffer, bytesCollected);
                    bytesCollected = 0;
                    bytesNeeded = 0;
                }
            }
        }
    }
}
