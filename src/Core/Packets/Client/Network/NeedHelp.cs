using AstralRoyaleV2.Crypto.Binary;
using AstralRoyaleV2.Logic.Traffic.Packets.Server.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using AstralRoyaleV2.Core.Packets.Server.Network;

namespace AstralRoyaleV2.Core.Packets.Client.Network
{
    internal class NeedHelp
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
        public NeedHelp()
        {
            this.Process();
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        public void Process()
        {
            new KeepAlive();
        }
    }
}
