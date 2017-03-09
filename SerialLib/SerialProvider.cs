using System.Collections.Generic;
using System.IO.Ports;
using System.Management;
using Lib.Glue;

namespace SerialLib
{
    public class SerialProvider : IRelayDeviceProvider
    {
        private List<IRelayBoard> _boards;
        public IEnumerable<IRelayBoard> GetBoards
        {
            get
            {
                if (_boards != null) return _boards;
                _boards = new List<IRelayBoard>();
                var serialsBoards= SerialPort.GetPortNames();
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\WMI", "SELECT * FROM MSSerial_PortName");
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    var sInstanceName = queryObj["InstanceName"].ToString();

                    if (sInstanceName.Contains("VID_1A86&PID_7523"))
                    {
                        _boards.Add(new SerialBoard(queryObj["PortName"].ToString()));
                    }
                }
                        
                return _boards;
            }
        }
    }
}
