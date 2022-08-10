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
    /// Interaction logic for AddDeviceTypeUserControl.xaml
    /// </summary>
    public partial class AddDeviceTypeUserControl : UserControl
    {
        public AddDeviceTypeUserControl()
        {
            InitializeComponent();
            ComboBoxNumberDecimalPlaces.Items.Add(0);
            for (int i = 1; i < 11; i++)
            {
                if (i < 5) { ComboBoxNumberDecimalPlaces.Items.Add(i); }
                ComboBoxNumberPriority.Items.Add(i);
                ComboBoxCustomPriority.Items.Add(i);
            }
        }

        public void ShowDeviceDialog()
        {
            AddDeviceTypeDialog.IsOpen = true;
        }

        public void CloseDeviceDialog()
        {
            TextBoxBaseCommand.Text = TextBoxCommandName.Text = TextBoxCommandRangeMaximum.Text = TextBoxCommandRangeMinimum.Text =
                TextBoxName.Text = String.Empty;
            AddDeviceTypeDialog.IsOpen = false;
        }
    }
}
