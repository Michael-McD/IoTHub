using System;
using System.Device.Gpio;
using System.Threading;

namespace Opener
{
    public class PiController
    {
        private readonly GpioController gpioController;
        private readonly int upPinNum = 24;
        private readonly int downPinNum = 26;

        private readonly int duration = 3000;

        public PiController(GpioController controller)
        {
            gpioController = controller;

            gpioController.OpenPin(upPinNum, PinMode.Output);
            gpioController.Write(upPinNum, PinValue.High);

            gpioController.OpenPin(downPinNum, PinMode.Output);
            gpioController.Write(downPinNum, PinValue.High);
        }

        public void Up()
        {
            gpioController.Write(upPinNum, PinValue.Low);
            Thread.Sleep(duration);
            Down();
            Down();
        }

        public void Down()
        {
            gpioController.Write(downPinNum, PinValue.Low);
            Thread.Sleep(duration);

            gpioController.Write(upPinNum, PinValue.High);
            gpioController.Write(downPinNum, PinValue.High);
        }
        public void Stop()
        {
            Console.WriteLine("Setting pins to HIGH -- i.e. off.");
            gpioController.Write(upPinNum, PinValue.High);
            gpioController.Write(downPinNum, PinValue.High);
        }

        internal void Exit()
        {            
            Console.WriteLine("Closing pins.");
            gpioController.ClosePin(upPinNum);
            gpioController.ClosePin(downPinNum);

            gpioController.Dispose();
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