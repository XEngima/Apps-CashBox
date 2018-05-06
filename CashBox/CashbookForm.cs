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
using DanielEiserman.DateAndTime;

namespace CashBox
{
    public partial class CashBookForm : Form
    {
        private bool _selectionChanged = true;
        private bool _firstActivate = true;

        public CashBookForm(DataCache dataCache)
        {
            InitializeComponent();
            DataCache = dataCache;
            FormLoading = true;

            ApplicationEvents.CashBookTransactionCreated += new CashBookTransactionCreatedEventHandler(CurrentApplication_CashBookTransactionCreated);
            ApplicationEvents.CashBookTransactionDeleted += new CashBookTransactionDeletedEventHandler(CurrentApplication_CashBookTransactionDeleted);
            ApplicationEvents.AccountTransactionSelectionChanged += new AccountTransactionSelectionChangedEventHandler(CurrentApplication_AccountTransactionSelectionChanged);
            ApplicationEvents.AccountTransactionCreated += new AccountTransactionCreatedEventHandler(CurrentApplication_AccountTransactionCreated);
            ApplicationEvents.AccountTransactionDeleted += new AccountTransactionDeletedEventHandler(CurrentApplication_AccountTransactionDeleted);
            ApplicationEvents.AccountTransactionUpdated += new AccountTransactionUpdatedEventHandler(CurrentApplication_AccountTransactionUpdated);
            ApplicationEvents.VerificationDeleted += new VerificationDeletedEventHandler(ApplicationEvents_VerificationDeleted);
        }

        private DataCache DataCache { get; set; }

        private void CurrentApplication_AccountTransactionCreated(object sender, AccountTransactionEventArgs e)
        {
            UpdateRowHighlights(ApplicationEvents.LastAccountTransactionSelectionChangedEventArgs);
        }

        private void CurrentApplication_CashBookTransactionCreated(object sender, CashBookTransactionEventArgs e)
        {
            LoadTransactionGrid();
            SelectGridTransaction(e.CashBookTransactionNo);
        }

        void CurrentApplication_CashBookTransactionDeleted(object sender, CashBookTransactionEventArgs e)
        {
            // If the deleted transaction is in the list, then reload the grid.

            foreach (DataGridViewRow row in transactionsDataGridView.Rows)
            {
                if ((int)row.Cells[CashBookTransaction.fNo].Value == e.CashBookTransactionNo)
                {
                    LoadTransactionGrid();
                    return;
                }
            }

            // If the deleted transaction's verification is represented in the list, then update the highlights

            foreach (DataGridViewRow row in transactionsDataGridView.Rows)
            {
                if ((int)row.Cells[CashBookTransaction.fVerificationNo].Value == (int)e.VerificationNo)
                {
                    UpdateRowHighlights(ApplicationEvents.LastAccountTransactionSelectionChangedEventArgs);
                    return;
                }
            }
        }

        private void CurrentApplication_AccountTransactionUpdated(object sender, AccountTransactionEventArgs e)
        {
            // If the updated transaction is in the list, then update the row highlights.
            UpdateRowHighlights(e);
        }

        private void UpdateRowHighlights(AccountTransactionEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in transactionsDataGridView.Rows)
                {
                    if ((int)row.Cells[CashBookTransaction.fVerificationNo].Value == e.VerificationNo)
                    {

                        using (var core = new StandardBusinessLayer(DataCache))
                        {
                            core.Connect();
                            UnbalancedVerificationNumbers = DataCache.GetUnbalancedAndEmptyVerifications().Select(v => v.No).ToArray(); // core.GetUnbalancedVerificationNumbers();
                        }

                        UpdateRowHighlightsNoDatabase(ApplicationEvents.LastAccountTransactionSelectionChangedEventArgs);
                    }
                }
            }
            catch {
            }
        }

        private void CurrentApplication_AccountTransactionDeleted(object sender, AccountTransactionEventArgs e)
        {
            // If the deleted transaction's source transaction is in the list, then update the highlights
            foreach (DataGridViewRow row in transactionsDataGridView.Rows) {
                if (((int)row.Cells[CashBookTransaction.fVerificationNo].Value == e.VerificationNo)) {
                    UpdateRowHighlights(e);
                    return;
                }
            }
        }

        private void ApplicationEvents_VerificationDeleted(object sender, VerificationDeletedEventArgs e)
        {
            // If transactions int the list belong to the deleted verification, then reload the list
            foreach (DataGridViewRow row in transactionsDataGridView.Rows)
            {
                if (((int)row.Cells[CashBookTransaction.fVerificationNo].Value == e.VerificationNo))
                {
                    LoadTransactionGrid();
                    return;
                }
            }
        }

        private void CurrentApplication_AccountTransactionSelectionChanged(object sender, AccountTransactionEventArgs e)
        {
            UpdateRowHighlightsNoDatabase(e);
        }

        private void UpdateRowHighlightsNoDatabase(AccountTransactionEventArgs e)
        {
            if (e != null) {
                foreach (DataGridViewRow row in transactionsDataGridView.Rows) {
                    if ((int)row.Cells[CashBookTransaction.fVerificationNo].Value == e.VerificationNo) {
                        row.DefaultCellStyle = new DataGridViewCellStyle() { ForeColor = System.Drawing.Color.Blue };
                    }
                    else {
                        row.DefaultCellStyle = new DataGridViewCellStyle() { ForeColor = System.Drawing.Color.Black };
                    }
                }
            }

            if (UnbalancedVerificationNumbers != null) {
                foreach (DataGridViewRow row in transactionsDataGridView.Rows) {
                    if (UnbalancedVerificationNumbers.Contains((int)row.Cells[CashBookTransaction.fVerificationNo].Value)) {
                        row.DefaultCellStyle = new DataGridViewCellStyle() { ForeColor = System.Drawing.Color.Red };
                    }
                }
            }
        }

        private void CashBookForm_Load(object sender, EventArgs e)
        {
            totalToolStripStatusLabel.Text = "";

            // Fyll i datumtypcomboboxen

            var dateTypes = new List<DictionaryItem<int, string>>();
            dateTypes.Add(new DictionaryItem<int, string>() { Key = (int)CashBoxDateType.TransactionDate, Value = "Kassadatum" });
            dateTypes.Add(new DictionaryItem<int, string>() { Key = (int)CashBoxDateType.AccountingDate, Value = "Bokf.datum." });

            dateTypeComboBox.ComboBox.DisplayMember = "Value";
            dateTypeComboBox.ComboBox.ValueMember = "Key";
            dateTypeComboBox.ComboBox.DataSource = dateTypes;
            dateTypeComboBox.ComboBox.SelectedValue = 1;

            LoadMonthComboBoxItems();

            using (var core = new StandardBusinessLayer(DataCache))
            {
                core.Connect();

                // Users
                var users = (from c in core.GetUsers()
                             select new DictionaryItem<int, string>()
                                {
                                    Key = c.No,
                                    Value = c.Name
                                }).ToList();

                users.Insert(0, new DictionaryItem<int, string>() { Key = 0, Value = "Alla användare" });
                
                userToolStripComboBox.ComboBox.DisplayMember = "Value";
                userToolStripComboBox.ComboBox.ValueMember = "Key";
                userToolStripComboBox.ComboBox.DataSource = users;
                userToolStripComboBox.ComboBox.SelectedValue = 0;

                // Categories
                var categories = (from c in core.GetCategoriesHierarchical()
                                  select new DictionaryItem<int, string>()
                                  {
                                      Key = c.Key,
                                      Value = c.Value
                                  }).ToList();

                categories.Insert(0, new DictionaryItem<int, string>() { Key = 0, Value = "*Alla kategorier*" });

                categoryToolStripComboBox.ComboBox.DisplayMember = "Value";
                categoryToolStripComboBox.ComboBox.ValueMember = "Key";
                categoryToolStripComboBox.ComboBox.DataSource = categories;
                categoryToolStripComboBox.ComboBox.SelectedValue = 0;
            }

            LoadTransactionGrid();
            FormLoading = false;
            EnableDisableControls();
        }

        private void LoadMonthComboBoxItems()
        {
            using (var core = new StandardBusinessLayer(DataCache))
            {
                core.Connect();

                CashBoxDateType dateType = (CashBoxDateType)dateTypeComboBox.ComboBox.SelectedValue;

                DateTime? firstAccountTransactionDate = core.GetEarliestVerificationDate(dateType);
                Date firstDate = CurrentApplication.DateNow;

                if (firstAccountTransactionDate != null)
                {
                    firstDate = new Date((DateTime) firstAccountTransactionDate);
                }

                // Fix so that the date is the first in its month
                firstDate = new Date(firstDate.Year, firstDate.Month, 1);

                // Month
                var monthList = new List<DictionaryItem<Date, string>>();

                DateTime? lastDate = core.GetLatestVerificationDate(dateType);
                Date date = CurrentApplication.DateNow;

                if (lastDate != null)
                {
                    date = (DateTime)lastDate;
                    date = new Date(date.Year, date.Month, 1);
                }

                while ((Date) date >= firstDate)
                {
                    monthList.Add(new DictionaryItem<Date, string>() {Key = date, Value = date.ToString("yyyy-MM")});
                    date = date.AddMonths(-1);
                }

                monthToolStripComboBox.ComboBox.ValueMember = "Key";
                monthToolStripComboBox.ComboBox.DisplayMember = "Value";
                monthToolStripComboBox.ComboBox.DataSource = monthList;
            }
        }

        private void CalculateTotal()
        {
            decimal total = 0;

            foreach (DataGridViewRow row in transactionsDataGridView.Rows)
            {
                total += (decimal)row.Cells[CashBookTransaction.fAmount].Value;
            }

            totalToolStripStatusLabel.Text = "Summa: " + total.ToString(CurrentApplication.MoneyDisplayFormat);
            UpdateRowHighlightsNoDatabase(ApplicationEvents.LastAccountTransactionSelectionChangedEventArgs);
        }

        private void LoadTransactionGrid()
        {
            using (var core = new StandardBusinessLayer(DataCache)) {
                core.Connect();

                var transactions = core.GetCashBookTransactions(
                    (Date)monthToolStripComboBox.ComboBox.SelectedValue,
                    (int)userToolStripComboBox.ComboBox.SelectedValue,
                    (int)categoryToolStripComboBox.ComboBox.SelectedValue,
                    (CashBoxDateType)dateTypeComboBox.ComboBox.SelectedValue);

                transactionsDataGridView.DataSource = transactions;

                // Hide all columns
                foreach (DataGridViewColumn column in transactionsDataGridView.Columns) {
                    column.Visible = false;
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                transactionsDataGridView.Columns[CashBookTransaction.fVerificationSerialNo].Visible = true;
                transactionsDataGridView.Columns[CashBookTransaction.fVerificationSerialNo].Width = 65;
                transactionsDataGridView.Columns[CashBookTransaction.fVerificationSerialNo].HeaderText = "Nr";

                transactionsDataGridView.Columns[CashBookTransaction.fTransactionTime].Visible = ((CashBoxDateType)dateTypeComboBox.ComboBox.SelectedValue == CashBoxDateType.TransactionDate);
                transactionsDataGridView.Columns[CashBookTransaction.fTransactionTime].Width = 70;
                transactionsDataGridView.Columns[CashBookTransaction.fTransactionTime].HeaderText = "Kassadatum";

                transactionsDataGridView.Columns[CashBookTransaction.fAccountingDate].Visible = ((CashBoxDateType)dateTypeComboBox.ComboBox.SelectedValue == CashBoxDateType.AccountingDate);
                transactionsDataGridView.Columns[CashBookTransaction.fAccountingDate].Width = 70;
                transactionsDataGridView.Columns[CashBookTransaction.fAccountingDate].HeaderText = "Bokf.datum";

                transactionsDataGridView.Columns[CashBookTransaction.fNote].Visible = true;
                transactionsDataGridView.Columns[CashBookTransaction.fNote].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                transactionsDataGridView.Columns[CashBookTransaction.fNote].HeaderText = "Notering";

                transactionsDataGridView.Columns[CashBookTransaction.fCategoryName].Visible = true;
                transactionsDataGridView.Columns[CashBookTransaction.fCategoryName].HeaderText = "Kategori";
                transactionsDataGridView.Columns[CashBookTransaction.fCategoryName].Width = 200;

                transactionsDataGridView.Columns[CashBookTransaction.fAmount].Width = CurrentApplication.GridColumnAmountWidth;
                transactionsDataGridView.Columns[CashBookTransaction.fAmount].Visible = true;
                transactionsDataGridView.Columns[CashBookTransaction.fAmount].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                transactionsDataGridView.Columns[CashBookTransaction.fAmount].HeaderText = "Belopp";

                transactionsDataGridView.Columns[CashBookTransaction.fAmount].DisplayIndex = 0;
                transactionsDataGridView.Columns[CashBookTransaction.fCategoryName].DisplayIndex = 0;
                transactionsDataGridView.Columns[CashBookTransaction.fNote].DisplayIndex = 0;
                transactionsDataGridView.Columns[CashBookTransaction.fAccountingDate].DisplayIndex = 0;
                transactionsDataGridView.Columns[CashBookTransaction.fAccountingDate].DisplayIndex = 0;
                transactionsDataGridView.Columns[CashBookTransaction.fTransactionTime].DisplayIndex = 0;
                transactionsDataGridView.Columns[CashBookTransaction.fVerificationSerialNo].DisplayIndex = 0;

                UnbalancedVerificationNumbers = DataCache.GetUnbalancedAndEmptyVerifications().Select(v => v.No).ToArray(); //core.GetUnbalancedVerificationNumbers();

                ((MainForm)ParentForm).CalculateBalance();
                CalculateTotal();

                if (transactionsDataGridView.Rows.Count > 0) {
                    transactionsDataGridView.Rows[0].Selected = false;
                }
            }
        }

        private int[] UnbalancedVerificationNumbers
        {
            get;
            set;
        }

        private void transactionsDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0) {
                if (transactionsDataGridView.Columns[e.ColumnIndex].Name == CashBookTransaction.fAmount) {
                    e.Value = ((decimal)e.Value).ToString(CurrentApplication.MoneyDisplayFormat) + " kr";
                }
            }
        }

        private void transactionsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int no = (int)transactionsDataGridView.Rows[e.RowIndex].Cells[CashBookTransaction.fNo].Value;
                int userNo = (int)transactionsDataGridView.Rows[e.RowIndex].Cells[CashBookTransaction.fUserNo].Value;
                int categoryNo = (int)transactionsDataGridView.Rows[e.RowIndex].Cells[CashBookTransaction.fCategoryNo].Value;
                int verificationNo = (int)transactionsDataGridView.Rows[e.RowIndex].Cells[CashBookTransaction.fVerificationNo].Value;

                var form = new CashBookTransactionForm(DataCache, MainForm.Guesser, no, verificationNo);
                DialogResult result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    CashBookTransaction transaction;
                    using (var core = new StandardBusinessLayer(DataCache))
                    {
                        core.Connect();
                        transaction = core.UpdateCashBookTransaction(no, form.VerificationDate, form.AccountingDate, form.UserNo, form.CategoryNo, form.Amount, form.Note);
                    }

                    LoadTransactionGrid();
                    SelectGridTransaction(no);

                    ApplicationEvents.OnCashBookTransactionUpdated(no, transaction.VerificationNo);
                }
            }
        }

        private void SelectGridTransaction(int transactionNo)
        {
            foreach (DataGridViewRow row in transactionsDataGridView.Rows)
            {
                if ((int)row.Cells[CashBookTransaction.fNo].Value == transactionNo)
                {
                    row.Selected = true;
                    return;
                }
            }
        }

        private bool FormLoading
        {
            get;
            set;
        }

        private void categoryToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!FormLoading)
            {
                LoadTransactionGrid();
            }
        }

        private void userToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!FormLoading)
            {
                LoadTransactionGrid();
            }
        }

        private void monthToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!FormLoading)
            {
                LoadTransactionGrid();
            }
        }

        private void transactionsDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            int cashBookTransactionNo = 0;
            int verificationNo = 0;
            
            if (transactionsDataGridView.SelectedRows.Count > 0) {
                cashBookTransactionNo = (int)transactionsDataGridView.SelectedRows[0].Cells[CashBookTransaction.fNo].Value;
                verificationNo = (int)transactionsDataGridView.SelectedRows[0].Cells[CashBookTransaction.fVerificationNo].Value;
            }

            ApplicationEvents.OnCashBookTransactionSelectionChanged(cashBookTransactionNo, verificationNo);
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

        private void CashBookForm_Activated(object sender, EventArgs e)
        {
            if (_firstActivate) {
                if (transactionsDataGridView.Rows.Count > 0) {
                    transactionsDataGridView.Rows[0].Selected = false;
                }

                UpdateRowHighlightsNoDatabase(ApplicationEvents.LastAccountTransactionSelectionChangedEventArgs);

                _firstActivate = false;
            }
        }

        private void deleteToolStripButton_Click(object sender, EventArgs e)
        {
            DeleteSelectedTransaction();
        }

        private void DeleteSelectedTransaction()
        {
            DialogResult result = MessageBox.Show("Du kommer att ta bort den markerade kassabokstransaktionen. Vill du även ta bort den aktuella affärshändelsen med alla associerade konto- och kassabokstransaktioner?\n\nJa: Alla assicierade transaktioner kommer att tas bort.\nNej: Endast den markerade transaktionen kommer att tas bort.", CurrentApplication.Name, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3);

            int transactionNo = (int)transactionsDataGridView.SelectedRows[0].Cells[CashBookTransaction.fNo].Value;
            int verificationNo = (int)transactionsDataGridView.SelectedRows[0].Cells[CashBookTransaction.fVerificationNo].Value;

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
                            DataCache.DeleteCashBookTransaction(transactionNo);
                            ApplicationEvents.OnCashBookTransactionDeleted(transactionNo, verificationNo);
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

        private void EnableDisableControls()
        {
            deleteToolStripButton.Enabled = transactionsDataGridView.SelectedRows.Count > 0;
            transactionContextMenuStrip.Enabled = transactionsDataGridView.SelectedRows.Count > 0;
        }

        private void newCashBookItemButton_Click(object sender, EventArgs e)
        {
            CashBookTransactionForm form = new CashBookTransactionForm(DataCache, MainForm.Guesser);

            using (var core = new StandardBusinessLayer(DataCache)) {
                core.Connect();

                int accountingYear = core.GetCashBoxSettings(CashBoxSettingsNo.CurrentApplicationNo).AccountingYear;
                Verification verification = core.GetFirstUnbalancedVerification(accountingYear);

                if (verification != null) {
                    decimal sum = core.GetVerificationBalance(verification.No);

                    form.Amount = sum;
                }
            }

            // Show dialog
            DialogResult result = form.ShowDialog();

            if (result == DialogResult.OK) {
                CashBookTransaction transaction;

                using (var core = new StandardBusinessLayer(DataCache)) {
                    core.Connect();

                    transaction = core.AddCashBookTransaction(form.VerificationNo, form.VerificationDate, form.AccountingDate, form.UserNo,
                                                              form.CategoryNo, form.Amount, form.Note);
                }

                LoadTransactionGrid();
                SelectGridTransaction(transaction.No);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteSelectedTransaction();
        }

        private void transactionsDataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                transactionsDataGridView.Rows[e.RowIndex].Selected = true;
            }
        }

        private void dateTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!FormLoading)
            {
                FormLoading = true;

                Date monthComboValue = (Date)monthToolStripComboBox.ComboBox.SelectedValue;
                LoadMonthComboBoxItems();

                bool contains = false;
                foreach (var item in monthToolStripComboBox.ComboBox.Items)
                {
                    DictionaryItem<Date, string> dicItem = (DictionaryItem<Date, string>)item;

                    if (dicItem.Key == monthComboValue)
                    {
                        contains = true;
                        break;
                    }
                }

                if (contains)
                {
                    monthToolStripComboBox.ComboBox.SelectedValue = monthComboValue;
                }

                LoadTransactionGrid();
                EnableDisableControls();

                FormLoading = false;
            }
        }
    }
}
