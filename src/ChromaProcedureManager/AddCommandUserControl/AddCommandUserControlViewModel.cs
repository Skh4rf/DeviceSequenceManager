using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DeviceSequenceManager
{
    internal class AddCommandUserControlViewModel : INotifyPropertyChanged
    {
        #region PropertyChangedIni
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion PropertyChangedIni

        private bool isOpen;
        private bool isEdit;
        private List<Device> devices;
        private Device device;
        private DeviceType deviceType;
        private Device selectedDevice;
        private int duration;
        private string durationUnitString;
        private string bufferText;

        #region PropertyIni
        public bool IsOpen
        {
            get { return isOpen; }
            set { isOpen = value; NotifyPropertyChanged(); }
        }
        public bool IsEdit
        {
            get { return isEdit; }
            set { isEdit = value; NotifyPropertyChanged(); }
        }
        public List<Device> Devices
        {
            get { return devices; }
            set { devices = value; NotifyPropertyChanged(); NotifyPropertyChanged("DeviceTypes"); }
        }
        public List<DeviceType> DeviceTypes
        {
            get
            {
                List<DeviceType> list = new List<DeviceType>();
                if (Devices == null) { return null; }
                foreach (Device d in Devices)
                {
                    if (!list.Contains(d.DeviceType)) { list.Add(d.DeviceType); }
                }
                return list;
            }
        }
        public string DurationUnitString
        {
            get { return durationUnitString; }
            set { durationUnitString = value; NotifyPropertyChanged(); }
        }
        public List<Device> MatchingDevices
        {
            get
            {
                List<Device> list = new List<Device>();
                if (DeviceType == null) { return null; }
                foreach (Device d in Devices)
                {
                    if (d.DeviceType == DeviceType) { list.Add(d); }
                }
                return list;
            }
        }
        public Device Device
        {
            get { return device; }
            set { device = value; NotifyPropertyChanged(); }
        }
        public DeviceType DeviceType
        {
            get { return deviceType; }
            set { deviceType = value; NotifyPropertyChanged(); NotifyPropertyChanged("MatchingDevices"); }
        }
        public Device SelectedDevice
        {
            get { return selectedDevice; }
            set { selectedDevice = value; NotifyPropertyChanged(); }
        }
        public int Duration
        {
            get { return duration; }
            set { duration = Convert.ToInt32(value); NotifyPropertyChanged(); }
        }
        public string BufferText
        {
            get { return bufferText; }
            set { NotifyPropertyChanged(); }
        }
        #endregion PropertyIni

        public void ShowDialogExecute()
        {
            IsOpen = true;
            Duration = 1;
            Devices = null;
            DeviceType = null;
            Devices = DataContainer.Devices;
            IsEdit = false;
        }

        public MVVM.DelegateCommand ShowSweepConfigurationCommand { get; set; }

        void ShowSweepConfigurationCommandExecute()
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.MainWindow;
            w.IsEnabled = false;
            ConfigureSweepWindow sweepWindow = new ConfigureSweepWindow(SelectedDevice);
            sweepWindow.Closed += SweepWindow_Closed;
            sweepWindow.Show();
        }

        public void ShowSweepConfiguration(SequenceOperation operation)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.MainWindow;
            w.IsEnabled = false;
            ConfigureSweepWindow sweepWindow = new ConfigureSweepWindow(operation);
            sweepWindow.Closed += SweepWindow_Closed;
            sweepWindow.Show();
        }

        private void SweepWindow_Closed(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.MainWindow;
            w.IsEnabled = true;
        }

        public AddCommandUserControlViewModel()
        {
            DataContainer.AddCommandUserControlVM = this;

            ShowSweepConfigurationCommand = new MVVM.DelegateCommand(
                (o) => true,
                (o) => ShowSweepConfigurationCommandExecute());
        }
    }
}
