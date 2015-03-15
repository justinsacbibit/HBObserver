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
    public class EventQueue : IEventQueue
    {
        private IList<IObserver<IEnumerable<Event>>> _observers;
        private Queue<Event> _events;
        private DateTime _timeLastEmptied;
        private Timer _timer;

        public EventQueue()
        {
            _observers = new List<IObserver<IEnumerable<Event>>>();
            _events = new Queue<Event>();
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

                _events = new Queue<Event>();
            }
        }

        public void EnqueueIfEnabled(Event event_)
        {
            if (event_.Enabled)
            {
                _events.Enqueue(event_);
            }
        }

        public IDisposable Subscribe(IObserver<IEnumerable<Event>> observer)
        {
            _observers.Add(observer);
            return new Unsubscriber<IEnumerable<Event>>(_observers, observer);
        }
    }
}
