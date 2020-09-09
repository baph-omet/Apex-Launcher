using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows.Forms;

namespace Apex_Launcher {
    static class Program {
        private static DownloadForm DownloadForm = null;

        public static Launcher Launcher;
        public static bool NetworkConnected;
        public static bool ForceUpdate = false;
        public static bool Downloading {
            get {
                if (DownloadForm == null) return false;
                return DownloadForm.Downloading;
            }
        }

        public static CultureInfo Culture = new CultureInfo("en-US");

        [STAThread]
        static void Main() {
            try {
                CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
                Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
                Initialize();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Launcher = new Launcher();
                Application.Run(Launcher);
            } catch (Exception e) {
                ErrorCatcher ec = new ErrorCatcher(e) {
                    Enabled = true
                };
                ec.ShowDialog();
            }
        }

        public static void Initialize() {
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

            // Remove or add fonts
            /*foreach (string filepath in Directory.GetFiles(GetInstallPath() + "\\Versions\\" + GetCurrentVersion().ToString() + "\\Fonts")) {
                if (filepath.Contains(".ttf")) {

                    string filename = filepath.Split('\\')[filepath.Split('\\').Length - 1];
                    if (Convert.ToBoolean(GetParameter("disableGameFonts"),Program.Culture)) {

                        if (File.Exists("C:\\Windows\\Fonts\\" + filename)) {
                            File.Delete("C:\\Windows\\Fonts\\" + filename);
                            
                        }
                    } else {
                        if (!File.Exists("C:\\Windows\\Fonts\\" + filename)) {
                            File.Copy(filepath, "C:\\Windows\\Fonts\\" + filename);
                        }
                    }
                }
            }*/
            return;
        }

        public static bool DownloadVersionManifests() {
            bool completed;
            string[] files = new[] {
                "http://www.mediafire.com/download/qkauu9oca3lcjw1/VersionManifest.xml",
                "http://www.mediafire.com/download/zvooruhs1b3e4c9/VersionManifestAudio.xml"
            };
            try {
                foreach (string file in files) {
                    HttpWebRequest filereq = (HttpWebRequest)WebRequest.Create(file);
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
            } catch (Exception) {
                completed = false;
            }
            return completed;
        }

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

        public static void DownloadVersion(IDownloadable v) {
            List<IDownloadable> queue = new List<IDownloadable>(new[] { v });
            VersionGameFiles vgf = v as VersionGameFiles;
            if (!Config.DisableAudioDownload && vgf.MinimumAudioVersion != null && vgf.MinimumAudioVersion.GreaterThan(Config.CurrentVersion)) {
                DialogResult result = MessageBox.Show($"New audio version found: {vgf.MinimumAudioVersion}.\nDownload and install this update?\n(Note, audio updates are often large downloads)", "Audio Update Found", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes) queue.Add(vgf.MinimumAudioVersion);
            }

            DownloadForm = new DownloadForm(queue);
            DownloadForm.Show();
            DownloadForm.StartDownload();
        }

        public static bool HasWriteAccess(string folderPath) {
            try {
                System.Security.AccessControl.DirectorySecurity ds = Directory.GetAccessControl(folderPath);
                return true;
            } catch (UnauthorizedAccessException) {
                return false;
            }
        }

        public static string GetLauncherVersion() {
            string v = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            return v.Substring(0, v.Length - 2);
        }

        public static string GetNextAvailableFilePath(string path) {
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
                            numbering = Convert.ToInt32(string.Join("", digits), Program.Culture);
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
    }
}
