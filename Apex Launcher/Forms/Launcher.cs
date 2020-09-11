using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Apex_Launcher {
    public partial class Launcher : Form {

        private bool loaded = false;

        public Launcher() {
            InitializeComponent();
            if (!Program.NetworkConnected) {
                TumblrBrowser.Hide();
                RedditBrowser.Hide();
                //GitHubBrowser.Hide();
                TabBox.Enabled = false;
            } else {
                NoConnectionLabel.Hide();
            }
        }

        private void Launcher_Shown(object sender, EventArgs e) {
            TumblrBrowser.IsWebBrowserContextMenuEnabled = false;
            RedditBrowser.IsWebBrowserContextMenuEnabled = false;
            //GitHubBrowser.IsWebBrowserContextMenuEnabled = false;
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
            if (loaded && !(e.Url.Equals(((WebBrowser)sender).Url)) && !(e.Url.ToString().Contains("redditmedia")) && (e.Url.ToString().Contains(((WebBrowser)sender).Url.ToString()))) {
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
            VersionGameFiles CurrentVersion = Config.CurrentVersion;
            string launchpath = Path.Combine(Config.InstallPath, "Versions", CurrentVersion.ToString(), "Game.exe");

            if (Program.ForceUpdate) {
                string path = Config.InstallPath + "\\Versions\\" + CurrentVersion.ToString();
                if (File.Exists(path + ".zip")) File.Delete(path + ".zip");
                if (Directory.Exists(path)) Directory.Delete(path, true);
                if (CurrentVersion.IsPatch) {
                    string previousPath = Config.InstallPath + "\\Versions\\" + CurrentVersion.Prerequisite.ToString();
                    if (File.Exists(previousPath + ".zip")) File.Delete(previousPath + ".zip");
                    if (Directory.Exists(previousPath)) Directory.Delete(previousPath, true);
                    Config.CurrentVersion = VersionGameFiles.FromString("ALPHA 0.0");
                }

                Program.DownloadVersion(VersionGameFiles.GetMostRecentVersion());
                Program.ForceUpdate = false;
            } else if (!File.Exists(launchpath)) {
                DialogResult res = MessageBox.Show(
                    "Cannot find the game in your install path. It might be moved or deleted.\nCheck \n" + launchpath +
                    "\nfor your files, or redownload them.\nWould you like to redownload?", "Game not found", MessageBoxButtons.YesNo
                );
                if (res == DialogResult.Yes) {
                    Program.DownloadVersion(VersionGameFiles.GetMostRecentVersion());
                } else return;
            }

            if (File.Exists(launchpath)) {
                Process.Start(launchpath);
                if (!Config.KeepLauncherOpen) Close();
            }
        }

        public void EnableLaunch() {
            LaunchButton.Enabled = true;
            UpdateStatus("Ready to launch");
        }

        public delegate void US(string message);
        public void UpdateStatus(string message) {
            if (StatusLabel.InvokeRequired) {
                US d = UpdateStatus;
                Invoke(d, new object[] { message });
            } else StatusLabel.Text = message;
        }

        public delegate void SGV(VersionGameFiles v);
        public void SetGameVersion(VersionGameFiles v) {
            if (GameVersionLabel.InvokeRequired) {
                SGV d = SetGameVersion;
                Invoke(d, new object[] { v });
            } else GameVersionLabel.Text = "Build: " + v.ToString();
        }

        public void SetLauncherVersion(string versionText) {
            LauncherVersionLabel.Text = "Launcher v" + versionText;
        }

        private void SettingsButton_Click(object sender, EventArgs e) {
            SettingsForm settings = new SettingsForm();
            settings.ShowDialog();
        }

        private void Launcher_FormClosing(object sender, FormClosingEventArgs e) {
            if (Program.Downloading) {
                DialogResult res = MessageBox.Show(
                    "A download is still in progress. Closing the launcher will cancel your download. Are you sure you want to quit?", "Warning",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );
                if (res == DialogResult.No) {
                    e.Cancel = true;
                }
            }
        }

        private void SaveMgmtButton_Click(object sender, EventArgs e) {
            SaveManagementForm saves = new SaveManagementForm();
            saves.ShowDialog();
        }
    }
}
