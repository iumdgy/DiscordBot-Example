using System;
using System.Net;
using System.Net.Sockets;

namespace DiscordBot
{
    class Client
    {
        Socket client;

        static void Main(string[] args)
        {
            Client client = new Client();
            client.Start();
        }

        public void Start()
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(new IPEndPoint(Dns.GetHostEntry("gateway.discord.gg").AddressList[0], 443));
        }
    }
}
