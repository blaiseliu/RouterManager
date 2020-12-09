using System;

namespace RouterCommand
{
    public class Configuration
    {
        //public string RouterUrl { get; set; }

        //public string Username { get; set; }

        //public string Password { get; set; }

        //public TimeSpan ShortDelay { get; set; }
        //public TimeSpan LongDelay { get; set; }
        public static string StartUrl = "192.168.1.1";
        public static TimeSpan ShortDelay = TimeSpan.FromSeconds(5);
        public static TimeSpan LongDelay = TimeSpan.FromSeconds(60);
        public static string Username = "admin";
        public static string Password = "password";
    }
}
