using FTD2XX_NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IziR.Glue;

namespace FtdiLib
{
    public class FtdiRelay:IRelay
    {
        private readonly IRelayBoard _device;

        public FtdiRelay(IRelayBoard device, int index)
        {
            Index = index;
            _device = device;
        }

        private bool _isOpen;
        public bool IsOpen
        {
            get { return _isOpen; }
            set
            {
                if (_device.Switch(this, value)) _isOpen = value;
            }
        }

        public int Index { get; }

        public bool SetStatus(bool status)
        {
            return IsOpen = status;
        }
    }
}
