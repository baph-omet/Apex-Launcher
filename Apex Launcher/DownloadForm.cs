using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace Apex_Launcher {
    public partial class DownloadForm : Form {
        
        private string filepath;
        private List<Version> downloadQueue;
        private Version v;
        private Version prerequisite;
        private Thread dlThread;

        public DownloadForm(Version v) {
            InitializeComponent();
            downloadQueue = new List<Version>();
            if (v.IsPatch) {
                prerequisite = v.Prerequisite;
                if (prerequisite.GreaterThan(Program.GetCurrentVersion())) downloadQueue.Add(prerequisite);
            } else prerequisite = null;
            downloadQueue.Add(v);
            dlThread = null;
            this.v = v;
        }

        public void StartDownload() {
            dlThread = new Thread(Download);
            dlThread.IsBackground = true;
            Program.Downloading = true;
            dlThread.SetApartmentState(ApartmentState.STA);
            dlThread.Start();
        }

        public void Download() {
            try {
                bool allFinished = true;
                foreach (Version queuedVersion in downloadQueue) {
                    string Source = queuedVersion.Location;
                    string Destination = Program.GetInstallPath() + "\\Versions\\" + queuedVersion.ToString();
                    filepath = Destination + ".zip";

                    Program.Launcher.UpdateStatus("Downloading version " + queuedVersion.ToString());
                    //Directory.CreateDirectory(Destination);
                    bool succeeded = true;

                    HttpWebRequest filereq = (HttpWebRequest)HttpWebRequest.Create(Source);
                    HttpWebResponse fileresp = null;
                    try {
                        fileresp = (HttpWebResponse)filereq.GetResponse();
                    } catch (WebException) {
                        MessageBox.Show(
                            "Network exception encountered when trying to start your download. You might not have a connection to the internet," +
                            " or access to the download link is restricted. If you think tihs is a program error, please report it to the Launcher's GitHub page.");
                        Program.Downloading = false;
                        CloseForm();
                        return;
                    }
                    if (filereq.ContentLength > 0) fileresp.ContentLength = filereq.ContentLength;
                    using (Stream dlstream = fileresp.GetResponseStream()) {
                        using (FileStream outputStream = new FileStream(filepath, FileMode.OpenOrCreate)) {
                            int buffersize = 10000;
                            try {
                                long bytesRead = 0;
                                int length = 1;
                                while (length > 0) {
                                    byte[] buffer = new Byte[buffersize];
                                    length = dlstream.Read(buffer, 0, buffersize);
                                    bytesRead += length;
                                    outputStream.Write(buffer, 0, length);
                                    UpdateProgress((int)(100 * bytesRead / fileresp.ContentLength));
                                    UpdateProgressText(
                                        "Downloading " + queuedVersion.ToString() +
                                        " (Package " + (downloadQueue.IndexOf(queuedVersion) + 1) + "/" + downloadQueue.Count + ") " +
                                        (bytesRead / 1048576) + "/" + (fileresp.ContentLength / 1048576) +
                                        " MB (" + Convert.ToInt16(100 * (double)bytesRead / fileresp.ContentLength, Program.Culture) + "%)..."
                                    );
                                }
                            } catch (WebException e) {
                                succeeded = false;
                                DialogResult res = MessageBox.Show(
                                    "An error has occurred during your download. Please check your network connection and try again.\n" +
                                    "If this error persists, please report this issue to the launcher's GitHub page. Click OK to copy the error " +
                                    "details to your clipboard or CANCEL to ignore this message.",
                                    "Download Error",
                                    MessageBoxButtons.OKCancel,
                                    MessageBoxIcon.Error
                                );
                                if (res == DialogResult.OK) Clipboard.SetText(e.ToString());
                            }
                        }
                    }

                    if (succeeded) {
                        Program.Launcher.UpdateStatus("Extracting version " + v.ToString());
                        try {
                            if (Directory.Exists(Destination)) Directory.Delete(Destination, true);
                            UpdateProgressText("Download " + (downloadQueue.IndexOf(queuedVersion) + 1) + "/" + downloadQueue.Count +
                                " completed. Extracting...");
                            ZipFile.ExtractToDirectory(filepath, Destination);
                            if (queuedVersion.IsPatch) {
                                UpdateProgressText("Patching...");
                                string versionpath = Program.GetInstallPath() + "\\Versions\\" + prerequisite.ToString();
                                RecursiveCopy(versionpath, Destination, false);
                            }
                        } catch (InvalidDataException) {
                            MessageBox.Show(
                                "Could not unzip file\n" + Destination + ".zip.\nThe file appears to be invalid. Please report this issue. In the meantime, try a manual download.",
                                "Apex Launcher Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );
                        }
                        File.Delete(filepath);
                    } else {
                        allFinished = false;
                        MessageBox.Show(
                            "The download couldn't be completed. Check your internet connection. If you think this is a program error, please report this to the Launcher's GitHub page."
                        );
                        break;
                    }
                }
                if (allFinished) {
                    Program.SetParameter("currentversion", v.ToString());
                    Program.Launcher.SetGameVersion(v);
                    Program.Launcher.UpdateStatus("Ready to launch");
                }
                Program.Downloading = false;
                CloseForm();
            } catch (ThreadAbortException) { }
            catch (Exception e) {
                (new ErrorCatcher(e)).ShowDialog();
                CloseForm();
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
                try {
                    this.Invoke(d, new object[] { message });
                } catch (ObjectDisposedException) { }
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
                Program.Launcher.UpdateStatus("Download cancelled.");
                return true;
            } return false;
        }

        private static void RecursiveCopy(string SourceDirectory, string DestinationDirectory) {
            RecursiveCopy(SourceDirectory, DestinationDirectory, true);
        }
        private static void RecursiveCopy(string SourceDirectory, string DestinationDirectory, bool overwriteFile) {
            if (!Directory.Exists(DestinationDirectory)) Directory.CreateDirectory(DestinationDirectory);
            foreach (string f in Directory.GetFiles(SourceDirectory)) {
                if (overwriteFile || !File.Exists(DestinationDirectory + "\\" + f.Split('\\').Last())) {
                    File.Copy(f,DestinationDirectory + "\\" + f.Split('\\').Last(),overwriteFile);
               }
            }
            foreach (string d in Directory.GetDirectories(SourceDirectory))  RecursiveCopy(d, DestinationDirectory + "\\" + d.Split('\\').Last(), overwriteFile);
        }

        private void CancelDownloadButton_Click(object sender, EventArgs e) {
            CloseForm();
        }

        private void DownloadForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (!PromptCancelDownload()) e.Cancel = true;
        }
    }
}
