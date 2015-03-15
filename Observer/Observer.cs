using Observer.Channels;
using Observer.Channels.Pushbullet;
using Observer.Events;
using Observer.Infrastructure;
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

        private static LocalPlayer Me { get { return StyxWoW.Me; } }
        private Form _settingsForm { get; set; }
        private bool _pluginEnabled { get { return PluginManager.Plugins.Find(x => x.Name == Name).Enabled; } }
        private bool _loggedIn { get; set; }

        // Ten seconds before adding a new event of the same type
        private readonly TimeSpan _generalTimeInterval = new TimeSpan(0, 0, 10);

        private DateTime _lastStuckTime = DateTime.MinValue;

        private IList<Channel> _channels { get; set; }
        private IEventQueue _eventQueue { get; set; }

        #endregion

        #region Setup

        public ObserverPlugin()
        {
            this._settingsForm = new SettingsForm();
            this._loggedIn = StyxWoW.IsInGame;
            this._eventQueue = new EventQueue();
            this._channels = new List<Channel>();

            Channel channel = new PushbulletChannel("gMFk52PIPaUQDDy6FKIBFyM5iFl4Dhkw");
            this._channels.Add(channel);
            this._eventQueue.Subscribe(channel);

            if (_pluginEnabled)
            {
                OnEnable();
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
            this._settingsForm.Show();
            this._settingsForm.Focus();
        }

        public override void OnEnable()
        {
            AttachEvents();
        }

        public override void OnDisable()
        {
            DetachEvents();
        }

        #endregion

        public override void Pulse()
        {
            if (_loggedIn && !StyxWoW.IsInGame)
            {
                _loggedIn = false;

                // TODO: Send event
            }
            else if (!_loggedIn && StyxWoW.IsInGame)
            {
                _loggedIn = true;

                // TODO: Send event
            }
        }

        #region Events

        private void AttachEvents()
        {
            BotEvents.OnBotStopped += BotEvents_OnBotStopped;
            BotEvents.OnBotStarted += BotEvents_OnBotStarted;
            Logging.OnLogMessage += Logging_OnLogMessage;
            Chat.Whisper += ChatEvents_Whisper;
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
            BotEvents.Player.OnPlayerDied -= PlayerEvents_OnPlayerDied;
            BotEvents.Player.OnLevelUp -= PlayerEvents_OnLevelUp;
            BotEvents.Battleground.OnBattlegroundEntered -= BattlegroundEvents_OnBattlegroundEntered;
            BotEvents.Battleground.OnBattlegroundLeft -= BattlegroundEvents_OnBattlegroundLeft;
        }

        private void BattlegroundEvents_OnBattlegroundEntered(BattlegroundType type)
        {
            // TODO: Send event
        }

        private void BattlegroundEvents_OnBattlegroundLeft(EventArgs args)
        {
            // TODO: Send event
        }

        private void BotEvents_OnBotStopped(EventArgs args)
        {
            _eventQueue.Enqueue(new BotStoppedEvent());
        }

        private void BotEvents_OnBotStarted(EventArgs args)
        {
            _eventQueue.Enqueue(new BotStartedEvent());
        }

        private void PlayerEvents_OnLevelUp(BotEvents.Player.LevelUpEventArgs args)
        {
            // TODO: Send event
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
            // TODO: send event
        }

        private void ChatEvents_Whisper(Chat.ChatWhisperEventArgs e)
        {
            Logger.Log("Got a whisper from {0}{1}: \"{2}\"", e.Status == "GM" ? "Game Master " : "", e.Author, e.Message);
            // TODO: send event
        }

        #endregion
    }
}
