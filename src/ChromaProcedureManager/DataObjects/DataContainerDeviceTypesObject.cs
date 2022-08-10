using System.Collections.Generic;

namespace DeviceSequenceManager
{
    internal class DataContainerDeviceTypesObject
    {
        public List<DeviceType> DeviceTypes { get; set; }

        public DataContainerDeviceTypesObject(List<DeviceType> deviceTypes)
        {
            DeviceTypes = deviceTypes;
        }
    }
}
