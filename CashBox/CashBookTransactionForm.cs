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
    public partial class CashBookTransactionForm : Form
    {
        private readonly int _initialVerificationNo;
        private readonly int _initialCashBookTransactionNo;

        public CashBookTransactionForm(DataCache dataCache, Guesser guesser)
        {
            InitializeComponent();
            _initialVerificationNo = 0;
            _initialCashBookTransactionNo = 0;
            DataCache = dataCache;
            Guesser = guesser;
        }

        public CashBookTransactionForm(DataCache dataCache, Guesser guesser, int cashBookTransactionNo = 0, int verificationNo = 0)
        {
            InitializeComponent();

            _initialVerificationNo = verificationNo;
            _initialCashBookTransactionNo = cashBookTransactionNo;
            DataCache = dataCache;
            Guesser = guesser;

            if (cashBookTransactionNo > 0 && verificationNo == 0)
            {
                throw new ArgumentException("Verification number must be set if account transaction number is set.");
            }
        }

        private DataCache DataCache
        {
            get;
            set;
        }

        private Guesser Guesser
        {
            get;
            set;
        }

        public int UserNo
        {
            get { return (int)userComboBox.SelectedValue; }
        }

        public DateTime VerificationDate
        {
            get { return new DateTime(dateTimePicker.Value.Year, dateTimePicker.Value.Month, dateTimePicker.Value.Day); }
        }

        public DateTime AccountingDate
        {
            get { return new DateTime(accountingDateTimePicker.Value.Year, accountingDateTimePicker.Value.Month, accountingDateTimePicker.Value.Day); }
        }

        public int CategoryNo
        {
            get { return (int)categoryComboBox.SelectedValue; }
        }

        public decimal Amount
        {
            get { return decimal.Parse(amountTextBox.Text); }
            set { amountTextBox.Text = value.ToString(CurrentApplication.MoneyEditFormat); }
        }

        public string Note
        {
            get { return noteTextBox.Text.Trim(); }
        }

        private bool ValidateVerification()
        {
            ComboBox ctrl = verificationComboBox;
            string errorMsg = "";

            if ((int)ctrl.SelectedValue == 0) {
                if (!CurrentApplication.CurrentVerification.HasTransactions) {
                    errorMsg = "Du kan inte välja en ny affärshändelse eftersom den nuvarande affärshändelsen (" + CurrentApplication.CurrentVerification + ") är tom och måste fyllas först.";
                }
                else if (!CurrentApplication.CurrentVerification.IsBalanced) {
                    errorMsg = "Du kan inte välja en ny affärshändelse eftersom den nuvarande affärshändelsen (" + CurrentApplication.CurrentVerification + ") inte är balanserad.";
                }
            }

            errorProvider.SetError(ctrl, errorMsg);
            return errorMsg == "";
        }

        private bool ValidateDateDimtePicker()
        {
            DateTimePicker ctrl = dateTimePicker;

            errorProvider.SetError(ctrl, "");

            using (var core = new StandardBusinessLayer(DataCache)) {
                core.Connect();

                int accountingYear = core.GetCashBoxSettings(CashBoxSettingsNo.CurrentApplicationNo).AccountingYear;

                if (ctrl.Value.Year != accountingYear) {
                    errorProvider.SetError(ctrl, "Du kan bara registrera transaktioner för systemets bokföringsår (" + accountingYear + ").");
                }
            }

            return errorProvider.GetError(ctrl) == "";
        }

        private bool ValidateAccountingDateDimtePicker()
        {
            DateTimePicker ctrl = accountingDateTimePicker;

            errorProvider.SetError(ctrl, "");

            using (var core = new StandardBusinessLayer(DataCache))
            {
                core.Connect();

                int accountingYear = core.GetCashBoxSettings(CashBoxSettingsNo.CurrentApplicationNo).AccountingYear;

                if (ctrl.Value.Year != accountingYear)
                {
                    errorProvider.SetError(ctrl, "Du kan bara registrera transaktioner för systemets bokföringsår (" + accountingYear + ").");
                }
            }

            return errorProvider.GetError(ctrl) == "";
        }

        private bool ValidateAmountTextBox()
        {
            TextBox ctrl = amountTextBox;
            decimal tmp;

            if (ctrl.Text == "") {
                errorProvider.SetError(ctrl, "Fältet får inte vara tomt.");
            }
            else if (!decimal.TryParse(ctrl.Text, out tmp)) {
                errorProvider.SetError(ctrl, "Felaktigt format. Värdet måste vara på formen \"" + CurrentApplication.MoneyEditFormat + "\".");
            }
            else {
                errorProvider.SetError(ctrl, "");
            }

            return errorProvider.GetError(ctrl) == "";
        }

        private bool ValidateCategoryComboBox()
        {
            ComboBox ctrl = categoryComboBox;

            if (ctrl.SelectedValue == null || (int)ctrl.SelectedValue == 0) {
                errorProvider.SetError(ctrl, "Du har inte valt någon kategori.");
            }
            else {
                errorProvider.SetError(ctrl, "");
            }

            return errorProvider.GetError(ctrl) == "";
        }

        private bool ValidateNoteTextBox()
        {
            TextBox ctrl = noteTextBox;

            if (ctrl.Text.Trim() == "") {
                errorProvider.SetError(ctrl, "Fältet får inte vara tomt.");
            }
            else {
                errorProvider.SetError(ctrl, "");
            }

            return errorProvider.GetError(ctrl) == "";
        }

        private bool ValidateForm()
        {
            bool b1 = ValidateVerification();
            bool b2 = ValidateDateDimtePicker();
            bool b3 = ValidateAccountingDateDimtePicker();
            bool b4 = ValidateAmountTextBox();
            bool b5 = ValidateCategoryComboBox();
            bool b6 = ValidateNoteTextBox();

            return b1 && b2 && b3 && b4 && b5 && b6;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm()) {
                Category category;

                using (var core = new StandardBusinessLayer(DataCache)) {
                    core.Connect();

                    category = core.GetCategory((int)categoryComboBox.SelectedValue);
                }

                if (category.Type == CategoryType.Income && decimal.Compare(Amount, 0) < 0) {
                    DialogResult result = MessageBox.Show("Varning!\n\nTransaktionen du håller på att genomföra är negativ (alltså en utgift), men den valda kategorin förväntas vara en inkomst. Detta är en ovanlig transaktion.\n\nVill du fortsätta?", "Ovanlig transaktion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result != System.Windows.Forms.DialogResult.Yes) {
                        return;
                    }
                }
                else if (category.Type == CategoryType.Expense && decimal.Compare(Amount, 0) > 0) {
                    DialogResult result = MessageBox.Show("Varning!\n\nTransaktionen du håller på att genomföra är positiv (alltså en inkomst), men den valda kategorin förväntas vara en utgift. Detta är en ovanlig transaktion.\n\nVill du fortsätta?", "Ovanlig transaktion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result != DialogResult.Yes) {
                        return;
                    }
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            else {
                MessageBox.Show("Formuläret innehåller fel.", "Fel i formuläret", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void CashBookTransactionForm_Load(object sender, EventArgs e)
        {
            using (var core = new StandardBusinessLayer(DataCache))
            {
                core.Connect();

                int accountingYear = core.GetCashBoxSettings(CashBoxSettingsNo.CurrentApplicationNo).AccountingYear;

                var verifications = (from v in DataCache.GetUnbalancedAndEmptyVerifications()
                                     orderby v.No descending
                                     select new
                                     {
                                         No = v.No,
                                         Name = v.Year.ToString() + "-" + v.SerialNo.ToString()
                                     }).ToList();

                if (!verifications.Any())
                {
                    verifications.Insert(0, new { No = 0, Name = "Ny affärshändelse" });
                }

                if (_initialVerificationNo > 0 && verifications.All(x => x.No != _initialVerificationNo))
                {
                    Verification verification = DataCache.GetVerification(_initialVerificationNo);
                    //Verification verification = core.GetVerification(_initialVerificationNo);
                    verifications.Add(new { No = verification.No, Name = verification.Date.Year.ToString() + "-" + verification.SerialNo.ToString() });
                }

                verificationComboBox.ValueMember = "No";
                verificationComboBox.DisplayMember = "Name";
                verificationComboBox.DataSource = verifications;

                categoryComboBox.ValueMember = Category.fNo;
                categoryComboBox.DisplayMember = Category.fName;
                categoryComboBox.DataSource = core.GetSubCategories();

                userComboBox.ValueMember = User.fNo;
                userComboBox.DisplayMember = User.fName;
                userComboBox.DataSource = core.GetUsers();

                if (_initialCashBookTransactionNo > 0)
                {
                    //CashBookTransaction transaction = core.GetCashBookTransaction(_initialCashBookTransactionNo);
                    CashBookTransaction transaction = DataCache.GetCashBookTransaction(_initialCashBookTransactionNo);

                    verificationComboBox.SelectedValue = _initialVerificationNo;

                    userComboBox.SelectedValue = transaction.UserNo;
                    categoryComboBox.SelectedValue = transaction.CategoryNo;
                    dateTimePicker.Value = transaction.TransactionTime;
                    accountingDateTimePicker.Value = transaction.AccountingDate;
                    amountTextBox.Text = transaction.Amount.ToString(CurrentApplication.MoneyEditFormat);
                    noteTextBox.Text = transaction.Note;
                }
                else
                {
                    verificationComboBox.SelectedIndex = verifications.Count() - 1;
                    userComboBox.SelectedValue = CurrentApplication.UserNo;

                    if (Guesser.CurrentGuess != null && Guesser.CurrentGuess.CashBookTransaction != null)
                    {
                        categoryComboBox.SelectedValue = Guesser.CurrentGuess.CashBookTransaction.CategoryNo;
                        noteTextBox.Text = Guesser.CurrentGuess.CashBookTransaction.Note;
                    }
                    else
                    {
                        categoryComboBox.SelectedValue = 0;
                        categoryComboBox.DroppedDown = true;
                    }
                }
            }

            EnableDisableControls();
        }

        private void EnableDisableControls()
        {
            verificationComboBox.Enabled = _initialCashBookTransactionNo == 0;
        }

        public int VerificationNo
        {
            get { return (int)verificationComboBox.SelectedValue; }
            set { verificationComboBox.SelectedValue = value; }
        }

        private void categoryComboBox_DropDownClosed(object sender, EventArgs e)
        {
            noteTextBox.Focus();
        }

        private void verificationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int verificationNo = (int)verificationComboBox.SelectedValue;

            if (verificationNo > 0)
            {
                using (var core = new StandardBusinessLayer(DataCache))
                {
                    core.Connect();
                    var verification = DataCache.GetVerification(verificationNo);

                    dateTimePicker.Value = verification.Date;
                    accountingDateTimePicker.Value = verification.AccountingDate;
                    amountTextBox.Text = (-verification.Balance).ToString(CurrentApplication.MoneyEditFormat);
                    noteTextBox.Text = "";
                }
            }
            else
            {
                dateTimePicker.Value = CurrentApplication.DateNow;
                accountingDateTimePicker.Value = CurrentApplication.DateNow;
                amountTextBox.Text = "";
                noteTextBox.Text = "";
            }
        }
    }
}
