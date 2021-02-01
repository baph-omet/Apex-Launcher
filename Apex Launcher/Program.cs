// <copyright file="Program.cs" company="IAMVISHNU Media">
// © Copyright by IAMVISHNU Media 2020 CC BY-NC-ND
// </copyright>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;

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
                            numbering = Convert.ToInt32(string.Join(string.Empty, digits), Culture);
                        } else {
                            try {
                                digits.Insert(0, Convert.ToInt16(preExtension[j].ToString(), Culture));
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

        /// <summary>
        /// Check to see if the user has installed the game fonts.
        /// </summary>
        /// <returns>True if any of the game fonts are found to be installed, else false.</returns>
        public static bool HasFontsInstalled() {
            List<string> fontNames = new List<string>() {
                "Power Clear",
                "Power Green",
                "Power Green Small Regular",
                "Power Red and Blue Intl Regular",
                "Power Red and Blue Regular",
                "Power Red and Green Regular"
            };

            using InstalledFontCollection ifc = new InstalledFontCollection();
            foreach (FontFamily f in ifc.Families) {
                if (fontNames.Contains(f.Name)) return true;
            }

            return false;
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

            Properties.Settings.Default.DisableFontPrompt = false;

            bool dis = Properties.Settings.Default.DisableFontPrompt;

            if (!dis) {
                if (!HasFontsInstalled()) {
                    using FontInstallForm fif = new FontInstallForm();
                    fif.ShowDialog();
                    if (fif.Response != FontInstallResponse.Cancel) {
                        Properties.Settings.Default.DisableFontPrompt = true;
                    }

                    if (fif.Response == FontInstallResponse.Install) {
                        RegisterFonts(GetFontResourceNames());

                        // GetFontResourceNames().ForEach(x => RegisterFont(x));
                    }
                } else {
                    Properties.Settings.Default.DisableFontPrompt = true;
                }

                Properties.Settings.Default.Save();
            }

            return;
        }

        private static List<string> GetFontResourceNames() {
            List<string> resourceNames = new List<string>();

            foreach (string file in Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "Font"), "*.ttf")) resourceNames.Add(Path.GetFileName(file));

            // foreach (string resourceName in Assembly.GetExecutingAssembly().GetManifestResourceNames()) resourceNames.Add(resourceName);

            /*ResourceManager rm = new ResourceManager(typeof(Properties.Resources));
            ResourceSet rset = rm.GetResourceSet(CultureInfo.InvariantCulture, false, true);
            foreach (DictionaryEntry entry in rset) if (entry.Key.ToString().Contains("pkmn")) resourceNames.Add(entry.Value.ToString());*/
            return resourceNames;
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

        /*[DllImport("gdi32", EntryPoint = "AddFontResource", CharSet = CharSet.Unicode)]
        private static extern int AddFontResourceA(string lpFileName);*/

        [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
        private static extern int AddFontResource(string lpszFilename);

        /*[DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
        private static extern int CreateScalableFontResource(uint fdwHidden, string lpszFontRes, string lpszFontFile, string lpszCurrentPath);*/

        /// <summary>
        /// Installs font on the user's system and adds it to the registry so it's available on the next session
        /// Your font must be included in your project with its build path set to 'Content' and its Copy property
        /// set to 'Copy Always'.
        /// </summary>
        /// <param name="contentFontName">Your font to be passed as a resource (i.e. "myfont.tff").</param>
        private static void RegisterFont(string contentFontName) {
            // Creates the full path where your font will be installed
            string fontDestination = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), contentFontName);

            if (!File.Exists(fontDestination)) {
                // Copies font to destination
                bool isElevated;
                using (WindowsIdentity identity = WindowsIdentity.GetCurrent()) {
                    WindowsPrincipal principal = new WindowsPrincipal(identity);
                    isElevated = principal.IsInRole(WindowsBuiltInRole.Administrator);
                }

                if (!isElevated) {
                    ProcessStartInfo psi = new ProcessStartInfo("Copy.bat", $"{contentFontName} {fontDestination}") { Verb = "runas" };
                    Process.Start(psi);
                    Thread.Sleep(1000);
                } else File.Copy(Path.Combine(Directory.GetCurrentDirectory(), "Font", contentFontName), fontDestination, true);

                // Retrieves font name
                // Makes sure you reference System.Drawing
                using PrivateFontCollection fontCol = new PrivateFontCollection();
                fontCol.AddFontFile(fontDestination);
                string actualFontName = fontCol.Families[0].Name;

                // Add font
                _ = AddFontResource(fontDestination);

                // Add registry entry
                if (isElevated) Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", actualFontName, contentFontName, RegistryValueKind.String);
                else {
                    ProcessStartInfo psi = new ProcessStartInfo(Path.Combine(Directory.GetCurrentDirectory(), "RegEdit.bat"), $"{actualFontName} {contentFontName}") { Verb = "runas" };
                    Process.Start(psi);
                    Thread.Sleep(1000);
                }
            }
        }

        private static void RegisterFonts(List<string> fontFiles) {
            bool isElevated;
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent()) {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                isElevated = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }

            // Copy font to destination
            if (isElevated) {
                fontFiles.ForEach(x => {
                    string fontDestination = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), x);
                    File.Copy(Path.Combine(Directory.GetCurrentDirectory(), "Font", x), fontDestination, true);
                });
            } else {
                ProcessStartInfo psi = new ProcessStartInfo("Copy.bat", Environment.GetFolderPath(Environment.SpecialFolder.Fonts)) { Verb = "runas" };
                Process.Start(psi);
                Thread.Sleep(1000);
            }

            List<string> lines = new List<string>() { "@echo off" };

            // Registry stuff
            fontFiles.ForEach(x => {
                string fontDestination = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), x);

                // Retrieves font name
                // Makes sure you reference System.Drawing
                using PrivateFontCollection fontCol = new PrivateFontCollection();
                fontCol.AddFontFile(fontDestination);
                string actualFontName = fontCol.Families[0].Name;

                // Add font
                _ = AddFontResource(fontDestination);
                if (isElevated) Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", actualFontName, x, RegistryValueKind.String);
                else lines.Add($@"REG ADD 'HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts' {actualFontName} /t REG_SZ {x}");
            });

            if (!isElevated) {
                File.WriteAllLines(Path.Combine(Directory.GetCurrentDirectory(), "RegEdit.bat"), lines);
                ProcessStartInfo psi = new ProcessStartInfo("RegEdit.bat") { Verb = "runas" };
                Process.Start(psi);
                Thread.Sleep(1000);
            }
        }
    }
}
