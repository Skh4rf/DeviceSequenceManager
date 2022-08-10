using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
