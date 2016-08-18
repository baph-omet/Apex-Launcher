using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Apex_Launcher {
    public partial class DownloadForm : Form {

        private string Source;
        private string Destination;
        private string installpath;
        private string filepath;
        private Version v;
        private Thread dlThread;

        public DownloadForm(Version v) {
            InitializeComponent();
            Source = v.Location;
            installpath = Program.GetInstallPath();
            Destination = installpath + "\\Versions\\" + v.ToString();
            filepath = Destination + ".zip";
            dlThread = null;
            this.v = v;
        }

        public void StartDownload() {
            dlThread = new Thread(Download);
            dlThread.IsBackground = true;
            Program.Downloading = true;
            dlThread.Start();
        }

        public void Download() {
            Program.launcher.UpdateStatus("Downloading version " + v.ToString());
            Directory.CreateDirectory(Destination);
            bool succeeded = true;

            HttpWebRequest filereq = (HttpWebRequest)HttpWebRequest.Create(Source);
            HttpWebResponse fileresp = (HttpWebResponse)filereq.GetResponse();
            if (filereq.ContentLength > 0) fileresp.ContentLength = filereq.ContentLength;
            using (Stream dlstream = fileresp.GetResponseStream()) {
                using (FileStream outputStream = new FileStream(filepath, FileMode.OpenOrCreate)) {
                    int buffersize = 10000;
                    //try {
                        long bytesRead = 0;
                        int length = 1;
                        while (length > 0) {
                            byte[] buffer = new Byte[buffersize];
                            length = dlstream.Read(buffer, 0, buffersize);
                            bytesRead += length;
                            outputStream.Write(buffer, 0, length);
                            UpdateProgress((int)(100 * bytesRead / fileresp.ContentLength));
                            UpdateProgressText("Downloading " + (bytesRead / 1048576) + "/" + (fileresp.ContentLength / 1048576) + " MB...");
                        }
                    /*} catch (Exception e) {
                        DialogResult res = MessageBox.Show("An error has occurred during your download. Please check your network connection and try again.\nIf this error persists, please report this issue to the launcher's GitHub page. Click OK to copy the error details to your clipboard or CANCEL to ignore this message.","Download Error",MessageBoxButtons.OKCancel,MessageBoxIcon.Error);
                        if (res == DialogResult.OK) Clipboard.SetText(e.ToString());
                    }*/
                }
            }

            if (succeeded) {
                Program.launcher.UpdateStatus("Exctracting version " + v.ToString());
                try {
                    if (Directory.Exists(Destination)) Directory.Delete(Destination, true);
                    UpdateProgressText("Doanload completed. Extracting...");
                    ZipFile.ExtractToDirectory(filepath, Destination);
                } catch (InvalidDataException) {
                    MessageBox.Show("Could not unzip file\n" + Destination + ".zip.\nThe file appears to be invalid. Please report this issue. In the meantime, try a manual download.", "Apex Launcher Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                File.Delete(filepath);
                Program.SetParameter("currentversion", v.ToString());
                Program.launcher.SetGameVersion(v);
                Program.Downloading = false;
                Program.launcher.UpdateStatus("Ready to launch");
                CloseForm();
            } else {
                MessageBox.Show("The download couldn't be completed. Check your internet connection. If you think this is a program error, please report this to the Launcher's GitHub page.");
            }
        }

        public delegate void UP(int progress);
        public void UpdateProgress(int progress) {
            if (DownloadProgressBar.InvokeRequired) {
                UP d = UpdateProgress;
                this.Invoke(d, new object[] { progress });
            } else DownloadProgressBar.Value = progress;
        }

        public delegate void UPT(string message);
        public void UpdateProgressText(string message) {
            if (ProgressLabel.InvokeRequired) {
                UPT d = UpdateProgressText;
                this.Invoke(d, new object[] { message });
            } else ProgressLabel.Text = message;
        }

        public delegate void CL();
        public void CloseForm() {
            if (InvokeRequired) {
                CL d = CloseForm;
                Invoke(d);
            } else Close();
        }

        private bool PromptCancelDownload() {
            if (!Program.Downloading) return true;
            DialogResult res = MessageBox.Show("This download is still in progress. Are you sure you want to cancel?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (res == DialogResult.Yes) {
                dlThread.Abort();
                try { File.Delete(filepath); } catch (Exception) { };
                Program.Downloading = false;
                Program.launcher.UpdateStatus("Download cancelled.");
                return true;
            } return false;
        }

        private void CancelDownloadButton_Click(object sender, EventArgs e) {
            CloseForm();
        }

        private void DownloadForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (!PromptCancelDownload()) e.Cancel = true;
        }
    }
}
