using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IziR.Glue
{
    public interface IRelay
    {
        bool IsOpen { get; set; }
        int Index { get; }
        bool SetStatus(bool status);
    }
}
