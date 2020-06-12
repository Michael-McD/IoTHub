using System;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;

namespace OpenerService
{
    public class IoTService
    {
        private static ServiceClient serviceClient;
        
        private readonly string _connectionString;

        public IoTService(string connectionString)
        {
            _connectionString = connectionString;
        }

        private static async Task InvokeService(string command)
        {
            var methodInvocation = new CloudToDeviceMethod("OperateDoor") { ResponseTimeout = TimeSpan.FromSeconds(30) };
            methodInvocation.SetPayloadJson("{'Value':'" + command + "'}");

            // Invoke the direct method asynchronously and get the response from the simulated device.
            var response = await serviceClient.InvokeDeviceMethodAsync("DoorOpener", methodInvocation);

            Console.WriteLine("Response status: {0}, payload:", response.Status);
            Console.WriteLine(response.GetPayloadAsJson());
        }

        public void DoorUp()
        {
            serviceClient = ServiceClient.CreateFromConnectionString(_connectionString);
            InvokeService("up").GetAwaiter().GetResult();

            serviceClient.Dispose();
        }

        public void DoorDown()
        {
            serviceClient = ServiceClient.CreateFromConnectionString(_connectionString);
            InvokeService("down").GetAwaiter().GetResult();

            serviceClient.Dispose();
        }

        public void DoorStop()
        {
            serviceClient = ServiceClient.CreateFromConnectionString(_connectionString);
            InvokeService("stop").GetAwaiter().GetResult();

            serviceClient.Dispose();
        }

    }
}
