using System.Collections.Generic;

namespace DeviceSequenceManager
{
    internal class Command
    {
        private string name;
        private string commandString;
        private CommandType commandtype;
        private double rangeMinimum;
        private double rangeMaximum;
        private int decimalPlaces;
        private double numberCommandValue;
        private int priority;
        private string customParameterName;
        private string customParameterCommand;

        #region PropertyIni
        public string Name { get { return name; } set { name = value; } }
        public string CommandString { get { return commandString; } set { commandString = value; } }
        public CommandType CommandType { get { return commandtype; } set { commandtype = value; } }
        public double RangeMinimum { get { return rangeMinimum; } set { rangeMinimum = value; } }
        public double RangeMaximum { get { return rangeMaximum; } set { rangeMaximum = value; } }
        public int DecimalPlaces { get { return decimalPlaces; } set { decimalPlaces = value; } }
        public int Priority { get { return priority; } set { priority = value; } }
        public string CustomParameterName { get { return customParameterName; } set { customParameterName = value; } }
        public string CustomParameterCommand { get { return customParameterCommand; } set { customParameterCommand = value; } }
        public double NumberCommandValue { get { return numberCommandValue; } set { numberCommandValue = value; } }
        #endregion PropertyIni

        public Command()
        {

        }

        public string CastCommandStringForCustomCommand
        {
            get
            {
                if (CommandType == CommandType.Custom)
                {
                    return CommandString + " " + CustomParameterCommand;
                }
                else { return commandString + "?"; }
            }
        }

        public string CastCommandStringForNumberCommand(double value)
        {
            return CommandString + " " + value.ToString();
        }

        public string CastCommandString
        {
            get
            {
                if (commandtype == CommandType.Number)
                {
                    return CastCommandStringForNumberCommand(numberCommandValue);
                }
                else
                {
                    return CastCommandStringForCustomCommand;
                }
            }
        }
    }

    public enum CommandType
    {
        Number = 1,
        Custom = 2,
        None = 4
    }

    internal class CommandGroup
    {
        public List<Command> Commands { get; set; }

        public CommandGroup()
        {
            Commands = new List<Command>();
        }
    }
}
