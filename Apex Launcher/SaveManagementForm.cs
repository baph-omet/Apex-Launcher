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
    public partial class SaveManagementForm : Form {
        private string savePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Saved Games\\Pokémon Apex\\";
        private const string currentSaveName = "[Current Save]";

        public SaveManagementForm() {
            InitializeComponent();
            GetSaveFiles();
        }

        private void GetSaveFiles() {
            FileView.Items.Clear();
            foreach (string path in Directory.GetFiles(savePath,"*.rxdata")) {
                string filename = path.Split('\\')[path.Split('\\').Length - 1];
                if (filename.Equals("Game.rxdata")) filename = currentSaveName;
                FileView.Items.Add(new ListViewItem(new[] {filename,File.GetLastWriteTime(path).ToString() }));
            }
        }

        private string GetPath(string filename) {
            if (filename.Equals(currentSaveName)) filename = "Game.rxdata";
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
                    if (it.SubItems[0].Text==newPath.Split('\\')[newPath.Split('\\').Length-1]) {
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
                    "Are you sure you want to delete this save?" + (filename.Equals(currentSaveName) ? " (This is your current save file!)" : ""),
                    "Warning",
                    MessageBoxButtons.YesNo
                );

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
                        MessageBoxButtons.YesNo
                        );
                    if (res != DialogResult.Yes) return;
                }
                string oldfile = GetPath(FileView.SelectedItems[0].SubItems[0].Text);
                TextEntryForm tef = new TextEntryForm();
                tef.ShowDialog();
                foreach (char c in tef.Result) {
                    if ((new[] { '/','\\','|','>','<',':','?','*','"'}).Contains(c)) {
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
                        try { File.Delete(GetPath(currentSaveName)); } catch { }
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
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.AddExtension = true;
                sfd.DefaultExt = ".rxdata";
                sfd.Filter = "RPGXP Data Files|*.rxdata";
                sfd.ShowDialog();
                if (sfd.FileName.Length > 0) File.Copy(GetPath(FileView.SelectedItems[0].SubItems[0].Text), sfd.FileName);
            }
        }

        private void ImportButton_Click(object sender, EventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
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
