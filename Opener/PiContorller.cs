using System;
using System.Threading;
using System.Device.Gpio;

namespace Opener
{
    public class PiController
    {
        private readonly GpioController piController;
        private readonly int upPinNum = 7;

        private readonly int downPinNum = 11;
        private readonly int duration = 300;

        public PiController(GpioController controller)
        {
            piController = controller;

            piController.OpenPin(upPinNum, PinMode.Output);
            piController.OpenPin(downPinNum, PinMode.Output);

            piController.Write(upPinNum, PinValue.High);
            piController.Write(downPinNum, PinValue.High);
        }

        public void Up()
        {
            piController.Write(upPinNum, PinValue.Low);
            Thread.Sleep(duration);
            Stop();
        }

        public void Down()
        {
            piController.Write(downPinNum, PinValue.Low);
            Thread.Sleep(duration);
            Stop();
        }
        public void Stop()
        {
            Console.WriteLine("Setting pins to HIGH then LOW.");
            piController.Write(upPinNum, PinValue.High);
            piController.Write(downPinNum, PinValue.High);
        }

        internal void Exit()
        {            
            Console.WriteLine("Closing pins.");
            piController.ClosePin(upPinNum);
            piController.ClosePin(downPinNum);
        }

        public void Test()
        {
            var shouldRun = 0;

            while (shouldRun < 2)
            {
                Up();
                Thread.Sleep(duration);
                Down();
                Thread.Sleep(duration);

                ++shouldRun;
            }
        }
    }
}