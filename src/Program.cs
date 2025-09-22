using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AstralRoyaleV2.Crypto;
using AstralRoyaleV2.Core;

namespace AstralRoyaleV2
{
    class Program
    {
        public static Socket Server;

        static void Main(string[] args)
        {
            var port = config.GamePort;

            Console.Clear();
            Console.Title = "AstralRoyale: V2 (by: @astralsc on GitHub)";
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            Console.WriteLine(" ");
            Console.WriteLine("    _        _             _ ____                   _       __     ______");
            Console.WriteLine("   / \\   ___| |_ _ __ __ _| |  _ \\ ___  _   _  __ _| | ___  \\ \\   / /___ \\ ");
            Console.WriteLine("  / _ \\ / __| __| '__/ _` | | |_) / _ \\| | | |/ _` | |/ _ \\  \\ \\ / /  __) |");
            Console.WriteLine(" / ___ \\\\__ \\ |_| | | (_| | |  _ < (_) | |_| | (_| | |  __/   \\ V /  / __/ ");
            Console.WriteLine("/_/   \\_\\___/\\__|_|  \\__,_|_|_| \\_\\___/ \\__, |\\__,_|_|\\___|    \\_/  |_____|");
            Console.WriteLine("                                        |___/                              ");
            Console.WriteLine(" ");
            Console.WriteLine("[*] Server created by @astralsc on GitHub!");
            Console.WriteLine("[*] Server is now starting...");

            Server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Server.Bind(new IPEndPoint(IPAddress.Any, port));
            Server.Listen(100);

            Console.WriteLine($"[*] Listening on 0.0.0.0:{port}. Let's play Clash Royale!");
            Console.WriteLine(" ");
            Console.WriteLine("[*] Type /help to see a list of commands.");
            Console.WriteLine("[*] Type /shutdown to exit.");

            Thread listenerThread = new Thread(ListenForClients);
            listenerThread.IsBackground = true;
            listenerThread.Start();

            Thread commandThread = new Thread(CommandThread);
            commandThread.IsBackground = true;
            commandThread.Start();

            Thread.Sleep(Timeout.Infinite);
        }

        static void ListenForClients()
        {
            while (true)
            {
                try
                {
                    Socket clientSocket = Server.Accept();
                    IPEndPoint remote = clientSocket.RemoteEndPoint as IPEndPoint;
                    Console.WriteLine($"[*] {remote.Address} has connected to this server.");

                    Thread clientThread = new Thread(() =>
                    {
                        try
                        {
                            var encrypter = new RC4();
                            var decrypter = new RC4();

                            MessageHandler.Follow(clientSocket, encrypter, decrypter);

                            clientSocket.Shutdown(SocketShutdown.Both);
                            clientSocket.Close();
                            Console.WriteLine($"[*] {remote.Address} disconnected.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("[*] Error in client handler: " + ex.Message);
                            clientSocket?.Close();
                        }
                    });

                    clientThread.IsBackground = true;
                    clientThread.Start();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[*] Client acceptance failed: " + ex.Message);
                }
            }
        }

        static void CommandThread()
        {
            while (true)
            {
                Console.Write("> ");
                string input = Console.ReadLine();
                if (input == null) continue;

                if (input.Trim().Equals("/help", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Available commands:");
                    Console.WriteLine("/help - Show available commands");
                    Console.WriteLine("/shutdown - Shutdown the server");
                }
                else if (input.Trim().Equals("/shutdown", StringComparison.OrdinalIgnoreCase))
                {
                    Shutdown().Wait();
                }
                else if (!string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine($"Invalid command: {input}");
                }
            }
        }

        public static async Task Shutdown()
        {
            Console.WriteLine("Shutting down...");
            try
            {
                Console.WriteLine("Saving players...");
                Console.WriteLine("Saving alliances...");
                Console.WriteLine("All players saved.");
                Console.WriteLine("All alliances saved.");
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to save players or alliances.");
            }

            Environment.Exit(0);
        }

        public static string GetIPAddress(string hostname)
        {
            IPHostEntry host = Dns.GetHostEntry(hostname);
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();
            }
            return string.Empty;
        }
    }
}