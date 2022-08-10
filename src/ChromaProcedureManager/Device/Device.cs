using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalInstruments.Visa;
using System.Windows;

namespace DeviceSequenceManager
{
    internal class Device
    {
        private DeviceType deviceType;
        private TcpAddress tcpAddress;
        private MessageBasedSession deviceSession;
        private bool deviceConnected;

        public DeviceType DeviceType
        {
            get { return deviceType; }
            set { deviceType = value; }
        }

        public TcpAddress TcpAddress
        {
            get { return tcpAddress; }
            set { tcpAddress = value; }
        }

        public void OpenSession()
        {
            using (ResourceManager rmSession = new ResourceManager())
            {
                try
                {
                    deviceSession = (MessageBasedSession)rmSession.Open(TcpAddress.TCP);
                    deviceConnected = true;
                }
                catch (InvalidCastException)
                {
                    MessageBox.Show("Resource selected must be a message-based session");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void CloseSession()
        {
            try
            {
                if (deviceSession != null)
                {
                    deviceSession.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void WriteCommand(string command)
        {
            try
            {
                if (deviceConnected == false) { throw new Exception("Not connected to the device. Did the device shut down?"); }
                deviceSession.RawIO.Write(command);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public bool ConnectionAvailable()
        {
            try
            {
                using (ResourceManager rmSession = new ResourceManager())
                {
                    deviceSession = (MessageBasedSession)rmSession.Open(TcpAddress.TCP);
                    deviceSession.RawIO.Write("*IDN?\n");
                    if (deviceSession != null)
                    {
                        deviceSession.Dispose();
                    }
                }
            }catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
