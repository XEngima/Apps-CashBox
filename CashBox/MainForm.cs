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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            ApplicationEvents.VerificationChanged += new VerificationEventHandler(ApplicationEvents_VerificationChanged);
        }

        static MainForm()
        {
            DataCache = new DataCache();
            Guesser = new Guesser(DataCache);
        }

        static DataCache DataCache
        {
            get; set;
        }

        void ApplicationEvents_VerificationChanged(object sender, VerificationEventArgs e)
        {
            Text = CurrentApplication.Name + " - (" + CurrentApplication.CurrentVerification + ")";

            if (Environment.CommandLine.Contains(" /developer"))
            {
                Text += " - Debug";
            }
        }

        public MdiClient GetMdiClientWindow()
        {
            foreach (Control ctl in this.Controls) {
                if (ctl is MdiClient) return ctl as MdiClient;
            }
            return null;
        }

        public Size ClientArea
        {
            get
            {
                MdiClient client = GetMdiClientWindow();
                return new Size(client.Size.Width - 5, client.Size.Height - 5);
            }
        }

        public static Guesser Guesser
        {
            get;
            private set;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            using (var core = new StandardBusinessLayer(DataCache))
            {
                core.Connect();

                int currentAccountingYear = 0;
                currentAccountingYear = core.GetCashBoxSettings(CashBoxSettingsNo.CurrentApplicationNo).AccountingYear;
                DataCache.Load(currentAccountingYear);
                CalculateBalance();

                Text = CurrentApplication.Name;
                if (Environment.CommandLine.Contains(" /developer"))
                {
                    Text += " - Developer";
                }

                AccountForm form = new AccountForm(DataCache);
                form.MdiParent = this;
                form.StartPosition = FormStartPosition.Manual;
                form.Location = new Point(0, 0);
                form.Size = new Size(ClientArea.Width / 2, ClientArea.Height);
                form.Show();

                CashBookForm cashBookForm = new CashBookForm(DataCache);
                cashBookForm.MdiParent = this;
                cashBookForm.StartPosition = FormStartPosition.Manual;
                cashBookForm.Location = new Point(ClientArea.Width / 2, 0);
                cashBookForm.Size = new Size(ClientArea.Width / 2, ClientArea.Height);
                cashBookForm.Show();

                Verification verification = DataCache.GetLastVerification();

                if (verification == null)
                {
                    verification = core.CreateNewVerification(currentAccountingYear);
                }
                else
                {
                    verification = DataCache.GetVerification(verification.No);
                }

                decimal a = verification.AccountTransactions.Sum(x => x.Amount);
                decimal c = verification.CashBookTransactions.Sum(x => x.Amount);

                CurrentApplication.CurrentVerification = verification;

                if (currentAccountingYear != CurrentApplication.DateTimeNow.Year)
                {
                    MessageBox.Show("Varning!\n\nSystemets bokföringsår står inställt på " + currentAccountingYear +
                                    ", vilket inte stämmer överrens med nuvarande år på din lokala klocka som står inställd på år " +
                                    CurrentApplication.DateTimeNow.Year +
                                    ".\n\nOm det precis har blivit ett nytt år, var noga med att avsluta det föregående året, och gå sedan in i Inställningar (under menyn Inställningar) och ändra aktuellt bokföringsår till aktuellt år, så att du får en ny nummerserie för det här årets affärshändelser.", "Varning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void openAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountForm form = new AccountForm(DataCache);
            form.MdiParent = this;
            form.Show();
        }

        private void openCashbookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CashBookForm form = new CashBookForm(DataCache);
            form.MdiParent = this;
            form.Show();
        }

        public void CalculateBalance()
        {
            using (var core = new StandardBusinessLayer(DataCache)) {
                core.Connect();

                decimal balanceAmount = core.CalculateBalance();
                balanceToolStripStatusLabel.Text = "Balans: " + balanceAmount.ToString("#,##0.00") + " kr";
                if (balanceAmount.CompareTo(0m) == 0) {
                    balanceToolStripStatusLabel.ForeColor = System.Drawing.Color.Black;
                    Guesser.ClearGuess();
                }
                else {
                    balanceToolStripStatusLabel.ForeColor = System.Drawing.Color.Red;
                }

                decimal totalWealth = core.CalculateTotalWealth();
                TotalWealthToolStripStatusLabel.Text = "Total förmögenhet: " + totalWealth.ToString("#,##0.00") + " kr";
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void skapaBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fileDialog = new SaveFileDialog();
            fileDialog.Filter = CurrentApplication.Name + "-backupfiler|*.backup|Alla filer|*.txt";
            fileDialog.DefaultExt = ".backup";
            fileDialog.FileName = CurrentApplication.DateTimeNow.ToString("yyyyMMdd_HHmm") + " " + CurrentApplication.Name + ".backup";

            DialogResult result = fileDialog.ShowDialog();
            
            if (result == DialogResult.OK) {
                using (var core = new StandardBusinessLayer(DataCache)) {
                    core.Connect();
                    core.CreateBackup(fileDialog.FileName);
                }

                MessageBox.Show("Backupen skapades.", CurrentApplication.Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void återläsBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog
                                 {
                                     Filter = CurrentApplication.Name + "-backupfiler|*.backup|Alla filer|*.*",
                                     CheckFileExists = true
                                 };

            DialogResult result = fileDialog.ShowDialog();

            if (result == DialogResult.OK) {
                result = MessageBox.Show("Varning!\n\nOm du återläser en backup kommer all befintlig data i systemet först att raderas. Är du säker på att du vill återläsa den aktuella backupen?", CurrentApplication.Name, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                if (result != DialogResult.Yes) {
                    return;
                }

                var form = new RestoreBackupForm {FullPath = fileDialog.FileName};
                form.ShowDialog();

                MessageBox.Show("Backupen återlästes. Du måste nu starta om programmet.", CurrentApplication.Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Environment.Exit(0);
            }
        }

        private void manageCategoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ManageCategoriesForm(DataCache);
            form.ShowDialog();
        }

        private void manageAccountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ManageAccountsForm(DataCache);
            form.ShowDialog();
        }

        private void statisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StatisticsForm form = new StatisticsForm(DataCache);
            form.MdiParent = this;
            form.Show();
        }

        private void newVerificationButton_Click(object sender, EventArgs e)
        {
            try {
                using (var core = new StandardBusinessLayer(DataCache)) {
                    core.Connect();
                    core.CreateNewVerification(core.GetCashBoxSettings(CashBoxSettingsNo.CurrentApplicationNo).AccountingYear);
                }
            }
            catch (VerificationException ex) {
                MessageBox.Show(ex.Message, CurrentApplication.Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm form = new SettingsForm(DataCache);
            form.ShowDialog();
        }
    }
}
