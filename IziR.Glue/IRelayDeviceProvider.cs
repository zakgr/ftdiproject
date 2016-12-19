using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IziR.Glue
{
    public interface IRelayDeviceProvider
    {
        IEnumerable<IRelayBoard> GetBoards { get; }
    }
}
