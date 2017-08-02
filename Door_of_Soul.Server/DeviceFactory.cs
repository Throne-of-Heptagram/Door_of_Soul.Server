using Door_of_Soul.Communication.Infrastructure.Server.Device;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Door_of_Soul.Server
{
    public class DeviceFactory
    {
        public static DeviceFactory Instance { get; private set; } = new DeviceFactory();

        private Dictionary<Guid, ServerDevice> deviceDictionary = new Dictionary<Guid, ServerDevice>();
        private object resourceLock = new object();
        public IEnumerable<ServerDevice> Devices
        {
            get
            {
                lock(resourceLock)
                {
                    return deviceDictionary.Values.ToArray();
                }
            }
        }

        public event Action<ServerDevice> OnDeviceAdded;
        public event Action<ServerDevice> OnDeviceRemoved;

        private DeviceFactory()
        {

        }

        

        public bool ContainsDevice(Guid guid)
        {
            return deviceDictionary.ContainsKey(guid);
        }

        public ServerDevice CreateDevice(DeviceCommunicationInterface communicationInterface)
        {
            lock(resourceLock)
            {
                Guid guid = Guid.NewGuid();
                while (ContainsDevice(guid))
                {
                    guid = Guid.NewGuid();
                }
                ServerDevice device = new ServerDevice(guid, communicationInterface);
                deviceDictionary.Add(device.Guid, device);
                OnDeviceAdded?.Invoke(device);
                return device;
            }
        }

        public bool RemoveDevice(Guid guid)
        {
            lock (resourceLock)
            {
                if (ContainsDevice(guid))
                {
                    ServerDevice device = deviceDictionary[guid];
                    deviceDictionary.Remove(device.Guid);
                    OnDeviceRemoved?.Invoke(device);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
