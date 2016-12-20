using System;
using System.Collections.Generic;
using FTD2XX_NET;
using IziR.Glue;

namespace FtdiLib
{
    public class FtdiBoard : IRelayBoard
    {
        private readonly FTDI _ftdi;
        private byte _currentStatus;
        private uint _bytesRead;

        public override string ToString()
        {
            return $"{Serial} (FTDI)";
        }

        public FtdiBoard(string serial)
        {
            _ftdi = new FTDI();
            Serial = serial;
           
            if (_ftdi.OpenBySerialNumber(serial) != FTDI.FT_STATUS.FT_OK
                || _ftdi.SetBaudRate(921600) != FTDI.FT_STATUS.FT_OK
                || _ftdi.SetBitMode(255, 4) != FTDI.FT_STATUS.FT_OK)
                throw new Exception("Can't set device params");
         
            WriteToDevice(0);
            _relays = new List<IRelay>()
            {
                new FtdiRelay(this, 0),
                new FtdiRelay(this, 1),
                new FtdiRelay(this, 2),
                new FtdiRelay(this, 3),
            };
        }

        private bool WriteToDevice(byte data)
        {
            return _ftdi.Write(new byte[] { data }, 1, ref _bytesRead) == FTDI.FT_STATUS.FT_OK;
        }

        public string Serial { get; }

        private readonly List<IRelay> _relays;
        public IEnumerable<IRelay> Relays => _relays;

        public bool Switch(IRelay relay, bool status)
        {
            if (status)  _currentStatus |= (byte) Math.Pow(2, relay.Index*2 + 1);
            else _currentStatus &= (byte)(255 ^ (byte) Math.Pow(2, relay.Index*2+1));
            return WriteToDevice(_currentStatus);
        }

        ~FtdiBoard()
        {
            _ftdi.Close();
        }
    }
}
