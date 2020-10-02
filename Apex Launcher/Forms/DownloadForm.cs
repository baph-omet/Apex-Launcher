// <copyright file="DownloadForm.cs" company="IAMVISHNU Media">
// © Copyright by IAMVISHNU Media 2020 CC BY-NC-ND
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace ApexLauncher {
    /// <summary>
    /// Form for downloading multiple versions in another thread.
    /// </summary>
    public partial class DownloadForm : Form {
        private readonly List<IDownloadable> downloadQueue;
        private readonly VersionGameFiles mostRecent;
        private Thread dlThread;
        private string currentFilepath;
        private bool finished;

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadForm"/> class.
        /// </summary>
        /// <param name="downloads">List of files to download.</param>
        public DownloadForm(List<IDownloadable> downloads) {
            if (downloads is null) throw new ArgumentNullException(nameof(downloads));
            InitializeComponent();
            downloadQueue = new List<IDownloadable>();
            foreach (IDownloadable d in downloads) {
                if (d.Prerequisite != null && d.Prerequisite.NewerThanDownloaded()) {
                    downloadQueue.Add(d.Prerequisite);
                }

                downloadQueue.Add(d);

                if (d is VersionGameFiles) {
                    VersionGameFiles vgf = d as VersionGameFiles;
                    if (mostRecent == null || d.GreaterThan(mostRecent)) mostRecent = vgf;
                }
            }

            Aborted += DownloadForm_Aborted;
            QueueComplete += DownloadForm_QueueComplete;
            Disposed += DownloadForm_Disposed;
        }

        private delegate void CL();

        private delegate void UPT(string message);

        private delegate void UP(int progress);

        /// <summary>
        /// Event that fires when the download queue is successfully completed.
        /// </summary>
        public event EventHandler QueueComplete;

        /// <summary>
        /// Event that fires when the download is aborted while running.
        /// </summary>
        public event EventHandler Aborted;

        /// <summary>
        /// Gets a value indicating whether or not the form is downloading.
        /// </summary>
        public bool Downloading { get; private set; }

        /// <summary>
        /// Starts the download process.
        /// </summary>
        public void StartDownload() {
            Downloading = true;
            dlThread = new Thread(Download) {
                IsBackground = true,
            };
            dlThread.SetApartmentState(ApartmentState.STA);
            dlThread.Start();
        }

        /// <summary>
        /// Main download method.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "For last-ditch error catching.")]
        public void Download() {
            try {
                finished = true;
                foreach (IDownloadable download in downloadQueue) {
                    string source = download.Location;
                    string destination = Path.Combine(Config.InstallPath, "Versions", download.ToString());
                    currentFilepath = destination + ".zip";

                    Program.Launcher.UpdateStatus("Downloading " + download.ToString());
                    Directory.CreateDirectory(destination);
                    bool succeeded = true;

                    // Download
                    HttpWebRequest filereq = (HttpWebRequest)WebRequest.Create(new Uri(source));
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
                    if (fileresp.ContentLength <= 0) {
                        MessageBox.Show($"Could not access file {download}. Package skipped.");
                        continue;
                    }

                    using (Stream dlstream = fileresp.GetResponseStream()) {
                        using FileStream outputStream = new FileStream(currentFilepath, FileMode.OpenOrCreate);
                        int buffersize = 10000;
                        try {
                            long bytesRead = 0;
                            int length = 1;
                            while (length > 0) {
                                byte[] buffer = new byte[buffersize];
                                length = dlstream.Read(buffer, 0, buffersize);
                                bytesRead += length;
                                outputStream.Write(buffer, 0, length);
                                UpdateProgress(Convert.ToInt32(100F * bytesRead / fileresp.ContentLength));
                                UpdateProgressText(
                                    $"Downloading {download} (Package {downloadQueue.IndexOf(download) + 1}/{downloadQueue.Count}) {bytesRead / 1048576}/{fileresp.ContentLength / 1048576} MB (" + Convert.ToInt16(100 * (double)bytesRead / fileresp.ContentLength, Program.Culture) + "%)...");
                            }
                        } catch (WebException e) {
                            succeeded = false;
                            DialogResult res = MessageBox.Show(
                                "An error has occurred during your download. Please check your network connection and try again.\n" +
                                "If this error persists, please report this issue to the launcher's GitHub page. Click OK to copy the error " +
                                "details to your clipboard or CANCEL to ignore this message.",
                                "Download Error",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Error);
                            if (res == DialogResult.OK) Clipboard.SetText(e.ToString());
                            Aborted?.Invoke(this, new EventArgs());
                            CloseForm();
                            return;
                        }
                    }

                    if (!succeeded) {
                        finished = false;
                        MessageBox.Show(
                            "The download couldn't be completed. Check your internet connection. If you think this is a program error, please report this to the Launcher's GitHub page.");
                        break;
                    }

                    // Extraction
                    Program.Launcher.UpdateStatus("Extracting " + download.ToString());
                    try {
                        if (Directory.Exists(destination)) Directory.Delete(destination, true);
                        UpdateProgressText("Download " + (downloadQueue.IndexOf(download) + 1) + "/" + downloadQueue.Count +
                            " completed. Extracting...");
                        ZipFile.ExtractToDirectory(currentFilepath, destination);
                    } catch (InvalidDataException) {
                        MessageBox.Show(
                            "Could not unzip file\n" + destination + ".zip.\nThe file appears to be invalid. Please report this issue. In the meantime, try a manual download.",
                            "Apex Launcher Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        continue;
                    }

                    // Patching
                    if (download is VersionGameFiles) {
                        VersionGameFiles vgf = download as VersionGameFiles;
                        if (vgf.IsPatch) {
                            UpdateProgressText("Patching...");
                            string versionpath = Path.Combine(Config.InstallPath, "Versions", download.Prerequisite.ToString());
                            RecursiveCopy(versionpath, destination, false);
                        }
                    } else if (download is VersionAudio) {
                        VersionAudio va = download as VersionAudio;
                        string versionpath = Path.Combine(Config.InstallPath, "Versions", mostRecent.ToString());
                        RecursiveCopy(destination, versionpath, true);
                    }

                    if (download == mostRecent && !(download is VersionAudio)) {
                        bool downloadingNewAudio = false;
                        foreach (IDownloadable d in downloadQueue) {
                            if (d is VersionAudio) {
                                downloadingNewAudio = true;
                                break;
                            }
                        }

                        if (!downloadingNewAudio) {
                            string versionpath = Path.Combine(Config.InstallPath, "Versions", mostRecent.ToString());
                            string audioversionpath = Path.Combine(Config.InstallPath, "Versions", VersionAudio.GetMostRecentVersion().ToString());
                            RecursiveCopy(audioversionpath, versionpath, true);
                        }
                    }

                    File.Delete(currentFilepath);
                }

                if (finished) QueueComplete?.Invoke(this, new EventArgs());
            } catch (ThreadAbortException) { } catch (ThreadInterruptedException) { } catch (Exception e) {
                new ErrorCatcher(e).ShowDialog();
                Aborted?.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// Update the progress bar percentage.
        /// </summary>
        /// <param name="progress">Percentage to set the progress bar to.</param>
        public void UpdateProgress(int progress) {
            if (IsDisposed || DownloadProgressBar.IsDisposed) return;
            if (DownloadProgressBar.InvokeRequired) {
                UP d = UpdateProgress;
                try {
                    Invoke(d, new object[] { progress });
                } catch (ObjectDisposedException) { }
            } else if (progress >= 0) DownloadProgressBar.Value = progress;
        }

        /// <summary>
        /// Update the progress text description.
        /// </summary>
        /// <param name="message">Message to set as description.</param>
        public void UpdateProgressText(string message) {
            if (IsDisposed || ProgressLabel.IsDisposed) return;
            if (ProgressLabel.InvokeRequired) {
                UPT d = UpdateProgressText;
                try {
                    Invoke(d, new object[] { message });
                } catch (ObjectDisposedException) { }
            } else ProgressLabel.Text = message;
        }

        /// <summary>
        /// Close the form.
        /// </summary>
        public void CloseForm() {
            if (IsDisposed) return;
            if (InvokeRequired) {
                CL d = CloseForm;
                Invoke(d);
            } else {
                Downloading = false;
                Close();
            }
        }

        /// <summary>
        /// Recursively copies directory and all contained files and directories to desired location.
        /// </summary>
        /// <param name="sourceDirectory">Directory to copy.</param>
        /// <param name="destinationDirectory">Destination to copy to.</param>
        /// <param name="overwriteFile">If true, existing files with same names in destination will be overwritten.</param>
        private static void RecursiveCopy(string sourceDirectory, string destinationDirectory, bool overwriteFile = true) {
            if (!Directory.Exists(destinationDirectory)) Directory.CreateDirectory(destinationDirectory);
            foreach (string f in from string f in Directory.GetFiles(sourceDirectory)
                                 where overwriteFile || !File.Exists(Path.Combine(destinationDirectory, Path.GetFileName(f)))
                                 select f) {
                File.Copy(f, Path.Combine(destinationDirectory, Path.GetFileName(f)), overwriteFile);
            }

            foreach (string d in Directory.GetDirectories(sourceDirectory)) RecursiveCopy(d, Path.Combine(destinationDirectory, Path.GetFileName(d)), overwriteFile);
        }

        private void DownloadForm_Disposed(object sender, EventArgs e) {
            if (dlThread.IsAlive) {
                dlThread.Abort();
                if (!finished) Aborted?.Invoke(sender, new EventArgs());
            }
        }

        private void DownloadForm_QueueComplete(object sender, EventArgs e) {
            if (mostRecent != null) {
                Config.CurrentVersion = mostRecent;
                Config.CurrentAudioVersion = mostRecent.MinimumAudioVersion;
            }

            Program.Launcher.SetGameVersion(mostRecent);
            Program.Launcher.UpdateStatus("Ready to launch");
            CloseForm();
        }

        private void DownloadForm_Aborted(object sender, EventArgs e) {
            if (dlThread.IsAlive) dlThread.Abort();
            Downloading = false;
            CloseForm();
            Program.Launcher.UpdateStatus("Download aborted");
        }

        private bool PromptCancelDownload() {
            if (!Program.Downloading) return true;
            DialogResult res = MessageBox.Show("There is a download in progress. Are you sure you want to cancel?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (res == DialogResult.Yes) {
                dlThread.Abort();
                try { File.Delete(currentFilepath); } catch (IOException) { }
                Downloading = false;
                Program.Launcher.UpdateStatus("Download cancelled.");
                return true;
            }

            return false;
        }

        private void CancelDownloadButton_Click(object sender, EventArgs e) {
            CloseForm();
        }

        private void DownloadForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (!PromptCancelDownload()) e.Cancel = true;
        }
    }
}
