using System;
using System.Collections.Generic;
using System.Text;
using Discord;

namespace Discord
{
    class Bot
    {
        static void Main()
        {
            Client client = new Client("YOUR TOKEN HERE");
            client.Connect();
            Console.ReadLine();
        }
    }
}
