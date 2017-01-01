namespace Apex_Launcher {
    partial class SaveManagementForm {
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
            this.FileView = new System.Windows.Forms.ListView();
            this.FileNameTab = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DateModifiedTab = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CopyButton = new System.Windows.Forms.Button();
            this.RenameButton = new System.Windows.Forms.Button();
            this.Delete = new System.Windows.Forms.Button();
            this.MakeDefaultButton = new System.Windows.Forms.Button();
            this.ExportButton = new System.Windows.Forms.Button();
            this.ImportButton = new System.Windows.Forms.Button();
            this.ExplorerOpenButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FileView
            // 
            this.FileView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FileNameTab,
            this.DateModifiedTab});
            this.FileView.FullRowSelect = true;
            this.FileView.Location = new System.Drawing.Point(20, 65);
            this.FileView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.FileView.MultiSelect = false;
            this.FileView.Name = "FileView";
            this.FileView.Size = new System.Drawing.Size(978, 470);
            this.FileView.TabIndex = 0;
            this.FileView.UseCompatibleStateImageBehavior = false;
            this.FileView.View = System.Windows.Forms.View.Details;
            // 
            // FileNameTab
            // 
            this.FileNameTab.Text = "File Name";
            this.FileNameTab.Width = 238;
            // 
            // DateModifiedTab
            // 
            this.DateModifiedTab.Text = "Date";
            this.DateModifiedTab.Width = 260;
            // 
            // CopyButton
            // 
            this.CopyButton.Location = new System.Drawing.Point(20, 20);
            this.CopyButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CopyButton.Name = "CopyButton";
            this.CopyButton.Size = new System.Drawing.Size(112, 35);
            this.CopyButton.TabIndex = 1;
            this.CopyButton.Text = "Copy";
            this.CopyButton.UseVisualStyleBackColor = true;
            this.CopyButton.Click += new System.EventHandler(this.CopyButton_Click);
            // 
            // RenameButton
            // 
            this.RenameButton.Location = new System.Drawing.Point(262, 20);
            this.RenameButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.RenameButton.Name = "RenameButton";
            this.RenameButton.Size = new System.Drawing.Size(112, 35);
            this.RenameButton.TabIndex = 2;
            this.RenameButton.Text = "Rename";
            this.RenameButton.UseVisualStyleBackColor = true;
            this.RenameButton.Click += new System.EventHandler(this.RenameButton_Click);
            // 
            // Delete
            // 
            this.Delete.Location = new System.Drawing.Point(141, 20);
            this.Delete.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(112, 35);
            this.Delete.TabIndex = 3;
            this.Delete.Text = "Delete";
            this.Delete.UseVisualStyleBackColor = true;
            this.Delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // MakeDefaultButton
            // 
            this.MakeDefaultButton.Location = new System.Drawing.Point(384, 20);
            this.MakeDefaultButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MakeDefaultButton.Name = "MakeDefaultButton";
            this.MakeDefaultButton.Size = new System.Drawing.Size(171, 35);
            this.MakeDefaultButton.TabIndex = 4;
            this.MakeDefaultButton.Text = "Set As Active Save";
            this.MakeDefaultButton.UseVisualStyleBackColor = true;
            this.MakeDefaultButton.Click += new System.EventHandler(this.MakeDefaultButton_Click);
            // 
            // ExportButton
            // 
            this.ExportButton.Location = new System.Drawing.Point(564, 20);
            this.ExportButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(112, 35);
            this.ExportButton.TabIndex = 5;
            this.ExportButton.Text = "Export";
            this.ExportButton.UseVisualStyleBackColor = true;
            this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // ImportButton
            // 
            this.ImportButton.Location = new System.Drawing.Point(686, 20);
            this.ImportButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ImportButton.Name = "ImportButton";
            this.ImportButton.Size = new System.Drawing.Size(112, 35);
            this.ImportButton.TabIndex = 6;
            this.ImportButton.Text = "Import";
            this.ImportButton.UseVisualStyleBackColor = true;
            this.ImportButton.Click += new System.EventHandler(this.ImportButton_Click);
            // 
            // ExplorerOpenButton
            // 
            this.ExplorerOpenButton.Location = new System.Drawing.Point(808, 20);
            this.ExplorerOpenButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ExplorerOpenButton.Name = "ExplorerOpenButton";
            this.ExplorerOpenButton.Size = new System.Drawing.Size(190, 35);
            this.ExplorerOpenButton.TabIndex = 7;
            this.ExplorerOpenButton.Text = "Open in Explorer";
            this.ExplorerOpenButton.UseVisualStyleBackColor = true;
            this.ExplorerOpenButton.Click += new System.EventHandler(this.ExplorerOpenButton_Click);
            // 
            // SaveManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 555);
            this.Controls.Add(this.ExplorerOpenButton);
            this.Controls.Add(this.ImportButton);
            this.Controls.Add(this.ExportButton);
            this.Controls.Add(this.MakeDefaultButton);
            this.Controls.Add(this.Delete);
            this.Controls.Add(this.RenameButton);
            this.Controls.Add(this.CopyButton);
            this.Controls.Add(this.FileView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SaveManagementForm";
            this.Text = "SaveManagementForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView FileView;
        private System.Windows.Forms.ColumnHeader FileNameTab;
        private System.Windows.Forms.ColumnHeader DateModifiedTab;
        private System.Windows.Forms.Button CopyButton;
        private System.Windows.Forms.Button RenameButton;
        private System.Windows.Forms.Button Delete;
        private System.Windows.Forms.Button MakeDefaultButton;
        private System.Windows.Forms.Button ExportButton;
        private System.Windows.Forms.Button ImportButton;
        private System.Windows.Forms.Button ExplorerOpenButton;
    }
}