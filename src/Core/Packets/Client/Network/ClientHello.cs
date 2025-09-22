using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AstralRoyaleV2.Core.Packets.Server.Home;
using AstralRoyaleV2.Core.Packets.Server.Network;

namespace AstralRoyaleV2.Core.Packets.Client.Network
{
    internal class ClientHello
    {
        /// <summary>
        /// Packet's ID.
        /// </summary>
        public static int Identifier
        {
            get
            {
                return 10101;
            }
        }

        /// <summary>
        /// Packet's new instance.
        /// </summary>
        public ClientHello()
        {
            this.Process();
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        public void Process()
        {
            new ServerHello();
            new GetHomeData();
        }
    }
}
