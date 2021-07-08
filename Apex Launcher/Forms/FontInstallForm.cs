// <copyright file="FontInstallForm.cs" company="IAMVISHNU Media">
// © Copyright by IAMVISHNU Media 2020 CC BY-NC-ND
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApexLauncher {
    /// <summary>
    /// Wrapper for responses.
    /// </summary>
    public enum FontInstallResponse {
        /// <summary>
        /// Returned when user cancels the dialog.
        /// </summary>
        Cancel,

        /// <summary>
        /// Returned when user chooses to install fonts.
        /// </summary>
        Install,

        /// <summary>
        /// Returned when user chooses to ignore font installation.
        /// </summary>
        Ignore,
    }

    /// <summary>
    /// Form for allowing user to choose to install fonts.
    /// </summary>
    public partial class FontInstallForm : Form {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontInstallForm"/> class.
        /// </summary>
        public FontInstallForm() {
            InitializeComponent();
            Response = FontInstallResponse.Cancel;
        }

        /// <summary>
        /// Gets user's response to this dialog.
        /// </summary>
        public FontInstallResponse Response { get; private set; }

        private void ButtonInstall_Click(object sender, EventArgs e) {
            Response = FontInstallResponse.Install;
            Close();
        }

        private void ButtonSuppress_Click(object sender, EventArgs e) {
            Response = FontInstallResponse.Ignore;
            Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e) {
            Close();
        }
    }
}
