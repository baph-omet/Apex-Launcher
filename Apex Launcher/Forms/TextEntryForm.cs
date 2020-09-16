// <copyright file="TextEntryForm.cs" company="IAMVISHNU Media">
// © Copyright by IAMVISHNU Media 2020 CC BY-NC-ND
// </copyright>

using System;
using System.Windows.Forms;

namespace ApexLauncher {
    /// <summary>
    /// Form for entering text.
    /// </summary>
    public partial class TextEntryForm : Form {
        private readonly string prompt;
        private readonly string title;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextEntryForm"/> class.
        /// </summary>
        /// <param name="prompt">Prompt text to show.</param>
        /// <param name="title">Window title.</param>
        public TextEntryForm(string prompt = "Please enter text...", string title = "Enter Text") {
            this.prompt = prompt;
            this.title = title;
            Result = string.Empty;
            InitializeComponent();
        }

        /// <summary>
        /// Gets resulting entered text.
        /// </summary>
        public string Result { get; private set; }

        private void CancelButton_Click(object sender, EventArgs e) {
            Result = string.Empty;
            Close();
        }

        private void Button1_Click(object sender, EventArgs e) {
            if (InputBox.Text.Length > 0) {
                Result = InputBox.Text;
                Close();
            } else MessageBox.Show("Please enter text or hit Cancel.");
        }

        private void TextEntryForm_Load(object sender, EventArgs e) {
            TextLabel.Text = prompt;
            Text = title;
        }
    }
}
