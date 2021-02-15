using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;

namespace DiscordBot
{
    class Bot
    {
        static void Main()
        {
            Client client = new Client("NzU0Njk4MTU0Nzc1MDE5NTgw.X14hbQ.exsUcyw56pjmAzuz9ws1-9Z0PFc");
            client.Connect();
            Console.ReadLine();
        }
    }
}
