using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace DeviceSequenceManager
{
    internal class ConfigureSweepWindowViewModel : INotifyPropertyChanged
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

        private Device device;
        private List<CommandGroup> commandGroups;
        private List<Command> commands;
        private Command command;
        private double startValue;
        private double stopValue;
        private double increment;
        private int timePerIncrement;
        private bool isEdit;
        private SequenceOperation oldOperation;

        #region PropertyIni
        public Device Device
        {
            get { return device; }
            set { device = (Device)value; if (device != null) { CommandGroups = device.DeviceType.CommandGroups; }; NotifyPropertyChanged(); }
        }
        public List<CommandGroup> CommandGroups
        {
            get { return commandGroups; }
            set { commandGroups = value; UpdateCommands(); NotifyPropertyChanged(); }
        }
        public List<Command> Commands
        {
            get { return commands; }
            set { commands = value; NotifyPropertyChanged(); }
        }
        public Command Command
        {
            get { return command; }
            set { command = value; NotifyPropertyChanged(); }
        }
        public double StartValue
        {
            get { return startValue; }
            set { startValue = value; NotifyPropertyChanged(); }
        }
        public double StopValue
        {
            get { return stopValue; }
            set { stopValue = value; NotifyPropertyChanged(); }
        }
        public double Increment
        {
            get { return increment; }
            set { increment = value; NotifyPropertyChanged(); NotifyPropertyChanged("IncrementString"); }
        }
        public string IncrementString
        {
            get { return increment.ToString(); }
            set { Increment = Convert.ToDouble(value.ToString()); NotifyPropertyChanged(); }
        }
        public int TimePerIncrement
        {
            get { return timePerIncrement; }
            set { timePerIncrement = value; NotifyPropertyChanged(); }
        }
        public bool IsEdit
        {
            get { return isEdit; }
            set { isEdit = value; NotifyPropertyChanged(); }
        }
        public SequenceOperation OldOperation
        {
            get { return oldOperation; }
            set { oldOperation = value; NotifyPropertyChanged(); }
        }
        #endregion PropertyIni
        void UpdateCommands()
        {
            List<Command> cmds = new List<Command>();
            
            foreach(CommandGroup cmdGroup in CommandGroups)
            {
                foreach(Command cmd in cmdGroup.Commands)
                {
                    if (cmd.CommandType == CommandType.Number)
                    {
                        cmds.Add(cmd);
                    }
                }
            }
            Commands = cmds;
        }

        public MVVM.DelegateCommand AcceptCommand { get; set; }
        public MVVM.DelegateCommand CancelCommand { get; set; }
        public MVVM.DelegateCommand DeleteCommand { get; set; }

        void AcceptCommandExecute()
        {
            SequenceOperation operation = new SequenceOperation()
            {
                Device = device,
                IsSweep = true,
                Sweep = new SweepOperation()
                {
                    Device = this.Device,
                    Command = this.Command,
                    EndValue = this.StopValue,
                    StartValue = this.StartValue,
                    Increment = this.Increment,
                    TimePerIncrement = this.TimePerIncrement,
                }
            };

            if (IsEdit)
            {
                DataContainer.Sequence.Operations[DataContainer.Sequence.Operations.IndexOf(OldOperation)] = operation;
                DataContainer.Sequence.UpdateDeviceList();
                DataContainer.Sequence.UpdateIndex();
                DataContainer.AddCommandUserControlVM.IsOpen = false;
                DataContainer.MainWindowVM.UpdateSequence();
            }
            else
            {
                DataContainer.Sequence.AddOperation(operation);
                DataContainer.AddCommandUserControlVM.IsOpen = false;
                DataContainer.MainWindowVM.UpdateSequence();
            }

            IsEdit = false;
        }

        void CancelCommandExecute()
        {
            DataContainer.AddCommandUserControlVM.IsOpen = false;
            foreach (Window w in Application.Current.Windows)
            {
                if (w.GetType() == typeof(ConfigureSweepWindow))
                {
                    w.Close();
                }
            }
        }
        void DeleteCommandExecute()
        {
            DataContainer.Sequence.Operations.Remove(oldOperation);
            DataContainer.MainWindowVM.UpdateSequence();
            DataContainer.AddCommandUserControlVM.IsOpen = false;
            foreach (Window w in Application.Current.Windows)
            {
                if (w.GetType() == typeof(ConfigureSweepWindow))
                {
                    w.Close();
                }
            }
        }

        public ConfigureSweepWindowViewModel()
        {
            DataContainer.ConfigureSweepWindowVM = this;

            AcceptCommand = new MVVM.DelegateCommand(
                (o) => true,
                (o) => AcceptCommandExecute());
            DeleteCommand = new MVVM.DelegateCommand(
                (o) => true,
                (o) => DeleteCommandExecute());
            CancelCommand = new MVVM.DelegateCommand(
                (o) => true,
                (o) => CancelCommandExecute());
        }
    }
}
