using Observer.Commands;
using Observer.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer.Channels
{
    public interface IChannel : IObservable<ICommand>, IObserver<IEnumerable<IEvent>>
    {
    }
}
