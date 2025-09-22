using AstralRoyaleV2;
using AstralRoyaleV2.Core.Packets;
using AstralRoyaleV2.Core.Packets.Client.Home;
using AstralRoyaleV2.Core.Packets.Client.Network;
using AstralRoyaleV2.Core.Packets.Server.Home;
using AstralRoyaleV2.Core.Packets.Server.Network;
using AstralRoyaleV2.Crypto;
using System;
using System.Diagnostics;
using System.Net.Sockets;

namespace AstralRoyaleV2.Core
{
    internal class MessageHandler
    {
        public static void Follow(Socket client, RC4 encrypter, RC4 decrypter)
        {
            //Console.WriteLine("Minion's king has connected.");

            try
            {
                while (true)
                {
                    byte[] incoming = Catch(client, decrypter);
                    int identifier = PacketHandler.Identify(incoming);

                    #region Packets
                    if (identifier == ClientHello.Identifier)
                    {
                        new ClientHello();
                    }
                    if (identifier == NeedHelp.Identifier)
                    {
                        new NeedHelp();
                    }
                    #endregion

                    
                    #region Commands
                    /*if (identifier == 14600)
                    {
                        new RequestNewName(incoming);
                    }

                    if (identifier == 14102)
                    {
                        new EndClient(incoming);
                    }

                    if (identifier == BattleNPC.Identifier)
                    {
                        new BattleNPC();
                    }*/

                    if (identifier == 14101)
                    {
                        new GetHomeData();
                    }
                    #endregion
                    
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static byte[] Catch(Socket client, RC4 decrypter)
        {
            byte[] draft = new byte[2048];
            int length = client.Receive(draft);

            if (length == 0)
                throw new Exception("Client disconnected.");

            byte[] packet = new byte[length];
            Array.Copy(draft, packet, length);

            byte[] patched = PacketHandler.Patch(packet, decrypter);
            Debug.WriteLine("[<] " + PacketHandler.Identify(packet));
            return patched;
        }

        public static void Throw(Socket client, byte[] packet, RC4 encrypter)
        {
            client.Send(packet);
            Debug.WriteLine("[>] " + PacketHandler.Identify(packet));
        }
    }
}