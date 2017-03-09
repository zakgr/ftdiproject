using Lib.Glue;

namespace SerialLib
{
    public class SerialRelay:IRelay
    {
        public SerialRelay(IRelayBoard device, int index)
        {
            Index = index;
            _device = device;
            IsOpen = false;
        }
        private readonly IRelayBoard _device;
        private bool _isOpen;

        public bool IsOpen {
            get { return _isOpen; }
            set
            {
                if (_device.Switch(this, value))
                    _isOpen = value;
            }
        }
        public int Index { get; }
        public bool SetStatus(bool status)
        {
            return IsOpen = status;
        }
    }
}
