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
    public partial class AccountTransactionForm : Form
    {
        private readonly int _initialAccountTransactionNo;
        private int _accountNo = 0;

        public AccountTransactionForm(DataCache dataCache)
        {
            InitializeComponent();
            DataCache = dataCache;
            _initialAccountTransactionNo = 0;
        }

        public AccountTransactionForm(DataCache dataCache, int accountTransactionNo)
        {
            InitializeComponent();
            DataCache = dataCache;
            _initialAccountTransactionNo = accountTransactionNo;
        }

        private DataCache DataCache { get; set; }

        private void AccountTransactionForm_Load(object sender, EventArgs e)
        {
            using (var core = new StandardBusinessLayer(DataCache))
            {
                core.Connect();

                accountComboBox.DisplayMember = Account.fName;
                accountComboBox.ValueMember = Account.fNo;

                Condition notArchived = new Condition(Account.fIsArchived, CompareOperator.NotEqualTo, true);
                accountComboBox.DataSource = core.GetAccounts(notArchived);
            }

            if (_initialAccountTransactionNo > 0)
            {
                using (var core = new StandardBusinessLayer(DataCache))
                {
                    core.Connect();

                    //AccountTransaction transaction = core.GetAccountTransaction(_initialAccountTransactionNo);
                    AccountTransaction transaction = DataCache.GetAccountTransaction(_initialAccountTransactionNo);

                    accountComboBox.SelectedValue = transaction.AccountNo;
                    dateTimePicker.Value = transaction.TransactionTime;
                    amountTextBox.Text = transaction.Amount.ToString(CurrentApplication.MoneyEditFormat);
                    noteTextBox.Text = transaction.Note;
                }

                accountComboBox.Enabled = false;
            }
            else
            {
                accountComboBox.SelectedValue = _accountNo;
            }

            amountTextBox.Focus();
        }

        public int SourceAccountTransactionNo
        {
            get { return int.Parse(accountTransactionNoTextBox.Text); }
            set { accountTransactionNoTextBox.Text = value.ToString(); }
        }

        public int AccountNo
        {
            get { return (int)accountComboBox.SelectedValue; }
            set { _accountNo = value; }
        }

        public DateTime Date
        {
            get { return new DateTime(dateTimePicker.Value.Year, dateTimePicker.Value.Month, dateTimePicker.Value.Day); }
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

        private void okButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm()) {
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
            else {
                MessageBox.Show("Formuläret innehåller fel.", "Fel i formuläret", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
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

        private bool ValidateForm()
        {
            bool b1 = ValidateAmountTextBox();

            return b1;
        }

        private void accountTransactionNoTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void accountComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
