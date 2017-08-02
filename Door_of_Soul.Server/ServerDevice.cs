using Door_of_Soul.Core;
using System;
using Door_of_Soul.Communication.Infrastructure.Server.Device;

namespace Door_of_Soul.Server
{
    public class ServerDevice : Device
    {
        public Guid Guid { get; private set; }
        public DeviceCommunicationService CommunicationService { get; private set; }

        public ServerDevice(Guid guid, DeviceCommunicationInterface communicationInterface) : base()
        {
            Guid = guid;
            CommunicationService = new DeviceCommunicationService(this, communicationInterface);
        }
        public override string ToString()
        {
            return $"Device{Guid}";
        }
    }
}
