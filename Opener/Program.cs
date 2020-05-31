using System;
using System.Device.Gpio;

namespace Opener
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello From Raspberry PI.");

            //var piController = new PiController(new GpioController(PinNumberingScheme.Board));
            var msgProcessor = new MsgProcessor();
            msgProcessor.StartProcessor();

            // var run = true;
            // while (run)
            // {
            //     Console.WriteLine("Enter command: Up, Down, Stop or exit.");
            //     var input = Console.ReadLine().ToLower();

            //     switch (input)
            //     {
            //         case "up":
            //             piController.Up();
            //             break;
            //         case "down":
            //             piController.Down();
            //             break;
            //         case "stop":
            //             piController.Stop();
            //             break;
            //         case "exit":
            //             run = false;
            //             piController.Exit();
            //             break;
            //         default:
            //             Console.WriteLine("Please enter a valid command.");
            //             break;
            //     }
            // }
        }
    }
}
