using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DeviceSequenceManager
{
    /// <summary>
    /// Interaction logic for ConfigureSweepWindow.xaml
    /// </summary>
    public partial class ConfigureSweepWindow : Window
    {
        internal ConfigureSweepWindow(Device device)
        {
            InitializeComponent();
            DataContainer.ConfigureSweepWindowVM.Device = device;
        }

        internal ConfigureSweepWindow(SequenceOperation operation)
        {
            InitializeComponent();
            DataContainer.ConfigureSweepWindowVM.Device = operation.Device;
            DataContainer.ConfigureSweepWindowVM.StopValue = operation.Sweep.EndValue;
            DataContainer.ConfigureSweepWindowVM.StartValue = operation.Sweep.StartValue;
            DataContainer.ConfigureSweepWindowVM.Increment = operation.Sweep.Increment;
            DataContainer.ConfigureSweepWindowVM.TimePerIncrement = operation.Sweep.TimePerIncrement;
            DataContainer.ConfigureSweepWindowVM.Command = operation.Sweep.Command;
        }

        private void ButtonAccept_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
