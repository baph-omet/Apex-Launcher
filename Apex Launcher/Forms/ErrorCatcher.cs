// <copyright file="ErrorCatcher.cs" company="Baphomet Media">
// © Copyright by Baphomet Media 2020 CC BY-NC-ND
// </copyright>

using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ApexLauncher {
    /// <summary>
    /// Form for showing unhandled exceptions and allowing users to report to the GitHub.
    /// </summary>
    public partial class ErrorCatcher : Form {
        private readonly Exception exception;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorCatcher"/> class.
        /// </summary>
        /// <param name="e">The Exception that this form is catching.</param>
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
            Process.Start("https://github.com/iamvishnu-media/Apex-Launcher/issues/new" + $"?title={exception.GetType().Name} in {exception.StackTrace.Split(new[] { Environment.NewLine }, StringSplitOptions.None).Last().Split(' ')[1]}&body={Uri.EscapeDataString(DetailsBox.Text)}");
        }

        private void ButtonViewIssues_Click(object sender, EventArgs e) {
            Process.Start("https://github.com/iamvishnu-media/Apex-Launcher/issues");
        }
    }
}
