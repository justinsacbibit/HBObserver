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

        [Setting, DefaultValue(true)]
        [EventIdentifier(Event.BOT_START_IDENTIFIER)]
        [SettingsTextAttribute("Start")]
        [EventType(EventType.Bot)]
        public bool NotifyOnBotStart { get; set; }

        [Setting, DefaultValue(true)]
        [EventIdentifier(Event.BOT_STOP_IDENTIFIER)]
        [SettingsTextAttribute("Stop")]
        [EventType(EventType.Bot)]
        public bool NotifyOnBotStop { get; set; }

        [Setting, DefaultValue(true)]
        [EventIdentifier(Event.WHISPER_IDENTIFIER)]
        [SettingsTextAttribute("Whisper")]
        [EventType(EventType.Chat)]
        public bool NotifyOnWhisper { get; set; }

        [Setting, DefaultValue(true)]
        [EventIdentifier(Event.BNET_WHISPER_IDENTIFIER)]
        [SettingsTextAttribute("Battle.net whisper")]
        [EventType(EventType.Chat)]
        public bool NotifyOnBNWhisper { get; set; }

        [Setting, DefaultValue(true)]
        [EventIdentifier(Event.BG_ENTER_IDENTIFIER)]
        [SettingsTextAttribute("Battleground join")]
        [EventType(EventType.Player)]
        public bool NotifyOnBattlegroundEnter { get; set; }

        [Setting, DefaultValue(true)]
        [EventIdentifier(Event.BG_LEFT_IDENTIFIER)]
        [SettingsTextAttribute("Battleground leave")]
        [EventType(EventType.Player)]
        public bool NotifyOnBattlegroundExit { get; set; }

        [Setting, DefaultValue(true)]
        [EventIdentifier(Event.DIED_IDENTIFIER)]
        [SettingsTextAttribute("Death")]
        [EventType(EventType.Player)]
        public bool NotifyOnDeath { get; set; }

        [Setting, DefaultValue(true)]
        [EventIdentifier(Event.LEVEL_UP_IDENTIFIER)]
        [SettingsTextAttribute("Level up")]
        [EventType(EventType.Player)]
        public bool NotifyOnLevelUp { get; set; }

        [Setting, DefaultValue(true)]
        [EventIdentifier(Event.LOG_IN_IDENTIFIER)]
        [SettingsTextAttribute("Log in")]
        [EventType(EventType.Player)]
        public bool NotifyOnLogIn { get; set; }

        [Setting, DefaultValue(true)]
        [EventIdentifier(Event.LOG_OUT_IDENTIFIER)]
        [SettingsTextAttribute("Log out")]
        [EventType(EventType.Player)]
        public bool NotifyOnLogOut { get; set; }
    }

    public enum EventType
    {
        Chat,
        Player,
        Bot
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EventTypeAttribute : Attribute
    {
        public readonly EventType Value;

        public EventTypeAttribute(EventType value)
        {
            Value = value;
        }
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

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SettingsTextAttribute : Attribute
    {
        public readonly string Value;

        public SettingsTextAttribute(string value)
        {
            Value = value;
        }
    }
}
