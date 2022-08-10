using System.Collections.Generic;

namespace DeviceSequenceManager
{
    internal class DataContainerObject
    {
        private List<Device> devices = new List<Device>();
        private Sequence sequence = new Sequence();

        public List<Device> Devices
        {
            get { return devices; }
            set { devices = value; }
        }

        public Sequence Sequence
        {
            get { return sequence; }
            set { sequence = value; }
        }

        public DataContainerObject()
        {

        }

        public DataContainerObject(List<Device> devices, Sequence sequence)
        {
            Devices = devices;
            Sequence = sequence;
        }
    }
}
