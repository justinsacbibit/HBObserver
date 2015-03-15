using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer.Events
{
    class BotStoppedEvent : IEvent
    {
        public string Message
        {
            get { return "Bot stopped"; }
        }

        public string Details
        {
            get { return "Bot has been stopped"; }
        }
    }
}
