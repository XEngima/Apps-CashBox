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
    public partial class DepositWithdrawalForm : Form
    {
        private TagHandler _tagHandler;
        private readonly int _initialAccountNo;
        private readonly int _initialVerificationNo;
        private readonly int _initialAccountTransactionNo;
        private bool _accountingDateSameAsTransactionTime;
        private bool _userChangingTransactionTime = true;

        public DepositWithdrawalForm(DataCache dataCache, Guesser guesser, string accountName, int accountNo, int accountTransactionNo = 0, int verificationNo = 0)
        {
            InitializeComponent();
            DataCache = dataCache;
            AccountName = accountName;
            _initialAccountNo = accountNo;
            _initialVerificationNo = verificationNo;
            _initialAccountTransactionNo = accountTransactionNo;
            Guesser = guesser;
            _okToAutoUpdateGuesses = true;
            Guesser.CurrentGuessIndex = -1;

            if (accountTransactionNo > 0 && verificationNo == 0)
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

        private string _accountName;
        private bool _okToAutoUpdateGuesses;

        private string AccountName
        {
            get { return _accountName; }
            set { _accountName = value; Text = "Insättning/Uttag - " + value; }
        }

        public TagComboBoxItem GetTagComboBoxItem
        {
            get
            {
                return (TagComboBoxItem) tagComboBox.SelectedValue;
            }
        }

        private Account CurrentAccount
        {
            get;
            set;
        }

        private void AccountTransactionForm_Load(object sender, EventArgs e)
        {
            suggestion1CheckBox.Visible = false;
            suggestion2CheckBox.Visible = false;
            suggestion3CheckBox.Visible = false;

            using (var core = new StandardBusinessLayer(DataCache))
            {
                core.Connect();

                CurrentAccount = core.GetAccount(_initialAccountNo);
                tagComboBox.Visible = CurrentAccount.Type == AccountType.Asset;
                tagLabel.Visible = tagComboBox.Visible;
                int accountingYear = DataCache.Settings.AccountingYear;

                //var verifications = (from v in core.GetUnbalancedAndEmptyVerifications(accountingYear)
                //                     orderby v.No descending 
                //                     select new
                //                                {
                //                                    No = v.No,
                //                                    Name = v.Year.ToString() + "-" + v.SerialNo.ToString()
                //                                }).ToList();

                var verifications = (from v in DataCache.GetUnbalancedAndEmptyVerifications()
                                     orderby v.No descending
                                     select new
                                     {
                                         No = v.No,
                                         Name = v.Year.ToString() + "-" + v.SerialNo.ToString()
                                     }).ToList();

                if (!verifications.Any())
                {
                    verifications.Insert(0, new {No = 0, Name = "Ny affärshändelse"});
                }

                if (_initialVerificationNo > 0 && verifications.All(x => x.No != _initialVerificationNo))
                {
                    Verification verification = DataCache.GetVerification(_initialVerificationNo);
                    //Verification verification = core.GetVerification(_initialVerificationNo);
                    verifications.Add(new {No = verification.No, Name=verification.Date.Year.ToString() + "-" + verification.SerialNo.ToString()});
                }

                verificationComboBox.ValueMember = "No";
                verificationComboBox.DisplayMember = "Name";
                verificationComboBox.DataSource = verifications;

                if (_initialAccountTransactionNo > 0) {
                    AccountTransaction transaction = DataCache.GetAccountTransaction(_initialAccountTransactionNo);

                    verificationComboBox.SelectedValue = _initialVerificationNo;

                    dateTimePicker.Value = transaction.TransactionTime;
                    accountingDateTimePicker.Value = transaction.AccountingDate;
                    amountTextBox.Text = transaction.Amount.ToString(CurrentApplication.MoneyEditFormat);
                    noteTextBox.Text = transaction.Note;
                }
                else
                {
                    verificationComboBox.SelectedIndex = verifications.Count() - 1;
                }

                _tagHandler = new TagHandler(DataCache, _initialAccountNo);

                tagComboBox.DataSource = _tagHandler.ComboBoxItems;
                tagComboBox.SelectedItem = _tagHandler.GetDefaultComboBoxItem();
            }

            _accountingDateSameAsTransactionTime = false;
            if (dateTimePicker.Value == accountingDateTimePicker.Value)
            {
                _accountingDateSameAsTransactionTime = true;
            }

            EnableDisableControls();
        }

        private void EnableDisableControls()
        {
            verificationComboBox.Enabled = _initialAccountTransactionNo == 0;
        }

        public int VerificationNo
        {
            get { return (int)verificationComboBox.SelectedValue; }
            set { verificationComboBox.SelectedValue = value; }
        }

        public DateTime VerificationDate
        {
            get { return new DateTime(dateTimePicker.Value.Year, dateTimePicker.Value.Month, dateTimePicker.Value.Day); }
        }

        public DateTime AccountingDate
        {
            get { return new DateTime(accountingDateTimePicker.Value.Year, accountingDateTimePicker.Value.Month, accountingDateTimePicker.Value.Day); }
        }

        public decimal Amount
        {
            get { return decimal.Parse(amountTextBox.Text); }
        }

        public string Note
        {
            get { return noteTextBox.Text.Trim(); }
        }

        public bool SuggestCashBookTransaction
        {
            get { return Guesser.CurrentGuess != null; }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            var comboBoxItem = tagComboBox.SelectedItem as TagComboBoxItem;

            decimal totalRelativeTagValue = 0;
            foreach (var item in _tagHandler.ComboBoxItems)
            {
                if (item.Action == TagHandlerAction.Specified && item.AccountTag.Type == AccountTagType.PercentOfRest)
                {
                    totalRelativeTagValue += item.AccountTag.RelativeValue;
                }
            }

            if ((comboBoxItem.Action == TagHandlerAction.Specified &&
                comboBoxItem.AccountTag.Type == AccountTagType.PercentOfRest &&
                comboBoxItem.AccountTag.RelativeValue < 1) ||
                (comboBoxItem.Action == TagHandlerAction.Untagged &&
                totalRelativeTagValue > 0))
            {
                DialogResult dialogResult =
                    MessageBox.Show(
                        "Den valda märkningen innebär att de relativa märkningarnas procentsatser kommer att räknas om. Är du säker på att du vill fortsätta?",
                        "Räkna om märkningar?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }

            if (ValidateForm()) {
                DialogResult = DialogResult.OK;
                Close();
            }
            else {
                MessageBox.Show("Formuläret innehåller fel.", "Fel i formuläret", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
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

        //private bool ValidateAccountingDateDimtePicker()
        //{
        //    DateTimePicker ctrl = accountingDateTimePicker;

        //    errorProvider.SetError(ctrl, "");

        //    using (var core = new StandardBusinessLayer(DataCache))
        //    {
        //        core.Connect();

        //        int accountingYear = core.GetCashBoxSettings(CashBoxSettingsNo.CurrentApplicationNo).AccountingYear;

        //        if (ctrl.Value.Year != accountingYear)
        //        {
        //            errorProvider.SetError(ctrl, "Du kan bara registrera transaktioner för systemets bokföringsår (" + accountingYear + ").");
        //        }
        //    }

        //    return errorProvider.GetError(ctrl) == "";
        //}

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

        private bool ValidateForm()
        {
            bool b1 = ValidateVerification();
            bool b2 = ValidateDateDimtePicker();
            bool b3 = ValidateAmountTextBox();

            return b1 && b2 && b3;
        }

        private void verificationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int verificationNo = (int) verificationComboBox.SelectedValue;

            if (verificationNo > 0)
            {
                using (var core = new StandardBusinessLayer(DataCache))
                {
                    core.Connect();
                    var verification = DataCache.GetVerification(verificationNo);

                    dateTimePicker.Value = verification.Date;
                    accountingDateTimePicker.Value = verification.AccountingDate;
                    amountTextBox.Text = verification.Balance.ToString(CurrentApplication.MoneyEditFormat);
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

        private void amountTextBox_Leave(object sender, EventArgs e)
        {
            //if (Guesser.Guesses.Any())
            //{
            //    noteTextBox.Text = Guesser.Guesses[0].AccountTransaction.Note;
            //}
        }

        private void noteTextBox_Leave(object sender, EventArgs e)
        {
            //if (Guesser.Guesses.Any())
            //{
            //    amountTextBox.Text = Guesser.Guesses[0].AccountTransaction.Amount.ToString(CurrentApplication.MoneyEditFormat);
            //}
        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (_accountingDateSameAsTransactionTime)
            {
                _userChangingTransactionTime = false;
                accountingDateTimePicker.Value = dateTimePicker.Value;
                _userChangingTransactionTime = true;
            }
        }

        private void accountingDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (_userChangingTransactionTime)
            {
                _accountingDateSameAsTransactionTime = false;
            }
        }

        private void noteTextBox_TextChanged(object sender, EventArgs e)
        {
            decimal amount;
            decimal.TryParse(amountTextBox.Text.Trim(), out amount);
            string note = noteTextBox.Text.Trim();

            if (_okToAutoUpdateGuesses)
            {
                Guesser.Guess(_initialAccountNo, amount, note);

                suggestion1CheckBox.Checked = false;
                suggestion2CheckBox.Checked = false;
                suggestion3CheckBox.Checked = false;

                UpdateGuesses();
            }
        }

        private void amountTextBox_TextChanged(object sender, EventArgs e)
        {
            decimal amount;
            decimal.TryParse(amountTextBox.Text.Trim(), out amount);
            string note = noteTextBox.Text.Trim();

            if (_okToAutoUpdateGuesses)
            {
                Guesser.Guess(_initialAccountNo, amount, note);

                suggestion1CheckBox.Checked = false;
                suggestion2CheckBox.Checked = false;
                suggestion3CheckBox.Checked = false;

                UpdateGuesses();
            }
        }

        private void UpdateGuesses()
        {
            if (Guesser.Guesses.Count() >= 1)
            {
                suggestion1CheckBox.Text =
                    Guesser.Guesses[0].AccountTransaction.Note.Trim() + ", " +
                    Guesser.Guesses[0].CashBookTransaction.Note.Trim();
            }
            if (Guesser.Guesses.Count() >= 2)
            {
                suggestion2CheckBox.Text =
                    Guesser.Guesses[1].AccountTransaction.Note.Trim() + ", " +
                    Guesser.Guesses[1].CashBookTransaction.Note.Trim();
            }
            if (Guesser.Guesses.Count() >= 3)
            {
                suggestion3CheckBox.Text =
                    Guesser.Guesses[2].AccountTransaction.Note.Trim() + ", " +
                    Guesser.Guesses[2].CashBookTransaction.Note.Trim();
            }

            suggestion1CheckBox.Visible = Guesser.Guesses.Count() >= 1;
            suggestion2CheckBox.Visible = Guesser.Guesses.Count() >= 2;
            suggestion3CheckBox.Visible = Guesser.Guesses.Count() >= 3;
        }

        private void suggestion1CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (suggestion1CheckBox.Checked)
            {
                Guesser.CurrentGuessIndex = 0;
                suggestion2CheckBox.Checked = false;
                suggestion3CheckBox.Checked = false;

                _okToAutoUpdateGuesses = false;

                if (amountTextBox.Text.Trim() == "")
                {
                    amountTextBox.Text = Guesser.Guesses[0].AccountTransaction.Amount.ToString(CurrentApplication.MoneyEditFormat);
                }
                string text = Guesser.Guesses[0].AccountTransaction.Note;
                if (noteTextBox.Text.Trim() == "" || text.ToLower().Contains(noteTextBox.Text.Trim().ToLower()))
                {
                    noteTextBox.Text = text;
                }

                _okToAutoUpdateGuesses = true;

                ToolTip toolTip1 = new ToolTip();
                toolTip1.AutoPopDelay = 5000;
                toolTip1.InitialDelay = 100;
                toolTip1.ReshowDelay = 100;
                toolTip1.ShowAlways = true;
                toolTip1.SetToolTip(suggestion1CheckBox, Guesser.Guesses[0].AccountTransaction.AccountingDate.ToShortDateString() + ": " + Guesser.Guesses[0].AccountTransaction.Amount.ToString(CurrentApplication.MoneyDisplayFormat));
            }
            else
            {
                Guesser.CurrentGuessIndex = -1;
            }
        }

        private void suggestion2CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (suggestion2CheckBox.Checked)
            {
                Guesser.CurrentGuessIndex = 1;
                suggestion1CheckBox.Checked = false;
                suggestion3CheckBox.Checked = false;

                _okToAutoUpdateGuesses = false;

                if (amountTextBox.Text.Trim() == "")
                {
                    amountTextBox.Text = Guesser.Guesses[1].AccountTransaction.Amount.ToString(CurrentApplication.MoneyEditFormat);
                }
                string text = Guesser.Guesses[1].AccountTransaction.Note;
                if (noteTextBox.Text.Trim() == "" || text.ToLower().Contains(noteTextBox.Text.Trim().ToLower()))
                {
                    noteTextBox.Text = text;
                }

                _okToAutoUpdateGuesses = true;

                ToolTip toolTip2 = new ToolTip();
                toolTip2.AutoPopDelay = 5000;
                toolTip2.InitialDelay = 100;
                toolTip2.ReshowDelay = 100;
                toolTip2.ShowAlways = true;
                toolTip2.SetToolTip(suggestion2CheckBox, Guesser.Guesses[1].AccountTransaction.AccountingDate.ToShortDateString() + ": " + Guesser.Guesses[1].AccountTransaction.Amount.ToString(CurrentApplication.MoneyDisplayFormat));
            }
            else
            {
                Guesser.CurrentGuessIndex = -1;
            }
        }

        private void suggestion3CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (suggestion3CheckBox.Checked)
            {
                Guesser.CurrentGuessIndex = 2;
                suggestion1CheckBox.Checked = false;
                suggestion2CheckBox.Checked = false;

                _okToAutoUpdateGuesses = false;

                if (amountTextBox.Text.Trim() == "")
                {
                    amountTextBox.Text = Guesser.Guesses[2].AccountTransaction.Amount.ToString(CurrentApplication.MoneyEditFormat);
                }
                string text = Guesser.Guesses[2].AccountTransaction.Note;
                if (noteTextBox.Text.Trim() == "" || text.ToLower().Contains(noteTextBox.Text.Trim().ToLower()))
                {
                    noteTextBox.Text = text;
                }

                _okToAutoUpdateGuesses = true;

                ToolTip toolTip3 = new ToolTip();
                toolTip3.AutoPopDelay = 5000;
                toolTip3.InitialDelay = 100;
                toolTip3.ReshowDelay = 100;
                toolTip3.ShowAlways = true;
                toolTip3.SetToolTip(suggestion3CheckBox, Guesser.Guesses[2].AccountTransaction.AccountingDate.ToShortDateString() + ": " + Guesser.Guesses[2].AccountTransaction.Amount.ToString(CurrentApplication.MoneyDisplayFormat));
            }
            else
            {
                Guesser.CurrentGuessIndex = -1;
            }
        }
    }
}
