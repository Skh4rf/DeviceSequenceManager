using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace DeviceSequenceManager
{
    /// <summary>
    /// Interaction logic for AddCommandUserControl.xaml
    /// </summary>
    public partial class AddCommandUserControl : UserControl
    {
        SequenceOperation SequenceOperation = new SequenceOperation();
        SequenceOperation editOperation;

        public AddCommandUserControl()
        {
            InitializeComponent();
            ComboBoxTimeUnit.Items.Add("s");
            ComboBoxTimeUnit.Items.Add("min");
            ComboBoxTimeUnit.SelectedIndex = 0;
            DataContainer.addCommandUserControl = this;
        }

        internal void LoadCommandSection(DeviceType deviceType)
        {
            StackPanel stackPanel = new StackPanel();
            int i = 0;

            foreach (CommandGroup commandGroup in deviceType.CommandGroups)
            {
                i++;
                StackPanel stackPanelHorizontal = new StackPanel()
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(0, 0, 0, 20),
                    Tag = commandGroup
                };

                List<Command> comboBoxCommands = new List<Command>();
                comboBoxCommands.Add(new Command() { CustomParameterName = "*Unselected*" });
                ComboBox comboBox = new ComboBox()
                {
                    ItemsSource = comboBoxCommands,
                    DisplayMemberPath = "CustomParameterName",
                    FontSize = 16,
                    VerticalAlignment = VerticalAlignment.Center,
                    SelectedIndex = 0,
                    Tag = stackPanelHorizontal
                };
                SequenceOperation.Device = DataContainer.AddCommandUserControlVM.SelectedDevice;

                foreach (Command command in commandGroup.Commands)
                {
                    switch (command.CommandType)
                    {
                        case CommandType.Number:
                            TextBlock textBlockNumber = new TextBlock()
                            {
                                Text = command.Name + ":",
                                FontSize = 18,
                                Foreground = new SolidColorBrush(Colors.Gray),
                                VerticalAlignment = VerticalAlignment.Center,
                                Tag = command
                            };
                            TextBox textBoxNumber = new TextBox
                            {
                                Width = 200,
                                VerticalAlignment = VerticalAlignment.Center,
                                Margin = new Thickness(20, 0, 0, 0),
                                FontSize = 16,
                                Tag = stackPanelHorizontal
                            };
                            Binding b = new Binding()
                            {
                                Source = DataContainer.AddCommandUserControlVM,
                                Path = new PropertyPath("BufferText"),
                                Mode = BindingMode.OneWayToSource,
                                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                            };
                            b.ValidationRules.Add(new MVVM.CommandConfigurationValidationRule()
                            {
                                DecimalPlaces = command.DecimalPlaces,
                                RangeMaximum = command.RangeMaximum,
                                RangeMinimum = command.RangeMinimum
                            });

                            textBoxNumber.SetBinding(TextBox.TextProperty, b);

                            textBoxNumber.TextChanged += NumberCommandActivated;
                            stackPanelHorizontal.Children.Add(textBlockNumber);
                            stackPanelHorizontal.Children.Add(textBoxNumber);

                            break;
                        case CommandType.Custom:
                            if (stackPanelHorizontal.Children.Count < 1)
                            {
                                TextBlock textBlockCustom = new TextBlock()
                                {
                                    Text = command.Name + ":",
                                    VerticalAlignment = VerticalAlignment.Center,
                                    Margin = new Thickness(0, 0, 10, 0),
                                    FontSize = 18,
                                    Foreground = new SolidColorBrush(Colors.Gray)
                                };
                                stackPanelHorizontal.Children.Add(textBlockCustom);
                            }
                            comboBoxCommands.Add(command);

                            break;
                        case CommandType.None:
                            break;
                    }
                }
                if (comboBox.Items.Count > 1) { stackPanelHorizontal.Children.Add(comboBox); }
                comboBox.SelectionChanged += CustomCommandActivated;
                stackPanel.Children.Add(stackPanelHorizontal);

                if (i == 10 || i == 20)
                {
                    StackPanelCommandConfiguration.Children.Add(stackPanel);
                    i = 0;
                    stackPanel = new StackPanel() { Margin = new Thickness(30, 0, 0, 0) };
                }
            }
            StackPanelCommandConfiguration.Children.Add(stackPanel);
        }

        private void UpdateButtonAcceptStatus()
        {
            bool ValidationHasError = false;
            foreach (StackPanel sP in StackPanelCommandConfiguration.Children)
            {
                foreach (StackPanel sp2 in sP.Children)
                {
                    foreach (UIElement element in sp2.Children)
                    {
                        if ((bool)element.GetValue(Validation.HasErrorProperty) == true)
                        {
                            ValidationHasError = true;
                        }
                    }
                }
            }
            if (ComboBoxTimeUnit.SelectedValue == null)
            {
                ValidationHasError = true;
            }
            if (SequenceOperation.Commands.Count > 0 && ValidationHasError == false)
            {
                ButtonAccept.IsEnabled = true;
            }
            else { ButtonAccept.IsEnabled = false; }
        }

        private void NumberCommandActivated(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            StackPanel stackPanel = (StackPanel)textBox.Tag;

            foreach (UIElement element in stackPanel.Children)
            {
                if (element.GetType() == typeof(TextBlock))
                {
                    TextBlock tB = (TextBlock)element;
                    tB.Foreground = new SolidColorBrush(Colors.White);
                    Command cmd = (Command)tB.Tag;
                    Command command = new Command()
                    {
                        Name = cmd.Name,
                        CommandString = cmd.CommandString,
                        CommandType = cmd.CommandType,
                        RangeMaximum = cmd.RangeMaximum,
                        RangeMinimum = cmd.RangeMinimum,
                        DecimalPlaces = cmd.DecimalPlaces,
                        NumberCommandValue = cmd.NumberCommandValue,
                        Priority = cmd.Priority,
                        CustomParameterName = cmd.CustomParameterName,
                        CustomParameterCommand = cmd.CustomParameterCommand
                    };

                    if (SequenceOperation.Commands.Any(x => x.Name == command.Name))
                    {
                        SequenceOperation.Commands.Remove(SequenceOperation.Commands.Where(x => x.Name == command.Name).FirstOrDefault());
                    }

                    if (!textBox.Text.Equals(String.Empty))
                    {
                        if ((bool)textBox.GetValue(Validation.HasErrorProperty) == false)
                        {
                            command.NumberCommandValue = Convert.ToDouble(textBox.Text);
                            SequenceOperation.Commands.Add(command);
                        }
                    }
                    else
                    {
                        tB.Foreground = new SolidColorBrush(Colors.Gray);
                    }
                }
            }
            UpdateButtonAcceptStatus();
        }

        private void CustomCommandActivated(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cB = (ComboBox)sender;
            StackPanel sP = (StackPanel)cB.Tag;

            if (cB.SelectedValue == null) { return; }

            foreach (UIElement element in sP.Children)
            {
                if (element.GetType() == typeof(TextBlock))
                {
                    TextBlock tB = (TextBlock)element;
                    tB.Foreground = new SolidColorBrush(Colors.White);
                    Command command = (Command)cB.SelectedValue;
                    Command deleteCommand = (Command)cB.Items[1];
                    SequenceOperation.Commands.Remove(SequenceOperation.Commands.Where(x => x.Name.Equals(deleteCommand.Name)).FirstOrDefault());

                    if (!command.CustomParameterName.Equals("*Unselected*"))
                    {
                        SequenceOperation.Commands.Add(command);
                    }
                    else { tB.Foreground = new SolidColorBrush(Colors.Gray); }
                }
            }
            UpdateButtonAcceptStatus();
        }

        private void ComboBoxIpAddress_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxIpAddress.SelectedValue != null)
            {
                LoadCommandSection(DataContainer.AddCommandUserControlVM.SelectedDevice.DeviceType);
                SequenceOperation = new SequenceOperation();
            }
            UpdateButtonAcceptStatus();
        }

        private void AddCommandDialog_DialogOpened(object sender, MaterialDesignThemes.Wpf.DialogOpenedEventArgs eventArgs)
        {
            StackPanelCommandConfiguration.Children.Clear();
            SequenceOperation = new SequenceOperation();
            ComboBoxDeviceType.IsEnabled = true;
            UpdateButtonAcceptStatus();
        }

        private void ComboBoxDeviceType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StackPanelCommandConfiguration.Children.Clear();
            SequenceOperation = new SequenceOperation();
            UpdateButtonAcceptStatus();
        }

        private void ButtonAccept_Click(object sender, RoutedEventArgs e)
        {
            SequenceOperation.Device = DataContainer.AddCommandUserControlVM.SelectedDevice;
            SequenceOperation.Duration = DataContainer.AddCommandUserControlVM.Duration;
            if (DataContainer.AddCommandUserControlVM.DurationUnitString.Equals("min"))
            {
                SequenceOperation.TimeUnit = TimeUnit.Minutes;
            }
            else { SequenceOperation.TimeUnit = TimeUnit.Seconds; }
            if (DataContainer.AddCommandUserControlVM.IsEdit == false)
            {
                DataContainer.Sequence.AddOperation(SequenceOperation);
                DataContainer.AddCommandUserControlVM.IsOpen = false;
                DataContainer.MainWindowVM.UpdateSequence();
            }
            else
            {
                DataContainer.Sequence.Operations[DataContainer.Sequence.Operations.IndexOf(editOperation)] = SequenceOperation;
                DataContainer.AddCommandUserControlVM.IsOpen = false;
                DataContainer.MainWindowVM.UpdateSequence();
            }
        }

        internal void OpenAsEdit(SequenceOperation operation)
        {
            editOperation = operation;
            DataContainer.AddCommandUserControlVM.ShowDialogExecute();
            DataContainer.AddCommandUserControlVM.Duration = operation.Duration;
            DataContainer.AddCommandUserControlVM.IsEdit = true;

            ComboBoxTimeUnit.SelectedItem = "s";
            if (operation.TimeUnit == TimeUnit.Minutes) { ComboBoxTimeUnit.SelectedItem = "min"; }
            ComboBoxDeviceType.SelectedItem = operation.Device.DeviceType;
            ComboBoxDeviceType.SelectedValue = operation.Device.DeviceType.Name;
            ComboBoxDeviceType.IsEnabled = false;
            ComboBoxIpAddress.SelectedItem = operation.Device;
            ComboBoxIpAddress.SelectedValue = operation.Device.TcpAddress.TCP;

            foreach (StackPanel stackPanel in StackPanelCommandConfiguration.Children)
            {
                foreach (StackPanel p in stackPanel.Children)
                {
                    TextBlock textBlock = null;
                    foreach (UIElement element in p.Children)
                    {
                        if (element.GetType() == typeof(TextBlock))
                        {
                            TextBlock tB = (TextBlock)element;
                            if (operation.Commands.Any(x => x.Name.Equals(tB.Text.Remove(tB.Text.Length - 1))))
                            {
                                textBlock = tB;
                            }
                        }
                        else if (textBlock != null)
                        {
                            if (element.GetType() == typeof(TextBox))
                            {
                                TextBox textBox = (TextBox)element;
                                textBox.Text = operation.Commands.First(x => x.Name.Equals(textBlock.Text.Remove(textBlock.Text.Length - 1))).NumberCommandValue.ToString();
                            }
                            if (element.GetType() == typeof(ComboBox))
                            {
                                ComboBox comboBox = (ComboBox)element;
                                Command command = operation.Commands.First(x => x.Name.Equals(textBlock.Text.Remove(textBlock.Text.Length - 1)));

                                foreach (Command c in comboBox.Items)
                                {
                                    if (c.CustomParameterName.Equals(command.CustomParameterName))
                                    {
                                        comboBox.SelectedItem = c;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            DataContainer.Sequence.Operations.Remove(editOperation);
            DataContainer.MainWindowVM.UpdateSequence();
            DataContainer.AddCommandUserControlVM.IsOpen = false;
        }

        private void ComboBoxTimeUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateButtonAcceptStatus();
        }
    }
}
