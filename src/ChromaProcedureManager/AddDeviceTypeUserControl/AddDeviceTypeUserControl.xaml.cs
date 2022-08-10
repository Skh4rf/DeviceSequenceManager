using System;
using System.Windows.Controls;

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
