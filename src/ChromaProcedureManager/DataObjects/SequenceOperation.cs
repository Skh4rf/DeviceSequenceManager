using System.Collections.Generic;

namespace DeviceSequenceManager
{
    internal class SequenceOperation
    {
        private Device device;
        private List<Command> commands;
        private int duration;
        private TimeUnit timeUnit;
        private int index;

        public int Index
        {
            get { return index; }
            set { index = value; }
        }
        public TimeUnit TimeUnit
        {
            get { return timeUnit; }
            set { timeUnit = value; }
        }
        public int Duration
        {
            get { return duration; }
            set { duration = value; }
        }
        public string DurationString
        {
            get
            {
                if (TimeUnit == TimeUnit.Minutes)
                {
                    return Duration + "min";
                }
                return Duration + "s";
            }
        }
        public Device Device
        {
            get { return device; }
            set { device = value; }
        }

        public List<Command> Commands
        {
            get { return commands; }
            set { commands = value; }
        }

        public string CommandList
        {
            get
            {
                string str = "";
                foreach (Command cmd in commands)
                {
                    str += cmd.CastCommandString + "; ";
                }
                return str.Remove(str.Length - 2);
            }
        }

        public SequenceOperation()
        {
            commands = new List<Command>();
        }
    }

    public enum TimeUnit
    {
        Seconds = 1,
        Minutes = 60
    }
}
