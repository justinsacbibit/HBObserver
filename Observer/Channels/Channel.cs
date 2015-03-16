using Observer.Commands;
using Observer.Events;
using Observer.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer.Channels
{
    public abstract class Channel : IObservable<ICommand>, IObservable<Exception>, IObserver<EventBatch>
    {
        protected IList<IObserver<ICommand>> _commandObservers;
        protected IList<IObserver<Exception>> _exceptionObservers;

        protected Channel()
        {
            _commandObservers = new List<IObserver<ICommand>>();
        }

        public IDisposable Subscribe(IObserver<ICommand> observer)
        {
            _commandObservers.Add(observer);
            return new Unsubscriber<ICommand>(_commandObservers, observer);
        }

        public IDisposable Subscribe(IObserver<Exception> observer)
        {
            _exceptionObservers.Add(observer);
            return new Unsubscriber<Exception>(_exceptionObservers, observer);
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public async void OnNext(EventBatch batch)
        {
            Logger.Log("{0}: {1}\n{2}", batch.Hash, batch.Title, batch.Message);

            Exception e = await SendEventBatch(batch);
            if (e != null)
            {
                Logger.Log("Observer failed to send message {0}", batch.Hash);
                foreach (var observer in _exceptionObservers)
                {
                    observer.OnError(e);
                }
            }
        }

        protected abstract Task<Exception> SendEventBatch(EventBatch batch);
    }
}
