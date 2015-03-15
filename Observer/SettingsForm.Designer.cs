namespace Observer
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.FormHeader = new System.Windows.Forms.Label();
            this.FormHeaderClose = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.tooltipHandler = new System.Windows.Forms.ToolTip(this.components);
            this.vt = new Observer.VerticalTabs();
            this.tabAbout = new System.Windows.Forms.TabPage();
            this.tableAbout = new System.Windows.Forms.TableLayoutPanel();
            this.btnSupport = new System.Windows.Forms.Button();
            this.rtfAbout = new System.Windows.Forms.RichTextBox();
            this.lblAboutCaption = new System.Windows.Forms.Label();
            this.btnPurchase = new System.Windows.Forms.Button();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.botEventsGroupBox = new System.Windows.Forms.GroupBox();
            this.botEventsCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.playerEventsGroupBox = new System.Windows.Forms.GroupBox();
            this.playerEventsCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.chatEventsGroupBox = new System.Windows.Forms.GroupBox();
            this.chatEventsCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.tabAccounts = new System.Windows.Forms.TabPage();
            this.pushbulletGroupBox = new System.Windows.Forms.GroupBox();
            this.pushbulletAccessTokenLabel = new System.Windows.Forms.Label();
            this.pushbulletTextBox = new System.Windows.Forms.TextBox();
            this.pushbulletEnabledCheckbox = new System.Windows.Forms.CheckBox();
            this.vt.SuspendLayout();
            this.tabAbout.SuspendLayout();
            this.tableAbout.SuspendLayout();
            this.tabSettings.SuspendLayout();
            this.botEventsGroupBox.SuspendLayout();
            this.playerEventsGroupBox.SuspendLayout();
            this.chatEventsGroupBox.SuspendLayout();
            this.tabAccounts.SuspendLayout();
            this.pushbulletGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // FormHeader
            // 
            this.FormHeader.BackColor = System.Drawing.Color.SteelBlue;
            this.FormHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.FormHeader.Font = new System.Drawing.Font("Verdana", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormHeader.ForeColor = System.Drawing.Color.White;
            this.FormHeader.Location = new System.Drawing.Point(0, 0);
            this.FormHeader.Name = "FormHeader";
            this.FormHeader.Size = new System.Drawing.Size(1030, 30);
            this.FormHeader.TabIndex = 0;
            this.FormHeader.Text = "My Plugin Settings";
            this.FormHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormHeaderClose
            // 
            this.FormHeaderClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FormHeaderClose.AutoSize = true;
            this.FormHeaderClose.BackColor = System.Drawing.Color.SteelBlue;
            this.FormHeaderClose.Font = new System.Drawing.Font("Verdana", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormHeaderClose.ForeColor = System.Drawing.Color.MistyRose;
            this.FormHeaderClose.Location = new System.Drawing.Point(1000, 4);
            this.FormHeaderClose.Name = "FormHeaderClose";
            this.FormHeaderClose.Size = new System.Drawing.Size(19, 17);
            this.FormHeaderClose.TabIndex = 1;
            this.FormHeaderClose.Text = "X";
            this.FormHeaderClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tooltipHandler.SetToolTip(this.FormHeaderClose, "Close Window");
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblVersion.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblVersion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblVersion.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblVersion.Location = new System.Drawing.Point(8, 468);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(190, 26);
            this.lblVersion.TabIndex = 3;
            this.lblVersion.Text = "Version 1.0.0.0";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // vt
            // 
            this.vt.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.vt.Controls.Add(this.tabAbout);
            this.vt.Controls.Add(this.tabSettings);
            this.vt.Controls.Add(this.tabAccounts);
            this.vt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vt.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.vt.Font = new System.Drawing.Font("Verdana", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vt.ItemSize = new System.Drawing.Size(30, 200);
            this.vt.Location = new System.Drawing.Point(0, 30);
            this.vt.Margin = new System.Windows.Forms.Padding(0);
            this.vt.Multiline = true;
            this.vt.myBackColor = System.Drawing.Color.Empty;
            this.vt.Name = "vt";
            this.vt.Padding = new System.Drawing.Point(0, 0);
            this.vt.SelectedIndex = 0;
            this.vt.Size = new System.Drawing.Size(1030, 471);
            this.vt.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.vt.TabBackgroundColor = System.Drawing.Color.DimGray;
            this.vt.TabFont = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.vt.TabFontColor = System.Drawing.Color.White;
            this.vt.TabIndex = 2;
            this.vt.TabSelectedBackgroundColor = System.Drawing.Color.White;
            this.vt.TabSelectedFontColor = System.Drawing.Color.Black;
            this.vt.TabTextHAlign = System.Drawing.StringAlignment.Near;
            this.vt.TabTextVAlign = System.Drawing.StringAlignment.Center;
            // 
            // tabAbout
            // 
            this.tabAbout.Controls.Add(this.tableAbout);
            this.tabAbout.Location = new System.Drawing.Point(204, 4);
            this.tabAbout.Name = "tabAbout";
            this.tabAbout.Padding = new System.Windows.Forms.Padding(3);
            this.tabAbout.Size = new System.Drawing.Size(822, 463);
            this.tabAbout.TabIndex = 1;
            this.tabAbout.Text = "About";
            this.tabAbout.UseVisualStyleBackColor = true;
            // 
            // tableAbout
            // 
            this.tableAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableAbout.ColumnCount = 3;
            this.tableAbout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableAbout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 158F));
            this.tableAbout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162F));
            this.tableAbout.Controls.Add(this.btnSupport, 2, 0);
            this.tableAbout.Controls.Add(this.rtfAbout, 0, 1);
            this.tableAbout.Controls.Add(this.lblAboutCaption, 0, 0);
            this.tableAbout.Controls.Add(this.btnPurchase, 1, 0);
            this.tableAbout.Location = new System.Drawing.Point(32, 33);
            this.tableAbout.Name = "tableAbout";
            this.tableAbout.RowCount = 2;
            this.tableAbout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableAbout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableAbout.Size = new System.Drawing.Size(762, 401);
            this.tableAbout.TabIndex = 9;
            // 
            // btnSupport
            // 
            this.btnSupport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSupport.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
            this.btnSupport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSupport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSupport.Location = new System.Drawing.Point(603, 3);
            this.btnSupport.Name = "btnSupport";
            this.btnSupport.Size = new System.Drawing.Size(156, 36);
            this.btnSupport.TabIndex = 10;
            this.btnSupport.Text = "Support";
            this.btnSupport.UseVisualStyleBackColor = true;
            this.btnSupport.Visible = false;
            this.btnSupport.Click += new System.EventHandler(this.btnSupport_Click);
            // 
            // rtfAbout
            // 
            this.rtfAbout.BackColor = System.Drawing.Color.WhiteSmoke;
            this.rtfAbout.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tableAbout.SetColumnSpan(this.rtfAbout, 3);
            this.rtfAbout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtfAbout.Location = new System.Drawing.Point(3, 45);
            this.rtfAbout.Name = "rtfAbout";
            this.rtfAbout.Size = new System.Drawing.Size(756, 353);
            this.rtfAbout.TabIndex = 7;
            this.rtfAbout.Text = "";
            // 
            // lblAboutCaption
            // 
            this.lblAboutCaption.AutoSize = true;
            this.lblAboutCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAboutCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAboutCaption.Location = new System.Drawing.Point(3, 0);
            this.lblAboutCaption.Name = "lblAboutCaption";
            this.lblAboutCaption.Size = new System.Drawing.Size(436, 42);
            this.lblAboutCaption.TabIndex = 8;
            this.lblAboutCaption.Text = "A Word from the Authors";
            this.lblAboutCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnPurchase
            // 
            this.btnPurchase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPurchase.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
            this.btnPurchase.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnPurchase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPurchase.Location = new System.Drawing.Point(445, 3);
            this.btnPurchase.Name = "btnPurchase";
            this.btnPurchase.Size = new System.Drawing.Size(152, 36);
            this.btnPurchase.TabIndex = 9;
            this.btnPurchase.Text = "Purchase";
            this.btnPurchase.UseVisualStyleBackColor = true;
            this.btnPurchase.Visible = false;
            this.btnPurchase.Click += new System.EventHandler(this.btnPurchase_Click);
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.botEventsGroupBox);
            this.tabSettings.Controls.Add(this.playerEventsGroupBox);
            this.tabSettings.Controls.Add(this.chatEventsGroupBox);
            this.tabSettings.Location = new System.Drawing.Point(204, 4);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabSettings.Size = new System.Drawing.Size(822, 463);
            this.tabSettings.TabIndex = 2;
            this.tabSettings.Text = "Settings";
            this.tabSettings.UseVisualStyleBackColor = true;
            // 
            // botEventsGroupBox
            // 
            this.botEventsGroupBox.Controls.Add(this.botEventsCheckedListBox);
            this.botEventsGroupBox.Location = new System.Drawing.Point(299, 6);
            this.botEventsGroupBox.Name = "botEventsGroupBox";
            this.botEventsGroupBox.Size = new System.Drawing.Size(373, 148);
            this.botEventsGroupBox.TabIndex = 2;
            this.botEventsGroupBox.TabStop = false;
            this.botEventsGroupBox.Text = "Bot Events";
            // 
            // botEventsCheckedListBox
            // 
            this.botEventsCheckedListBox.CheckOnClick = true;
            this.botEventsCheckedListBox.FormattingEnabled = true;
            this.botEventsCheckedListBox.Items.AddRange(new object[] {
            "Start",
            "Stop"});
            this.botEventsCheckedListBox.Location = new System.Drawing.Point(7, 24);
            this.botEventsCheckedListBox.Name = "botEventsCheckedListBox";
            this.botEventsCheckedListBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.botEventsCheckedListBox.Size = new System.Drawing.Size(360, 118);
            this.botEventsCheckedListBox.TabIndex = 0;
            // 
            // playerEventsGroupBox
            // 
            this.playerEventsGroupBox.Controls.Add(this.playerEventsCheckedListBox);
            this.playerEventsGroupBox.Location = new System.Drawing.Point(6, 169);
            this.playerEventsGroupBox.Name = "playerEventsGroupBox";
            this.playerEventsGroupBox.Size = new System.Drawing.Size(287, 148);
            this.playerEventsGroupBox.TabIndex = 1;
            this.playerEventsGroupBox.TabStop = false;
            this.playerEventsGroupBox.Text = "Player Events";
            // 
            // playerEventsCheckedListBox
            // 
            this.playerEventsCheckedListBox.CheckOnClick = true;
            this.playerEventsCheckedListBox.FormattingEnabled = true;
            this.playerEventsCheckedListBox.Items.AddRange(new object[] {
            "Battleground join",
            "Battleground leave",
            "Death",
            "Level up",
            "Log in",
            "Log out"});
            this.playerEventsCheckedListBox.Location = new System.Drawing.Point(7, 24);
            this.playerEventsCheckedListBox.Name = "playerEventsCheckedListBox";
            this.playerEventsCheckedListBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.playerEventsCheckedListBox.Size = new System.Drawing.Size(274, 118);
            this.playerEventsCheckedListBox.TabIndex = 0;
            // 
            // chatEventsGroupBox
            // 
            this.chatEventsGroupBox.Controls.Add(this.chatEventsCheckedListBox);
            this.chatEventsGroupBox.Location = new System.Drawing.Point(6, 6);
            this.chatEventsGroupBox.Name = "chatEventsGroupBox";
            this.chatEventsGroupBox.Size = new System.Drawing.Size(287, 148);
            this.chatEventsGroupBox.TabIndex = 0;
            this.chatEventsGroupBox.TabStop = false;
            this.chatEventsGroupBox.Text = "Chat Events";
            // 
            // chatEventsCheckedListBox
            // 
            this.chatEventsCheckedListBox.CheckOnClick = true;
            this.chatEventsCheckedListBox.FormattingEnabled = true;
            this.chatEventsCheckedListBox.Items.AddRange(new object[] {
            "Whisper",
            "Battle.net whisper"});
            this.chatEventsCheckedListBox.Location = new System.Drawing.Point(7, 24);
            this.chatEventsCheckedListBox.Name = "chatEventsCheckedListBox";
            this.chatEventsCheckedListBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.chatEventsCheckedListBox.Size = new System.Drawing.Size(274, 118);
            this.chatEventsCheckedListBox.TabIndex = 0;
            // 
            // tabAccounts
            // 
            this.tabAccounts.Controls.Add(this.pushbulletGroupBox);
            this.tabAccounts.Location = new System.Drawing.Point(204, 4);
            this.tabAccounts.Name = "tabAccounts";
            this.tabAccounts.Padding = new System.Windows.Forms.Padding(3);
            this.tabAccounts.Size = new System.Drawing.Size(822, 463);
            this.tabAccounts.TabIndex = 3;
            this.tabAccounts.Text = "Accounts";
            this.tabAccounts.UseVisualStyleBackColor = true;
            // 
            // pushbulletGroupBox
            // 
            this.pushbulletGroupBox.Controls.Add(this.pushbulletAccessTokenLabel);
            this.pushbulletGroupBox.Controls.Add(this.pushbulletTextBox);
            this.pushbulletGroupBox.Controls.Add(this.pushbulletEnabledCheckbox);
            this.pushbulletGroupBox.Location = new System.Drawing.Point(6, 6);
            this.pushbulletGroupBox.Name = "pushbulletGroupBox";
            this.pushbulletGroupBox.Size = new System.Drawing.Size(357, 114);
            this.pushbulletGroupBox.TabIndex = 0;
            this.pushbulletGroupBox.TabStop = false;
            this.pushbulletGroupBox.Text = "Pushbullet";
            // 
            // pushbulletAccessTokenLabel
            // 
            this.pushbulletAccessTokenLabel.AutoSize = true;
            this.pushbulletAccessTokenLabel.Location = new System.Drawing.Point(7, 52);
            this.pushbulletAccessTokenLabel.Name = "pushbulletAccessTokenLabel";
            this.pushbulletAccessTokenLabel.Size = new System.Drawing.Size(104, 17);
            this.pushbulletAccessTokenLabel.TabIndex = 2;
            this.pushbulletAccessTokenLabel.Text = "Access Token";
            // 
            // pushbulletTextBox
            // 
            this.pushbulletTextBox.Location = new System.Drawing.Point(7, 72);
            this.pushbulletTextBox.Name = "pushbulletTextBox";
            this.pushbulletTextBox.Size = new System.Drawing.Size(344, 24);
            this.pushbulletTextBox.TabIndex = 1;
            // 
            // pushbulletEnabledCheckbox
            // 
            this.pushbulletEnabledCheckbox.AutoSize = true;
            this.pushbulletEnabledCheckbox.Location = new System.Drawing.Point(7, 24);
            this.pushbulletEnabledCheckbox.Name = "pushbulletEnabledCheckbox";
            this.pushbulletEnabledCheckbox.Size = new System.Drawing.Size(82, 21);
            this.pushbulletEnabledCheckbox.TabIndex = 0;
            this.pushbulletEnabledCheckbox.Text = "Enabled";
            this.pushbulletEnabledCheckbox.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1030, 501);
            this.ControlBox = false;
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.vt);
            this.Controls.Add(this.FormHeaderClose);
            this.Controls.Add(this.FormHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Settings_Load);
            this.vt.ResumeLayout(false);
            this.tabAbout.ResumeLayout(false);
            this.tableAbout.ResumeLayout(false);
            this.tableAbout.PerformLayout();
            this.tabSettings.ResumeLayout(false);
            this.botEventsGroupBox.ResumeLayout(false);
            this.playerEventsGroupBox.ResumeLayout(false);
            this.chatEventsGroupBox.ResumeLayout(false);
            this.tabAccounts.ResumeLayout(false);
            this.pushbulletGroupBox.ResumeLayout(false);
            this.pushbulletGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label FormHeader;
        private System.Windows.Forms.Label FormHeaderClose;
        private VerticalTabs vt;
        private System.Windows.Forms.TabPage tabAbout;
        private System.Windows.Forms.TabPage tabSettings;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.ToolTip tooltipHandler;
        private System.Windows.Forms.TableLayoutPanel tableAbout;
        private System.Windows.Forms.Button btnSupport;
        private System.Windows.Forms.RichTextBox rtfAbout;
        private System.Windows.Forms.Label lblAboutCaption;
        private System.Windows.Forms.Button btnPurchase;
        private System.Windows.Forms.TabPage tabAccounts;
        private System.Windows.Forms.GroupBox pushbulletGroupBox;
        private System.Windows.Forms.Label pushbulletAccessTokenLabel;
        private System.Windows.Forms.TextBox pushbulletTextBox;
        private System.Windows.Forms.CheckBox pushbulletEnabledCheckbox;
        private System.Windows.Forms.GroupBox botEventsGroupBox;
        private System.Windows.Forms.CheckedListBox botEventsCheckedListBox;
        private System.Windows.Forms.GroupBox playerEventsGroupBox;
        private System.Windows.Forms.CheckedListBox playerEventsCheckedListBox;
        private System.Windows.Forms.GroupBox chatEventsGroupBox;
        private System.Windows.Forms.CheckedListBox chatEventsCheckedListBox;
    }
}