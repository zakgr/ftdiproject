using System.Collections.Generic;

namespace IziR.Glue
{
    public interface IRelayDeviceProvider
    {
        IEnumerable<IRelayBoard> GetBoards { get; }
    }
}
