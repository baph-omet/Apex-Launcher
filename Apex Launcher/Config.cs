using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Apex_Launcher {
    public static class Config {
        private static string Filepath => Path.Combine(Directory.GetCurrentDirectory(), "config.txt");

        private static VersionGameFiles _currentVersion;
        public static VersionGameFiles CurrentVersion {
            get {
                return _currentVersion;
            }
            set {
                _currentVersion = value;
                SaveConfig();
            }
        }

        private static VersionAudio _currentVersionAudio;
        public static VersionAudio CurrentAudioVersion {
            get {
                return _currentVersionAudio;
            }
            set {
                _currentVersionAudio = value;
                SaveConfig();
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
                SaveConfig();
            }
        }

        private static bool _keepLauncherOpen;
        public static bool KeepLauncherOpen {
            get {
                return _keepLauncherOpen;
            }
            set {
                _keepLauncherOpen = value;
                SaveConfig();
            }
        }

        private static bool _disableAudioDownload;
        public static bool DisableAudioDownload {
            get {
                return _disableAudioDownload;
            }
            set {
                _disableAudioDownload = value;
                SaveConfig();
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
                    if (pi.Name.ToLower() == param) {
                        try {
                            object v = null;
                            if (pi.PropertyType.IsAssignableFrom(typeof(IDownloadable))) {
                                if (pi.PropertyType == typeof(VersionGameFiles)) v = VersionGameFiles.FromString(value);
                                if (pi.PropertyType == typeof(VersionAudio)) v = VersionAudio.FromString(value);
                            } else pi.SetValue(null, Convert.ChangeType(value, pi.PropertyType));
                        } catch (Exception) {
                            pi.SetValue(null, default);
                        }
                        break;
                    }
                }
            }
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
    }
}
