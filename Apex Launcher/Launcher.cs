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
            }
        }

        private void Launcher_Shown(object sender, EventArgs e) {
            TumblrBrowser.IsWebBrowserContextMenuEnabled = false;
            RedditBrowser.IsWebBrowserContextMenuEnabled = false;
            Program.initialize();
            EnableLaunch();
        }

        private void DiscordLogo_Click(object sender, EventArgs e) {
            Process.Start("https://discord.gg/0uEmVbfB955hPDDR");
        }

        private void pictureBox1_Click(object sender, EventArgs e) {
            Process.Start("https://reddit.com/r/PokemonApex");
        }

        private void BrowserNavigation(object sender, WebBrowserNavigatingEventArgs e) {
            if (loaded && !(e.Url.Equals(((WebBrowser)sender).Url)) && (e.Url.ToString().Contains(((WebBrowser)sender).Url.ToString()))) {
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
            string launchpath = Program.GetInstallPath() + "\\Versions\\" + Program.GetParameter("currentversion") + "\\Game.exe";

            if (!File.Exists(launchpath)) {
                DialogResult res = MessageBox.Show("Cannot find the game in your install path. It might be moved or deleted.\nCheck " + launchpath + " for your files, or redownload them.\nWould you like to redownload?", "Game not found", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes) {
                    Program.DownloadVersion(Program.GetCurrentVersion());
                } else return;
            }
            Process.Start(launchpath);
            if (!Convert.ToBoolean(Program.GetParameter("keepLauncherOpen"))) Close();
        }

        public void EnableLaunch() {
            LaunchButton.Enabled = true;
            UpdateStatus("Ready to launch");
        }

        public void UpdateStatus(string message) {
            StatusLabel.Text = message;
        }

        public void SetGameVersion(Version v) {
            GameVersionLabel.Text = "Build: " + v.ToString();
        }

        public void SetLauncherVersion(string versionText) {
            LauncherVersionLabel.Text = "Launcher v" + versionText;
        }

        private void SettingsButton_Click(object sender, EventArgs e) {
            SettingsForm settings = new SettingsForm();
            settings.ShowDialog();
        }
    }
}
