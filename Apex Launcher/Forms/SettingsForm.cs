// <copyright file="SettingsForm.cs" company="IAMVISHNU Media">
// © Copyright by IAMVISHNU Media 2020 CC BY-NC-ND
// </copyright>

using System;
using System.Windows.Forms;

namespace ApexLauncher {
    /// <summary>
    /// Form for modifying Launcher settings.
    /// </summary>
    public partial class SettingsForm : Form {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsForm"/> class.
        /// </summary>
        public SettingsForm() {
            InitializeComponent();

            PathTextbox.Text = Config.InstallPath;
            KeepOpenCheckbox.Checked = Convert.ToBoolean(Config.KeepLauncherOpen, Program.Culture);
            ForceUpdateCheckbox.Checked = Program.ForceUpdate;
        }

        private void CancelButton_Click(object sender, EventArgs e) {
            Close();
        }

        private void OKButton_Click(object sender, EventArgs e) {
            bool validated = true;
            if (Config.InstallPath.Length > 0 && PathTextbox.Text != Config.InstallPath) {
                if (
                    MessageBox.Show(
                        "If you change your install path, you will need to move your game data to the new path or redownload it before you will be able to play. Is this OK?",
                        "Warning",
                        MessageBoxButtons.YesNo) == DialogResult.No) return;
            }

            if (!Program.HasWriteAccess(PathTextbox.Text)) {
                MessageBox.Show("You don't have permission to write to that folder.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (validated) {
                Config.InstallPath = PathTextbox.Text;
                Config.KeepLauncherOpen = KeepOpenCheckbox.Checked;
                Program.ForceUpdate = ForceUpdateCheckbox.Checked;
                Close();
            }
        }

        private void BrowseButton_Click(object sender, EventArgs e) {
            using FolderBrowserDialog fbd = new FolderBrowserDialog { Description = "Choose a Folder" };
            fbd.ShowDialog();

            string selectedPath = fbd.SelectedPath;
            if (selectedPath == null || selectedPath.Length == 0) return;
            if (Program.HasWriteAccess(selectedPath)) {
                PathTextbox.Text = selectedPath;
            } else {
                MessageBox.Show("You don't have permission to write to that folder.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ButtonSysConf_Click(object sender, EventArgs e) {
            Clipboard.SetText(Config.GetSystemConfigurationPaste());
            MessageBox.Show("System configuration copied to clipboard.");
        }
    }
}
