using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AstralRoyaleV2.Crypto;

namespace AstralRoyaleV2.Core.Packets
{
    internal class PacketHandler
    {
        /// <summary>
        /// Identifies the specified packet.
        /// </summary>
        /// <param name="Packet"></param>
        /// <returns></returns>
        public static int Identify(byte[] Packet)
        {
            try
            {
                string[] Bytes = BitConverter.ToString(Packet).Split('-');
                return int.Parse(Bytes[0] + Bytes[1], NumberStyles.HexNumber);
            }

            catch (IndexOutOfRangeException)
            {
                //Console.WriteLine("Minion's king has disconnected.");
                Thread.Sleep(2500);
                Environment.Exit(0);
            }

            return 0;
        }

        /// <summary>
        /// Patches the specified packet.
        /// </summary>
        /// <param name="Packet"></param>
        /// <param name="Decrypter"></param>
        /// <returns></returns>
        public static byte[] Patch(byte[] Packet, RC4 Decrypter)
        {
            List<byte> Header = new List<byte>(Packet.Take(7).Reverse());
            List<byte> Decrypted = new List<byte>(Decrypter.Decrypt(Packet.Skip(7).ToArray()));

            foreach (byte Byte in Header)
            {
                Decrypted.Insert(0, Byte);
            }

            return Decrypted.ToArray();
        }
    }
}
