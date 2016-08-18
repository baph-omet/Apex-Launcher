using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Apex_Launcher {
    public partial class Launcher : Form {

        private bool loaded = false;

        public Launcher() {
            InitializeComponent();
            if (!Program.NetworkConnected) {
                TumblrBrowser.Hide();
                RedditBrowser.Hide();
                TabBox.Enabled = false;
            } else {
                NoConnectionLabel.Hide();
            }
        }

        private void Launcher_Shown(object sender, EventArgs e) {
            TumblrBrowser.IsWebBrowserContextMenuEnabled = false;
            RedditBrowser.IsWebBrowserContextMenuEnabled = false;
            Program.initialize();
            SetGameVersion(Program.GetCurrentVersion());
            LauncherVersionLabel.Text = "Launcher v" + Program.GetLauncherVersion();
            EnableLaunch();
        }

        private void DiscordLogo_Click(object sender, EventArgs e) {
            Process.Start("https://discord.gg/0uEmVbfB955hPDDR");
        }

        private void pictureBox1_Click(object sender, EventArgs e) {
            Process.Start("https://reddit.com/r/PokemonApex");
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
                MessageBox.Show("A download is currently in progress. Please cancel your download or wait until it finishes.","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            string launchpath = Program.GetInstallPath() + "\\Versions\\" + Program.GetParameter("currentversion") + "\\Game.exe";

            if (Program.forceUpdate) {
                string path = Directory.GetCurrentDirectory() + "\\Versions\\" + Program.GetCurrentVersion().ToString();
                if (File.Exists(path + ".zip")) File.Delete(path + ".zip");
                if (Directory.Exists(path)) Directory.Delete(path,true);
                Program.DownloadVersion(Program.GetMostRecentVersion());
                Program.forceUpdate = false;
            } else if (!File.Exists(launchpath)) {
                DialogResult res = MessageBox.Show("Cannot find the game in your install path. It might be moved or deleted.\nCheck \n" + launchpath + "\nfor your files, or redownload them.\nWould you like to redownload?", "Game not found", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes) {
                    Program.DownloadVersion(Program.GetMostRecentVersion());
                } else return;
            }

            if (File.Exists(launchpath)) {
                Process.Start(launchpath);
                if (!Convert.ToBoolean(Program.GetParameter("keepLauncherOpen"))) Close();
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
                this.Invoke(d, new object[] { message });
            } else StatusLabel.Text = message;
        }

        public delegate void SGV(Version v);
        public void SetGameVersion(Version v) {
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
                DialogResult res = MessageBox.Show("A download is still in progress. Closing the launcher will cancel your download. Are you sure you want to quit?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res == DialogResult.No) {
                    e.Cancel = true;
                }
            }
        }
    }
}
