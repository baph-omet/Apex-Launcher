namespace Apex_Launcher {
    partial class ErrorCatcher {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrorCatcher));
            this.YorickHead = new System.Windows.Forms.PictureBox();
            this.DetailsBox = new System.Windows.Forms.RichTextBox();
            this.CopyButton = new System.Windows.Forms.Button();
            this.LinkButton = new System.Windows.Forms.Button();
            this.DescriptionBox = new System.Windows.Forms.RichTextBox();
            this.ButtonViewIssues = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.YorickHead)).BeginInit();
            this.SuspendLayout();
            // 
            // YorickHead
            // 
            this.YorickHead.Image = ((System.Drawing.Image)(resources.GetObject("YorickHead.Image")));
            this.YorickHead.Location = new System.Drawing.Point(-1, 12);
            this.YorickHead.Name = "YorickHead";
            this.YorickHead.Size = new System.Drawing.Size(52, 52);
            this.YorickHead.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.YorickHead.TabIndex = 0;
            this.YorickHead.TabStop = false;
            // 
            // DetailsBox
            // 
            this.DetailsBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DetailsBox.Location = new System.Drawing.Point(13, 82);
            this.DetailsBox.Name = "DetailsBox";
            this.DetailsBox.ReadOnly = true;
            this.DetailsBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.DetailsBox.Size = new System.Drawing.Size(647, 208);
            this.DetailsBox.TabIndex = 1;
            this.DetailsBox.Text = "";
            // 
            // CopyButton
            // 
            this.CopyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CopyButton.Location = new System.Drawing.Point(335, 296);
            this.CopyButton.Name = "CopyButton";
            this.CopyButton.Size = new System.Drawing.Size(115, 23);
            this.CopyButton.TabIndex = 2;
            this.CopyButton.Text = "Copy to clipboard";
            this.CopyButton.UseVisualStyleBackColor = true;
            this.CopyButton.Click += new System.EventHandler(this.CopyButton_Click);
            // 
            // LinkButton
            // 
            this.LinkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LinkButton.Location = new System.Drawing.Point(549, 296);
            this.LinkButton.Name = "LinkButton";
            this.LinkButton.Size = new System.Drawing.Size(111, 23);
            this.LinkButton.TabIndex = 3;
            this.LinkButton.Text = "Create new issue...";
            this.LinkButton.UseVisualStyleBackColor = true;
            this.LinkButton.Click += new System.EventHandler(this.LinkButton_Click);
            // 
            // DescriptionBox
            // 
            this.DescriptionBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DescriptionBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DescriptionBox.Location = new System.Drawing.Point(57, 8);
            this.DescriptionBox.Name = "DescriptionBox";
            this.DescriptionBox.ReadOnly = true;
            this.DescriptionBox.Size = new System.Drawing.Size(607, 67);
            this.DescriptionBox.TabIndex = 4;
            this.DescriptionBox.Text = resources.GetString("DescriptionBox.Text");
            // 
            // ButtonViewIssues
            // 
            this.ButtonViewIssues.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonViewIssues.Location = new System.Drawing.Point(456, 296);
            this.ButtonViewIssues.Name = "ButtonViewIssues";
            this.ButtonViewIssues.Size = new System.Drawing.Size(87, 23);
            this.ButtonViewIssues.TabIndex = 5;
            this.ButtonViewIssues.Text = "View issues...";
            this.ButtonViewIssues.UseVisualStyleBackColor = true;
            this.ButtonViewIssues.Click += new System.EventHandler(this.ButtonViewIssues_Click);
            // 
            // ErrorCatcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 331);
            this.Controls.Add(this.ButtonViewIssues);
            this.Controls.Add(this.DescriptionBox);
            this.Controls.Add(this.LinkButton);
            this.Controls.Add(this.CopyButton);
            this.Controls.Add(this.DetailsBox);
            this.Controls.Add(this.YorickHead);
            this.Enabled = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ErrorCatcher";
            this.Text = "Error Catcher";
            ((System.ComponentModel.ISupportInitialize)(this.YorickHead)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox YorickHead;
        private System.Windows.Forms.RichTextBox DetailsBox;
        private System.Windows.Forms.Button CopyButton;
        private System.Windows.Forms.Button LinkButton;
        private System.Windows.Forms.RichTextBox DescriptionBox;
        private System.Windows.Forms.Button ButtonViewIssues;
    }
}