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
using System.Reflection;

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

            InitMap();
            InitSettings();
        }

        #endregion

        #region Settings

        private IDictionary<EventType, CheckedListBox> _eventTypeToListBoxMap;
        private IEnumerable<EventProperty> _eventProperties;
        private IDictionary<string, Tuple<CheckedListBox, int>> _eventPropertyToCheckboxMap;

        private void InitMap()
        {
            botEventsCheckedListBox.Items.Clear();
            chatEventsCheckedListBox.Items.Clear();
            playerEventsCheckedListBox.Items.Clear();
            _eventTypeToListBoxMap = new Dictionary<EventType, CheckedListBox>()
            {
                { EventType.Bot, botEventsCheckedListBox },
                { EventType.Chat, chatEventsCheckedListBox },
                { EventType.Player, playerEventsCheckedListBox }
            };
        }

        private class EventProperty
        {
            public EventProperty() { }

            public PropertyInfo Property { get; set; }
            public EventTypeAttribute TypeAttribute { get; set; }
            public EventIdentifierAttribute IdentifierAttribute { get; set; }
            public SettingsTextAttribute TextAttribute { get; set; }
        }

        /// <summary>
        /// Load any settings here
        /// </summary>
        private void InitSettings()
        {
            // Channels
            pushbulletEnabledCheckbox.Checked = ObserverSettings.SharedInstance.PushbulletEnabled;
            pushbulletTextBox.Text = ObserverSettings.SharedInstance.PushbulletAccessToken;

            // Get all properties from ObserverSettings.SharedInstance with the following attributes:
            // EventTypeAttribute, EventIdentifierAttribute, SettingsTextAttribute
            _eventProperties = from p in ObserverSettings.SharedInstance.GetType().GetProperties()
                               let typeAttr = p.GetCustomAttributes(typeof(EventTypeAttribute), true)
                               let idAttr = p.GetCustomAttributes(typeof(EventIdentifierAttribute), true)
                               let textAttr = p.GetCustomAttributes(typeof(SettingsTextAttribute), true)
                               where typeAttr.Length == 1 && idAttr.Length == 1 && textAttr.Length == 1
                               orderby (textAttr.First() as SettingsTextAttribute).Value
                               select new EventProperty()
                               {
                                   Property = p,
                                   TypeAttribute = typeAttr.First() as EventTypeAttribute,
                                   IdentifierAttribute = idAttr.First() as EventIdentifierAttribute,
                                   TextAttribute = textAttr.First() as SettingsTextAttribute
                               };

            var typeCounts = new Dictionary<EventType, int>();
            _eventPropertyToCheckboxMap = new Dictionary<string, Tuple<CheckedListBox, int>>();

            foreach (var eventProperty in _eventProperties)
            {
                EventType eventType = eventProperty.TypeAttribute.Value;

                if (!typeCounts.ContainsKey(eventType))
                {
                    typeCounts[eventType] = 0;
                }

                // Get the appropriate checked list box for this event type
                var checkedListBox = _eventTypeToListBoxMap[eventType];
                int count = typeCounts[eventType];

                // Add checkboxes
                checkedListBox.Items.Add(eventProperty.TextAttribute.Value, (bool)eventProperty.Property.GetValue(ObserverSettings.SharedInstance, null));

                // Keep track of which properties map to which checkboxes for saving purposes
                _eventPropertyToCheckboxMap[eventProperty.Property.Name] = new Tuple<CheckedListBox, int>(checkedListBox, count);

                ++typeCounts[eventType];
            }
        }

        /// <summary>
        /// Commit any settings here
        /// </summary>
        private void CommitSettings()
        {
            ObserverSettings.SharedInstance.PushbulletEnabled = pushbulletEnabledCheckbox.Checked;
            ObserverSettings.SharedInstance.PushbulletAccessToken = pushbulletTextBox.Text;

            foreach (var eventProperty in _eventProperties)
            {
                var checkedListBoxAndIndex = _eventPropertyToCheckboxMap[eventProperty.Property.Name];
                bool enabled = checkedListBoxAndIndex.Item1.GetItemChecked(checkedListBoxAndIndex.Item2);
                eventProperty.Property.SetValue(ObserverSettings.SharedInstance, enabled);
            }

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
