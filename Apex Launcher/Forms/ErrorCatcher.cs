using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Apex_Launcher {
    public partial class ErrorCatcher : Form {
        private readonly Exception exception;

        public ErrorCatcher(Exception e) {
            exception = e;
            InitializeComponent();
            Enabled = true;
            Initialize();
        }

        private void Initialize() {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("# Exception Details");
            sb.AppendLine(exception.ToString());
            sb.AppendLine();
            sb.AppendLine("# Configuration");
            try {
                sb.AppendLine($"* Current Launcher Version: {Assembly.GetExecutingAssembly().GetName().Version}");
                sb.AppendLine($"* Current Game Version: {Config.CurrentVersion}");
                sb.AppendLine($"* Install Path: {Config.InstallPath}");
            } catch (Exception) { }
            try {
                sb.AppendLine();
                sb.AppendLine("# Files");
                if (File.Exists(Directory.GetCurrentDirectory() + "\\config.txt")) sb.AppendLine("* `config.txt`");

                string installpath = Config.InstallPath;
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
            DetailsBox.Text = sb.ToString();
        }

        private void CopyButton_Click(object sender, EventArgs e) {
            Clipboard.SetText(DetailsBox.Text);
        }

        private void LinkButton_Click(object sender, EventArgs e) {
            Process.Start("https://github.com/griffenx/Apex-Launcher/issues/new" + $"?title={exception.GetType().Name} in {exception.TargetSite.Name}&body={Uri.EscapeDataString(DetailsBox.Text)}");
        }

        private void ButtonViewIssues_Click(object sender, EventArgs e) {
            Process.Start("https://github.com/griffenx/Apex-Launcher/issues");
        }
    }
}
