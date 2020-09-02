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
        private readonly List<IDownloadable> downloadQueue;
        private Thread dlThread = null;

        public bool Downloading { get; private set; }

        [Obsolete("Switch to download queueing")]
        public DownloadForm(IDownloadable v) : this(new List<IDownloadable>() { v }) { }

        public DownloadForm(List<IDownloadable> downloads) {
            InitializeComponent();
            foreach (IDownloadable d in downloads) {
                if (d.Prerequisite != null && d.Prerequisite.NewerThanDownloaded()) downloadQueue.Add(d.Prerequisite);
                downloadQueue.Add(d);
            }

            Aborted += DownloadForm_Aborted;
        }

        private void DownloadForm_Aborted(object sender, EventArgs e) {
            Downloading = false;
            CloseForm();
        }

        public event EventHandler QueueComplete;
        public event EventHandler Aborted;

        public void StartDownload() {
            Downloading = true;
            dlThread = new Thread(Download);
            dlThread.IsBackground = true;
            dlThread.SetApartmentState(ApartmentState.STA);
            dlThread.Start();
        }

        public void Download() {
            try {
                bool allFinished = true;
                foreach (IDownloadable download in downloadQueue) {
                    string Source = download.Location;
                    string Destination = Path.Combine(Program.GetInstallPath(), "Versions", download.ToString());
                    string filepath = Destination + ".zip";

                    Program.Launcher.UpdateStatus("Downloading " + download.ToString());
                    Directory.CreateDirectory(Destination);
                    bool succeeded = true;

                    HttpWebRequest filereq = (HttpWebRequest)WebRequest.Create(Source);
                    HttpWebResponse fileresp = null;
                    try {
                        fileresp = (HttpWebResponse)filereq.GetResponse();
                    } catch (WebException) {
                        MessageBox.Show(
                            "Network exception encountered when trying to start your download. You might not have a connection to the internet," +
                            " or access to the download link is restricted. If you think tihs is a program error, please report it to the Launcher's GitHub page.");
                        Aborted?.Invoke(this, new EventArgs());
                        Downloading = false;
                        CloseForm();
                        return;
                    }
                    if (filereq.ContentLength > 0) fileresp.ContentLength = filereq.ContentLength;
                    using (Stream dlstream = fileresp.GetResponseStream()) {
                        using FileStream outputStream = new FileStream(filepath, FileMode.OpenOrCreate);
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
                                    "Downloading " + download.ToString() +
                                    " (Package " + (downloadQueue.IndexOf(download) + 1) + "/" + downloadQueue.Count + ") " +
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
                            Aborted?.Invoke(this, new EventArgs());
                            CloseForm();
                            return;
                        }
                    }

                    if (succeeded) {
                        Program.Launcher.UpdateStatus("Extracting version " + download.ToString());
                        try {
                            if (Directory.Exists(Destination)) Directory.Delete(Destination, true);
                            UpdateProgressText("Download " + (downloadQueue.IndexOf(download) + 1) + "/" + downloadQueue.Count +
                                " completed. Extracting...");
                            ZipFile.ExtractToDirectory(filepath, Destination);

                            if (download is VersionGameFiles) {
                                VersionGameFiles vgf = download as VersionGameFiles;
                                if (vgf.IsPatch) {
                                    UpdateProgressText("Patching...");
                                    string versionpath = Program.GetInstallPath() + "\\Versions\\" + download.Prerequisite.ToString();
                                    RecursiveCopy(versionpath, Destination, false);
                                }
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
                    QueueComplete?.Invoke(this, new EventArgs());
                    /*Program.SetParameter("currentversion", v.ToString());
                    Program.Launcher.SetGameVersion(v);
                    Program.Launcher.UpdateStatus("Ready to launch");*/
                }
                CloseForm();
            } catch (ThreadAbortException) { } catch (Exception e) {
                new ErrorCatcher(e).ShowDialog();
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
            } else {
                Downloading = false;
                Close();
            }
        }

        private bool PromptCancelDownload() {
            if (!Program.Downloading) return true;
            DialogResult res = MessageBox.Show("There is a download in progress. Are you sure you want to cancel?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (res == DialogResult.Yes) {
                dlThread.Abort();
                try { File.Delete(filepath); } catch (Exception) { };
                Downloading = false;
                Program.Launcher.UpdateStatus("Download cancelled.");
                return true;
            }
            return false;
        }

        private static void RecursiveCopy(string SourceDirectory, string DestinationDirectory) {
            RecursiveCopy(SourceDirectory, DestinationDirectory, true);
        }
        private static void RecursiveCopy(string SourceDirectory, string DestinationDirectory, bool overwriteFile) {
            if (!Directory.Exists(DestinationDirectory)) Directory.CreateDirectory(DestinationDirectory);
            foreach (string f in Directory.GetFiles(SourceDirectory)) {
                if (overwriteFile || !File.Exists(DestinationDirectory + "\\" + f.Split('\\').Last())) {
                    File.Copy(f, DestinationDirectory + "\\" + f.Split('\\').Last(), overwriteFile);
                }
            }
            foreach (string d in Directory.GetDirectories(SourceDirectory)) RecursiveCopy(d, DestinationDirectory + "\\" + d.Split('\\').Last(), overwriteFile);
        }

        private void CancelDownloadButton_Click(object sender, EventArgs e) {
            CloseForm();
        }

        private void DownloadForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (!PromptCancelDownload()) e.Cancel = true;
        }
    }
}
