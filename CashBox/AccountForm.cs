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
using EasyBase;

namespace CashBox
{
    public partial class AccountForm : Form
    {
        private bool _selectionChanged = false;

        public AccountForm(DataCache dataCache)
        {
            InitializeComponent();
            DataCache = dataCache;
            FormLoading = true;
            BalanceBroughtForward = 0;

            ApplicationEvents.AccountTransactionCreated += new AccountTransactionCreatedEventHandler(CurrentApplication_AccountTransactionCreated);
            ApplicationEvents.AccountTransactionDeleted += new AccountTransactionDeletedEventHandler(CurrentApplication_AccountTransactionDeleted);
            ApplicationEvents.CashBookTransactionSelectionChanged += new CashBookTransactionSelectionChangedEventHandler(CurrentApplication_CashBookTransactionSelectionChanged);
            ApplicationEvents.CashBookTransactionCreated += new CashBookTransactionCreatedEventHandler(ApplicationEvents_CashBookTransactionCreated);
            ApplicationEvents.CashBookTransactionUpdated += new CashBookTransactionUpdatedEventHandler(CurrentApplication_CashBookTransactionUpdated);
            ApplicationEvents.CashBookTransactionDeleted += new CashBookTransactionDeletedEventHandler(CurrentApplication_CashBookTransactionDeleted);
            ApplicationEvents.VerificationDeleted += new VerificationDeletedEventHandler(ApplicationEvents_VerificationDeleted);
            ApplicationEvents.AccountingYearChanged += new AccountingYearChangedEventHandler(ApplicationEvents_AccountingYearChanged);
        }

        private DataCache DataCache
        {
            get; set;
        }

        void ApplicationEvents_CashBookTransactionCreated(object sender, CashBookTransactionEventArgs e)
        {
            UpdateRowHighlights(e);
        }

        void CurrentApplication_AccountTransactionCreated(object sender, AccountTransactionEventArgs e)
        {
            UpdateRowHighlights(e);
        }

        void CurrentApplication_AccountTransactionDeleted(object sender, AccountTransactionEventArgs e)
        {
            // If the deleted transaction is in the list, then reload the grid.

            foreach (DataGridViewRow row in transactionsDataGridView.Rows) {
                if ((int)row.Cells[AccountTransaction.fNo].Value == e.AccountTransactionNo) {
                    LoadTransactionGrid();
                    return;
                }
            }

            // If the deleted transaction's verification is represented in the list, then update the highlights

            foreach (DataGridViewRow row in transactionsDataGridView.Rows) {
                if ((int)row.Cells[AccountTransaction.fVerificationNo].Value == (int)e.VerificationNo) {
                    UpdateRowHighlights(ApplicationEvents.LastCashBookTransactionSelectionChangedEventArgs);
                    return;
                }
            }
        }

        void CurrentApplication_CashBookTransactionDeleted(object sender, CashBookTransactionEventArgs e)
        {
            // If the deleted transaction is in the list, then update the row highlights.
            UpdateRowHighlights(e);
        }
        
        private void ApplicationEvents_VerificationDeleted(object sender, VerificationDeletedEventArgs e)
        {
            // If transactions int the list belong to the deleted verification, then reload the list
            foreach (DataGridViewRow row in transactionsDataGridView.Rows)
            {
                if (((int)row.Cells[AccountTransaction.fVerificationNo].Value == e.VerificationNo))
                {
                    LoadTransactionGrid();
                    return;
                }
            }
        }

        private void ApplicationEvents_AccountingYearChanged(object sender, AccountingYearChangedEventArgs e)
        {
            LoadTransactionGrid();
        }

        void CurrentApplication_CashBookTransactionUpdated(object sender, CashBookTransactionEventArgs e)
        {
            // If the updated transaction is in the list, then update the row highlights.
            UpdateRowHighlights(e);
        }

        private void UpdateRowHighlights(AccountTransactionEventArgs e)
        {
            foreach (DataGridViewRow row in transactionsDataGridView.Rows) {
                if ((int)row.Cells[AccountTransaction.fNo].Value == e.VerificationNo) {

                    using (var core = new StandardBusinessLayer(DataCache)) {
                        core.Connect();
                        UnbalancedVerificationNumbers = DataCache.GetUnbalancedAndEmptyVerifications().Select(v => v.No).ToArray(); //core.GetUnbalancedVerificationNumbers();
                    }

                    UpdateRowHighlightsNoDatabase(ApplicationEvents.LastCashBookTransactionSelectionChangedEventArgs);
                }
            }
        }

        private void UpdateRowHighlights(CashBookTransactionEventArgs e)
        {
            foreach (DataGridViewRow row in transactionsDataGridView.Rows) {
                if ((int)row.Cells[AccountTransaction.fVerificationNo].Value == e.VerificationNo) {

                    using (var core = new StandardBusinessLayer(DataCache)) {
                        core.Connect();
                        UnbalancedVerificationNumbers = DataCache.GetUnbalancedAndEmptyVerifications().Select(v => v.No).ToArray(); //core.GetUnbalancedVerificationNumbers();
                    }

                    UpdateRowHighlightsNoDatabase(ApplicationEvents.LastCashBookTransactionSelectionChangedEventArgs);
                }
            }
        }

        void CurrentApplication_CashBookTransactionSelectionChanged(object sender, CashBookTransactionEventArgs e)
        {
            UpdateRowHighlightsNoDatabase(e);
        }

        private void UpdateRowHighlightsNoDatabase(CashBookTransactionEventArgs e)
        {
            if (e != null) {
                foreach (DataGridViewRow row in transactionsDataGridView.Rows) {
                    if ((int)row.Cells[AccountTransaction.fVerificationNo].Value == e.VerificationNo) {
                        row.DefaultCellStyle = new DataGridViewCellStyle() { ForeColor = System.Drawing.Color.Blue };
                    }
                    else {
                        row.DefaultCellStyle = new DataGridViewCellStyle() { ForeColor = System.Drawing.Color.Black };
                    }
                }
            }

            if (UnbalancedVerificationNumbers != null) {
                foreach (DataGridViewRow row in transactionsDataGridView.Rows) {
                    if (UnbalancedVerificationNumbers.Contains((int)row.Cells[AccountTransaction.fVerificationNo].Value)) {
                        row.DefaultCellStyle = new DataGridViewCellStyle() { ForeColor = System.Drawing.Color.Red };
                    }
                }
            }
        }

        private bool FormLoading
        {
            get;
            set;
        }

        private void AccountForm_Load(object sender, EventArgs e)
        {
            balanceToolStripStatusLabel.Text = "";

            using (var core = new StandardBusinessLayer(DataCache)) {
                core.Connect();

                Condition notArchived = new Condition(Account.fIsArchived, CompareOperator.NotEqualTo, true);
                EasyBase.Classes.SortOrder byName = new EasyBase.Classes.SortOrder(Account.fName, OrderDirection.Ascending);

                DataTable accountDataTable = core.GetAccountsTable(notArchived, byName, Account.fNo, Account.fName);

                accountComboBox.ComboBox.ValueMember = Account.fNo;
                accountComboBox.ComboBox.DisplayMember = Account.fName;
                accountComboBox.ComboBox.DataSource = accountDataTable;
                accountComboBox.ComboBox.SelectedValue = 0;
            }

            FormLoading = false;
            EnableDisableControls();
        }

        private void accountComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!FormLoading)
            {
                //int accountNo = (int)accountComboBox.ComboBox.SelectedValue;

                //using (var core = new StandardBusinessLayer(DataCache))
                //{
                //    core.Connect();

                //    int accountingYear = core.GetCashBoxSettings(CashBoxSettingsNo.CurrentApplicationNo).AccountingYear;

                //    Condition belongToAccount = new Condition(AccountTransaction.fAccountNo, CompareOperator.EqualTo, accountNo);
                //    Condition laterThanOrEqualToMonth = new Condition(AccountTransaction.fTransactionTime, CompareOperator.GreaterThanOrEqualTo, new DateTime(accountingYear, 1, 1), DateTimeResolution.Month);
                //    Condition earlierThanOrEqualToMonth = new Condition(AccountTransaction.fTransactionTime, CompareOperator.LessThanOrEqualTo, new DateTime(accountingYear, 12, 31), DateTimeResolution.Month);
                //    EasyBase.Classes.SortOrder orderByDate = new EasyBase.Classes.SortOrder(AccountTransaction.fAccountNo, OrderDirection.Ascending);

                //    AccountTransactionCollection transactions = core.GetAccountTransactions(belongToAccount & laterThanOrEqualToMonth & earlierThanOrEqualToMonth, orderByDate);

                //    DataTable dataTable = new DataTable();
                //    dataTable.Columns.Add(new DataColumn(AccountTransaction.fTransactionTime, typeof(DateTime)));
                //    dataTable.Columns.Add(new DataColumn(AccountTransaction.fAccountingDate, typeof(DateTime)));
                //    dataTable.Columns.Add(new DataColumn(AccountTransaction.fAmount, typeof(decimal)));
                //}

                LoadTransactionGrid();
            }

            EnableDisableControls();
        }

        private decimal BalanceBroughtForward
        {
            get;
            set;
        }

        private void UpdateAccountBalanceColumn()
        {
            decimal balance = BalanceBroughtForward;

            for (int i = transactionsDataGridView.Rows.Count - 1; i >= 0; i -= 1) {
                balance += (decimal)transactionsDataGridView.Rows[i].Cells[AccountTransaction.fAmount].Value;
                transactionsDataGridView.Rows[i].Cells["AccountBalance"].Value = balance;
            }
        }

        private void LoadTransactionGrid()
        {
            if (accountComboBox.ComboBox.SelectedValue == null)
            {
                return;
            }

            using (var core = new StandardBusinessLayer(DataCache)) {
                core.Connect();

                //int accountingYear = core.GetCashBoxSettings(CashBoxSettingsNo.CurrentApplicationNo).AccountingYear;
                int accountingYear = DataCache.Settings.AccountingYear;

                var sortOrder = EasyBase.Classes.SortOrder.Create(AccountTransaction.fTransactionTime, OrderDirection.Descending,
                    new EasyBase.Classes.SortOrder(AccountTransaction.fNo, OrderDirection.Descending));

                Condition belongsToAccount = new Condition(AccountTransaction.fAccountNo, CompareOperator.EqualTo, (int)accountComboBox.ComboBox.SelectedValue);
                Condition laterThanOrEqualToMonth = new Condition(AccountTransaction.fTransactionTime, CompareOperator.GreaterThanOrEqualTo, new DateTime(accountingYear, 1, 1), DateTimeResolution.Month);
                Condition earlierThanOrEqualToMonth = new Condition(AccountTransaction.fTransactionTime, CompareOperator.LessThanOrEqualTo, new DateTime(accountingYear, 12, 31), DateTimeResolution.Month);

                var accountTransactionsTable = core.GetAccountTransactionsTable(belongsToAccount & laterThanOrEqualToMonth & earlierThanOrEqualToMonth, sortOrder,
                    AccountTransaction.fNo, AccountTransaction.fVerificationNo, AccountTransaction.fVerificationSerialNo, AccountTransaction.fTransactionTime, AccountTransaction.fAccountingDate, AccountTransaction.fNote, AccountTransaction.fAmount,
                    AccountTransaction.fUserNo, AccountTransaction.fAccountNo);

                accountTransactionsTable.Columns.Add("AccountBalance", typeof(decimal));

                //var accountTransactions = core.GetAccountTransactionsByAccountNo((int)accountComboBox.ComboBox.SelectedValue, sortOrder);

                transactionsDataGridView.DataSource = accountTransactionsTable;

                //BalanceBroughtForward = core.GetAccount((int)accountComboBox.ComboBox.SelectedValue).BalanceBroughtForwardAmount;
                BalanceBroughtForward =
                    core.GetAccountBalanceBroughtForward((int) accountComboBox.ComboBox.SelectedValue, accountingYear);
                UpdateAccountBalanceColumn();

                // Hide all columns
                foreach (DataGridViewColumn column in transactionsDataGridView.Columns) {
                    column.Visible = false;
                    //column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                // Show selected columns
                transactionsDataGridView.Columns[AccountTransaction.fVerificationSerialNo].Visible = true;
                transactionsDataGridView.Columns[AccountTransaction.fVerificationSerialNo].Width = 75;
                transactionsDataGridView.Columns[AccountTransaction.fVerificationSerialNo].HeaderText = "Nr";

                transactionsDataGridView.Columns[AccountTransaction.fTransactionTime].Visible = true;
                transactionsDataGridView.Columns[AccountTransaction.fTransactionTime].Width = 70;
                transactionsDataGridView.Columns[AccountTransaction.fTransactionTime].HeaderText = "Kassadatum";

                transactionsDataGridView.Columns[AccountTransaction.fAccountingDate].Visible = true;
                transactionsDataGridView.Columns[AccountTransaction.fAccountingDate].Width = 70;
                transactionsDataGridView.Columns[AccountTransaction.fAccountingDate].HeaderText = "Bokf.datum";

                transactionsDataGridView.Columns[AccountTransaction.fAmount].Width = CurrentApplication.GridColumnAmountWidth;
                transactionsDataGridView.Columns[AccountTransaction.fAmount].Visible = true;
                transactionsDataGridView.Columns[AccountTransaction.fAmount].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                transactionsDataGridView.Columns[AccountTransaction.fAmount].HeaderText = "Belopp";

                transactionsDataGridView.Columns[AccountTransaction.fNote].Visible = true;
                transactionsDataGridView.Columns[AccountTransaction.fNote].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                transactionsDataGridView.Columns[AccountTransaction.fNote].HeaderText = "Notering";

                transactionsDataGridView.Columns["AccountBalance"].Width = CurrentApplication.GridColumnAmountWidth;
                transactionsDataGridView.Columns["AccountBalance"].Visible = true;
                transactionsDataGridView.Columns["AccountBalance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                transactionsDataGridView.Columns["AccountBalance"].HeaderText = "Balans";

                CalculateBalance();
                ((MainForm)ParentForm).CalculateBalance();
                EnableDisableControls();

                if (transactionsDataGridView.Rows.Count > 0) {
                    transactionsDataGridView.Rows[0].Selected = false;
                }

                UnbalancedVerificationNumbers = DataCache.GetUnbalancedAndEmptyVerifications().Select(v => v.No).ToArray(); //core.GetUnbalancedVerificationNumbers();

                UpdateRowHighlightsNoDatabase(ApplicationEvents.LastCashBookTransactionSelectionChangedEventArgs);
            }
        }

        private int[] UnbalancedVerificationNumbers
        {
            get;
            set;
        }

        private void CalculateBalance()
        {
            using (var core = new StandardBusinessLayer(DataCache)) {
                core.Connect();

                //decimal balance = core.CalculateAccountBalance((int)accountComboBox.ComboBox.SelectedValue);
                decimal balance = DataCache.CalculateAccountBalance((int)accountComboBox.ComboBox.SelectedValue);
                balanceToolStripStatusLabel.Text = "Saldo: " + balance.ToString(CurrentApplication.MoneyDisplayFormat) + " kr";
            }
        }

        private void transactionsDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0) {
                if (transactionsDataGridView.Columns[e.ColumnIndex].Name == AccountTransaction.fAmount || transactionsDataGridView.Columns[e.ColumnIndex].Name == "AccountBalance") {
                    e.Value = ((decimal)e.Value).ToString(CurrentApplication.MoneyDisplayFormat) + " kr";
                }
                else if (transactionsDataGridView.Columns[e.ColumnIndex].Name == AccountTransaction.fAccountingDate)
                {
                    DateTime transactionTime = (DateTime)transactionsDataGridView.Rows[e.RowIndex].Cells[AccountTransaction.fTransactionTime].Value;

                    if ((DateTime) e.Value == transactionTime)
                    {
                        e.Value = "";
                    }
                }
            }
        }

        private void depositWithdrawalButton_Click(object sender, EventArgs e)
        {
            DepositWithdrawalForm form = new DepositWithdrawalForm(DataCache, MainForm.Guesser, accountComboBox.Text, (int)accountComboBox.ComboBox.SelectedValue);
            DialogResult result = form.ShowDialog();

            if (result == DialogResult.OK) {
                AccountTransaction transaction = null;
                using (var core = new StandardBusinessLayer(DataCache)) {
                    core.Connect();

                    try
                    {
                        transaction = core.AddAccountTransaction(form.VerificationNo, form.VerificationDate, form.AccountingDate,
                            CurrentApplication.UserNo, (int)accountComboBox.ComboBox.SelectedValue, form.Amount,
                            form.Note, form.GetTagComboBoxItem.Action,
                            form.GetTagComboBoxItem.AccountTag != null ? form.GetTagComboBoxItem.AccountTag.No : 0);
                    }
                    catch (MoneyTagException ex)
                    {
                        MessageBox.Show(ex.Message, "Felaktig transaktion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                if (transaction != null)
                {
                    LoadTransactionGrid();
                    SelectGridTransaction(transaction.No);

                    if (form.SuggestCashBookTransaction)
                    {
                        createCashBookItemToolStripMenuItem_Click(sender, e);
                    }
                }
            }
        }

        private void SelectGridTransaction(int transactionNo)
        {
            foreach (DataGridViewRow row in transactionsDataGridView.Rows)
            {
                if ((int)row.Cells[AccountTransaction.fNo].Value == transactionNo)
                {
                    row.Selected = true;
                    return;
                }
            }
        }

        private void transactionsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) {
                int no = (int)transactionsDataGridView.Rows[e.RowIndex].Cells[AccountTransaction.fNo].Value;
                int verificationNo = (int)transactionsDataGridView.Rows[e.RowIndex].Cells[AccountTransaction.fVerificationNo].Value;
                int userNo = (int)transactionsDataGridView.Rows[e.RowIndex].Cells[AccountTransaction.fUserNo].Value;
                int accountNo = (int)transactionsDataGridView.Rows[e.RowIndex].Cells[AccountTransaction.fAccountNo].Value;

                var form = new DepositWithdrawalForm(DataCache, MainForm.Guesser, accountComboBox.Text, (int)accountComboBox.ComboBox.SelectedValue, no, verificationNo);
                DialogResult result = form.ShowDialog();

                if (result == DialogResult.OK) {
                    AccountTransaction transaction = null;
                    using (var core = new StandardBusinessLayer(DataCache))
                    {
                        try
                        {
                            core.Connect();
                            transaction = core.UpdateAccountTransaction(no, form.VerificationDate, form.AccountingDate, userNo, accountNo,
                                form.Amount, form.Note, form.GetTagComboBoxItem.Action,
                                form.GetTagComboBoxItem.AccountTag != null ? form.GetTagComboBoxItem.AccountTag.No : 0);
                        }
                        catch (MoneyTagException ex)
                        {
                            MessageBox.Show(ex.Message, "Felaktig transaktion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    LoadTransactionGrid();
                    SelectGridTransaction(no);

                    if (transaction != null)
                    {
                        ApplicationEvents.OnAccountTransactionUpdated(no, transaction.VerificationNo);
                    }
                }
            }
        }

        private void EnableDisableControls()
        {
            depositWithdrawalButton.Enabled = accountComboBox.ComboBox.SelectedValue != null;
            deleteToolStripButton.Enabled = transactionsDataGridView.SelectedRows.Count > 0;
            transactionContextMenuStrip.Enabled = transactionsDataGridView.SelectedRows.Count > 0;
        }

        private void transactionsDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            int transactionNo = 0;
            int verificationNo = 0;

            if (transactionsDataGridView.SelectedRows.Count > 0) {
                transactionNo = (int)transactionsDataGridView.SelectedRows[0].Cells[AccountTransaction.fNo].Value;
                verificationNo = (int)transactionsDataGridView.SelectedRows[0].Cells[AccountTransaction.fVerificationNo].Value;
            }

            ApplicationEvents.OnAccountTransactionSelectionChanged(transactionNo, verificationNo);
            _selectionChanged = true;

            EnableDisableControls();
        }

        private void transactionsDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!_selectionChanged) {
                if (e.RowIndex >= 0) {
                    DataGridViewRow clickedRow = transactionsDataGridView.Rows[e.RowIndex];

                    if (transactionsDataGridView.SelectedRows.Contains(clickedRow)) {
                        clickedRow.Selected = false;
                    }
                }
            }

            _selectionChanged = false;
        }

        private void deleteToolStripButton_Click(object sender, EventArgs e)
        {
            DeleteSelectedTransaction();
        }

        private void DeleteSelectedTransaction()
        {
            DialogResult result = MessageBox.Show("Du kommer att ta bort den markerade kontotransaktionen. Vill du även ta bort den aktuella affärshändelsen med alla associerade konto- och kassabokstransaktioner?\n\nJa: Alla assicierade transaktioner kommer att tas bort.\nNej: Endast den markerade transaktionen kommer att tas bort.", CurrentApplication.Name, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3);

            int transactionNo = (int)transactionsDataGridView.SelectedRows[0].Cells[AccountTransaction.fNo].Value;
            int verificationNo = (int)transactionsDataGridView.SelectedRows[0].Cells[AccountTransaction.fVerificationNo].Value;

            try
            {
                if (result == DialogResult.Yes)
                {
                    using (var core = new StandardBusinessLayer(DataCache))
                    {
                        core.Connect();
                        core.DeleteVerificationAndAssociatedTransactions(verificationNo);
                    }

                    ApplicationEvents.OnVerificationDeleted(verificationNo);
                }
                else if (result == DialogResult.No)
                {
                    using (var core = new StandardBusinessLayer(DataCache))
                    {
                        core.Connect();

                        int noOfTransactions = core.CountAccountTransactionsByVerificationNo(verificationNo) +
                                               core.CountCashBookTransactionsByVerificationNo(verificationNo);

                        if (noOfTransactions > 1)
                        {
                            //var accountTransaction = core.GetAccountTransaction(transactionNo);
                            var accountTransaction = DataCache.GetAccountTransaction(transactionNo);
                            bool accountHasTags = core.CountAccountTagsByAccountNo(accountTransaction.AccountNo) > 0;

                            if (accountTransaction.Amount != 0 && accountHasTags)
                            {
                                throw new VerificationException("Du försöker ta bort en kontotransaktion från ett konto som har kontomärkningar och vars belopp inte är 0. Detta är inte tillåtet eftersom befintliga kontomärkningar kan bli felaktiga. Sätt transaktionsbeloppet för transaktionen till 0 och försök igen.");
                            }

                            DataCache.DeleteAccountTransaction(transactionNo);
                            ApplicationEvents.OnAccountTransactionDeleted(transactionNo, verificationNo);
                        }
                        else
                        {
                            core.DeleteVerificationAndAssociatedTransactions(verificationNo);
                            ApplicationEvents.OnVerificationDeleted(verificationNo);
                        }
                    }
                }
            }
            catch (VerificationException ex)
            {
                MessageBox.Show(ex.Message, CurrentApplication.Name, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void createCashBookItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int transactionNo = (int)transactionsDataGridView.SelectedRows[0].Cells[AccountTransaction.fNo].Value;
            int verificationNo = (int)transactionsDataGridView.SelectedRows[0].Cells[AccountTransaction.fVerificationNo].Value;

            CashBookTransactionForm form = new CashBookTransactionForm(DataCache, MainForm.Guesser, verificationNo: verificationNo);

            using (var core = new StandardBusinessLayer(DataCache)) {
                core.Connect();
                //AccountTransaction accountTransaction = core.GetAccountTransaction(transactionNo);
                AccountTransaction accountTransaction = DataCache.GetAccountTransaction(transactionNo);

                if (accountTransaction != null) {
                    CashBookTransactionCollection cashBookTransactions = core.GetCashBookTransactionsByVerificationNo(verificationNo);
                    AccountTransactionCollection accountTransactions = core.GetAccountTransactionsByVerificationNo(verificationNo);

                    decimal sum = accountTransactions.Sum(t => t.Amount) - cashBookTransactions.Sum(t => t.Amount);
                    form.Amount = sum;
                }
            }

            DialogResult result = form.ShowDialog();

            if (result == DialogResult.OK) {
                using (var core = new StandardBusinessLayer(DataCache)) {
                    core.Connect();

                    core.AddCashBookTransaction(form.VerificationNo, form.VerificationDate, form.AccountingDate, form.UserNo,
                                                              form.CategoryNo, form.Amount, form.Note);
                }

                LoadTransactionGrid();
                SelectGridTransaction(transactionNo);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteSelectedTransaction();
        }

        private void transactionsDataGridView_Sorted(object sender, EventArgs e)
        {
            UpdateAccountBalanceColumn();
        }

        private void transactionsDataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                transactionsDataGridView.Rows[e.RowIndex].Selected = true;
            }
        }

        private void accountComboBox_Click(object sender, EventArgs e)
        {

        }
    }
}
