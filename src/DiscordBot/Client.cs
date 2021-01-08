using System;
using System.Net;
using System.Net.WebSockets;
using System.Threading;
using Discord.Payload;

namespace Discord
{
    class Client
    {
        const string GATEWAY_URI = "wss://gateway.discord.gg/?v=6&encoding=json";

        ClientWebSocket client;
        byte[] buffer = new byte[4096];
        CancellationToken cat = new CancellationToken();

        static void Main(string[] args)
        {
            Client client = new Client();
            client.Start();
        }

        public void Start()
        {
            client = new ClientWebSocket();
            client.ConnectAsync(new Uri(GATEWAY_URI), cat);
        }
    }
}
