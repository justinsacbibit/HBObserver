using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer.Infrastructure
{
    public static class Logger
    {
        public static System.Windows.Media.Color LogColor { get { return System.Windows.Media.Color.FromRgb(114, 237, 196); } }

        public static void Log(string message, params object[] args)
        {
            Styx.Common.Logging.Write(LogColor, string.Format("{0}: {1}", ObserverPlugin.PluginName, string.Format(message, args)));
        }
    }
}
