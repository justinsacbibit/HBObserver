using Observer.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer.Events
{
    // To add a new event:
    // 1. Add identifier to Event
    // 2. Add property to ObserverSettings
    // 3. Enqueue the event

    public class Event
    {
        public const string BOT_START_IDENTIFIER = "BotStart";
        public const string BOT_STOP_IDENTIFIER = "BotStop";
        public const string WHISPER_IDENTIFIER = "Whisper";
        public const string BNET_WHISPER_IDENTIFIER = "BNWhisper";
        public const string BG_ENTER_IDENTIFIER = "BGEnter";
        public const string BG_LEFT_IDENTIFIER = "BGLeft";
        public const string DIED_IDENTIFIER = "Died";
        public const string LEVEL_UP_IDENTIFIER = "LevelUp";
        public const string LOG_IN_IDENTIFIER = "LogIn";
        public const string LOG_OUT_IDENTIFIER = "LogOut";

        public Event(string identifier, string message)
        {
            Identifier = identifier;
            Message = message;
        }

        public string Identifier { get; set; }
        public string Message { get; set; }

        public bool Enabled
        {
            get
            {
                var props = from p in ObserverSettings.SharedInstance.GetType().GetProperties()
                            let attr = p.GetCustomAttributes(typeof(EventIdentifierAttribute), true)
                            where attr.Length == 1
                            select new { Property = p, Attribute = attr.First() as EventIdentifierAttribute };
                var prop = props.First(x => x.Attribute.Value == Identifier);
                return (bool)prop.Property.GetValue(ObserverSettings.SharedInstance, null);
            }
        }
    }
}
