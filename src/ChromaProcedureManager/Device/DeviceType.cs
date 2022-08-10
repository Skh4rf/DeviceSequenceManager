using System.Collections.Generic;

namespace DeviceSequenceManager
{
    internal class DeviceType
    {
        private string name;
        private List<CommandGroup> commandGroups;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public List<CommandGroup> CommandGroups
        {
            get { return commandGroups; }
            set { commandGroups = value; }
        }
    }
}
