﻿namespace ApexLauncher {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Launcher));
            this.LaunchButton = new System.Windows.Forms.Button();
            this.SettingsButton = new System.Windows.Forms.Button();
            this.DiscordLogo = new System.Windows.Forms.PictureBox();
            this.TabBox = new System.Windows.Forms.TabControl();
            this.UpdatesPage = new System.Windows.Forms.TabPage();
            this.NoConnectionLabel = new System.Windows.Forms.Label();
            this.TumblrBrowser = new System.Windows.Forms.WebBrowser();
            this.ForumPage = new System.Windows.Forms.TabPage();
            this.ForumBrowser = new System.Windows.Forms.WebBrowser();
            this.ForumLogo = new System.Windows.Forms.PictureBox();
            this.SaveMgmtButton = new System.Windows.Forms.Button();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.LauncherVersionLabel = new System.Windows.Forms.Label();
            this.GameVersionLabel = new System.Windows.Forms.Label();
            this.LauncherTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.WikiLogo = new System.Windows.Forms.PictureBox();
            this.WikiPage = new System.Windows.Forms.TabPage();
            this.WikiBrowser = new System.Windows.Forms.WebBrowser();
            ((System.ComponentModel.ISupportInitialize)(this.DiscordLogo)).BeginInit();
            this.TabBox.SuspendLayout();
            this.UpdatesPage.SuspendLayout();
            this.ForumPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ForumLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WikiLogo)).BeginInit();
            this.WikiPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // LaunchButton
            // 
            this.LaunchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LaunchButton.Enabled = false;
            this.LaunchButton.Font = new System.Drawing.Font("Lucida Console", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LaunchButton.Location = new System.Drawing.Point(1038, 516);
            this.LaunchButton.Name = "LaunchButton";
            this.LaunchButton.Size = new System.Drawing.Size(295, 122);
            this.LaunchButton.TabIndex = 0;
            this.LaunchButton.Text = "Launch Game";
            this.LauncherTooltip.SetToolTip(this.LaunchButton, "Launch the game!");
            this.LaunchButton.UseVisualStyleBackColor = true;
            this.LaunchButton.Click += new System.EventHandler(this.LaunchButton_Click);
            // 
            // SettingsButton
            // 
            this.SettingsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SettingsButton.Font = new System.Drawing.Font("Lucida Console", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsButton.Location = new System.Drawing.Point(779, 516);
            this.SettingsButton.Name = "SettingsButton";
            this.SettingsButton.Size = new System.Drawing.Size(253, 58);
            this.SettingsButton.TabIndex = 2;
            this.SettingsButton.Text = "Settings";
            this.LauncherTooltip.SetToolTip(this.SettingsButton, "Change launcher settings here.");
            this.SettingsButton.UseVisualStyleBackColor = true;
            this.SettingsButton.Click += new System.EventHandler(this.SettingsButton_Click);
            // 
            // DiscordLogo
            // 
            this.DiscordLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DiscordLogo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("DiscordLogo.BackgroundImage")));
            this.DiscordLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DiscordLogo.Location = new System.Drawing.Point(12, 516);
            this.DiscordLogo.Name = "DiscordLogo";
            this.DiscordLogo.Size = new System.Drawing.Size(250, 122);
            this.DiscordLogo.TabIndex = 3;
            this.DiscordLogo.TabStop = false;
            this.LauncherTooltip.SetToolTip(this.DiscordLogo, "Join our Discord server to chat with the community!");
            this.DiscordLogo.Click += new System.EventHandler(this.DiscordLogo_Click);
            // 
            // TabBox
            // 
            this.TabBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabBox.Controls.Add(this.UpdatesPage);
            this.TabBox.Controls.Add(this.ForumPage);
            this.TabBox.Controls.Add(this.WikiPage);
            this.TabBox.Location = new System.Drawing.Point(12, 12);
            this.TabBox.Name = "TabBox";
            this.TabBox.SelectedIndex = 0;
            this.TabBox.Size = new System.Drawing.Size(1321, 478);
            this.TabBox.TabIndex = 4;
            // 
            // UpdatesPage
            // 
            this.UpdatesPage.Controls.Add(this.NoConnectionLabel);
            this.UpdatesPage.Controls.Add(this.TumblrBrowser);
            this.UpdatesPage.Location = new System.Drawing.Point(4, 22);
            this.UpdatesPage.Name = "UpdatesPage";
            this.UpdatesPage.Padding = new System.Windows.Forms.Padding(3);
            this.UpdatesPage.Size = new System.Drawing.Size(1313, 452);
            this.UpdatesPage.TabIndex = 0;
            this.UpdatesPage.Text = "Game Updates";
            this.UpdatesPage.UseVisualStyleBackColor = true;
            // 
            // NoConnectionLabel
            // 
            this.NoConnectionLabel.AutoSize = true;
            this.NoConnectionLabel.Location = new System.Drawing.Point(16, 19);
            this.NoConnectionLabel.Name = "NoConnectionLabel";
            this.NoConnectionLabel.Size = new System.Drawing.Size(284, 13);
            this.NoConnectionLabel.TabIndex = 1;
            this.NoConnectionLabel.Text = "Could not connect to the internet. Can\'t check for updates.";
            // 
            // TumblrBrowser
            // 
            this.TumblrBrowser.AllowWebBrowserDrop = false;
            this.TumblrBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TumblrBrowser.Location = new System.Drawing.Point(3, 3);
            this.TumblrBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.TumblrBrowser.Name = "TumblrBrowser";
            this.TumblrBrowser.ScriptErrorsSuppressed = true;
            this.TumblrBrowser.Size = new System.Drawing.Size(1307, 446);
            this.TumblrBrowser.TabIndex = 0;
            this.TumblrBrowser.Url = new System.Uri("https://apex.baph.xyz#recentposts", System.UriKind.Absolute);
            this.TumblrBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.BrowserLoaded);
            this.TumblrBrowser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.BrowserNavigation);
            // 
            // ForumPage
            // 
            this.ForumPage.Controls.Add(this.ForumBrowser);
            this.ForumPage.Location = new System.Drawing.Point(4, 22);
            this.ForumPage.Name = "ForumPage";
            this.ForumPage.Size = new System.Drawing.Size(1313, 452);
            this.ForumPage.TabIndex = 2;
            this.ForumPage.Text = "Forum";
            this.ForumPage.UseVisualStyleBackColor = true;
            // 
            // ForumBrowser
            // 
            this.ForumBrowser.AllowWebBrowserDrop = false;
            this.ForumBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ForumBrowser.Location = new System.Drawing.Point(0, 0);
            this.ForumBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.ForumBrowser.Name = "ForumBrowser";
            this.ForumBrowser.ScriptErrorsSuppressed = true;
            this.ForumBrowser.Size = new System.Drawing.Size(1313, 452);
            this.ForumBrowser.TabIndex = 2;
            this.ForumBrowser.Url = new System.Uri("https://forum.baph.xyz", System.UriKind.Absolute);
            // 
            // ForumLogo
            // 
            this.ForumLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ForumLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ForumLogo.Image = ((System.Drawing.Image)(resources.GetObject("ForumLogo.Image")));
            this.ForumLogo.Location = new System.Drawing.Point(268, 516);
            this.ForumLogo.Name = "ForumLogo";
            this.ForumLogo.Size = new System.Drawing.Size(250, 122);
            this.ForumLogo.TabIndex = 6;
            this.ForumLogo.TabStop = false;
            this.LauncherTooltip.SetToolTip(this.ForumLogo, "https://forum.iamvishnu.net");
            this.ForumLogo.Click += new System.EventHandler(this.PictureBox1_Click);
            // 
            // SaveMgmtButton
            // 
            this.SaveMgmtButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveMgmtButton.Font = new System.Drawing.Font("Lucida Console", 14.25F, System.Drawing.FontStyle.Bold);
            this.SaveMgmtButton.Location = new System.Drawing.Point(779, 580);
            this.SaveMgmtButton.Name = "SaveMgmtButton";
            this.SaveMgmtButton.Size = new System.Drawing.Size(253, 58);
            this.SaveMgmtButton.TabIndex = 7;
            this.SaveMgmtButton.Text = "Manage Saves";
            this.LauncherTooltip.SetToolTip(this.SaveMgmtButton, "Manage game save files.");
            this.SaveMgmtButton.UseVisualStyleBackColor = true;
            this.SaveMgmtButton.Click += new System.EventHandler(this.SaveMgmtButton_Click);
            // 
            // StatusLabel
            // 
            this.StatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Font = new System.Drawing.Font("Lucida Console", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusLabel.ForeColor = System.Drawing.Color.White;
            this.StatusLabel.Location = new System.Drawing.Point(12, 643);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(174, 19);
            this.StatusLabel.TabIndex = 8;
            this.StatusLabel.Text = "Ready to Launch";
            this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LauncherVersionLabel
            // 
            this.LauncherVersionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LauncherVersionLabel.AutoSize = true;
            this.LauncherVersionLabel.ForeColor = System.Drawing.Color.White;
            this.LauncherVersionLabel.Location = new System.Drawing.Point(1248, 500);
            this.LauncherVersionLabel.Name = "LauncherVersionLabel";
            this.LauncherVersionLabel.Size = new System.Drawing.Size(85, 13);
            this.LauncherVersionLabel.TabIndex = 9;
            this.LauncherVersionLabel.Text = "Launcher v1.4.0";
            this.LauncherTooltip.SetToolTip(this.LauncherVersionLabel, "Your current launcher version.");
            // 
            // GameVersionLabel
            // 
            this.GameVersionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.GameVersionLabel.AutoSize = true;
            this.GameVersionLabel.Font = new System.Drawing.Font("Lucida Console", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GameVersionLabel.ForeColor = System.Drawing.Color.White;
            this.GameVersionLabel.Location = new System.Drawing.Point(1093, 646);
            this.GameVersionLabel.Name = "GameVersionLabel";
            this.GameVersionLabel.Size = new System.Drawing.Size(240, 19);
            this.GameVersionLabel.TabIndex = 10;
            this.GameVersionLabel.Text = "Loading Build Info...";
            this.GameVersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.LauncherTooltip.SetToolTip(this.GameVersionLabel, "Your currently installed game version. Updated automatically.");
            // 
            // WikiLogo
            // 
            this.WikiLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.WikiLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.WikiLogo.Image = ((System.Drawing.Image)(resources.GetObject("WikiLogo.Image")));
            this.WikiLogo.Location = new System.Drawing.Point(523, 516);
            this.WikiLogo.Name = "WikiLogo";
            this.WikiLogo.Size = new System.Drawing.Size(250, 122);
            this.WikiLogo.TabIndex = 11;
            this.WikiLogo.TabStop = false;
            this.LauncherTooltip.SetToolTip(this.WikiLogo, "http://pokemonapex.wikia.com");
            this.WikiLogo.Click += new System.EventHandler(this.WikiLogo_Click);
            // 
            // WikiPage
            // 
            this.WikiPage.Controls.Add(this.WikiBrowser);
            this.WikiPage.Location = new System.Drawing.Point(4, 22);
            this.WikiPage.Name = "WikiPage";
            this.WikiPage.Size = new System.Drawing.Size(1313, 452);
            this.WikiPage.TabIndex = 3;
            this.WikiPage.Text = "Wiki";
            this.WikiPage.UseVisualStyleBackColor = true;
            // 
            // WikiBrowser
            // 
            this.WikiBrowser.AllowWebBrowserDrop = false;
            this.WikiBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WikiBrowser.Location = new System.Drawing.Point(0, 0);
            this.WikiBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.WikiBrowser.Name = "WikiBrowser";
            this.WikiBrowser.ScriptErrorsSuppressed = true;
            this.WikiBrowser.Size = new System.Drawing.Size(1313, 452);
            this.WikiBrowser.TabIndex = 1;
            this.WikiBrowser.Url = new System.Uri("https://pokemonapex.fandom.com/wiki/Pok%C3%A9mon_Apex_Wikia", System.UriKind.Absolute);
            // 
            // Launcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1345, 674);
            this.Controls.Add(this.WikiLogo);
            this.Controls.Add(this.GameVersionLabel);
            this.Controls.Add(this.LauncherVersionLabel);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.SaveMgmtButton);
            this.Controls.Add(this.ForumLogo);
            this.Controls.Add(this.TabBox);
            this.Controls.Add(this.DiscordLogo);
            this.Controls.Add(this.SettingsButton);
            this.Controls.Add(this.LaunchButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1361, 198);
            this.Name = "Launcher";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Apex Launcher";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Launcher_FormClosing);
            this.Shown += new System.EventHandler(this.Launcher_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.DiscordLogo)).EndInit();
            this.TabBox.ResumeLayout(false);
            this.UpdatesPage.ResumeLayout(false);
            this.UpdatesPage.PerformLayout();
            this.ForumPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ForumLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WikiLogo)).EndInit();
            this.WikiPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.TabPage ForumPage;
        private System.Windows.Forms.WebBrowser ForumBrowser;
        private System.Windows.Forms.TabPage WikiPage;
        private System.Windows.Forms.WebBrowser WikiBrowser;
    }
}

