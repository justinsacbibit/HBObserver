using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer.Events
{
    public class EventBatch
    {
        protected EventBatch() { }

        public EventBatch(IEnumerable<Event> values)
        {
            Events = values;
            Load();
        }

        private void Load()
        {
            if (Events == null || Events.Count() <= 0)
            {
                throw new Exception("Null or empty event batch");
            }

            StringBuilder sb = null;
            int count = 0;
            foreach (var value in Events)
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

            Hash = string.Format("{0:X}", DateTime.Now.GetHashCode());
            Title = string.Format("{0} Observer event{1}: {2}", count, count == 1 ? "" : "s", string.Join(", ", Events.Select(x => x.Identifier)));
            Message = sb.ToString();
        }

        public IEnumerable<Event> Events { get; private set; }
        public string Hash { get; private set; }
        public string Title { get; private set; }
        public string Message { get; private set; }
    }
}
