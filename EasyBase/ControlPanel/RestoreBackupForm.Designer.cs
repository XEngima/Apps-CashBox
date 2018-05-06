namespace EasyBase
{
    partial class RestoreBackupForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
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
        private void InitializeComponent()
        {
            this.restoreBackupBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.restoreLabel = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // restoreBackupBackgroundWorker
            // 
            this.restoreBackupBackgroundWorker.WorkerReportsProgress = true;
            this.restoreBackupBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.restoreBackupBackgroundWorker_DoWork);
            this.restoreBackupBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.restoreBackupBackgroundWorker_ProgressChanged);
            this.restoreBackupBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.restoreBackupBackgroundWorker_RunWorkerCompleted);
            // 
            // restoreLabel
            // 
            this.restoreLabel.AutoSize = true;
            this.restoreLabel.Location = new System.Drawing.Point(8, 13);
            this.restoreLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.restoreLabel.Name = "restoreLabel";
            this.restoreLabel.Size = new System.Drawing.Size(96, 13);
            this.restoreLabel.TabIndex = 0;
            this.restoreLabel.Text = "Återläser backup...";
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(10, 28);
            this.progressBar.Margin = new System.Windows.Forms.Padding(2);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(224, 19);
            this.progressBar.Step = 1;
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 1;
            // 
            // RestoreBackupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(242, 61);
            this.ControlBox = false;
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.restoreLabel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "RestoreBackupForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Återläser backup";
            this.Load += new System.EventHandler(this.RestoreBackupForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker restoreBackupBackgroundWorker;
        private System.Windows.Forms.Label restoreLabel;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}