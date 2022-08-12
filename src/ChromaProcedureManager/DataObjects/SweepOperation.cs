using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DeviceSequenceManager
{
    internal class SweepOperation
    {
        private Device device;
        private Command command;
        private double startValue;
        private double endValue;
        private double increment;
        private int timePerIncrement;

        public Device Device
        {
            get { return device; }
            set { device = value; }
        }
        public Command Command
        {
            get { return command; }
            set { command = value; }
        }
        public double StartValue
        {
            get { return startValue; }
            set { startValue = value; }
        }
        public double EndValue
        {
            get { return endValue; }
            set { endValue = value; }
        }
        public double Increment
        {
            get { return increment; }
            set { increment = value; }
        }
        public int TimePerIncrement
        {
            get { return timePerIncrement; }
            set { timePerIncrement = value; }
        }

        public SweepOperation()
        {

        }

        public async Task ExecuteSweep()
        {
            double localValue = startValue;
            MainWindow w = (MainWindow)Application.Current.MainWindow;

            Device.OpenSession();
            for (double i = localValue; i <= EndValue; i += increment)
            {
                command.NumberCommandValue = i;
                string cmdString = command.CastCommandString;

                Device.WriteCommand(cmdString + "\n");
                w.TextBoxConsole.Text += $">> {Device.TcpAddress.TCP}\n{cmdString}\n----------------\n\n";
                await Task.Delay(timePerIncrement);
                
            }
            Device.CloseSession();
        }
    }
}
