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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DeviceSequenceManager
{
    /// <summary>
    /// Interaction logic for AddDeviceUserControl.xaml
    /// </summary>
    public partial class AddDeviceUserControl : UserControl
    {
        public AddDeviceUserControl()
        {
            InitializeComponent();
            ComboBoxSocket.Items.Add("SOCKET");
            ComboBoxSocket.Items.Add("INSTR");
        }

        public void ShowDeviceDialog()
        {
            AddDeviceDialog.IsOpen = true;
            TextBoxIP.Text = null;
            TextBoxPort.Text = null;
            ComboBoxDeviceType.SelectedIndex = -1;
            ComboBoxSocket.SelectedIndex = -1;
            DataContainer.AddDeviceUserControlVM.UpdateDeviceTypes();
        }

        public void UpdateDeviceType()
        {
            foreach (DeviceType devicetype in ComboBoxDeviceType.Items)
            {
                string test = DataContainer.AddDeviceUserControlVM.DeviceType.Name;
                if (devicetype.Name == DataContainer.AddDeviceUserControlVM.DeviceType.Name)
                {
                    ComboBoxDeviceType.SelectedItem = devicetype;
                }
            }
        }
    }
}
