using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection.Abstractions;

namespace Opener
{
    public static class Program
    {
        public static IConfigurationRoot Configuration { get; set; }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello From Raspberry PI.");

            //var piController = new PiController(new GpioController(PinNumberingScheme.Board));
            var msgProcessor = new MsgProcessor();
            msgProcessor.StartProcessor();


        }
    }
}
