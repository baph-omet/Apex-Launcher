using System;
using System.Windows.Forms;

namespace Apex_Launcher {
    public partial class TextEntryForm : Form {
        private string Prompt;
        private string Title;

        public string Result;

        public TextEntryForm() : this("Please enter text...","Enter Text"){ }
        public TextEntryForm(string prompt) : this(prompt,"Enter Text"){ }
        public TextEntryForm(string prompt, string title) {
            Prompt = prompt;
            Title = title;
            Result = "";
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, EventArgs e) {
            Result = "";
            Close();
        }

        private void button1_Click(object sender, EventArgs e) {
            if (InputBox.Text.Length > 0) {
                Result = InputBox.Text;
                Close();
            } else MessageBox.Show("Please enter text or hit Cancel.");
        }

        private void TextEntryForm_Load(object sender, EventArgs e) {
            TextLabel.Text = Prompt;
            Text = Title;
        }
    }
}
