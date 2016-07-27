namespace Apex_Launcher {
    partial class SettingsForm {
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
            this.components = new System.ComponentModel.Container();
            this.PathTextbox = new System.Windows.Forms.TextBox();
            this.PathLabel = new System.Windows.Forms.Label();
            this.BrowseButton = new System.Windows.Forms.Button();
            this.KeepOpenCheckbox = new System.Windows.Forms.CheckBox();
            this.KeepOpenCheckboxTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.OKButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.PathTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.DisableFontBox = new System.Windows.Forms.CheckBox();
            this.FontTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.ForceUpdateCheckbox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // PathTextbox
            // 
            this.PathTextbox.Location = new System.Drawing.Point(79, 12);
            this.PathTextbox.Name = "PathTextbox";
            this.PathTextbox.ReadOnly = true;
            this.PathTextbox.Size = new System.Drawing.Size(308, 20);
            this.PathTextbox.TabIndex = 0;
            // 
            // PathLabel
            // 
            this.PathLabel.AutoSize = true;
            this.PathLabel.Location = new System.Drawing.Point(11, 16);
            this.PathLabel.Name = "PathLabel";
            this.PathLabel.Size = new System.Drawing.Size(62, 13);
            this.PathLabel.TabIndex = 1;
            this.PathLabel.Text = "Install Path:";
            // 
            // BrowseButton
            // 
            this.BrowseButton.Location = new System.Drawing.Point(393, 12);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(75, 20);
            this.BrowseButton.TabIndex = 2;
            this.BrowseButton.Text = "Browse";
            this.BrowseButton.UseVisualStyleBackColor = true;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // KeepOpenCheckbox
            // 
            this.KeepOpenCheckbox.AutoSize = true;
            this.KeepOpenCheckbox.Location = new System.Drawing.Point(12, 38);
            this.KeepOpenCheckbox.Name = "KeepOpenCheckbox";
            this.KeepOpenCheckbox.Size = new System.Drawing.Size(128, 17);
            this.KeepOpenCheckbox.TabIndex = 3;
            this.KeepOpenCheckbox.Text = "Keep Launcher Open";
            this.KeepOpenCheckbox.UseVisualStyleBackColor = true;
            // 
            // KeepOpenCheckboxTooltip
            // 
            this.KeepOpenCheckboxTooltip.ToolTipTitle = "Keep Launcher open after the game has been launched (Default: Off)";
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(312, 61);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 4;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(393, 61);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 5;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // PathTooltip
            // 
            this.PathTooltip.ToolTipTitle = "The location where your game files will be downloaded and kept (Default: The same" +
    " location as this program)";
            // 
            // DisableFontBox
            // 
            this.DisableFontBox.AutoSize = true;
            this.DisableFontBox.Location = new System.Drawing.Point(147, 38);
            this.DisableFontBox.Name = "DisableFontBox";
            this.DisableFontBox.Size = new System.Drawing.Size(121, 17);
            this.DisableFontBox.TabIndex = 6;
            this.DisableFontBox.Text = "Disable Game Fonts";
            this.DisableFontBox.UseVisualStyleBackColor = true;
            // 
            // FontTooltip
            // 
            this.FontTooltip.Tag = "Use system fonts instead of game fonts (not recommended)";
            // 
            // ForceUpdateCheckbox
            // 
            this.ForceUpdateCheckbox.AutoSize = true;
            this.ForceUpdateCheckbox.Location = new System.Drawing.Point(275, 38);
            this.ForceUpdateCheckbox.Name = "ForceUpdateCheckbox";
            this.ForceUpdateCheckbox.Size = new System.Drawing.Size(91, 17);
            this.ForceUpdateCheckbox.TabIndex = 7;
            this.ForceUpdateCheckbox.Text = "Force Update";
            this.ForceUpdateCheckbox.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 96);
            this.Controls.Add(this.ForceUpdateCheckbox);
            this.Controls.Add(this.DisableFontBox);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.KeepOpenCheckbox);
            this.Controls.Add(this.BrowseButton);
            this.Controls.Add(this.PathLabel);
            this.Controls.Add(this.PathTextbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox PathTextbox;
        private System.Windows.Forms.Label PathLabel;
        private System.Windows.Forms.Button BrowseButton;
        private System.Windows.Forms.CheckBox KeepOpenCheckbox;
        private System.Windows.Forms.ToolTip KeepOpenCheckboxTooltip;
        private System.Windows.Forms.Button OKButton;
        private new System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.ToolTip PathTooltip;
        private System.Windows.Forms.CheckBox DisableFontBox;
        private System.Windows.Forms.ToolTip FontTooltip;
        private System.Windows.Forms.CheckBox ForceUpdateCheckbox;
    }
}