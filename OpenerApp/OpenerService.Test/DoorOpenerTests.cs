using System;
using Xunit;
using OpenerService;

namespace OpenerService.Test
{
    public class DoorOpenerTests
    {
        [Fact]
        public void Test1()
        {
            var iotService = new IoTService();
            var actual = iotService.Test();

            Assert.Equal("Test method called.", actual);
        }

        [Fact]
        public void DoorUp()
        {
            var iotService = new IoTService();
            iotService.DoorUp();
        }
    }
}
