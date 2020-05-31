using System;
using Microsoft.Azure.Devices.Client;
using System.Threading.Tasks;
using System.Text;

namespace Opener
{
    public class MsgProcessor
    {
        private DeviceClient s_deviceClient;

       private readonly static string s_connectionString =Environment.GetEnvironmentVariable("DEVICE_CON_STR");
       private PiController piController;

        public void StartProcessor()
        {
            Console.WriteLine("Door Mesage Processor Listening. Ctrl-C to exit.\n");

            piController = new PiController(new System.Device.Gpio.GpioController());

            // Connect to the IoT hub using the MQTT protocol
            s_deviceClient = DeviceClient.CreateFromConnectionString(s_connectionString, TransportType.Mqtt);

            // Create a handler for the direct method call
            s_deviceClient.SetMethodHandlerAsync("OperateDoor", OperateDoor, null).Wait();
        }

        // Handle the direct method call
        private Task<MethodResponse> OperateDoor(MethodRequest methodRequest, object userContext)
        {
            var data = Encoding.UTF8.GetString(methodRequest.Data);
            string result;

            switch (data)
            {
                case "up":
                    piController.Up();
                    break;
                case "down":
                    piController.Down();
                    break;
                case "stop":
                    piController.Stop();
                    break;
                case "exit":
                    piController.Exit();
                    break;
                default:
                    result = "{\"result\":\"Invalid parameter\"}";
                    return Task.FromResult(new MethodResponse(Encoding.UTF8.GetBytes(result), 400));
            }

            // Acknowledge the direct method call with a 200 success message
            result = "{\"result\":\"Executed direct method: " + methodRequest.Name + "\"}";
            return Task.FromResult(new MethodResponse(Encoding.UTF8.GetBytes(result), 200));            
        }
    }
}