using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DeviceSequenceManager
{
    internal class Sequence
    {
        private List<SequenceOperation> operations;
        private List<Device> devices;

        public Sequence()
        {
            operations = new List<SequenceOperation>();
            devices = new List<Device>();
        }

        public List<SequenceOperation> Operations
        {
            get { return operations; }
            set { operations = value; }
        }

        public void AddOperation(SequenceOperation operation)
        {
            operations.Add(operation);
            if (!devices.Contains(operation.Device))
            {
                devices.Add(operation.Device);
            }
        }

        public void UpdateDeviceList()
        {
            devices.Clear();
            foreach (SequenceOperation operation in operations)
            {
                if (!devices.Any(x => x.TcpAddress.TCP.Equals(operation.Device.TcpAddress.TCP)))
                {
                    devices.Add(operation.Device);
                }
            }
        }

        public void UpdateIndex()
        {
            for (int i = 0; i < operations.Count; i++)
            {
                operations[i].Index = i + 1;
            }
        }

        public async void ExecuteSequence()
        {
            MainWindow w = (MainWindow)Application.Current.MainWindow;
            w.DisableButtons();
            w.ProgressBarSequence.Value = 0;
            w.ProgressBarSequence.Visibility = Visibility.Visible;
            w.TextBoxConsole.Text = String.Empty;

            if (devices.Any(x => x.ConnectionAvailable() == false))
            {
                MessageBox.Show("Could not connect to every device. Please check device status!");
                w.ProgressBarSequence.Visibility = Visibility.Collapsed;
                w.EnableButtons();
                return;
            }

            double duration = operations.Sum(x => x.Duration * (int)x.TimeUnit);
            w.ProgressBarSequence.Maximum = duration;

            foreach (SequenceOperation operation in operations)
            {
                if (operation.IsSweep)
                {
                    operation.Sweep.ExecuteSweep();
                }
                else
                {
                    List<Command> commands = operation.Commands;
                    commands.Sort((x, y) => x.Priority.CompareTo(y.Priority));

                    string cmdString = "\n";

                    foreach (Command c in commands)
                    {
                        cmdString += c.CastCommandString + "\n";
                    }

                    operation.Device.OpenSession();
                    operation.Device.WriteCommand(cmdString);
                    w.TextBoxConsole.Text += $">> {operation.Device.TcpAddress.TCP}\n{cmdString}\n----------------\n\n";
                    await Task.Delay(operation.Duration * (int)operation.TimeUnit * 1000);
                    operation.Device.CloseSession();
                    w.ProgressBarSequence.Value += operation.Duration * (int)operation.TimeUnit;
                }
            }

            w.ProgressBarSequence.Visibility = Visibility.Collapsed;
            w.EnableButtons();
        }
    }
}
