using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Apex_Launcher {
    public static class Config {
        private static bool _loaded = false;
        private static string Filepath => Path.Combine(Directory.GetCurrentDirectory(), "config.txt");

        private static VersionGameFiles _currentVersion;
        public static VersionGameFiles CurrentVersion {
            get {
                return _currentVersion;
            }
            set {
                _currentVersion = value;
                if (_loaded) SaveConfig();
            }
        }

        private static VersionAudio _currentVersionAudio;
        public static VersionAudio CurrentAudioVersion {
            get {
                return _currentVersionAudio;
            }
            set {
                _currentVersionAudio = value;
                if (_loaded) SaveConfig();
            }
        }

        private static string _installPath;
        public static string InstallPath {
            get {
                if (string.IsNullOrEmpty(_installPath)) return Directory.GetCurrentDirectory();
                return _installPath;
            }
            set {
                _installPath = value;
                if (_loaded) SaveConfig();
            }
        }

        private static bool _keepLauncherOpen;
        public static bool KeepLauncherOpen {
            get {
                return _keepLauncherOpen;
            }
            set {
                _keepLauncherOpen = value;
                if (_loaded) SaveConfig();
            }
        }

        private static bool _disableAudioDownload;
        public static bool DisableAudioDownload {
            get {
                return _disableAudioDownload;
            }
            set {
                _disableAudioDownload = value;
                if (_loaded) SaveConfig();
            }
        }

        public static void LoadConfig() {
            if (!File.Exists(Filepath)) CreateConfig();
            foreach (string line in File.ReadAllLines(Filepath)) {
                if (line.Length == 0 || new[] { '\n', ' ', '#' }.Contains(line[0]) || !line.Contains('=')) continue;
                string[] split = line.Split('=');
                string param = split[0].Trim();
                string value = split[1].Trim();

                foreach (PropertyInfo pi in typeof(Config).GetProperties()) {
                    if (pi.Name.ToLower() == param.ToLower()) {
                        try {
                            object v = null;
                            if (pi.PropertyType.GetInterfaces().Contains(typeof(IDownloadable))) {
                                if (pi.PropertyType == typeof(VersionGameFiles)) v = VersionGameFiles.FromString(value);
                                if (pi.PropertyType == typeof(VersionAudio)) v = VersionAudio.FromString(value);
                            } else v = Convert.ChangeType(value, pi.PropertyType);

                            pi.SetValue(null, v);
                        } catch (Exception) {
                            pi.SetValue(null, default);
                        }
                        break;
                    }
                }
            }

            _loaded = true;
        }

        public static void SaveConfig() {
            List<string> lines = new List<string>();
            foreach (PropertyInfo pi in typeof(Config).GetProperties()) lines.Add($"{pi.Name}={pi.GetValue(null)}");
            File.WriteAllLines(Filepath, lines);
        }

        public static void CreateConfig() {
            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Apex_Launcher.config.txt");
            using FileStream fileStream = new FileStream(Filepath, FileMode.CreateNew);
            for (int i = 0; i < stream.Length; i++) fileStream.WriteByte((byte)stream.ReadByte());
        }

        public static string GetSystemConfigurationPaste() {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("# Configuration");
            try {
                sb.AppendLine($"* Current Launcher Version: {Assembly.GetExecutingAssembly().GetName().Version}");
                sb.AppendLine($"* Current Game Version: {CurrentVersion}");
                sb.AppendLine($"* Install Path: {InstallPath}");
            } catch (Exception) { }
            try {
                sb.AppendLine();
                sb.AppendLine("# Files");
                if (File.Exists(Directory.GetCurrentDirectory() + "\\config.txt")) sb.AppendLine("* `config.txt`");

                string installpath = InstallPath;
                if (Directory.Exists(installpath)) {
                    sb.AppendLine("* " + installpath + ":");
                    if (Directory.Exists(installpath + "\\Versions")) {
                        foreach (string folder in Directory.GetDirectories(installpath + "\\Versions")) {
                            sb.AppendLine("    * " + Path.GetFileName(folder) + ":");
                            foreach (string subfolder in Directory.GetDirectories(folder)) sb.AppendLine("        * " + Path.GetFileName(subfolder));
                            foreach (string contents in Directory.GetFiles(folder)) sb.AppendLine("        * `" + Path.GetFileName(contents) + "`");
                        }
                    }
                }
            } catch (Exception) { }
            try {
                sb.AppendLine();
                sb.AppendLine("# Environment");
                sb.AppendLine($"* Locale: {CultureInfo.CurrentCulture.Name}");
                sb.AppendLine($"* Operating System: {Environment.OSVersion.VersionString}");
                sb.AppendLine($"* .NET Runtime Version: {Environment.Version}");
            } catch (Exception e) {
                sb.AppendLine($"Couldn't get Environment info: \n{e}");
            }
            return sb.ToString();
        }
    }
}
