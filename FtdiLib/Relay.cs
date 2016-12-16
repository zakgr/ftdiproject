using FTD2XX_NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtdiLib
{
    
    public class Relay
    {
        private readonly FTDI _ftdi = new FTDI();
        private uint _i;

        public byte PositionNo(int p)
        {
            return (byte)((byte)1 << ((2 * p) + 1));
        }
        public byte PositionNc(int p)
        {
            return (byte)((byte)1 << ((2 * p) + 2));
        }

        public string WriteDevice(FTDI ftdi, byte data)
        {
            _i = 0;
            var status = ftdi.Write(new byte[1] { data }, 1, ref _i);
            return status.ToString();
        }
        public string ReadDevice(FTDI ftdi, out byte[] data)
        {
            _i = 0;
            data = new byte[1];
            var status = ftdi.Read(data, 1, ref _i);
            return status.ToString();
        }
    }
}
