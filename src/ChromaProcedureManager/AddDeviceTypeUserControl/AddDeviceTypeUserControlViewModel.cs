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
using System.Text.Json;
using System.IO;

namespace DeviceSequenceManager
{
    internal class AddDeviceTypeUserControlViewModel : UserControl, INotifyPropertyChanged
    {
        public AddDeviceTypeUserControlViewModel()
        {
            IsNumberEnabled = true;
            SaveCommand = new MVVM.DelegateCommand(
                (o) => true,
                (o) => SaveCommandExecute());
            SaveCustomParameter = new MVVM.DelegateCommand(
                (o) => true,
                (o) => SaveCustomParameterExecute());
            CloseDialogWithSave = new MVVM.DelegateCommand(
                (o) => true,
                (o) => CloseDialogWithSaveExecute());
            DataContainer.AddDeviceTypeUserControlVM = this;
            ParameterTypeNone = true;
        }

        #region NotifyPropertyChangedIni
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion NotifyPropertyChangedIni

        private List<CommandGroup> commandGroups = new List<CommandGroup>();
        private List<Command> commands = new List<Command>();
        private string deviceName;
        private string commandName;
        private string commandBase;
        private bool parameterTypeNumber;
        private bool parameterTypeCustom;
        public bool parameterTypeNone;
        private double commandNumberRangeMinimum;
        private double commandNumberRangeMaximum;
        private int commandNumberDecimalPlaces;
        private int commandNumberPriority;
        private string commandCustomName;
        private string commandCustomCommandParameter;
        private int commandCustomPriority;
        private bool isNumberEnabled;
        private bool buttonAcceptIsEnabled;
        private bool isOpen;

        #region PropertyIni
        public string DeviceName
        {
            get { return deviceName; }
            set { deviceName = value; NotifyPropertyChanged(); }
        }
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; NotifyPropertyChanged(); }
        }
        public string CommandBase
        {
            get { return commandBase; }
            set { commandBase = value; NotifyPropertyChanged(); }
        }
        public bool ParameterTypeNumber
        {
            get { return parameterTypeNumber; }
            set { parameterTypeNumber = value; NotifyPropertyChanged(); }
        }
        public bool ParameterTypeCustom
        {
            get { return parameterTypeCustom; }
            set { parameterTypeCustom = value; NotifyPropertyChanged(); }
        }
        public bool ParameterTypeNone
        {
            get { return parameterTypeNone; }
            set { parameterTypeNone = value; NotifyPropertyChanged(); }
        }
        public double CommandNumberRangeMinimum
        {
            get { return commandNumberRangeMinimum; }
            set { commandNumberRangeMinimum = value; NotifyPropertyChanged(); }
        }
        public double CommandNumberRangeMaximum
        {
            get { return commandNumberRangeMaximum; }
            set { commandNumberRangeMaximum = value; NotifyPropertyChanged(); }
        }
        public int CommandNumberDecimalPlaces
        {
            get { return commandNumberDecimalPlaces; }
            set { commandNumberDecimalPlaces = value; NotifyPropertyChanged(); }
        }
        public int CommandNumberPriority
        {
            get { return commandNumberPriority; }
            set { commandNumberPriority = value; NotifyPropertyChanged(); }
        }
        public string CommandCustomName
        {
            get { return commandCustomName; }
            set { commandCustomName = value; NotifyPropertyChanged(); }
        }
        public string CommandCustomCommandParameter
        {
            get { return commandCustomCommandParameter; }
            set { commandCustomCommandParameter = value; NotifyPropertyChanged(); }
        }
        public int CommandCustomPriority
        {
            get { return commandCustomPriority; }
            set { commandCustomPriority = value; NotifyPropertyChanged(); }
        }
        public bool IsNumberEnabled
        {
            get { return isNumberEnabled; }
            set { isNumberEnabled = value; NotifyPropertyChanged(); }
        }
        public List<CommandGroup> CommandGroups
        {
            get { return commandGroups; }
            set { commandGroups = value; NotifyPropertyChanged(); NotifyPropertyChanged("FullCommandList"); }
        }

        public List<Command> FullCommandList
        {
            get
            {
                if (CommandGroups != null)
                {
                    List<Command> list = new List<Command>();
                    foreach (CommandGroup cg in commandGroups)
                    {
                        foreach (Command c in cg.Commands)
                        {
                            list.Add(c);
                        }
                    }
                    return list;
                }
                return null;
            }
        }
        public bool ButtonAcceptIsEnabled
        {
            get { return buttonAcceptIsEnabled; }
            set { buttonAcceptIsEnabled = value; NotifyPropertyChanged(); }
        }
        public bool IsOpen
        {
            get { return isOpen; }
            set { isOpen = value; NotifyPropertyChanged(); }
        }
        #endregion PropertyIni

        public MVVM.DelegateCommand SaveCommand { get; private set; }
        public MVVM.DelegateCommand SaveCustomParameter { get; private set; }
        public MVVM.DelegateCommand CloseDialogWithSave { get; private set; }

        void SaveCommandExecute()
        {
            if (parameterTypeNumber && !parameterTypeCustom && !parameterTypeNone)
            {
                List<Command> commandList = new List<Command>();
                commandList.Add(new Command
                {
                    Name = commandName,
                    CommandString = commandBase,
                    CommandType = CommandType.Number,
                    RangeMinimum = commandNumberRangeMinimum,
                    RangeMaximum = commandNumberRangeMaximum,
                    DecimalPlaces = commandNumberDecimalPlaces,
                    Priority = commandNumberPriority
                });

                CommandGroups.Add(new CommandGroup() { Commands = commandList });
                NotifyPropertyChanged("FullCommandList");
                ButtonAcceptIsEnabled = true;
            }
            else if (parameterTypeCustom && !parameterTypeNumber && !parameterTypeNone)
            {
                List<Command> cmds = new List<Command>();
                foreach (Command c in commands) { cmds.Add(c); }
                CommandGroups.Add(new CommandGroup()
                {
                    Commands = cmds
                });
                NotifyPropertyChanged("FullCommandList");
                ButtonAcceptIsEnabled = true;
            }
            else if (!parameterTypeCustom && !parameterTypeNumber && parameterTypeNone)
            {
                List<Command> commandList = new List<Command>();
                commandList.Add(new Command
                {
                    Name = commandName,
                    CommandString = commandBase,
                    CommandType = CommandType.None
                });
                CommandGroups.Add(new CommandGroup() { Commands = commandList });
                NotifyPropertyChanged("FullCommandList");
                ButtonAcceptIsEnabled = true;
            }
            commands.Clear();
            CommandName = null;
            CommandBase = null;
            CommandNumberRangeMinimum = 0;
            CommandNumberRangeMaximum = 0;
            IsNumberEnabled = true;
        }
        
        void SaveCustomParameterExecute()
        {
            IsNumberEnabled = false;
            commands.Add(new Command()
            {
                Name = commandName,
                CommandString = commandBase,
                CommandType= CommandType.Custom,
                CustomParameterName = commandCustomName,
                CustomParameterCommand = commandCustomCommandParameter,
                Priority = commandCustomPriority
            });

            CommandCustomName = null;
            CommandCustomCommandParameter = null;
        }

        void CloseDialogWithSaveExecute()
        {
            DeviceType deviceType = new DeviceType();
            deviceType.Name = DeviceName;
            deviceType.CommandGroups = CommandGroups;
            DataContainer.DeviceTypes.Add(deviceType);
            DataContainer.AddDeviceUserControlVM.DeviceTypes = DataContainer.DeviceTypes;

            JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented=true};
            DataContainerDeviceTypesObject dataContainerDeviceTypesObject = new DataContainerDeviceTypesObject(DataContainer.DeviceTypes);
            string str = JsonSerializer.Serialize(dataContainerDeviceTypesObject, options);
            File.WriteAllText(@".\devicetypes.json", str);

            IsOpen = false;
        }
    }
}
