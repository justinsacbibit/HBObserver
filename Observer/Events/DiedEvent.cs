using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer.Events
{
    public class DiedEvent : IEvent
    {
        public string Message
        {
            get { return "You died!"; }
        }

        public string Details
        {
            get { return "You died!"; }
        }
    }
}
