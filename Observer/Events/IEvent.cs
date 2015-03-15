using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer.Events
{
    public interface IEvent
    {
        string Message { get; }
        string Details { get; }
    }
}
