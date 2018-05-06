using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EasyBase.BusinessLayer;
using EasyBase.Classes;

namespace CashBox
{
    public partial class RestoreBackupForm : Form
    {
        public RestoreBackupForm()
        {
            InitializeComponent();
        }

        public string FullPath
        {
            get;
            set;
        }

        private void restoreBackupBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            using (var core = new StandardBusinessLayer()) {
                core.Connect(true);

                core.Progress += new ProgressEventHandler(core_Progress);
                core.RestoreBackup(FullPath);
            }
        }

        void core_Progress(object sender, ProgressEventArgs e)
        {
            restoreBackupBackgroundWorker.ReportProgress(e.Percent, e.CurrentValue.ToString() + " of " + e.TargetValue.ToString());
        }

        private void restoreBackupBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Style = ProgressBarStyle.Blocks;
            progressBar.Value = e.ProgressPercentage;
            restoreLabel.Text = "Restored table " + (string)e.UserState;
        }

        private void restoreBackupBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            System.Threading.Thread.Sleep(1000);
            Close();
        }

        private void RestoreBackupForm_Load(object sender, EventArgs e)
        {
            if (!restoreBackupBackgroundWorker.IsBusy) {
                restoreBackupBackgroundWorker.RunWorkerAsync();
            }
        }
    }
}
