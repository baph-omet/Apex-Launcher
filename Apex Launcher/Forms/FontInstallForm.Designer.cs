
namespace ApexLauncher {
    partial class FontInstallForm {
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
            this.ButtonInstall = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.ButtonSuppress = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ButtonInstall
            // 
            this.ButtonInstall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonInstall.Location = new System.Drawing.Point(444, 56);
            this.ButtonInstall.Name = "ButtonInstall";
            this.ButtonInstall.Size = new System.Drawing.Size(75, 23);
            this.ButtonInstall.TabIndex = 0;
            this.ButtonInstall.Text = "Install Fonts";
            this.ButtonInstall.UseVisualStyleBackColor = true;
            this.ButtonInstall.Click += new System.EventHandler(this.ButtonInstall_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(13, 13);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(511, 26);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "Pokémon Apex uses dedicated font files to ensure correct text display. The launch" +
    "er has detected that these font files are not installed. Would you like to insta" +
    "ll fonts now?";
            // 
            // ButtonSuppress
            // 
            this.ButtonSuppress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonSuppress.Location = new System.Drawing.Point(338, 56);
            this.ButtonSuppress.Name = "ButtonSuppress";
            this.ButtonSuppress.Size = new System.Drawing.Size(100, 23);
            this.ButtonSuppress.TabIndex = 2;
            this.ButtonSuppress.Text = "Do Not Ask Again";
            this.ButtonSuppress.UseVisualStyleBackColor = true;
            this.ButtonSuppress.Click += new System.EventHandler(this.ButtonSuppress_Click);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonCancel.Location = new System.Drawing.Point(259, 56);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(73, 23);
            this.ButtonCancel.TabIndex = 3;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // FontInstallForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.ButtonCancel;
            this.ClientSize = new System.Drawing.Size(531, 91);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonSuppress);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.ButtonInstall);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FontInstallForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Font Installation";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ButtonInstall;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button ButtonSuppress;
        private System.Windows.Forms.Button ButtonCancel;
    }
}