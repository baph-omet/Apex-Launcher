namespace ApexLauncher {
    partial class Launcher {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Launcher));
            LaunchButton = new System.Windows.Forms.Button();
            SettingsButton = new System.Windows.Forms.Button();
            DiscordLogo = new System.Windows.Forms.PictureBox();
            TabBox = new System.Windows.Forms.TabControl();
            UpdatesPage = new System.Windows.Forms.TabPage();
            NoConnectionLabel = new System.Windows.Forms.Label();
            TumblrBrowser = new System.Windows.Forms.WebBrowser();
            WikiPage = new System.Windows.Forms.TabPage();
            WikiBrowser = new System.Windows.Forms.WebBrowser();
            ForumLogo = new System.Windows.Forms.PictureBox();
            SaveMgmtButton = new System.Windows.Forms.Button();
            StatusLabel = new System.Windows.Forms.Label();
            LauncherVersionLabel = new System.Windows.Forms.Label();
            GameVersionLabel = new System.Windows.Forms.Label();
            LauncherTooltip = new System.Windows.Forms.ToolTip(components);
            WikiLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)DiscordLogo).BeginInit();
            TabBox.SuspendLayout();
            UpdatesPage.SuspendLayout();
            WikiPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ForumLogo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)WikiLogo).BeginInit();
            SuspendLayout();
            // 
            // LaunchButton
            // 
            LaunchButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            LaunchButton.Enabled = false;
            LaunchButton.Font = new System.Drawing.Font("Lucida Console", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            LaunchButton.Location = new System.Drawing.Point(1211, 595);
            LaunchButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            LaunchButton.Name = "LaunchButton";
            LaunchButton.Size = new System.Drawing.Size(344, 141);
            LaunchButton.TabIndex = 0;
            LaunchButton.Text = "Launch Game";
            LauncherTooltip.SetToolTip(LaunchButton, "Launch the game!");
            LaunchButton.UseVisualStyleBackColor = true;
            LaunchButton.Click += LaunchButton_Click;
            // 
            // SettingsButton
            // 
            SettingsButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            SettingsButton.Font = new System.Drawing.Font("Lucida Console", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            SettingsButton.Location = new System.Drawing.Point(909, 595);
            SettingsButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            SettingsButton.Name = "SettingsButton";
            SettingsButton.Size = new System.Drawing.Size(295, 67);
            SettingsButton.TabIndex = 2;
            SettingsButton.Text = "Settings";
            LauncherTooltip.SetToolTip(SettingsButton, "Change launcher settings here.");
            SettingsButton.UseVisualStyleBackColor = true;
            SettingsButton.Click += SettingsButton_Click;
            // 
            // DiscordLogo
            // 
            DiscordLogo.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            DiscordLogo.BackgroundImage = (System.Drawing.Image)resources.GetObject("DiscordLogo.BackgroundImage");
            DiscordLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            DiscordLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            DiscordLogo.Location = new System.Drawing.Point(14, 595);
            DiscordLogo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            DiscordLogo.Name = "DiscordLogo";
            DiscordLogo.Size = new System.Drawing.Size(292, 141);
            DiscordLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            DiscordLogo.TabIndex = 3;
            DiscordLogo.TabStop = false;
            LauncherTooltip.SetToolTip(DiscordLogo, "Join our Discord server to chat with the community!");
            DiscordLogo.Click += DiscordLogo_Click;
            // 
            // TabBox
            // 
            TabBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            TabBox.Controls.Add(UpdatesPage);
            TabBox.Controls.Add(WikiPage);
            TabBox.Location = new System.Drawing.Point(14, 14);
            TabBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            TabBox.Name = "TabBox";
            TabBox.SelectedIndex = 0;
            TabBox.Size = new System.Drawing.Size(1541, 552);
            TabBox.TabIndex = 4;
            // 
            // UpdatesPage
            // 
            UpdatesPage.Controls.Add(NoConnectionLabel);
            UpdatesPage.Controls.Add(TumblrBrowser);
            UpdatesPage.Location = new System.Drawing.Point(4, 24);
            UpdatesPage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            UpdatesPage.Name = "UpdatesPage";
            UpdatesPage.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            UpdatesPage.Size = new System.Drawing.Size(1533, 524);
            UpdatesPage.TabIndex = 0;
            UpdatesPage.Text = "Game Updates";
            UpdatesPage.UseVisualStyleBackColor = true;
            // 
            // NoConnectionLabel
            // 
            NoConnectionLabel.AutoSize = true;
            NoConnectionLabel.Location = new System.Drawing.Point(19, 22);
            NoConnectionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            NoConnectionLabel.Name = "NoConnectionLabel";
            NoConnectionLabel.Size = new System.Drawing.Size(318, 15);
            NoConnectionLabel.TabIndex = 1;
            NoConnectionLabel.Text = "Could not connect to the internet. Can't check for updates.";
            // 
            // TumblrBrowser
            // 
            TumblrBrowser.AllowWebBrowserDrop = false;
            TumblrBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            TumblrBrowser.Location = new System.Drawing.Point(4, 3);
            TumblrBrowser.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            TumblrBrowser.MinimumSize = new System.Drawing.Size(23, 23);
            TumblrBrowser.Name = "TumblrBrowser";
            TumblrBrowser.ScriptErrorsSuppressed = true;
            TumblrBrowser.Size = new System.Drawing.Size(1525, 518);
            TumblrBrowser.TabIndex = 0;
            TumblrBrowser.Url = new System.Uri("https://apex.baph.xyz/#recentposts", System.UriKind.Absolute);
            TumblrBrowser.DocumentCompleted += BrowserLoaded;
            TumblrBrowser.Navigating += BrowserNavigation;
            // 
            // WikiPage
            // 
            WikiPage.Controls.Add(WikiBrowser);
            WikiPage.Location = new System.Drawing.Point(4, 24);
            WikiPage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            WikiPage.Name = "WikiPage";
            WikiPage.Size = new System.Drawing.Size(1533, 524);
            WikiPage.TabIndex = 3;
            WikiPage.Text = "Wiki";
            WikiPage.UseVisualStyleBackColor = true;
            // 
            // WikiBrowser
            // 
            WikiBrowser.AllowWebBrowserDrop = false;
            WikiBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            WikiBrowser.IsWebBrowserContextMenuEnabled = false;
            WikiBrowser.Location = new System.Drawing.Point(0, 0);
            WikiBrowser.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            WikiBrowser.MinimumSize = new System.Drawing.Size(23, 23);
            WikiBrowser.Name = "WikiBrowser";
            WikiBrowser.ScriptErrorsSuppressed = true;
            WikiBrowser.Size = new System.Drawing.Size(1533, 524);
            WikiBrowser.TabIndex = 1;
            WikiBrowser.Url = new System.Uri("https://pokemonapex.fandom.com/wiki/Pok%C3%A9mon_Apex_Wikia", System.UriKind.Absolute);
            // 
            // ForumLogo
            // 
            ForumLogo.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            ForumLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            ForumLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            ForumLogo.Image = (System.Drawing.Image)resources.GetObject("ForumLogo.Image");
            ForumLogo.Location = new System.Drawing.Point(313, 595);
            ForumLogo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ForumLogo.Name = "ForumLogo";
            ForumLogo.Size = new System.Drawing.Size(290, 141);
            ForumLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            ForumLogo.TabIndex = 6;
            ForumLogo.TabStop = false;
            LauncherTooltip.SetToolTip(ForumLogo, "Baphomet Media");
            ForumLogo.Click += PictureBox1_Click;
            // 
            // SaveMgmtButton
            // 
            SaveMgmtButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            SaveMgmtButton.Font = new System.Drawing.Font("Lucida Console", 14.25F, System.Drawing.FontStyle.Bold);
            SaveMgmtButton.Location = new System.Drawing.Point(909, 669);
            SaveMgmtButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            SaveMgmtButton.Name = "SaveMgmtButton";
            SaveMgmtButton.Size = new System.Drawing.Size(295, 67);
            SaveMgmtButton.TabIndex = 7;
            SaveMgmtButton.Text = "Manage Saves";
            LauncherTooltip.SetToolTip(SaveMgmtButton, "Manage game save files.");
            SaveMgmtButton.UseVisualStyleBackColor = true;
            SaveMgmtButton.Click += SaveMgmtButton_Click;
            // 
            // StatusLabel
            // 
            StatusLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            StatusLabel.AutoSize = true;
            StatusLabel.Font = new System.Drawing.Font("Lucida Console", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            StatusLabel.ForeColor = System.Drawing.Color.White;
            StatusLabel.Location = new System.Drawing.Point(14, 742);
            StatusLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            StatusLabel.Name = "StatusLabel";
            StatusLabel.Size = new System.Drawing.Size(174, 19);
            StatusLabel.TabIndex = 8;
            StatusLabel.Text = "Ready to Launch";
            StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LauncherVersionLabel
            // 
            LauncherVersionLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            LauncherVersionLabel.AutoSize = true;
            LauncherVersionLabel.ForeColor = System.Drawing.Color.White;
            LauncherVersionLabel.Location = new System.Drawing.Point(1456, 577);
            LauncherVersionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            LauncherVersionLabel.Name = "LauncherVersionLabel";
            LauncherVersionLabel.Size = new System.Drawing.Size(89, 15);
            LauncherVersionLabel.TabIndex = 9;
            LauncherVersionLabel.Text = "Launcher v1.4.0";
            LauncherTooltip.SetToolTip(LauncherVersionLabel, "Your current launcher version.");
            // 
            // GameVersionLabel
            // 
            GameVersionLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            GameVersionLabel.AutoSize = true;
            GameVersionLabel.Font = new System.Drawing.Font("Lucida Console", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            GameVersionLabel.ForeColor = System.Drawing.Color.White;
            GameVersionLabel.Location = new System.Drawing.Point(1275, 745);
            GameVersionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            GameVersionLabel.Name = "GameVersionLabel";
            GameVersionLabel.Size = new System.Drawing.Size(240, 19);
            GameVersionLabel.TabIndex = 10;
            GameVersionLabel.Text = "Loading Build Info...";
            GameVersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            LauncherTooltip.SetToolTip(GameVersionLabel, "Your currently installed game version. Updated automatically.");
            // 
            // WikiLogo
            // 
            WikiLogo.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            WikiLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            WikiLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            WikiLogo.Image = (System.Drawing.Image)resources.GetObject("WikiLogo.Image");
            WikiLogo.Location = new System.Drawing.Point(610, 595);
            WikiLogo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            WikiLogo.Name = "WikiLogo";
            WikiLogo.Size = new System.Drawing.Size(292, 141);
            WikiLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            WikiLogo.TabIndex = 11;
            WikiLogo.TabStop = false;
            LauncherTooltip.SetToolTip(WikiLogo, "http://pokemonapex.wikia.com");
            WikiLogo.Click += WikiLogo_Click;
            // 
            // Launcher
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
            ClientSize = new System.Drawing.Size(1569, 778);
            Controls.Add(WikiLogo);
            Controls.Add(GameVersionLabel);
            Controls.Add(LauncherVersionLabel);
            Controls.Add(StatusLabel);
            Controls.Add(SaveMgmtButton);
            Controls.Add(ForumLogo);
            Controls.Add(TabBox);
            Controls.Add(DiscordLogo);
            Controls.Add(SettingsButton);
            Controls.Add(LaunchButton);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MinimumSize = new System.Drawing.Size(1585, 222);
            Name = "Launcher";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Apex Launcher";
            FormClosing += Launcher_FormClosing;
            Shown += Launcher_Shown;
            ((System.ComponentModel.ISupportInitialize)DiscordLogo).EndInit();
            TabBox.ResumeLayout(false);
            UpdatesPage.ResumeLayout(false);
            UpdatesPage.PerformLayout();
            WikiPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ForumLogo).EndInit();
            ((System.ComponentModel.ISupportInitialize)WikiLogo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button LaunchButton;
        private System.Windows.Forms.Button SettingsButton;
        private System.Windows.Forms.PictureBox DiscordLogo;
        private System.Windows.Forms.TabControl TabBox;
        private System.Windows.Forms.TabPage UpdatesPage;
        private System.Windows.Forms.PictureBox ForumLogo;
        private System.Windows.Forms.Button SaveMgmtButton;
        private System.Windows.Forms.WebBrowser TumblrBrowser;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.Label LauncherVersionLabel;
        private System.Windows.Forms.Label NoConnectionLabel;
        public System.Windows.Forms.Label GameVersionLabel;
        private System.Windows.Forms.ToolTip LauncherTooltip;
        private System.Windows.Forms.PictureBox WikiLogo;
        private System.Windows.Forms.TabPage WikiPage;
        private System.Windows.Forms.WebBrowser WikiBrowser;
    }
}

