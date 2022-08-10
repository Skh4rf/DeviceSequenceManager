using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceSequenceManager
{
    internal class TcpAddress
    {
        private string ip;
        private string port;
        private string typ;

        public string IP
        {
            get { return ip; }
            set { ip = value; }
        }

        public string Port
        {
            get { return port; }
            set { port = value; }
        }

        public string Typ
        {
            get { return typ; }
            set { typ = value; }
        }

        public string TCP
        {
            get { return $"TCPIP0::{ip}::{port}::{typ}"; }
        }
    }
}
