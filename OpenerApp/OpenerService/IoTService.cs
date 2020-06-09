using System;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;

namespace OpenerService
{
    public class IoTService
    {
        public string Test()
        {
            return "Test method called.";
        }

        private static ServiceClient serviceClient;
        
        // Connection string for your IoT Hub
        // az iot hub show-connection-string --hub-name {your iot hub name} --policy-name service
        private readonly static string connectionString = "HostName=GarageDoorHub-y500ew.azure-devices.net;SharedAccessKeyName=service;SharedAccessKey=NWZyS3CVul4ypICoRLoLuzqIyBvLi/UhscJoO3zL/vQ=";

        // Invoke the direct method on the device, passing the payload
        private static async Task InvokeUp()
        {
            var methodInvocation = new CloudToDeviceMethod("OperateDoor") { ResponseTimeout = TimeSpan.FromSeconds(30) };
            methodInvocation.SetPayloadJson("{'Value':'up'}");

            // Invoke the direct method asynchronously and get the response from the simulated device.
            var response = await serviceClient.InvokeDeviceMethodAsync("DoorOpener", methodInvocation);

            Console.WriteLine("Response status: {0}, payload:", response.Status);
            Console.WriteLine(response.GetPayloadAsJson());
        }

        public void DoorUp()
        {
            Console.WriteLine("IoT Hub Quickstarts #2 - Back-end application.\n");

            // Create a ServiceClient to communicate with service-facing endpoint on your hub.
            serviceClient = ServiceClient.CreateFromConnectionString(connectionString);
            InvokeUp().GetAwaiter().GetResult();
            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
        }

    }
}
