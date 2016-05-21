using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Apex_Launcher {
    public partial class Launcher : Form {

        private bool loaded = false;

        public Launcher() {
            InitializeComponent();
            if (Program.initialize()) EnableLaunch();
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
            string launchpath = Program.GetParameter("installpath") + "\\versions\\" + Program.GetParameter("currentversion") + "\\Game.exe";
            Process.Start(launchpath);
        }

        private void EnableLaunch() {
            LaunchButton.Enabled = true;
        }
    }
}
