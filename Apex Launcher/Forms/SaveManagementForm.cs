// <copyright file="SaveManagementForm.cs" company="IAMVISHNU Media">
// © Copyright by IAMVISHNU Media 2020 CC BY-NC-ND
// </copyright>

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ApexLauncher {
    /// <summary>
    /// Form for managing save files.
    /// </summary>
    public partial class SaveManagementForm : Form {
        private readonly string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Saved Games", "Pokémon Apex");
        private readonly string currentSaveName = "[Current Save]";

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveManagementForm"/> class.
        /// </summary>
        public SaveManagementForm() {
            InitializeComponent();
            GetSaveFiles();
        }

        private void GetSaveFiles() {
            FileView.Items.Clear();
            if (!Directory.Exists(savePath)) Directory.CreateDirectory(savePath);
            foreach (string path in Directory.GetFiles(savePath, "*.rxdata")) {
                string filename = Path.GetFileName(path);
                if (filename.Equals("Game.rxdata", StringComparison.InvariantCultureIgnoreCase)) filename = currentSaveName;
                FileView.Items.Add(new ListViewItem(new[] { filename, File.GetLastWriteTime(path).ToString() }));
            }
        }

        private string GetPath(string filename) {
            if (filename.Equals(currentSaveName, StringComparison.InvariantCulture)) filename = "Game.rxdata";
            return savePath + filename;
        }

        private void CopyButton_Click(object sender, EventArgs e) {
            if (FileView.SelectedItems.Count > 0) {
                ListViewItem item = FileView.SelectedItems[0];

                string oldPath = GetPath(item.SubItems[0].Text);
                string newPath = Program.GetNextAvailableFilePath(oldPath);

                File.Copy(oldPath, newPath);
                GetSaveFiles();

                foreach (ListViewItem it in FileView.Items) {
                    if (it.SubItems[0].Text == Path.GetFileName(newPath)) {
                        it.Selected = true;
                        FileView.Select();
                    }
                }
            }
        }

        private void Delete_Click(object sender, EventArgs e) {
            if (FileView.SelectedItems.Count > 0) {
                string filename = FileView.SelectedItems[0].SubItems[0].Text;

                int i = FileView.SelectedItems[0].Index;
                DialogResult res = MessageBox.Show(
                    $"Are you sure you want to delete this save?" + (filename.Equals(currentSaveName, StringComparison.InvariantCulture) ? " (This is your current save file!)" : string.Empty),
                    "Warning",
                    MessageBoxButtons.YesNo);

                if (res == DialogResult.Yes) {
                    File.Delete(GetPath(filename));
                    GetSaveFiles();
                    if (FileView.Items.Count > 0) {
                        if (FileView.Items.Count > i) FileView.Items[i].Selected = true;
                        else FileView.Items[FileView.Items.Count - 1].Selected = true;
                        FileView.Select();
                    }
                }
            }
        }

        private void RenameButton_Click(object sender, EventArgs e) {
            if (FileView.SelectedItems.Count > 0) {
                if (FileView.SelectedItems[0].SubItems[0].Text.Equals(currentSaveName)) {
                    DialogResult res = MessageBox.Show(
                        "This is your current save file. If you rename it, the game will no longer recognize it until you have changed it back to the current save file. Are you sure you want to rename it?",
                        "Warning",
                        MessageBoxButtons.YesNo);
                    if (res != DialogResult.Yes) return;
                }

                string oldfile = GetPath(FileView.SelectedItems[0].SubItems[0].Text);
                using TextEntryForm tef = new TextEntryForm();
                tef.ShowDialog();
                foreach (char c in tef.Result) {
                    if (new[] { '/', '\\', '|', '>', '<', ':', '?', '*', '"' }.Contains(c)) {
                        MessageBox.Show("File name contains an invalid character (" + c + "). Please choose another file name.");
                        return;
                    }
                }

                File.Copy(oldfile, GetPath(tef.Result + ".rxdata"));
                File.Delete(oldfile);
                GetSaveFiles();
                FileView.Select();
            }
        }

        private void MakeDefaultButton_Click(object sender, EventArgs e) {
            if (FileView.SelectedItems.Count > 0) {
                string filename = FileView.SelectedItems[0].SubItems[0].Text;
                if (!filename.Equals(currentSaveName)) {
                    DialogResult res = MessageBox.Show(
                        "Are you sure you want to make this your active save file? This will overwrite the current active save.",
                        "Warning",
                        MessageBoxButtons.YesNo);
                    if (res == DialogResult.Yes) {
                        try { File.Delete(GetPath(currentSaveName)); } catch (IOException) { }
                        File.Copy(GetPath(filename), GetPath(currentSaveName));
                        File.Delete(GetPath(filename));
                        GetSaveFiles();
                        FileView.Select();
                    }
                }
            }
        }

        private void ExportButton_Click(object sender, EventArgs e) {
            if (FileView.SelectedItems.Count > 0) {
                using SaveFileDialog sfd = new SaveFileDialog {
                    AddExtension = true,
                    DefaultExt = ".rxdata",
                    Filter = "RPGXP Data Files|*.rxdata"
                };
                sfd.ShowDialog();
                if (sfd.FileName.Length > 0) File.Copy(GetPath(FileView.SelectedItems[0].SubItems[0].Text), sfd.FileName);
            }
        }

        private void ImportButton_Click(object sender, EventArgs e) {
            using OpenFileDialog ofd = new OpenFileDialog();
            ofd.AddExtension = true;
            ofd.DefaultExt = ".rxdata";
            ofd.Filter = "RPGXP Data Files|*.rxdata";
            ofd.ShowDialog();

            if (ofd.FileName.Length > 0) {
                string newfilename = Program.GetNextAvailableFilePath(GetPath(ofd.FileName.Split('\\')[ofd.FileName.Split('\\').Length - 1]));

                File.Copy(ofd.FileName, newfilename);

                GetSaveFiles();
            }

            FileView.Select();
        }

        private void ExplorerOpenButton_Click(object sender, EventArgs e) {
            Process.Start(savePath);
        }
    }
}
