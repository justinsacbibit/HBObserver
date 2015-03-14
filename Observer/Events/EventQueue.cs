using Observer.Channels;
using Observer.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Observer.Events
{
    public class EventQueue : IObservable<IEnumerable<IEvent>>
    {
        private IList<IObserver<IEnumerable<IEvent>>> _observers;
        private Queue<IEvent> _events;
        private DateTime _timeLastEmptied;
        private Timer _timer;

        public EventQueue()
        {
            _observers = new List<IObserver<IEnumerable<IEvent>>>();
            _events = new Queue<IEvent>();
            _timeLastEmptied = DateTime.MinValue;

            // TODO: Pull from settings
            TimeSpan batchInterval = new TimeSpan(0, 0, 30);

            _timer = new Timer();
            _timer.Elapsed += _timer_Elapsed;
            _timer.Interval = batchInterval.TotalMilliseconds;
            _timer.Start();
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_events.Count > 0)
            {
                foreach (var observer in _observers)
                {
                    observer.OnNext(_events);
                }

                _events = new Queue<IEvent>();
            }
        }

        public void Enqueue(IEvent event_)
        {
            _events.Enqueue(event_);
        }

        public IDisposable Subscribe(IObserver<IEnumerable<IEvent>> observer)
        {
            _observers.Add(observer);
            return new Unsubscriber<IEnumerable<IEvent>>(_observers, observer);
        }
    }
}
