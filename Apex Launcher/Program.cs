using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Apex_Launcher {
    static class Program {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Launcher());
        }

        public static bool initialize() {
            if (!File.Exists(Directory.GetCurrentDirectory() + "\\config.txt")) {
                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Apex_Launcher.config.txt")) {
                    using (FileStream fileStream = new FileStream(Directory.GetCurrentDirectory() + "\\config.txt", FileMode.CreateNew)) {
                        for (int i = 0; i < stream.Length; i++) fileStream.WriteByte((byte)stream.ReadByte());
                    }
                }
            }

            //TODO: check for launcher update

            //TODO: check for game udpate

            // if updates are completed
            //launcher.EnableLaunch();

            return true;

            //TODO: return false at some point if an error was encountered

        }

        //public static void CheckForLauncherUpdate()

        /*public static bool CheckForGameUpdate() {

        }*/

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

        public static string GetLatestVersionNumber() {

        }
    }
}
