using System;
using System.Collections.Generic;
using System.Text;

namespace Discord.Payload
{
    class Payload
    {
        public static string heartbeat = "{\"op\" : 1, \"d\" : null}";
        public static string identify = @"{
    ""op"" : 2,
    ""d"" : {
        ""token"" : """",
        ""properties"" : {
            ""$os"" : ""window"",
            ""$browser"" : ""c#"",
            ""$device"" : ""c#""
        }
    }
}";
    }
}
