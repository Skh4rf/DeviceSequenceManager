using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows;

namespace DeviceSequenceManager
{
    internal class MainWindowViewModel : INotifyPropertyChanged
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

        private List<Device> devices;
        private Sequence sequence = new Sequence();

        #region PropertyIni
        public Sequence Sequence
        {
            get { return sequence; }
            set { sequence = value; NotifyPropertyChanged(); }
        }

        public List<Device> Devices
        {
            get { return devices; }
            set { devices = value; NotifyPropertyChanged(); }
        }
        #endregion PropertyIni

        public MVVM.DelegateCommand EditDevice { get; set; }
        public MVVM.DelegateCommand CreateNewCommand { get; set; }
        public MVVM.DelegateCommand SaveCommand { get; set; }
        public MVVM.DelegateCommand ExitCommand { get; set; }
        public MVVM.DelegateCommand OpenCommand { get; set; }
        public MVVM.DelegateCommand MoveOperationUpCommand { get; set; }
        public MVVM.DelegateCommand MoveOperationDownCommand { get; set; }
        public MVVM.DelegateCommand EditOperationCommand { get; set; }
        public MVVM.DelegateCommand NewCommand { get; set; }
        public MVVM.DelegateCommand HelpCommand { get; set; }
        public MVVM.DelegateCommand ChromaDatasheetCommand { get; set; }
        public MVVM.DelegateCommand ExtendDeviceTypesCommand { get; set; }
        public MVVM.DelegateCommand ReplaceDeviceTypesCommand { get; set; }
        public MVVM.DelegateCommand ExportDeviceTypesCommand { get; set; }


        public void UpdateDevices()
        {
            Devices = null;
            Devices = DataContainer.Devices;
        }

        void EditDeviceExecute(object value)
        {
            TcpAddress tcpAddress = (TcpAddress)value;
            DataContainer.AddDeviceUserControlVM.DialogIsOpen = true;
            DataContainer.AddDeviceUserControlVM.DeviceType = DataContainer.Devices.First(x => x.TcpAddress.TCP.Equals(tcpAddress.TCP)).DeviceType;
            DataContainer.AddDeviceUserControlVM.IPAddress = tcpAddress.IP;
            DataContainer.AddDeviceUserControlVM.Port = tcpAddress.Port;
            DataContainer.AddDeviceUserControlVM.Socket = tcpAddress.Typ;
            DataContainer.AddDeviceUserControlVM.IsEdit = true;
            DataContainer.AddDeviceUserControlVM.EditIndex = DataContainer.Devices.FindIndex(x => x.TcpAddress == tcpAddress);
            MainWindow w = (MainWindow)Application.Current.MainWindow;
            w.AddDevice.UpdateDeviceType();
        }
        void CreateNewCommandExecute()
        {
            DataContainer.AddCommandUserControlVM.ShowDialogExecute();
        }
        void SaveCommandExecute()
        {
            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog
            {
                Title = "Save sequence file",
                DefaultExt = "*.seq",
                Filter = "sequence files (*.seq)|*.seq"
            };

            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DataContainerObject dataContainerObject = new DataContainerObject(DataContainer.Devices, DataContainer.Sequence);

                string str = JsonSerializer.Serialize(dataContainerObject, new JsonSerializerOptions { WriteIndented = true });

                File.WriteAllText(saveFileDialog.FileName, str);
            }
        }
        void OpenCommandExecute()
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog
            {
                Title = "Open sequence file",
                Filter = "sequence files (*.seq)|*.seq"
            };

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string str = File.ReadAllText(openFileDialog.FileName);
                DataContainerObject dataContainerObject = JsonSerializer.Deserialize<DataContainerObject>(str);
                DataContainer.Devices = dataContainerObject.Devices;
                DataContainer.Sequence = dataContainerObject.Sequence;
                UpdateSequence();
                DataContainer.Sequence.UpdateDeviceList();
            }
        }
        void MoveOperationUpCommandExcecute(object value)
        {
            int i = DataContainer.Sequence.Operations.IndexOf((SequenceOperation)value);
            if (i < 1) { return; }
            DataContainer.Sequence.Operations.RemoveAt(i);
            DataContainer.Sequence.Operations.Insert(i - 1, (SequenceOperation)value);
            UpdateSequence();
        }
        void MoveOperationDownCommandExecute(object value)
        {
            int i = DataContainer.Sequence.Operations.IndexOf((SequenceOperation)value);
            if (i - 1 == DataContainer.Sequence.Operations.Count) { return; }
            DataContainer.Sequence.Operations.RemoveAt(i);
            DataContainer.Sequence.Operations.Insert(i + 1, (SequenceOperation)value);
            UpdateSequence();
        }

        void EditOperationCommandExecute(object value)
        {
            DataContainer.addCommandUserControl.OpenAsEdit((SequenceOperation)value);
        }
        void NewCommandExecute()
        {
            DataContainer.Devices.Clear();
            DataContainer.Sequence.Operations.Clear();
            UpdateSequence();
            DataContainer.Sequence.UpdateDeviceList();
            UpdateDevices();
        }
        void HelpCommandExecute()
        {
            try
            {
                System.Diagnostics.Process.Start(@".\DeviceSequenceManagerManual.pdf");
            }
            catch (Exception)
            {
                System.Diagnostics.Process.Start("https://github.com/Skh4rf/DeviceSequenceManager/blob/main/doc/DeviceSequenceManagerManual.pdf");
            }

        }
        void ChromaDatasheetCommandExecute()
        {
            try
            {
                System.Diagnostics.Process.Start(@".\EN_61611_61612_UserManual_202101.pdf");
            }
            catch (Exception)
            {
                System.Diagnostics.Process.Start("https://github.com/Skh4rf/DeviceSequenceManager/blob/main/src/ChromaProcedureManager/bin/Debug/EN_61611_61612_UserManual_202101.pdf");
            }

        }
        void ExtendDeviceTypesCommandExecute()
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog
            {
                Title = "Extend device types",
                Filter = "device-types file (*.json)|*.json"
            };

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string str = File.ReadAllText(openFileDialog.FileName);
                DataContainerDeviceTypesObject dataContainerDeviceTypesObject = JsonSerializer.Deserialize<DataContainerDeviceTypesObject>(str);
                
                List<DeviceType> deviceTypes = new List<DeviceType>();
                deviceTypes = dataContainerDeviceTypesObject.DeviceTypes;

                foreach (DeviceType d in deviceTypes)
                {
                    if (!DataContainer.DeviceTypes.Any(x => x.Name.Equals(d.Name)))
                    {
                        DataContainer.DeviceTypes.Add(d);
                    }
                }
                DataContainer.AddDeviceUserControlVM.DeviceTypes = DataContainer.DeviceTypes;

                JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };
                DataContainerDeviceTypesObject dataContainerDeviceTypesObject2 = new DataContainerDeviceTypesObject(DataContainer.DeviceTypes);
                string cmdstr = JsonSerializer.Serialize(dataContainerDeviceTypesObject, options);
                File.WriteAllText(@".\devicetypes.json", cmdstr);
            }
        }
        void ReplaceDeviceTypesCommandExecute()
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog()
            {
                Title = "Replace device types",
                Filter = "device-types file (*.json)|*.json"
            };

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string str = File.ReadAllText(openFileDialog.FileName);
                DataContainerDeviceTypesObject dataContainerDeviceTypesObject = JsonSerializer.Deserialize<DataContainerDeviceTypesObject>(str);

                DataContainer.DeviceTypes = dataContainerDeviceTypesObject.DeviceTypes;
                DataContainer.AddDeviceUserControlVM.DeviceTypes = dataContainerDeviceTypesObject.DeviceTypes;

                JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };
                string str2 = JsonSerializer.Serialize(dataContainerDeviceTypesObject, options);
                File.WriteAllText(@".\devicetypes.json", str2);
            }
        }
        void ExportDeviceTypesCommandExecute()
        {
            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog()
            {
                Title = "Export device types",
                DefaultExt = "*.json",
                Filter = "device-types file (*.json)|*.json"
            };

            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                File.Copy(@".\devicetypes.json", saveFileDialog.FileName);
                File.Open(saveFileDialog.FileName, FileMode.Open);
            }
        }
        public void UpdateSequence()
        {
            DataContainer.Sequence.UpdateIndex();
            this.Sequence = null;
            this.Sequence = DataContainer.Sequence;
        }


        public MainWindowViewModel()
        {
            Devices = DataContainer.Devices;
            DataContainer.MainWindowVM = this;

            EditDevice = new MVVM.DelegateCommand(
                (o) => true,
                EditDeviceExecute);
            CreateNewCommand = new MVVM.DelegateCommand(
                (o) => true,
                (o) => CreateNewCommandExecute());
            SaveCommand = new MVVM.DelegateCommand(
                (o) => true,
                (o) => SaveCommandExecute());
            OpenCommand = new MVVM.DelegateCommand(
                (o) => true,
                (o) => OpenCommandExecute());
            MoveOperationUpCommand = new MVVM.DelegateCommand(
                (o) => true,
                MoveOperationUpCommandExcecute);
            MoveOperationDownCommand = new MVVM.DelegateCommand(
                (o) => true,
                MoveOperationDownCommandExecute);
            EditOperationCommand = new MVVM.DelegateCommand(
                (o) => true,
                EditOperationCommandExecute);
            NewCommand = new MVVM.DelegateCommand(
                (o) => true,
                (o) => NewCommandExecute());
            HelpCommand = new MVVM.DelegateCommand(
                (o) => true,
                (o) => HelpCommandExecute());
            ChromaDatasheetCommand = new MVVM.DelegateCommand(
                (o) => true,
                (o) => ChromaDatasheetCommandExecute());
            ExtendDeviceTypesCommand = new MVVM.DelegateCommand(
                (o) => true,
                (o) => ExtendDeviceTypesCommandExecute());
            ReplaceDeviceTypesCommand = new MVVM.DelegateCommand(
                (o) => true,
                (o) => ReplaceDeviceTypesCommandExecute());
            ExportDeviceTypesCommand = new MVVM.DelegateCommand(
                (o) => true,
                (o) => ExportDeviceTypesCommandExecute());
        }
    }
}
