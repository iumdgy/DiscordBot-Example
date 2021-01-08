using System;
using static System.Console;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Discord
{
    class Client
    {
        const string GATEWAY_URI = "wss://gateway.discord.gg/?v=6&encoding=json";
        const string PAYLOAD = @"{
    ""op"": 2,
    ""d"": {
        ""token"": ""NzU0Njk4MTU0Nzc1MDE5NTgw.X14hbQ.Nt3KyFUqOmu5CfoTzCgBiYEo0r8"",
        ""properties"": {
            ""$os"": ""linux"",
            ""$browser"": ""disco"",
            ""$device"": ""disco""
        }
    },
    ""s"": null,
    ""t"": null
}";
        const string HEARTBEAT = @"{
    ""op"": 11
}";
        const string RESUME = @"{
  ""op"": 6,
  ""d"": {
    ""token"": ""NzU0Njk4MTU0Nzc1MDE5NTgw.X14hbQ.Nt3KyFUqOmu5CfoTzCgBiYEo0r8"",
    ""session_id"": ""session_id_i_stored"",
    ""seq"": 1337
  }
}";

        ClientWebSocket client;
        byte[] buffer = new byte[4096];
        JObject jsonPool;
        CancellationToken cat = new CancellationToken();

        static void Main(string[] args)
        {
            Client client = new Client();
            client.Start().GetAwaiter().GetResult();
        }

        public async Task Start()
        {
            client = new ClientWebSocket();
            await client.ConnectAsync(new Uri(GATEWAY_URI), cat);
            await client.ReceiveAsync(buffer, cat);
            jsonPool = JObject.Parse(Encoding.UTF8.GetString(buffer));
            Array.Clear(buffer, 0, buffer.Length);

            WriteLine(client.State);

            /* start heartbeat */
            new Thread(new ThreadStart(SendHeardBeat)).Start();

            WriteLine(client.State);

            /* send payload */
            await client.SendAsync(Encoding.UTF8.GetBytes(PAYLOAD), WebSocketMessageType.Binary, false, cat);
            await client.ReceiveAsync(buffer, cat);
            WriteLine(Encoding.UTF8.GetString(buffer));
            Array.Clear(buffer, 0, buffer.Length);

            WriteLine(client.State);

            /* send resume */
            await client.SendAsync(Encoding.UTF8.GetBytes(RESUME), WebSocketMessageType.Binary, false, cat);
            await client.ReceiveAsync(buffer, cat);
            WriteLine(Encoding.UTF8.GetString(buffer));
            Array.Clear(buffer, 0, buffer.Length);

            WriteLine(client.State);
        }

        public void SendHeardBeat()
        {
            while (true)
            {
                client.SendAsync(Encoding.UTF8.GetBytes(HEARTBEAT), WebSocketMessageType.Binary, false, cat);
                Thread.Sleep(int.Parse(jsonPool["d"]["heartbeat_interval"].ToString()));
            }
        }
    }
}
