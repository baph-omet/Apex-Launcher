using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Apex_Launcher {
    public partial class ErrorCatcher : Form {
        private Exception exception;

        public ErrorCatcher(Exception e) {
            exception = e;
            InitializeComponent();

            Initialize();
        }

        private void Initialize() {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("# Exception Details");
            sb.AppendLine(exception.ToString());
            sb.AppendLine();
            sb.AppendLine("# Configuration");
            try {
                sb.AppendLine("* Current Launcher Version: " + Assembly.GetExecutingAssembly().GetName().Version.ToString());
                sb.AppendLine("* Current Game Version: " + Program.GetParameter("currentversion"));
                sb.AppendLine("* Install Path: " + Program.GetParameter("installpath"));
            } catch (Exception) { }
            try {
                sb.AppendLine();
                sb.AppendLine("# Files");
                if (File.Exists(Directory.GetCurrentDirectory() + "\\config.txt")) sb.AppendLine("* `config.txt`");

                string installpath = Program.GetParameter("installpath");
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
                sb.AppendLine("* Locale: " + CultureInfo.CurrentCulture.Name);
                sb.AppendLine("* Operating System: " + Environment.OSVersion.VersionString);
            } catch (Exception) { }
            DetailsBox.Text = sb.ToString();
        }
        
        private void CopyButton_Click(object sender, EventArgs e) {
            /*Thread t = new Thread(CopyToClipboard);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();*/
            Clipboard.SetText(DetailsBox.Text);
        }

        /*public static void CopyToClipboard() {
            Clipboard.SetText(Details)
        }*/

        private void LinkButton_Click(object sender, EventArgs e) {
            Process.Start("https://github.com/griffenx/Apex-Launcher/issues/new");
        }
    }
}
