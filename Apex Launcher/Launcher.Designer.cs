namespace Apex_Launcher {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Launcher));
            this.LaunchButton = new System.Windows.Forms.Button();
            this.SettingsButton = new System.Windows.Forms.Button();
            this.DiscordLogo = new System.Windows.Forms.PictureBox();
            this.TabBox = new System.Windows.Forms.TabControl();
            this.UpdatesPage = new System.Windows.Forms.TabPage();
            this.TumblrBrowser = new System.Windows.Forms.WebBrowser();
            this.CommunityPage = new System.Windows.Forms.TabPage();
            this.RedditBrowser = new System.Windows.Forms.WebBrowser();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.SaveMgmtButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DiscordLogo)).BeginInit();
            this.TabBox.SuspendLayout();
            this.UpdatesPage.SuspendLayout();
            this.CommunityPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // LaunchButton
            // 
            this.LaunchButton.Enabled = false;
            this.LaunchButton.Font = new System.Drawing.Font("Lucida Console", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LaunchButton.Location = new System.Drawing.Point(1039, 536);
            this.LaunchButton.Name = "LaunchButton";
            this.LaunchButton.Size = new System.Drawing.Size(295, 122);
            this.LaunchButton.TabIndex = 0;
            this.LaunchButton.Text = "Launch Game";
            this.LaunchButton.UseVisualStyleBackColor = true;
            this.LaunchButton.Click += new System.EventHandler(this.LaunchButton_Click);
            // 
            // SettingsButton
            // 
            this.SettingsButton.Font = new System.Drawing.Font("Lucida Console", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsButton.Location = new System.Drawing.Point(524, 536);
            this.SettingsButton.Name = "SettingsButton";
            this.SettingsButton.Size = new System.Drawing.Size(253, 122);
            this.SettingsButton.TabIndex = 2;
            this.SettingsButton.Text = "Settings";
            this.SettingsButton.UseVisualStyleBackColor = true;
            // 
            // DiscordLogo
            // 
            this.DiscordLogo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("DiscordLogo.BackgroundImage")));
            this.DiscordLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DiscordLogo.Location = new System.Drawing.Point(12, 536);
            this.DiscordLogo.Name = "DiscordLogo";
            this.DiscordLogo.Size = new System.Drawing.Size(250, 122);
            this.DiscordLogo.TabIndex = 3;
            this.DiscordLogo.TabStop = false;
            this.DiscordLogo.Click += new System.EventHandler(this.DiscordLogo_Click);
            // 
            // TabBox
            // 
            this.TabBox.Controls.Add(this.UpdatesPage);
            this.TabBox.Controls.Add(this.CommunityPage);
            this.TabBox.Location = new System.Drawing.Point(13, 13);
            this.TabBox.Name = "TabBox";
            this.TabBox.SelectedIndex = 0;
            this.TabBox.Size = new System.Drawing.Size(1321, 486);
            this.TabBox.TabIndex = 4;
            // 
            // UpdatesPage
            // 
            this.UpdatesPage.Controls.Add(this.TumblrBrowser);
            this.UpdatesPage.Location = new System.Drawing.Point(4, 22);
            this.UpdatesPage.Name = "UpdatesPage";
            this.UpdatesPage.Padding = new System.Windows.Forms.Padding(3);
            this.UpdatesPage.Size = new System.Drawing.Size(1313, 460);
            this.UpdatesPage.TabIndex = 0;
            this.UpdatesPage.Text = "Game Updates";
            this.UpdatesPage.UseVisualStyleBackColor = true;
            // 
            // TumblrBrowser
            // 
            this.TumblrBrowser.AllowWebBrowserDrop = false;
            this.TumblrBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TumblrBrowser.Location = new System.Drawing.Point(3, 3);
            this.TumblrBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.TumblrBrowser.Name = "TumblrBrowser";
            this.TumblrBrowser.ScriptErrorsSuppressed = true;
            this.TumblrBrowser.Size = new System.Drawing.Size(1307, 454);
            this.TumblrBrowser.TabIndex = 0;
            this.TumblrBrowser.Url = new System.Uri("https://pokemonapex.tumblr.com", System.UriKind.Absolute);
            this.TumblrBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.BrowserLoaded);
            this.TumblrBrowser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.BrowserNavigation);
            // 
            // CommunityPage
            // 
            this.CommunityPage.Controls.Add(this.RedditBrowser);
            this.CommunityPage.Location = new System.Drawing.Point(4, 22);
            this.CommunityPage.Name = "CommunityPage";
            this.CommunityPage.Padding = new System.Windows.Forms.Padding(3);
            this.CommunityPage.Size = new System.Drawing.Size(1130, 460);
            this.CommunityPage.TabIndex = 1;
            this.CommunityPage.Text = "Community Posts";
            this.CommunityPage.UseVisualStyleBackColor = true;
            // 
            // RedditBrowser
            // 
            this.RedditBrowser.AllowWebBrowserDrop = false;
            this.RedditBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RedditBrowser.Location = new System.Drawing.Point(3, 3);
            this.RedditBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.RedditBrowser.Name = "RedditBrowser";
            this.RedditBrowser.ScriptErrorsSuppressed = true;
            this.RedditBrowser.Size = new System.Drawing.Size(1124, 454);
            this.RedditBrowser.TabIndex = 0;
            this.RedditBrowser.Url = new System.Uri("https://reddit.com/r/PokemonApex", System.UriKind.Absolute);
            this.RedditBrowser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.BrowserNavigation);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 508);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1322, 22);
            this.progressBar1.TabIndex = 5;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(268, 536);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(250, 122);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // SaveMgmtButton
            // 
            this.SaveMgmtButton.Enabled = false;
            this.SaveMgmtButton.Font = new System.Drawing.Font("Lucida Console", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveMgmtButton.Location = new System.Drawing.Point(783, 536);
            this.SaveMgmtButton.Name = "SaveMgmtButton";
            this.SaveMgmtButton.Size = new System.Drawing.Size(250, 122);
            this.SaveMgmtButton.TabIndex = 7;
            this.SaveMgmtButton.Text = "Manage Saves";
            this.SaveMgmtButton.UseVisualStyleBackColor = true;
            // 
            // Launcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1346, 669);
            this.Controls.Add(this.SaveMgmtButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.TabBox);
            this.Controls.Add(this.DiscordLogo);
            this.Controls.Add(this.SettingsButton);
            this.Controls.Add(this.LaunchButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Launcher";
            this.Text = "Pokémon Apex Launcher";
            ((System.ComponentModel.ISupportInitialize)(this.DiscordLogo)).EndInit();
            this.TabBox.ResumeLayout(false);
            this.UpdatesPage.ResumeLayout(false);
            this.CommunityPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button LaunchButton;
        private System.Windows.Forms.Button SettingsButton;
        private System.Windows.Forms.PictureBox DiscordLogo;
        private System.Windows.Forms.TabControl TabBox;
        private System.Windows.Forms.TabPage UpdatesPage;
        private System.Windows.Forms.TabPage CommunityPage;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button SaveMgmtButton;
        private System.Windows.Forms.WebBrowser TumblrBrowser;
        private System.Windows.Forms.WebBrowser RedditBrowser;
    }
}

