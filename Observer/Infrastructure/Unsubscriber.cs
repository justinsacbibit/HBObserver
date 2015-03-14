using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer.Infrastructure
{
    internal class Unsubscriber<T> : IDisposable where T : class
    {
        private IList<IObserver<T>> _observers;
        private IObserver<T> _observer;

        internal Unsubscriber(IList<IObserver<T>> observers, IObserver<T> observer)
        {
            _observer = observer;
            _observers = observers;
        }

        public void Dispose()
        {
            if (_observers.Contains(_observer))
            {
                _observers.Remove(_observer);
            }
        }
    }
}
