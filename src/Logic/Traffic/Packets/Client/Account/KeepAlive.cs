using AstralRoyaleV2.Crypto.Binary;
using AstralRoyaleV2.Logic.Traffic.Packets.Server.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AstralRoyaleV2.Logic.Traffic.Packets.Client.Account
{
    internal class KeepAlive
    {
        /// <summary>
        /// Packet's ID.
        /// </summary>
        public static int Identifier
        {
            get
            {
                return 10108;
            }
        }

        /// <summary>
        /// Packet's new instance.
        /// </summary>
        public KeepAlive()
        {
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        public void Process(Socket client)
        {
            new KeepAliveOk().Send(client);
        }
    }
}