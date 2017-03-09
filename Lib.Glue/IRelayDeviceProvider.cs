using System.Collections.Generic;

namespace Lib.Glue
{
    public interface IRelayDeviceProvider
    {
        IEnumerable<IRelayBoard> GetBoards { get; }
    }
}
