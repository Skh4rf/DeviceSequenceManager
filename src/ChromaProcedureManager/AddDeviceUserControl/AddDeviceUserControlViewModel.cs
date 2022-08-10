using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Threading;

namespace DeviceSequenceManager
{
    internal class AddDeviceUserControlViewModel : UserControl, INotifyPropertyChanged
    {
        #region NotifyPropertyChangedIni
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion NotifyPropertyChangedIni

        private List<Device> devices;
        private List<DeviceType> deviceTypes;
        private string ipAddress;
        private string port;
        private string socket;
        private DeviceType deviceType;
        private bool dialogIsOpen;
        private bool isEdit;
        private int editIndex;
        private bool isChecking;
        private int checkProgress;
        private bool isCheckComplete;
        private bool isCheckFailed;

        #region PropertyIni
        public List<Device> Devices
        {
            get { return devices; }
            set { devices = value; NotifyPropertyChanged(); }
        }
        public List<DeviceType> DeviceTypes
        {
            get { return deviceTypes; }
            set { deviceTypes = value; NotifyPropertyChanged(); }
        }
        public string IPAddress
        {
            get { return ipAddress; }
            set { ipAddress = value; NotifyPropertyChanged(); }
        }
        public string Port
        {
            get { return port; }
            set { port = value; NotifyPropertyChanged(); }
        }
        public string Socket
        {
            get { return socket; }
            set { socket = value.ToString(); NotifyPropertyChanged(); }
        }
        public DeviceType DeviceType
        {
            get { return deviceType; }
            set { deviceType = value; NotifyPropertyChanged(); }
        }
        public bool DialogIsOpen
        {
            get { return dialogIsOpen; }
            set { dialogIsOpen = value; IsEdit = false; DeviceType = null; NotifyPropertyChanged(); }
        }
        public bool IsEdit
        {
            get { return isEdit; }
            set { isEdit = value; NotifyPropertyChanged(); }
        }
        public int EditIndex
        {
            get { return editIndex; }
            set { editIndex = value; NotifyPropertyChanged(); }
        }
        public bool IsChecking
        {
            get { return isChecking; }
            set { isChecking = value; NotifyPropertyChanged(); }
        }
        public int CheckProgress
        {
            get { return checkProgress; }
            set { checkProgress = value; NotifyPropertyChanged(); }
        }
        public bool IsCheckComplete
        {
            get { return isCheckComplete; }
            set { isCheckComplete = value; NotifyPropertyChanged(); }
        }
        public bool IsCheckFailed
        {
            get { return isCheckFailed; }
            set { isCheckFailed = value; NotifyPropertyChanged(); }
        }
        #endregion PropertyIni

        public MVVM.DelegateCommand CloseDialogWithSave { get; private set; }
        public MVVM.DelegateCommand CheckStatus { get; set; }

        void CloseDialogWithSaveExecute()
        {
            if (isEdit)
            {
                Device device = new Device();

                TcpAddress tcpAddress = new TcpAddress()
                {
                    Typ = Socket,
                    Port = Port,
                    IP = IPAddress
                };

                device.TcpAddress = tcpAddress;
                device.DeviceType = DataContainer.DeviceTypes.Find(x => x == DeviceType);

                List<Device> devices = new List<Device>();
                devices = DataContainer.Devices;
                Device oldDevice = devices[EditIndex];

                foreach (SequenceOperation operation in DataContainer.Sequence.Operations)
                {
                    if (operation.Device.TcpAddress.TCP.Equals(oldDevice.TcpAddress.TCP))
                    {
                        operation.Device = device;
                    }
                }
                DataContainer.Sequence.UpdateDeviceList();

                devices[EditIndex] = device;

                DataContainer.Devices = null;
                DataContainer.Devices = devices;

                DataContainer.MainWindowVM.UpdateSequence();

                IsEdit = false;
                DialogIsOpen = false;
            }
            else
            {
                Device device = new Device();
                TcpAddress tcpAddress = new TcpAddress()
                {
                    Typ = Socket,
                    Port = Port,
                    IP = IPAddress
                };

                device.TcpAddress = tcpAddress;
                device.DeviceType = DataContainer.DeviceTypes.Find(x => x == DeviceType);

                List<Device> devices = new List<Device>();
                if (DataContainer.Devices != null) { devices = DataContainer.Devices; }

                devices.Add(device);

                DataContainer.Devices = null;
                DataContainer.Devices = devices;

                DialogIsOpen = false;
            }
        }

        public void UpdateDeviceTypes()
        {
            DeviceTypes = DataContainer.DeviceTypes;
        }

        void CheckStatusExcecute()
        {
            IsCheckFailed = false;
            IsCheckComplete = false;
            IsChecking = true;
            Device test = new Device()
            {
                TcpAddress = new TcpAddress()
                {
                    IP = IPAddress,
                    Port = Port,
                    Typ = Socket
                }
            };
            if (test.ConnectionAvailable())
            {
                IsCheckComplete = true;
            }
            else
            {
                IsCheckFailed = true;
            }
            IsChecking = false;
        }

        public AddDeviceUserControlViewModel()
        {
            this.DeviceTypes = DataContainer.DeviceTypes;

            DataContainer.AddDeviceUserControlVM = this;

            CloseDialogWithSave = new MVVM.DelegateCommand(
                (o) => true,
                (o) => CloseDialogWithSaveExecute());
            CheckStatus = new MVVM.DelegateCommand(
                (o) => true,
                (o) => CheckStatusExcecute());
        }
    }
}
