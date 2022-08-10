using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
