﻿namespace Apex_Launcher {
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
            ((System.ComponentModel.ISupportInitialize)(this.YorickHead)).BeginInit();
            this.SuspendLayout();
            // 
            // YorickHead
            // 
            this.YorickHead.Image = ((System.Drawing.Image)(resources.GetObject("YorickHead.Image")));
            this.YorickHead.Location = new System.Drawing.Point(24, 41);
            this.YorickHead.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.YorickHead.Name = "YorickHead";
            this.YorickHead.Size = new System.Drawing.Size(62, 66);
            this.YorickHead.TabIndex = 0;
            this.YorickHead.TabStop = false;
            // 
            // DetailsBox
            // 
            this.DetailsBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DetailsBox.Location = new System.Drawing.Point(24, 151);
            this.DetailsBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.DetailsBox.Name = "DetailsBox";
            this.DetailsBox.ReadOnly = true;
            this.DetailsBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.DetailsBox.Size = new System.Drawing.Size(1183, 381);
            this.DetailsBox.TabIndex = 1;
            this.DetailsBox.Text = "";
            // 
            // CopyButton
            // 
            this.CopyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CopyButton.Location = new System.Drawing.Point(785, 546);
            this.CopyButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.CopyButton.Name = "CopyButton";
            this.CopyButton.Size = new System.Drawing.Size(211, 42);
            this.CopyButton.TabIndex = 2;
            this.CopyButton.Text = "Copy to clipboard...";
            this.CopyButton.UseVisualStyleBackColor = true;
            this.CopyButton.Click += new System.EventHandler(this.CopyButton_Click);
            // 
            // LinkButton
            // 
            this.LinkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LinkButton.Location = new System.Drawing.Point(1007, 546);
            this.LinkButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.LinkButton.Name = "LinkButton";
            this.LinkButton.Size = new System.Drawing.Size(204, 42);
            this.LinkButton.TabIndex = 3;
            this.LinkButton.Text = "Create new issue...";
            this.LinkButton.UseVisualStyleBackColor = true;
            this.LinkButton.Click += new System.EventHandler(this.LinkButton_Click);
            // 
            // DescriptionBox
            // 
            this.DescriptionBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DescriptionBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DescriptionBox.Location = new System.Drawing.Point(104, 15);
            this.DescriptionBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.DescriptionBox.Name = "DescriptionBox";
            this.DescriptionBox.ReadOnly = true;
            this.DescriptionBox.Size = new System.Drawing.Size(1113, 124);
            this.DescriptionBox.TabIndex = 4;
            this.DescriptionBox.Text = resources.GetString("DescriptionBox.Text");
            // 
            // ErrorCatcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1232, 611);
            this.Controls.Add(this.DescriptionBox);
            this.Controls.Add(this.LinkButton);
            this.Controls.Add(this.CopyButton);
            this.Controls.Add(this.DetailsBox);
            this.Controls.Add(this.YorickHead);
            this.Enabled = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.MaximizeBox = false;
            this.Name = "ErrorCatcher";
            this.Text = "Error Catcher";
            ((System.ComponentModel.ISupportInitialize)(this.YorickHead)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox YorickHead;
        private System.Windows.Forms.RichTextBox DetailsBox;
        private System.Windows.Forms.Button CopyButton;
        private System.Windows.Forms.Button LinkButton;
        private System.Windows.Forms.RichTextBox DescriptionBox;
    }
}