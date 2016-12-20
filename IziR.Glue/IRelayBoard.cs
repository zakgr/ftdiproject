using System.Collections.Generic;

namespace IziR.Glue
{
    public interface IRelayBoard
    {
        string Serial { get; }
        bool Switch(IRelay relay, bool status);
        IEnumerable<IRelay> Relays { get; }
    }
}
