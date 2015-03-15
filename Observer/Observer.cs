using Observer.Channels;
using Observer.Channels.Pushbullet;
using Observer.Events;
using Observer.Infrastructure;
using Observer.Settings;
using Styx;
using Styx.Common;
using Styx.CommonBot;
using Styx.Helpers;
using Styx.Pathing;
using Styx.Plugins;
using Styx.WoWInternals;
using Styx.WoWInternals.World;
using Styx.WoWInternals.WoWObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Observer
{
    public class ObserverPlugin : HBPlugin
    {
        #region Metadata

        public static string PluginName = "Observer";
        public static string PluginAuthor = "gh0st3";
        public static Version PluginVersion = new Version(0, 0, 0, 0);
        public static string PluginSettingsButton = "Settings";

        public override string Name { get { return PluginName; } }
        public override string Author { get { return PluginAuthor; } }
        public override Version Version { get { return PluginVersion; } }
        public override string ButtonText { get { return PluginSettingsButton; } }
        public override bool WantButton { get { return !string.IsNullOrEmpty(PluginSettingsButton); } }

        #endregion

        #region Properties

        internal static LocalPlayer Me { get { return StyxWoW.Me; } }
        private SettingsForm _settingsForm;
        private bool _pluginEnabled;
        private bool _loggedIn;

        // Ten seconds before adding a new event of the same type
        private readonly TimeSpan _generalTimeInterval = new TimeSpan(0, 0, 10);

        private DateTime _lastStuckTime = DateTime.MinValue;

        private IDictionary<Channel, IDisposable> _channels;
        private IEventQueue _eventQueue;

        #endregion

        #region Setup

        public ObserverPlugin()
        {
            _settingsForm = new SettingsForm();
            _loggedIn = StyxWoW.IsInGame;
            _eventQueue = new EventQueue();
            _channels = new Dictionary<Channel, IDisposable>();

            SettingsDidCommit();

            if (_pluginEnabled)
            {
                OnEnable();
            }
        }

        private void SettingsDidCommit()
        {
            if (ObserverSettings.SharedInstance.PushbulletEnabled)
            {
                Channel channel = new PushbulletChannel(ObserverSettings.SharedInstance.PushbulletAccessToken);
                _channels.Add(channel, _eventQueue.Subscribe(channel));
            }
            else
            {
                var kvp = _channels.FirstOrDefault(x => x.Key as PushbulletChannel != null);
                if (kvp.Equals(default(KeyValuePair<Channel, IDisposable>)))
                {
                    return;
                }
                kvp.Value.Dispose();
                _channels.Remove(kvp.Key);
            }
        }

        //private void Dispose()
        //{
        //    try
        //    {
        //        this._settingsForm.Dispose();
        //        this._settingsForm = null;
        //    }
        //    catch (Exception e)
        //    {
        //        Logger.Log(e.Message);
        //    }
        //}

        /// <summary>
        /// Called when user clicks Settings button
        /// </summary>
        public override void OnButtonPress()
        {
            _settingsForm.ShowDialog();
            if (_settingsForm.DialogResult == DialogResult.OK)
            {
                SettingsDidCommit();
            }
        }

        public override void OnEnable()
        {
            _pluginEnabled = true;
            AttachEvents();
            Logger.Log("Observer initialized!");
        }

        public override void OnDisable()
        {
            _pluginEnabled = false;
            DetachEvents();
            Logger.Log("Observer disabled!");
        }

        #endregion

        public override void Pulse()
        {
            if (_loggedIn && !StyxWoW.IsInGame)
            {
                _loggedIn = false;

                _eventQueue.EnqueueIfEnabled(Event.LOG_OUT_IDENTIFIER, "Not in game!");
            }
            else if (!_loggedIn && StyxWoW.IsInGame)
            {
                _loggedIn = true;

                _eventQueue.EnqueueIfEnabled(Event.LOG_IN_IDENTIFIER, "In game!");
            }


        }

        #region Events

        private void AttachEvents()
        {
            BotEvents.OnBotStopped += BotEvents_OnBotStopped;
            BotEvents.OnBotStarted += BotEvents_OnBotStarted;
            Logging.OnLogMessage += Logging_OnLogMessage;
            Chat.Whisper += ChatEvents_Whisper;
            Lua.Events.DetachEvent("CHAT_MSG_BN_WHISPER", LuaEvents_BattleNetWhisper);
            Lua.Events.AttachEvent("CHAT_MSG_BN_WHISPER", LuaEvents_BattleNetWhisper);
            BotEvents.Player.OnPlayerDied += PlayerEvents_OnPlayerDied;
            BotEvents.Player.OnLevelUp += PlayerEvents_OnLevelUp;
            BotEvents.Battleground.OnBattlegroundEntered += BattlegroundEvents_OnBattlegroundEntered;
            BotEvents.Battleground.OnBattlegroundLeft += BattlegroundEvents_OnBattlegroundLeft;
        }

        private void DetachEvents()
        {
            BotEvents.OnBotStopped -= BotEvents_OnBotStopped;
            BotEvents.OnBotStarted -= BotEvents_OnBotStarted;
            Logging.OnLogMessage -= Logging_OnLogMessage;
            Chat.Whisper -= ChatEvents_Whisper;
            Lua.Events.DetachEvent("CHAT_MSG_BN_WHISPER", LuaEvents_BattleNetWhisper);
            BotEvents.Player.OnPlayerDied -= PlayerEvents_OnPlayerDied;
            BotEvents.Player.OnLevelUp -= PlayerEvents_OnLevelUp;
            BotEvents.Battleground.OnBattlegroundEntered -= BattlegroundEvents_OnBattlegroundEntered;
            BotEvents.Battleground.OnBattlegroundLeft -= BattlegroundEvents_OnBattlegroundLeft;
        }

        private void BattlegroundEvents_OnBattlegroundEntered(BattlegroundType type)
        {
            _eventQueue.EnqueueIfEnabled(Event.BG_ENTER_IDENTIFIER, "Entered battleground");
        }

        private void BattlegroundEvents_OnBattlegroundLeft(EventArgs args)
        {
            _eventQueue.EnqueueIfEnabled(Event.BG_LEFT_IDENTIFIER, "Left battleground");
        }

        private void BotEvents_OnBotStopped(EventArgs args)
        {
            _eventQueue.EnqueueIfEnabled(Event.BOT_STOP_IDENTIFIER, "Bot stopped!");
        }

        private void BotEvents_OnBotStarted(EventArgs args)
        {
            _eventQueue.EnqueueIfEnabled(Event.BOT_START_IDENTIFIER, "Bot started!");
        }

        private void PlayerEvents_OnLevelUp(BotEvents.Player.LevelUpEventArgs args)
        {
            _eventQueue.EnqueueIfEnabled(Event.LEVEL_UP_IDENTIFIER, "Leveled from {0} to {1}!", args.OldLevel, args.NewLevel);
        }

        private void Logging_OnLogMessage(System.Collections.ObjectModel.ReadOnlyCollection<Logging.LogMessage> messages)
        {
            foreach (Logging.LogMessage message in messages)
            {
                bool sendStuckEvent = DateTime.Now - _lastStuckTime > _generalTimeInterval && message.Message.Contains("We are stuck!");
                if (sendStuckEvent)
                {
                    _lastStuckTime = DateTime.Now;

                    // TODO: send event
                }
            }
        }

        private void PlayerEvents_OnPlayerDied()
        {
            if (!Battlegrounds.IsInsideBattleground)
            {
                _eventQueue.EnqueueIfEnabled(Event.DIED_IDENTIFIER, "You died!");
            }
        }

        private void ChatEvents_Whisper(Chat.ChatWhisperEventArgs e)
        {
            bool gmMessage = e.Status == "GM";
            _eventQueue.EnqueueIfEnabled(Event.WHISPER_IDENTIFIER, "Whisper from {0}{1}: \"{2}\"", gmMessage ? "Game Master " : "", e.Author, e.Message);
        }

        private void LuaEvents_BattleNetWhisper(object sender, LuaEventArgs e)
        {
            string message = e.Args[0].ToString();
            string author = Lua.GetReturnValues(string.Format("return BNGetFriendInfoByID({0})", e.Args[12].ToString()))[2];

            _eventQueue.EnqueueIfEnabled(Event.BNET_WHISPER_IDENTIFIER, "Bnet whisper from {0}: \"{1}\"", author, message);
        }

        #endregion
    }

    internal static class EventQueueExtensions
    {
        internal static void EnqueueIfEnabled(this IEventQueue eventQueue, string identifier, string format, params object[] args)
        {
            eventQueue.EnqueueIfEnabled(new Event(identifier, string.Format(format, args)));
        }
    }
}
