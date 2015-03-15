﻿using Observer.Commands;
using Observer.Events;
using Observer.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer.Channels
{
    public abstract class Channel : IObservable<ICommand>, IObservable<Exception>, IObserver<IEnumerable<Event>>
    {
        protected IList<IObserver<ICommand>> _commandObservers;
        protected IList<IObserver<Exception>> _exceptionObservers;

        public Channel()
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

        public async void OnNext(IEnumerable<Event> values)
        {
            if (values.Count() <= 0)
            {
                return;
            }

            StringBuilder sb = null;
            int count = 0;
            foreach (var value in values)
            {
                ++count;

                if (sb == null)
                {
                    sb = new StringBuilder(value.Message);
                }
                else
                {
                    sb.Append("\n");
                    sb.Append(value.Message);
                }
            }

            string hash = string.Format("{0:X}", DateTime.Now.GetHashCode());
            string title = string.Format("{0} Observer event{1}: {2}", count, count == 1 ? "" : "s", string.Join(", ", values.Select(x => x.Identifier)));
            string message = sb.ToString();

            Logger.Log("{0}: {1}\n{2}", hash, title, message);

            Exception e = await SendMessage(title, message);
            if (e != null)
            {
                Logger.Log("Observer failed to send message {0}", hash);
                foreach (var observer in _exceptionObservers)
                {
                    observer.OnError(e);
                }
            }
        }

        protected abstract Task<Exception> SendMessage(string title, string message);
    }
}