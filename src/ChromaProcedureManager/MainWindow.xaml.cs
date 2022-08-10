using System;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace DeviceSequenceManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            LoadDeviceTypes();
            InitializeComponent();
        }

        private void ButtonAddDevice_Click(object sender, RoutedEventArgs e)
        {
            AddDevice.ShowDeviceDialog();
        }

        private void MenuItemCreateNewDeviceType_Click(object sender, RoutedEventArgs e)
        {
            AddDeviceType.ShowDeviceDialog();
        }
        private void LoadDeviceTypes()
        {
            try
            {
                string jsonstr = File.ReadAllText(@".\devicetypes.json");
                DataContainerDeviceTypesObject dataContainerDeviceTypesObject = JsonSerializer.Deserialize<DataContainerDeviceTypesObject>(jsonstr);
                DataContainer.DeviceTypes = dataContainerDeviceTypesObject.DeviceTypes;
            }
            catch (Exception) { }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataContainer.Sequence.ExecuteSequence();
        }
    }
}
