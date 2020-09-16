// <copyright file="Config.cs" company="IAMVISHNU Media">
// © Copyright by IAMVISHNU Media 2020 CC BY-NC-ND
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ApexLauncher {
    /// <summary>
    /// Static class to contain the program's configuration settings.
    /// </summary>
    public static class Config {
        private static bool loaded;

        private static VersionGameFiles currentVersion;

        private static VersionAudio currentVersionAudio;

        private static string installPath;

        private static bool keepLauncherOpen;

        private static bool disableAudioDownload;

        /// <summary>
        /// Gets or sets the currently installed version of the game's files.
        /// </summary>
        public static VersionGameFiles CurrentVersion {
            get {
                return currentVersion;
            }

            set {
                currentVersion = value;
                if (loaded) SaveConfig();
            }
        }

        /// <summary>
        /// Gets or sets the currently installed version of the game's audio files.
        /// </summary>
        public static VersionAudio CurrentAudioVersion {
            get {
                return currentVersionAudio;
            }

            set {
                currentVersionAudio = value;
                if (loaded) SaveConfig();
            }
        }

        /// <summary>
        /// Gets or sets the program's installation file path.
        /// </summary>
        public static string InstallPath {
            get {
                if (string.IsNullOrEmpty(installPath)) return Directory.GetCurrentDirectory();
                return installPath;
            }

            set {
                installPath = value;
                if (loaded) SaveConfig();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether launcher should be left open after launching the game proper.
        /// </summary>
        public static bool KeepLauncherOpen {
            get {
                return keepLauncherOpen;
            }

            set {
                keepLauncherOpen = value;
                if (loaded) SaveConfig();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether audio downloads should be disabled.
        /// </summary>
        public static bool DisableAudioDownload {
            get {
                return disableAudioDownload;
            }

            set {
                disableAudioDownload = value;
                if (loaded) SaveConfig();
            }
        }

        private static string Filepath => Path.Combine(Directory.GetCurrentDirectory(), "config.txt");

        /// <summary>
        /// Load underlying configuration file to memory.
        /// </summary>
        public static void LoadConfig() {
            if (!File.Exists(Filepath)) CreateConfig();
            foreach (string line in File.ReadAllLines(Filepath)) {
                if (line.Length == 0 || new[] { '\n', ' ', '#' }.Contains(line[0]) || !line.Contains('=')) continue;
                string[] split = line.Split('=');
                string param = split[0].Trim();
                string value = split[1].Trim();

                foreach (PropertyInfo pi in typeof(Config).GetProperties()) {
                    if (pi.Name.ToLower(Program.Culture) == param.ToLower(Program.Culture)) {
                        try {
                            object v = null;
                            if (pi.PropertyType.GetInterfaces().Contains(typeof(IDownloadable))) {
                                if (pi.PropertyType == typeof(VersionGameFiles)) v = VersionGameFiles.FromString(value);
                                if (pi.PropertyType == typeof(VersionAudio)) v = VersionAudio.FromString(value);
                            } else v = Convert.ChangeType(value, pi.PropertyType);

                            pi.SetValue(null, v);
                        } catch (FormatException) {
                            pi.SetValue(null, default);
                        }

                        break;
                    }
                }
            }

            loaded = true;
        }

        /// <summary>
        /// Save configuration variables to underlying file.
        /// </summary>
        public static void SaveConfig() {
            List<string> lines = new List<string>();
            foreach (PropertyInfo pi in typeof(Config).GetProperties()) lines.Add($"{pi.Name}={pi.GetValue(null)}");
            File.WriteAllLines(Filepath, lines);
        }

        /// <summary>
        /// Create a default config file.
        /// </summary>
        public static void CreateConfig() {
            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ApexLauncher.config.txt");
            using FileStream fileStream = new FileStream(Filepath, FileMode.CreateNew);
            for (int i = 0; i < stream.Length; i++) fileStream.WriteByte((byte)stream.ReadByte());
        }

        /// <summary>
        /// Gets a block of text that contains system data for debugging.
        /// </summary>
        /// <returns>String block that contains a bunch of system information.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Typically used only in debugging.")]
        public static string GetSystemConfigurationPaste() {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("# Configuration");
            sb.AppendLine($"* Current Launcher Version: {Assembly.GetExecutingAssembly().GetName().Version}");
            sb.AppendLine($"* Current Game Version: {CurrentVersion}");
            sb.AppendLine($"* Install Path: {InstallPath}");
            try {
                sb.AppendLine();
                sb.AppendLine("# Files");
                if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "config.txt"))) sb.AppendLine("* `config.txt`");

                string installpath = InstallPath;
                if (Directory.Exists(installpath)) {
                    sb.AppendLine($"* {installpath}:");
                    if (Directory.Exists(Path.Combine(installpath, "Versions"))) {
                        foreach (string folder in Directory.GetDirectories(Path.Combine(installpath, "Versions"))) {
                            sb.AppendLine($"    * {Path.GetDirectoryName(folder)}:");
                            foreach (string subfolder in Directory.GetDirectories(folder)) sb.AppendLine($"        * {Path.GetDirectoryName(subfolder)}");
                            foreach (string contents in Directory.GetFiles(folder)) sb.AppendLine($"        * `{Path.GetFileName(contents)}`");
                        }
                    }
                }
            } catch (Exception) { }
            sb.AppendLine();
            sb.AppendLine("# Environment");
            sb.AppendLine($"* Locale: {Program.Culture.Name}");
            sb.AppendLine($"* Operating System: {Environment.OSVersion.VersionString}");
            sb.AppendLine($"* .NET Runtime Version: {Environment.Version}");

            return sb.ToString();
        }
    }
}
