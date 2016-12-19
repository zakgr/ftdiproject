using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IziR.Glue
{
    public interface IRelayBoard
    {
        string Serial { get; }
        bool Switch(IRelay relay, bool status);
        IEnumerable<IRelay> Relays { get; }
    }
}
