using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Net;

// Honorbuddy includes
using Styx;
using Styx.Common;
using Styx.CommonBot;
using Styx.Helpers;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;
using Styx.Plugins;
using Styx.Pathing;
using Styx.WoWInternals.World;

using Observer.Settings;
using Observer.Infrastructure;
using System.Linq.Expressions;

namespace Observer
{
    public partial class SettingsForm : Form
    {
        #region FormInitialize

        public SettingsForm()
        {
            InitializeComponent();

            // Hook events for custom form handling
            FormHeader.MouseDown += FormHeader_MouseDown;
            FormHeader.MouseMove += FormHeader_MouseMove;
            FormHeaderClose.Click += FormHeaderClose_Click;

            // Determine which buttons to show based on tag values
            if (Convert.ToString(btnPurchase.Tag) != "" && Convert.ToString(btnSupport.Tag) != "")
            {
                btnPurchase.Visible = true;
                btnSupport.Visible = true;
            }
            else if (Convert.ToString(btnPurchase.Tag) != "")
            {
                btnSupport.Tag = btnPurchase.Tag;
                btnSupport.Text = btnPurchase.Text;
                btnSupport.Visible = true;
            }
            else if (Convert.ToString(btnSupport.Tag) != "")
            {
                btnSupport.Visible = true;
            }
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            Version ver = ObserverPlugin.PluginVersion;
            lblVersion.Text = String.Format("v{0}.{1}.{2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision);

            rtfAbout.Rtf = Resources.About;

            InitSettings();
        }

        #endregion

        #region Settings

        /// <summary>
        /// Load any settings here
        /// </summary>
        private void InitSettings()
        {
            // Channels
            pushbulletEnabledCheckbox.Checked = ObserverSettings.SharedInstance.PushbulletEnabled;
            pushbulletTextBox.Text = ObserverSettings.SharedInstance.PushbulletAccessToken;

            // Events
            chatEventsCheckedListBox.SetItemChecked(0, ObserverSettings.SharedInstance.NotifyOnWhisper);
            chatEventsCheckedListBox.SetItemChecked(1, ObserverSettings.SharedInstance.NotifyOnBNWhisper);

            playerEventsCheckedListBox.SetItemChecked(0, ObserverSettings.SharedInstance.NotifyOnBattlegroundEnter);
            playerEventsCheckedListBox.SetItemChecked(1, ObserverSettings.SharedInstance.NotifyOnBattlegroundExit);
            playerEventsCheckedListBox.SetItemChecked(2, ObserverSettings.SharedInstance.NotifyOnDeath);
            playerEventsCheckedListBox.SetItemChecked(3, ObserverSettings.SharedInstance.NotifyOnLevelUp);
            playerEventsCheckedListBox.SetItemChecked(4, ObserverSettings.SharedInstance.NotifyOnLogIn);
            playerEventsCheckedListBox.SetItemChecked(5, ObserverSettings.SharedInstance.NotifyOnLogOut);

            botEventsCheckedListBox.SetItemChecked(0, ObserverSettings.SharedInstance.NotifyOnBotStart);
            botEventsCheckedListBox.SetItemChecked(1, ObserverSettings.SharedInstance.NotifyOnBotStop);
        }

        /// <summary>
        /// Commit any settings here
        /// </summary>
        private void CommitSettings()
        {
            ObserverSettings.SharedInstance.PushbulletEnabled = pushbulletEnabledCheckbox.Checked;
            ObserverSettings.SharedInstance.PushbulletAccessToken = pushbulletTextBox.Text;

            ObserverSettings.SharedInstance.NotifyOnWhisper = chatEventsCheckedListBox.GetItemChecked(0);
            ObserverSettings.SharedInstance.NotifyOnBNWhisper = chatEventsCheckedListBox.GetItemChecked(1);

            ObserverSettings.SharedInstance.NotifyOnBattlegroundEnter = playerEventsCheckedListBox.GetItemChecked(0);
            ObserverSettings.SharedInstance.NotifyOnBattlegroundExit = playerEventsCheckedListBox.GetItemChecked(1);
            ObserverSettings.SharedInstance.NotifyOnDeath = playerEventsCheckedListBox.GetItemChecked(2);
            ObserverSettings.SharedInstance.NotifyOnLevelUp = playerEventsCheckedListBox.GetItemChecked(3);
            ObserverSettings.SharedInstance.NotifyOnLogIn = playerEventsCheckedListBox.GetItemChecked(4);
            ObserverSettings.SharedInstance.NotifyOnLogOut = playerEventsCheckedListBox.GetItemChecked(5);

            ObserverSettings.SharedInstance.NotifyOnBotStart = botEventsCheckedListBox.GetItemChecked(0);
            ObserverSettings.SharedInstance.NotifyOnBotStop = botEventsCheckedListBox.GetItemChecked(1);

            ObserverSettings.SharedInstance.Save();
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        #endregion

        #region CustomUX
        private void Settings_Dispose(object sender, EventArgs e) { }

        /// <summary>
        /// Custom form drag and close handler.
        /// </summary>
        Point FormHeaderCursorPosition;
        private void FormHeader_MouseDown(object sender, MouseEventArgs e) { FormHeaderCursorPosition = new Point(-e.X, -e.Y); }
        private void FormHeader_MouseMove(object sender, MouseEventArgs e) { if (e.Button == MouseButtons.Left) { Point mousePos = Control.MousePosition; mousePos.Offset(FormHeaderCursorPosition.X, FormHeaderCursorPosition.Y); Location = mousePos; } }
        private void FormHeaderClose_Click(object sender, EventArgs e) { this.Hide(); CommitSettings(); }

        /// <summary>
        /// Resize a borderless form. It does have flicker on resize, but the form sizes.
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            const int wmNcHitTest = 0x84;
            const int htLeft = 10;
            const int htRight = 11;
            const int htTop = 12;
            const int htTopLeft = 13;
            const int htTopRight = 14;
            const int htBottom = 15;
            const int htBottomLeft = 16;
            const int htBottomRight = 17;

            if (m.Msg == wmNcHitTest)
            {
                int x = (int)(m.LParam.ToInt64() & 0xFFFF);
                int y = (int)((m.LParam.ToInt64() & 0xFFFF0000) >> 16);
                Point pt = PointToClient(new Point(x, y));
                Size clientSize = ClientSize;
                ///allow resize on the lower right corner
                if (pt.X >= clientSize.Width - 16 && pt.Y >= clientSize.Height - 16 && clientSize.Height >= 16)
                {
                    m.Result = (IntPtr)(IsMirrored ? htBottomLeft : htBottomRight);
                    return;
                }

                ///allow resize on the lower left corner
                if (pt.X <= 16 && pt.Y >= clientSize.Height - 16 && clientSize.Height >= 16)
                {
                    m.Result = (IntPtr)(IsMirrored ? htBottomRight : htBottomLeft);
                    return;
                }

                ///allow resize on the upper right corner
                if (pt.X <= 16 && pt.Y <= 16 && clientSize.Height >= 16)
                {
                    m.Result = (IntPtr)(IsMirrored ? htTopRight : htTopLeft);
                    return;
                }

                ///allow resize on the upper left corner
                if (pt.X >= clientSize.Width - 16 && pt.Y <= 16 && clientSize.Height >= 16)
                {
                    m.Result = (IntPtr)(IsMirrored ? htTopLeft : htTopRight);
                    return;
                }

                ///allow resize on the top border
                if (pt.Y <= 16 && clientSize.Height >= 16)
                {
                    m.Result = (IntPtr)(htTop);
                    return;
                }

                ///allow resize on the bottom border
                if (pt.Y >= clientSize.Height - 16 && clientSize.Height >= 16)
                {
                    m.Result = (IntPtr)(htBottom);
                    return;
                }

                ///allow resize on the left border
                if (pt.X <= 16 && clientSize.Height >= 16)
                {
                    m.Result = (IntPtr)(htLeft);
                    return;
                }

                ///allow resize on the right border
                if (pt.X >= clientSize.Width - 16 && clientSize.Height >= 16)
                {
                    m.Result = (IntPtr)(htRight);
                    return;
                }
            }

            base.WndProc(ref m);
        }

        /// <summary>
        /// Opens the purchase URL. Use the 'tag' field on btnPurchase to set the url location.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPurchase_Click(object sender, EventArgs e)
        {
            string url = Convert.ToString(btnPurchase.Tag);
            System.Diagnostics.Process.Start(url);
        }

        /// <summary>
        /// Opens the support URL. Use the 'tag' field on btnSupport to set the url location.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSupport_Click(object sender, EventArgs e)
        {
            string url = Convert.ToString(btnPurchase.Tag);
            System.Diagnostics.Process.Start(url);
        }
        #endregion
    }
}
