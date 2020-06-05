using System;
using Microsoft.Extensions.Configuration;

namespace Opener
{
    public static class Program
    {
        public static IConfigurationRoot Configuration { get; set; }
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Garage Door Opener listener.");

            var msgProcessor = new MsgProcessor();
            msgProcessor.StartProcessor();

            Console.WriteLine("Ctl-C or enter CRLF to close.");
            Console.ReadLine();
        }
    }
}
