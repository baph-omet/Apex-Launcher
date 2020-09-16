// <copyright file="Launcher.cs" company="IAMVISHNU Media">
// © Copyright by IAMVISHNU Media 2020 CC BY-NC-ND
// </copyright>

using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace ApexLauncher {
    /// <summary>
    /// Main form for program.
    /// </summary>
    public partial class Launcher : Form {
        private bool loaded;

        /// <summary>
        /// Initializes a new instance of the <see cref="Launcher"/> class.
        /// </summary>
        public Launcher() {
            InitializeComponent();
            if (!Program.NetworkConnected) {
                TumblrBrowser.Hide();
                RedditBrowser.Hide();
                ForumBrowser.Hide();
                TabBox.Enabled = false;
            } else {
                NoConnectionLabel.Hide();
            }
        }

        private delegate void SGV(VersionGameFiles v);

        private delegate void US(string message);

        /// <summary>
        /// Enable launching the game.
        /// </summary>
        public void EnableLaunch() {
            LaunchButton.Enabled = true;
            UpdateStatus("Ready to launch");
        }

        /// <summary>
        /// Update the launcher status message.
        /// </summary>
        /// <param name="message">The message to set.</param>
        public void UpdateStatus(string message) {
            if (StatusLabel.InvokeRequired) {
                US d = UpdateStatus;
                Invoke(d, new object[] { message });
            } else StatusLabel.Text = message;
        }

        /// <summary>
        /// Sets the current game version text.
        /// </summary>
        /// <param name="v">Version to set.</param>
        public void SetGameVersion(VersionGameFiles v) {
            if (v is null) throw new ArgumentNullException(nameof(v));
            if (GameVersionLabel.InvokeRequired) {
                SGV d = SetGameVersion;
                Invoke(d, new object[] { v });
            } else GameVersionLabel.Text = "Build: " + v.ToString();
        }

        /// <summary>
        /// Sets the text for the launcher version.
        /// </summary>
        /// <param name="versionText">The text to set.</param>
        public void SetLauncherVersion(string versionText) {
            LauncherVersionLabel.Text = "Launcher v" + versionText;
        }

        private void SettingsButton_Click(object sender, EventArgs e) {
            using SettingsForm settings = new SettingsForm();
            settings.ShowDialog();
        }

        private void Launcher_FormClosing(object sender, FormClosingEventArgs e) {
            if (Program.Downloading) {
                DialogResult res = MessageBox.Show(
                    "A download is still in progress. Closing the launcher will cancel your download. Are you sure you want to quit?",
                    "Warning",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                if (res == DialogResult.No) e.Cancel = true;
            }
        }

        private void SaveMgmtButton_Click(object sender, EventArgs e) {
            using SaveManagementForm saves = new SaveManagementForm();
            saves.ShowDialog();
        }

        private void Launcher_Shown(object sender, EventArgs e) {
            TumblrBrowser.IsWebBrowserContextMenuEnabled = false;
            RedditBrowser.IsWebBrowserContextMenuEnabled = false;
            ForumBrowser.IsWebBrowserContextMenuEnabled = false;
            VersionGameFiles vgf = Config.CurrentVersion;
            if (vgf != null) SetGameVersion(vgf);
            LauncherVersionLabel.Text = "Launcher v" + Program.GetLauncherVersion();
            if (Program.NetworkConnected) {
                try {
                    Program.InstallLatestVersion();
                } catch (WebException) {
                    Program.NetworkConnected = false;
                }
            }

            EnableLaunch();
        }

        private void DiscordLogo_Click(object sender, EventArgs e) {
            Process.Start("https://apex.iamvishnu.net/discord");
        }

        private void PictureBox1_Click(object sender, EventArgs e) {
            Process.Start("https://forum.iamvishnu.net");
        }

        private void WikiLogo_Click(object sender, EventArgs e) {
            Process.Start("http://pokemonapex.wikia.com/wiki/Pok%C3%A9mon_Apex_Wikia");
        }

        private void BrowserNavigation(object sender, WebBrowserNavigatingEventArgs e) {
            if (loaded && !e.Url.Equals(((WebBrowser)sender).Url) && !e.Url.ToString().Contains("redditmedia") && e.Url.ToString().Contains(((WebBrowser)sender).Url.ToString())) {
                Process.Start(e.Url.ToString());
                e.Cancel = true;
            } else if (loaded) {
                e.Cancel = true;
            }
        }

        private void BrowserLoaded(object sender, WebBrowserDocumentCompletedEventArgs e) {
            loaded = true;
        }

        private void LaunchButton_Click(object sender, EventArgs e) {
            if (Program.Downloading) {
                MessageBox.Show("A download is currently in progress. Please cancel your download or wait until it finishes.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            VersionGameFiles currentVersion = Config.CurrentVersion;
            if (currentVersion is null) {
                if (MessageBox.Show("No installed version found. Would you like to download now?", "No Version Found", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

                Program.InstallLatestVersion();
                return;
            }

            string launchpath = Path.Combine(Config.InstallPath, "Versions", currentVersion.ToString(), "Game.exe");

            if (Program.ForceUpdate) {
                string path = Path.Combine(Config.InstallPath, "Versions", currentVersion.ToString());
                if (File.Exists(path + ".zip")) File.Delete(path + ".zip");
                if (Directory.Exists(path)) Directory.Delete(path, true);
                if (currentVersion.IsPatch) {
                    string previousPath = Path.Combine(Config.InstallPath, "Versions", currentVersion.Prerequisite.ToString());
                    if (File.Exists(previousPath + ".zip")) File.Delete(previousPath + ".zip");
                    if (Directory.Exists(previousPath)) Directory.Delete(previousPath, true);
                    Config.CurrentVersion = VersionGameFiles.FromString("ALPHA 0.0");
                }

                Program.DownloadVersion(VersionGameFiles.GetMostRecentVersion());
                Program.ForceUpdate = false;
            } else if (!File.Exists(launchpath)) {
                DialogResult res = MessageBox.Show(
                    $"Cannot find the game in your install path. It might be moved or deleted.\nCheck \n{launchpath}\nfor your files, or redownload them.\nWould you like to redownload?",
                    "Game not found",
                    MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes) {
                    Program.InstallLatestVersion();
                } else return;
            }

            if (File.Exists(launchpath)) {
                Process.Start(launchpath);
                if (!Config.KeepLauncherOpen) Close();
            }
        }
    }
}
