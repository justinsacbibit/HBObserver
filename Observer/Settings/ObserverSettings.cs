using Observer.Events;
using Styx.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer.Settings
{
    public class ObserverSettings : Styx.Helpers.Settings
    {
        public static readonly ObserverSettings SharedInstance = new ObserverSettings();

        protected ObserverSettings()
            : base(Path.Combine(Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName), string.Format(@"Settings/Observer/{0}-{1}.xml", ObserverPlugin.Me.Name, ObserverPlugin.Me.RealmName)))
        {
            Load();
        }

        [Setting, DefaultValue(false)]
        public bool PushbulletEnabled { get; set; }

        [Setting, DefaultValue("")]
        public string PushbulletAccessToken { get; set; }

        [Setting, DefaultValue(true), EventIdentifier(Event.BOT_START_IDENTIFIER)]
        public bool NotifyOnBotStart { get; set; }

        [Setting, DefaultValue(true), EventIdentifier(Event.BOT_STOP_IDENTIFIER)]
        public bool NotifyOnBotStop { get; set; }

        [Setting, DefaultValue(true), EventIdentifier(Event.WHISPER_IDENTIFIER)]
        public bool NotifyOnWhisper { get; set; }

        [Setting, DefaultValue(true), EventIdentifier(Event.BNET_WHISPER_IDENTIFIER)]
        public bool NotifyOnBNWhisper { get; set; }

        [Setting, DefaultValue(true), EventIdentifier(Event.BG_ENTER_IDENTIFIER)]
        public bool NotifyOnBattlegroundEnter { get; set; }

        [Setting, DefaultValue(true), EventIdentifier(Event.BG_LEFT_IDENTIFIER)]
        public bool NotifyOnBattlegroundExit { get; set; }

        [Setting, DefaultValue(true), EventIdentifier(Event.DIED_IDENTIFIER)]
        public bool NotifyOnDeath { get; set; }

        [Setting, DefaultValue(true), EventIdentifier(Event.LEVEL_UP_IDENTIFIER)]
        public bool NotifyOnLevelUp { get; set; }

        [Setting, DefaultValue(true), EventIdentifier(Event.LOG_IN_IDENTIFIER)]
        public bool NotifyOnLogIn { get; set; }

        [Setting, DefaultValue(true), EventIdentifier(Event.LOG_OUT_IDENTIFIER)]
        public bool NotifyOnLogOut { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EventIdentifierAttribute : Attribute
    {
        public readonly string Value;

        public EventIdentifierAttribute(string value)
        {
            Value = value;
        }
    }
}
