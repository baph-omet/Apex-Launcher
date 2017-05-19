using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Net;
using System.Globalization;

namespace Apex_Launcher {
    static class Program {

        public static Launcher Launcher;
        public static bool NetworkConnected;
        public static bool ForceUpdate = false;
        public static bool Downloading = false;

        public static CultureInfo Culture = new CultureInfo("en-US");
        
        [STAThread]
        static void Main() {
            try {
                CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
                initialize();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Launcher = new Launcher();
                Application.Run(Launcher);
            } catch(Exception e) {
                ErrorCatcher ec = new ErrorCatcher(e);
                ec.ShowDialog();
            }
        }

        public static void initialize() {
            if (!File.Exists(Directory.GetCurrentDirectory() + "\\config.txt")) {
                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Apex_Launcher.config.txt")) {
                    using (FileStream fileStream = new FileStream(Directory.GetCurrentDirectory() + "\\config.txt", FileMode.CreateNew)) {
                        for (int i = 0; i < stream.Length; i++) fileStream.WriteByte((byte)stream.ReadByte());
                    }
                }
            }

            if (GithubBridge.CheckForLauncherUpdate()) {
                Application.Exit();
                return;
            }

            if (!Directory.Exists(GetInstallPath() + "\\Versions")) {
                Directory.CreateDirectory(GetInstallPath() + "\\Versions");
                if (!GetParameter("currentversion").Equals("ALPHA 0.0")) SetParameter("currentversion", "ALPHA 0.0");
            }
            try {
                NetworkConnected = DownloadVersionManifest();
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

        public static string GetParameter(string parameter) {
            foreach (string line in File.ReadAllLines(Directory.GetCurrentDirectory() + "\\config.txt")) {
                if (line.Length > 0 && !(new[] { '\n', ' ', '#' }.Contains(line[0])) && line.Contains('=')) {
                    if (line.Split('=')[0].ToLower().Equals(parameter.ToLower())) {
                        return line.Split('=')[1];
                    }
                }
            }
            return null;
        }

        public static void SetParameter(string parameter, string value) {
            if (GetParameter(parameter) == null) {
                File.AppendAllText(Directory.GetCurrentDirectory() + "\\config.txt", parameter + "=" + value + "\n");
            } else {
                string[] lines = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\config.txt");
                int foundline = -1;
                foreach (string line in lines) {
                    if (line.Length > 0 && !(new[] { '\n', ' ', '#' }.Contains(line[0])) && line.Contains('=')) {
                        if (line.Split('=')[0].ToLower().Equals(parameter.ToLower())) {
                            foundline = Array.IndexOf(lines, line);
                            break;
                        }
                    }
                }
                if (foundline > -1) {
                    lines[foundline] = parameter + "=" + value;
                    File.WriteAllLines(Directory.GetCurrentDirectory() + "\\config.txt", lines);
                }
            }
        }

        public static bool DownloadVersionManifest() {
            bool completed = false;
            try {
                HttpWebRequest filereq = (HttpWebRequest)HttpWebRequest.Create("http://www.mediafire.com/download/qkauu9oca3lcjw1/VersionManifest.xml");
                HttpWebResponse fileresp = (HttpWebResponse)filereq.GetResponse();
                if (filereq.ContentLength > 0) fileresp.ContentLength = filereq.ContentLength;
                using (Stream dlstream = fileresp.GetResponseStream()) {
                    using (FileStream outputStream = new FileStream(GetInstallPath() + "\\Versions\\VersionManifest.xml", FileMode.OpenOrCreate)) {
                        int buffersize = 1000;
                        long bytesRead = 0;
                        int length = 1;
                        while (length > 0) {
                            byte[] buffer = new Byte[buffersize];
                            length = dlstream.Read(buffer, 0, buffersize);
                            bytesRead += length;
                            outputStream.Write(buffer, 0, length);
                        }
                    }
                }
                completed = true;
            } catch(Exception) {
                completed = false;
            }
            return completed;
        }

        public static Version GetCurrentVersion() {
            return Version.FromString(GetParameter("currentversion"));
        }

        public static bool InstallLatestVersion() {
            Launcher.UpdateStatus("Checking for new versions...");

            Version mostRecent = Version.GetMostRecentVersion();

            if (mostRecent != null && mostRecent.GreaterThan(GetCurrentVersion())) {
                DialogResult result = MessageBox.Show("New version found: " + mostRecent.Channel.ToString() + " " + mostRecent.Number +
                    "\nDownload and install this update?", "Update Found", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes) {
                    DownloadVersion(mostRecent);
                    return true;
                } else return false;
            }

            Launcher.UpdateStatus("No new version found.");
            return false;
        }

        public static void DownloadVersion(Version v) {
            DownloadForm dlf = new DownloadForm(v);
            dlf.Show();
            dlf.StartDownload();
        }

        public static string GetInstallPath() {
            string installpath = GetParameter("installpath");
            if (installpath.Length == 0) installpath = Directory.GetCurrentDirectory();
            return installpath;
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
            return v.Substring(0,v.Length - 2);
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
                while (preExtension[j]!='\\' && numbering < 0) {
                    if (j == preExtension.Length - 1 && preExtension[j] != ')') break;
                    if (j < preExtension.Length - 1) {
                        if (preExtension[j] == '(') {
                            numbering = Convert.ToInt32(String.Join("", digits), Program.Culture);
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
                    path = preExtension.Substring(0,preExtension.Length - 3 - numbering.ToString().Length) + " (" + i + ")." + extension;
                } else path = preExtension + " (" + i + ")." + extension;
                i++;
            }

            return path;
        }
    }
}
