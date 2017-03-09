using System.Collections.Generic;
using System.IO.Ports;
using Lib.Glue;

namespace SerialLib
{
    public class SerialBoard : IRelayBoard
    {
        private readonly SerialPort _serialBoard;
        private byte[] _currentStatus;
        private readonly List<IRelay> _relays;

        public override string ToString()
        {
            return $"{Serial} (Serial)";
        }

        public SerialBoard(string serial)
        {
            _serialBoard = new SerialPort(serial, 9600, Parity.None, 8, StopBits.One);
            _serialBoard.Open();
            Serial = serial;

            _relays = new List<IRelay>()
            {
                new SerialRelay(this, 0)
            };
        }

        public IEnumerable<IRelay> Relays => _relays;

        public string Serial { get; }

        public bool Switch(IRelay relay, bool status)
        {
            _currentStatus = status ? new byte[] {0xA0, 0x01, 0x01, 0xA2} : new byte[] {0xA0, 0x01, 0x00, 0xA1};
            return WriteToDevice(_currentStatus);
        }

        private bool WriteToDevice(byte[] data)
        {
            try
            {

                _serialBoard.Write(data, 0, data.Length);
                return true;
            }
            catch
            {
                return false;
            }
        }

        ~SerialBoard()
        {
            _serialBoard.Dispose();
        }
    }
}
