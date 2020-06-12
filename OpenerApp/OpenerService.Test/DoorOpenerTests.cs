using System;
using Xunit;
using OpenerService;

namespace OpenerService.Test
{
    public class DoorOpenerTests
    {
        // az iot hub show-connection-string --policy-name service --name GarageDoorHub-y500ew --output tsv
        private readonly string ConnStr = "";
        
        [Fact]
        public void DoorUp()
        {
            var iotService = new IoTService(ConnStr);
            iotService.DoorUp();
        }
        [Fact]
        public void DoorDown()
        {
            var iotService = new IoTService(ConnStr);
            iotService.DoorDown();
        }
        [Fact]
        public void DoorStop()
        {
            var iotService = new IoTService(ConnStr);
            iotService.DoorStop();
        }
    }
}
