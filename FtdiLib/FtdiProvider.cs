using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FTD2XX_NET;
using IziR.Glue;

namespace FtdiLib
{
    public class FtdiProvider : IRelayDeviceProvider
    {
        private List<IRelayBoard> _boards;

        public IEnumerable<IRelayBoard> GetBoards
        {
            get
            {
                if (_boards != null) return _boards;
                _boards=new List<IRelayBoard>();
                var arrayDevices = new FTDI.FT_DEVICE_INFO_NODE[100];

                new FTDI().GetDeviceList(arrayDevices);
                foreach (var ftDeviceInfoNode in arrayDevices.Where(d=>d!=null))
                    {
                    _boards.Add( new FtdiBoard(ftDeviceInfoNode.SerialNumber));
                }
                return _boards;
            }

        }
        
    }
}
