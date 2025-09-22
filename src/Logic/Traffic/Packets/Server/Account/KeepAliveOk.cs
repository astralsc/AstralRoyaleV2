using AstralRoyaleV2.Crypto.Binary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AstralRoyaleV2.Logic.Traffic.Packets.Server.Account
{
    internal class KeepAliveOk
    {
        /// <summary>
        /// Packet's ID.
        /// </summary>
        public int Identifier
        {
            get
            {
                return 20108;
            }
        }

        /// <summary>
        /// Packet's version.
        /// </summary>
        public int Version
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Packet's writer.
        /// </summary>
        public Writer Writer;

        /// <summary>
        /// Packet's new instance.
        /// </summary>
        public KeepAliveOk()
        {
            this.Writer = new Writer();
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        private byte[] Encode()
        {
            return this.Writer.Patch(this.Identifier, this.Version);
        }

        /// <summary>
        /// Sends this instance.
        /// </summary>
        public void Send(Socket client)
        {
            client.Send(this.Encode());
        }
    }
}