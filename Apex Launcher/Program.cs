// <copyright file="Program.cs" company="IAMVISHNU Media">
// © Copyright by IAMVISHNU Media 2020 CC BY-NC-ND
// </copyright>

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows.Forms;

namespace ApexLauncher {
    /// <summary>
    /// Top-level class for program. Contains startup logic.
    /// </summary>
    public static class Program {
        private static DownloadForm downloadForm;

        /// <summary>
        /// Gets a value indicating whether or not the program is downloading a new version.
        /// </summary>
        public static bool Downloading {
            get {
                if (downloadForm == null) return false;
                return downloadForm.Downloading;
            }
        }

        /// <summary>
        /// Gets program's culture info.
        /// </summary>
        public static CultureInfo Culture {
            get {
                return new CultureInfo("en-US");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether network connection has been found.
        /// </summary>
        public static bool NetworkConnected { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether updates should be forced.
        /// </summary>
        public static bool ForceUpdate { get; set; }

        /// <summary>
        /// Gets or sets the main form for this program.
        /// </summary>
        public static Launcher Launcher { get; set; }

        /// <summary>
        /// Check for and download the latest version of the game files.
        /// </summary>
        /// <returns>True if a download was made.</returns>
        public static bool InstallLatestVersion() {
            Launcher.UpdateStatus("Checking for new versions...");

            VersionGameFiles mostRecent = VersionGameFiles.GetMostRecentVersion();

            if (mostRecent != null && mostRecent.GreaterThan(Config.CurrentVersion)) {
                DialogResult result = MessageBox.Show($"New version found: {mostRecent.Channel} {mostRecent.Number}\nDownload and install this update?", "Update Found", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes) {
                    DownloadVersion(mostRecent);
                    return true;
                }
            }

            VersionAudio mostRecentAudio = VersionAudio.GetMostRecentVersion();
            if (!Config.DisableAudioDownload && mostRecentAudio.GreaterThan(Config.CurrentAudioVersion)) {
                DialogResult result = MessageBox.Show($"New audio version found: {mostRecentAudio}.\nDownload and install this update?\n(Note, audio updates are often large downloads)", "Audio Update Found", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes) DownloadVersion(mostRecentAudio);
            }

            Launcher.UpdateStatus("No new version found.");
            return false;
        }

        /// <summary>
        /// Download a specific version.
        /// </summary>
        /// <param name="v">Version to download.</param>
        public static void DownloadVersion(IDownloadable v) {
            if (v is null) throw new ArgumentNullException(nameof(v));
            List<IDownloadable> queue = new List<IDownloadable>(new[] { v });
            VersionGameFiles vgf = v as VersionGameFiles;
            if (!Config.DisableAudioDownload && vgf.MinimumAudioVersion != null && vgf.MinimumAudioVersion.GreaterThan(Config.CurrentVersion)) {
                DialogResult result = MessageBox.Show($"New audio version found: {vgf.MinimumAudioVersion}.\nDownload and install this update?\n(Note, audio updates are often large downloads)", "Audio Update Found", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes) queue.Add(vgf.MinimumAudioVersion);
            }

            downloadForm = new DownloadForm(queue);
            downloadForm.Show();
            downloadForm.StartDownload();
        }

        /// <summary>
        /// Checks to see if the user has write access to a specific path.
        /// </summary>
        /// <param name="folderPath">Path to check.</param>
        /// <returns>True if the current user has write access to specified folder.</returns>
        public static bool HasWriteAccess(string folderPath) {
            try {
                System.Security.AccessControl.DirectorySecurity ds = Directory.GetAccessControl(folderPath);
                return true;
            } catch (UnauthorizedAccessException) {
                return false;
            }
        }

        /// <summary>
        /// Gets the version of the launcher.
        /// </summary>
        /// <returns>A string version of the game launcher version.</returns>
        public static string GetLauncherVersion() {
            string v = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            return v.Substring(0, v.Length - 2);
        }

        /// <summary>
        /// If a file already exists, gets the next available name, using numbering.
        /// </summary>
        /// <param name="path">Path to check.</param>
        /// <returns>The first available numbered filepath relative to the specified path.</returns>
        public static string GetNextAvailableFilePath(string path) {
            if (path is null) throw new ArgumentNullException(nameof(path));
            path = path.Replace('/', '\\');

            int i = 1;
            while (File.Exists(path) && i < 1000000) {
                string extension = path.Split('.')[path.Split('.').Length - 1];
                string preExtension = path.Substring(0, path.Length - extension.Length - 1);

                int numbering = -1;
                List<int> digits = new List<int>();
                int j = preExtension.Length - 1;
                while (preExtension[j] != '\\' && numbering < 0) {
                    if (j == preExtension.Length - 1 && preExtension[j] != ')') break;
                    if (j < preExtension.Length - 1) {
                        if (preExtension[j] == '(') {
                            numbering = Convert.ToInt32(string.Join(string.Empty, digits), Program.Culture);
                        } else {
                            try {
                                digits.Insert(0, Convert.ToInt16(preExtension[j].ToString(), Program.Culture));
                            } catch (FormatException) { break; }
                        }
                    }

                    j--;
                }

                if (numbering >= 0) {
                    i = numbering + 1;
                    path = preExtension.Substring(0, preExtension.Length - 3 - numbering.ToString().Length) + " (" + i + ")." + extension;
                } else path = preExtension + " (" + i + ")." + extension;
                i++;
            }

            return path;
        }

        [STAThread]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Last-ditch error catching.")]
        private static void Main() {
            try {
                CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
                Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
                Initialize();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Launcher = new Launcher();
                Application.Run(Launcher);
            } catch (Exception e) {
                using ErrorCatcher ec = new ErrorCatcher(e) {
                    Enabled = true
                };
                ec.ShowDialog();
            }
        }

        private static void Initialize() {
            Config.LoadConfig();

            if (GithubBridge.CheckForLauncherUpdate()) {
                Application.Exit();
                return;
            }

            if (!Directory.Exists(Path.Combine(Config.InstallPath, "Versions"))) {
                Directory.CreateDirectory(Path.Combine(Config.InstallPath, "Versions"));
                if (Config.CurrentVersion?.ToString().Equals("ALPHA 0.0") != false) Config.CurrentVersion = VersionGameFiles.FromString("ALPHA 0.0");
            }

            try {
                NetworkConnected = DownloadVersionManifests();
            } catch (WebException) {
                NetworkConnected = false;
            }

            return;
        }

        private static bool DownloadVersionManifests() {
            bool completed;
            string[] files = new[] {
                "http://www.mediafire.com/download/qkauu9oca3lcjw1/VersionManifest.xml",
                "http://www.mediafire.com/download/zvooruhs1b3e4c9/VersionManifestAudio.xml"
            };
            try {
                foreach (string file in files) {
                    HttpWebRequest filereq = (HttpWebRequest)WebRequest.Create(new Uri(file));
                    HttpWebResponse fileresp = (HttpWebResponse)filereq.GetResponse();
                    if (filereq.ContentLength > 0) fileresp.ContentLength = filereq.ContentLength;
                    using Stream dlstream = fileresp.GetResponseStream();
                    using FileStream outputStream = new FileStream(Path.Combine(Config.InstallPath, "Versions", Path.GetFileName(file)), FileMode.OpenOrCreate);
                    int buffersize = 1000;
                    long bytesRead = 0;
                    int length = 1;
                    while (length > 0) {
                        byte[] buffer = new byte[buffersize];
                        length = dlstream.Read(buffer, 0, buffersize);
                        bytesRead += length;
                        outputStream.Write(buffer, 0, length);
                    }
                }

                completed = true;
            } catch (WebException) {
                completed = false;
            }

            return completed;
        }
    }
}
