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
            Client client = new Client("NzU0Njk4MTU0Nzc1MDE5NTgw.X14hbQ.X2iYmIVkfhxtfnSAyvFMPIE7W38");
            client.Connect();
            Console.ReadLine();
        }
    }
}
