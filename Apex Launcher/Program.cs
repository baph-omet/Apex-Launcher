using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Xml;
using System.IO.Compression;

namespace Apex_Launcher {
    static class Program {

        private static Launcher launcher;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            launcher = new Launcher();
            Application.Run(launcher);
        }

        public static void initialize() {
            if (!File.Exists(Directory.GetCurrentDirectory() + "\\config.txt")) {
                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Apex_Launcher.config.txt")) {
                    using (FileStream fileStream = new FileStream(Directory.GetCurrentDirectory() + "\\config.txt", FileMode.CreateNew)) {
                        for (int i = 0; i < stream.Length; i++) fileStream.WriteByte((byte)stream.ReadByte());
                    }
                }
            }

            //TODO: check for launcher update

            InstallLatestVersion();

            return;
        }

        //public static void CheckForLauncherUpdate()

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

        public static Version GetCurrentVersion() {
            return Version.FromString(GetParameter("currentversion"));
        }

        public static Version InstallLatestVersion() {
            launcher.UpdateStatus("Checking for new versions...");

            XmlDocument doc = new XmlDocument();
            doc.Load("https://raw.githubusercontent.com/griffenx/Apex-Launcher/master/Apex%20Launcher/VersionManifest.xml");

            Version mostRecent = GetCurrentVersion();
            foreach (XmlNode node in doc.GetElementsByTagName("version")) {
                Channel channel = Channel.NONE;
                double number = 0.0;
                string location = "";

                foreach (XmlNode prop in node.ChildNodes) {
                    switch (prop.Name.ToLower()) {
                        case "channel":
                            switch (prop.InnerText[0]) {
                                case 'a':
                                    channel = Channel.ALPHA;
                                    break;
                                case 'b':
                                    channel = Channel.BETA;
                                    break;
                                case 'r':
                                    channel = Channel.RELEASE;
                                    break;
                                default:
                                    channel = Channel.NONE;
                                    break;
                            }
                            break;
                        case "number":
                            try {
                                number = Convert.ToDouble(prop.InnerText);
                            } catch (FormatException) {
                                number = 0.0;
                            }
                            break;
                        case "location":
                            location = prop.InnerText;
                            break;
                    }
                }
                Version v = new Version(channel, number, location);
                if (mostRecent == null || v.GreaterThan(mostRecent)) mostRecent = v;

            }

            if (mostRecent != null && mostRecent.GreaterThan(GetCurrentVersion())) {
                DialogResult result = MessageBox.Show("New version found: " + mostRecent.Channel.ToString() + " " + mostRecent.Number + "\nDownload and install this update?", "Update Found", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes) {
                    DownloadVersion(mostRecent);
                    SetParameter("currentversion", mostRecent.ToString());
                    launcher.SetGameVersion(mostRecent);
                    return mostRecent;
                } else return null;
            }

            launcher.UpdateStatus("No new version found.");
            return null;
        }

        public static void DownloadVersion(Version v) {
            launcher.UpdateStatus("Downloading version " + v.ToString());
            WebClient wc = new WebClient();
            string installpath = GetInstallPath();
            string filename = installpath + "\\Versions\\" + v.ToString();

            Directory.CreateDirectory(installpath + "\\Versions");

            wc.DownloadFile(v.Location, filename + ".zip");

            launcher.UpdateStatus("Exctracting version " + v.ToString());
            ZipFile.ExtractToDirectory(filename + ".zip",filename);
            File.Delete(filename + ".zip");
        }

        public static string GetInstallPath() {
            string installpath = GetParameter("installpath");
            if (installpath.Length == 0) installpath = Directory.GetCurrentDirectory();
            return installpath;
        }

        public static bool hasWriteAccess(string folderPath) {
            try {
                System.Security.AccessControl.DirectorySecurity ds = Directory.GetAccessControl(folderPath);
                return true;
            } catch (UnauthorizedAccessException) {
                return false;
            }
        }
    }
}
