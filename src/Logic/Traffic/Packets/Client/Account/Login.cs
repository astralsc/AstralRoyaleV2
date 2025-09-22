using AstralRoyaleV2.Logic.Traffic.Packets.Server.Home;
using AstralRoyaleV2.Logic.Traffic.Packets.Server.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace AstralRoyaleV2.Logic.Traffic.Packets.Client.Account
{
    internal class Login
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
        public Login()
        {
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        public void Process(Socket client)
        {
            new LoginOk().Send(client);
            new OwnHome().Send(client);
        }
    }
}