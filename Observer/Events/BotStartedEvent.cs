using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer.Events
{
    public class BotStartedEvent : IEvent
    {
        public string Message
        {
            get { return "Bot started"; }
        }

        public string Details
        {
            get { return "Bot has been started"; }
        }
    }
}
