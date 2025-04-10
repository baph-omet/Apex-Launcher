﻿// <copyright file="Program.cs" company="Baphomet Media">
// © Copyright by Baphomet Media 2020 CC BY-NC-ND
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ApexLauncher.Properties;
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
                DialogResult result = MessageBox.Show($"New audio version found: {mostRecentAudio}.\nDownload and install this update?", "Audio Update Found", MessageBoxButtons.YesNo);
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
            ArgumentNullException.ThrowIfNull(v);
            List<IDownloadable> queue = new([v]);
            if (v is VersionGameFiles) {
                VersionGameFiles vgf = v as VersionGameFiles;
                if (!Config.DisableAudioDownload && vgf?.MinimumAudioVersion != null && vgf.MinimumAudioVersion.GreaterThan(Config.CurrentVersion)) {
                    queue.Add(vgf.MinimumAudioVersion);
                }
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
                System.Security.AccessControl.DirectorySecurity ds = new DirectoryInfo(folderPath).GetAccessControl();
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
            return v[..^2];
        }

        /// <summary>
        /// If a file already exists, gets the next available name, using numbering.
        /// </summary>
        /// <param name="path">Path to check.</param>
        /// <returns>The first available numbered filepath relative to the specified path.</returns>
        public static string GetNextAvailableFilePath(string path) {
            ArgumentNullException.ThrowIfNull(path);
            path = path.Replace('/', '\\');

            int i = 1;
            while (File.Exists(path) && i < 1000000) {
                string extension = path.Split('.')[^1];
                string preExtension = path[..(path.Length - extension.Length - 1)];

                int numbering = -1;
                List<int> digits = [];
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
                    path = preExtension[..(preExtension.Length - 3 - numbering.ToString().Length)] + " (" + i + ")." + extension;
                } else path = $"{preExtension} ({i}).{extension}";
                i++;
            }

            return path;
        }

        /// <summary>
        /// Check to see if the user has installed the game fonts.
        /// </summary>
        /// <returns>True if any of the game fonts are found to be installed, else false.</returns>
        public static bool HasFontsInstalled() {
            List<string> fontNames = [
                "Power Clear",
                "Power Green",
                "Power Green Small Regular",
                "Power Red and Blue Intl Regular",
                "Power Red and Blue Regular",
                "Power Red and Green Regular",
            ];

            using InstalledFontCollection ifc = new();
            foreach (FontFamily f in ifc.Families) {
                if (fontNames.Contains(f.Name)) return true;
            }

            return false;
        }

        [STAThread]
        private static void Main() {
            try {
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
                Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
                Initialize();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                CheckFonts();
                Launcher = new Launcher();
                Application.Run(Launcher);
            } catch (Exception e) {
                using ErrorCatcher ec = new(e) { Enabled = true };
                ec.ShowDialog();
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
            using ErrorCatcher ec = new((Exception)e.ExceptionObject) { Enabled = true };
            ec.ShowDialog();
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
                NetworkConnected = DownloadVersionManifests().Result;
            } catch (WebException) {
                NetworkConnected = false;
            }

            return;
        }

        private static void CheckFonts() {
#if DEBUG
            Config.DisableFontPrompt = false;
#endif

            bool dis = Config.DisableFontPrompt;

            if (!dis) {
                if (!HasFontsInstalled()) {
                    using FontInstallForm fif = new();
                    fif.ShowDialog();
                    if (fif.Response != FontInstallResponse.Cancel) Config.DisableFontPrompt = true;
                    if (fif.Response == FontInstallResponse.Install) {
                        ExtractFonts();
                        foreach (string fontName in GetFontResourceNames()) {
                            Process.Start(new ProcessStartInfo() {
                                FileName = "fontview",
                                Arguments = fontName,
                                CreateNoWindow = true,
                                WorkingDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Font"),
                            });
                        }
                    }
                } else Config.DisableFontPrompt = true;
            }
        }

        private static void ExtractFonts() {
            ResourceManager rm = new(typeof(Resources));
            ResourceSet rs = rm.GetResourceSet(CultureInfo.CurrentCulture, true, true);
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Font");
            Directory.CreateDirectory(path);
            foreach (DictionaryEntry entry in rs) {
                string key = entry.Key.ToString();
                if (key.Contains("pkmn")) File.WriteAllBytes(Path.Combine(path, $"{key}.ttf"), entry.Value as byte[]);
            }
        }

        private static List<string> GetFontResourceNames() {
            List<string> resourceNames = [];
            foreach (string file in Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "Font"), "*.ttf")) resourceNames.Add(Path.GetFileName(file));
            return resourceNames;
        }

        private static async Task<bool> DownloadVersionManifests() {
            bool completed;
            string[] files = [
                "https://raw.githubusercontent.com/baph-omet/Apex-Launcher/refs/heads/master/Apex%20Launcher/VersionManifest.xml",
                "https://raw.githubusercontent.com/baph-omet/Apex-Launcher/refs/heads/master/Apex%20Launcher/VersionManifestAudio.xml",
            ];
            try {
                HttpClient client = new();
                foreach (string file in files) {
                    HttpResponseMessage response = await client.GetAsync(file);
                    string body = await response.Content.ReadAsStringAsync();
                    using Stream dlstream = await client.GetStreamAsync(file);
                    using FileStream outputStream = new(Path.Combine(Config.InstallPath, "Versions", Path.GetFileName(file)), FileMode.OpenOrCreate);
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
            } catch (Exception e) {
                if (e is WebException or HttpRequestException || (e is AggregateException && e.InnerException is WebException or HttpRequestException)) {
                    completed = false;
                } else throw;
            }

            return completed;
        }
    }
}
