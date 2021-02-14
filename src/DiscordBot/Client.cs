using System;
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
        readonly string TOKEN;

        ClientWebSocket client;
        byte[] buffer = new byte[4096];
        UTF8Encoding encoding = new UTF8Encoding();

        public Client(string token)
        {
            TOKEN = token;
        }

        public async void Connect()
        {
            client = new ClientWebSocket();
            await client.ConnectAsync(new Uri(GATEWAY_URI), CancellationToken.None);
            await client.ReceiveAsync(buffer, CancellationToken.None);

            JObject json = JObject.Parse(Encoding.UTF8.GetString(buffer));
            Array.Clear(buffer, 0, buffer.Length);

            string t = json["t"].ToString();
            string s = json["s"].ToString();
            string op = json["op"].ToString();
            int heartbeatInterval = int.Parse(json["d"]["heartbeat_interval"].ToString());
            Console.WriteLine(json.ToString());
            Thread sendHeartbeat = new Thread(() => SendHeartbeat(heartbeatInterval));
            sendHeartbeat.Start();
            SendIdentify();

        }

        async void SendIdentify()
        {
            string properties = @"{""$os"" : ""window"", ""$browser"" : ""c#"", ""$device"" : ""c#""}";
            string identify = @$"{{""op"" : 2, ""d"" : {{""token"" : ""{TOKEN}"", ""properties"" : {properties}}} }}";
            await client.SendAsync(new ArraySegment<byte>(encoding.GetBytes(identify)), WebSocketMessageType.Text, true, CancellationToken.None);
            await client.ReceiveAsync(buffer, CancellationToken.None);
            Console.WriteLine(Encoding.UTF8.GetString(buffer));
        }

        async void SendHeartbeat(int interval)
        {
            string h = @"{""op"" : 1, ""d"" : null}";
            while (true)
            {
                await client.SendAsync(new ArraySegment<byte>(encoding.GetBytes(h)), WebSocketMessageType.Text, true, CancellationToken.None);
                await Task.Delay(interval);
            }
        }
    }
}
