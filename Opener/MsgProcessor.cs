using System;
using System.Device.Gpio;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Text.Json;
using Opener.Models;

namespace Opener
{
    public class MsgProcessor
    {
        private DeviceClient deviceClient;
        readonly IServiceProvider services = ServiceProviderBuilder.GetServiceProvider(Array.Empty<string>());

        private PiController piController;

        public void StartProcessor()
        {
            Console.WriteLine("Door Message Processor Listening. Ctrl-C to exit.\n");

            var secret = services.GetRequiredService<IOptions<SecretOptions>>();

            piController = new PiController(new GpioController(PinNumberingScheme.Board));

            // Connect to the IoT hub using the MQTT protocol
            deviceClient = DeviceClient.CreateFromConnectionString(secret.Value.DeviceConnStr, TransportType.Mqtt);

            // Create a handler for the direct method call
            deviceClient.SetMethodHandlerAsync("OperateDoor", OperateDoor, null).Wait();
        }

        // Handle the direct method call
        private Task<MethodResponse> OperateDoor(MethodRequest methodRequest, object userContext)
        {
            var command = Encoding.UTF8.GetString(methodRequest.Data);
            string result;

            var garageCommand = JsonSerializer.Deserialize<GarageCommand>(command);

            switch (garageCommand.Value)
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