namespace ApexLauncher {
    partial class DownloadForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DownloadForm));
            this.DownloadProgressBar = new System.Windows.Forms.ProgressBar();
            this.CancelDownloadButton = new System.Windows.Forms.Button();
            this.ProgressLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // DownloadProgressBar
            // 
            this.DownloadProgressBar.Location = new System.Drawing.Point(13, 12);
            this.DownloadProgressBar.Name = "DownloadProgressBar";
            this.DownloadProgressBar.Size = new System.Drawing.Size(595, 23);
            this.DownloadProgressBar.TabIndex = 0;
            // 
            // CancelDownloadButton
            // 
            this.CancelDownloadButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelDownloadButton.Location = new System.Drawing.Point(533, 67);
            this.CancelDownloadButton.Name = "CancelDownloadButton";
            this.CancelDownloadButton.Size = new System.Drawing.Size(75, 23);
            this.CancelDownloadButton.TabIndex = 1;
            this.CancelDownloadButton.Text = "Cancel";
            this.CancelDownloadButton.UseVisualStyleBackColor = true;
            this.CancelDownloadButton.Click += new System.EventHandler(this.CancelDownloadButton_Click);
            // 
            // ProgressLabel
            // 
            this.ProgressLabel.AutoSize = true;
            this.ProgressLabel.Location = new System.Drawing.Point(13, 42);
            this.ProgressLabel.Name = "ProgressLabel";
            this.ProgressLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ProgressLabel.Size = new System.Drawing.Size(112, 13);
            this.ProgressLabel.TabIndex = 2;
            this.ProgressLabel.Text = "Beginning download...";
            // 
            // DownloadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelDownloadButton;
            this.ClientSize = new System.Drawing.Size(620, 102);
            this.Controls.Add(this.ProgressLabel);
            this.Controls.Add(this.CancelDownloadButton);
            this.Controls.Add(this.DownloadProgressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DownloadForm";
            this.Text = "Downloading...";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DownloadForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ProgressBar DownloadProgressBar;
        private System.Windows.Forms.Button CancelDownloadButton;
        public System.Windows.Forms.Label ProgressLabel;
    }
}