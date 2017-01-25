using System;
using System.Windows.Forms;

namespace Apex_Launcher {
    public partial class SettingsForm : Form {
        public SettingsForm() {
            InitializeComponent();

            PathTextbox.Text = Program.GetInstallPath();
            KeepOpenCheckbox.Checked = Convert.ToBoolean(Program.GetParameter("keepLauncherOpen"));
            ForceUpdateCheckbox.Checked = Program.ForceUpdate;
        }

        private void CancelButton_Click(object sender, EventArgs e) {
            Close();
        }

        private void OKButton_Click(object sender, EventArgs e) {
            bool validated = true;
            if (PathTextbox.Text != Program.GetParameter("installpath")) {
                if (
                    MessageBox.Show(
                        "If you change your install path, you will need to move your game data to the new path or redownload it before you will be able to play. Is this OK?",
                        "Warning",
                        MessageBoxButtons.YesNo
                    ) == DialogResult.No
                ) return;
            }

            if (!Program.HasWriteAccess(PathTextbox.Text)) {
                MessageBox.Show("You don't have permission to write to that folder.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (validated) {
                Program.SetParameter("installpath", PathTextbox.Text);
                Program.SetParameter("keepLauncherOpen", KeepOpenCheckbox.Checked.ToString());
                //Program.SetParameter("disableGameFonts", DisableFontBox.Checked.ToString());
                Program.ForceUpdate = ForceUpdateCheckbox.Checked;
                Close();
            }
        }

        private void BrowseButton_Click(object sender, EventArgs e) {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Choose a Folder";
            fbd.ShowDialog();

            string selectedPath = fbd.SelectedPath;
            if (Program.HasWriteAccess(selectedPath)) {
                PathTextbox.Text = selectedPath;
            } else {
                MessageBox.Show("You don't have permission to write to that folder.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
