using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Door_of_Soul.Server.Test
{
    [TestClass]
    public class DeviceFactoryUnitTest
    {
        [TestMethod]
        public void CreateDevice1()
        {
            ServerDevice device = DeviceFactory.Instance.CreateDevice(null);
            Assert.IsNotNull(device);
            Assert.IsTrue(DeviceFactory.Instance.ContainsDevice(device.Guid));
        }
        [TestMethod]
        public void RemoveDevice1()
        {
            ServerDevice device = DeviceFactory.Instance.CreateDevice(null);
            Assert.IsTrue(DeviceFactory.Instance.ContainsDevice(device.Guid));
            Assert.IsTrue(DeviceFactory.Instance.RemoveDevice(device.Guid));
        }
    }
}
