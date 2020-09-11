using System;
using System.Diagnostics;
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

            sb.AppendLine(Config.GetSystemConfigurationPaste());
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
