using System.Collections.Generic;

namespace DeviceSequenceManager
{
    internal static class DataContainer
    {
        private static List<DeviceType> deviceTypes = new List<DeviceType>();
        private static List<Device> devices = new List<Device>();
        private static Sequence sequence = new Sequence();

        public static AddDeviceUserControlViewModel AddDeviceUserControlVM;
        public static MainWindowViewModel MainWindowVM;
        public static AddDeviceTypeUserControlViewModel AddDeviceTypeUserControlVM;
        public static AddCommandUserControlViewModel AddCommandUserControlVM;

        public static AddCommandUserControl addCommandUserControl;

        public static List<DeviceType> DeviceTypes
        {
            get { return deviceTypes; }
            set { deviceTypes = value; }
        }

        public static List<Device> Devices
        {
            get { return devices; }
            set { devices = value; MainWindowVM.UpdateDevices(); }
        }

        public static Sequence Sequence
        {
            get { return sequence; }
            set { sequence = value; }
        }
    }
}
