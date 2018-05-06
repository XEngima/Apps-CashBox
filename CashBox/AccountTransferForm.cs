using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EasyBase.Classes;
using EasyBase.BusinessLayer;

namespace CashBox
{
    public partial class AccountTransferForm : Form
    {
        public AccountTransferForm(DataCache dataCache)
        {
            InitializeComponent();
            DataCache = dataCache;
        }

        private DataCache DataCache { get; set; }

        private void AccountTransactionForm_Load(object sender, EventArgs e)
        {
            using (var core = new StandardBusinessLayer(DataCache)) {
                core.Connect();

                sourceAccountComboBox.ValueMember = Category.fNo;
                sourceAccountComboBox.DisplayMember = Category.fName;
                sourceAccountComboBox.DataSource = core.GetAccounts(EasyBase.Classes.SortOrder.Create(Account.fName, OrderDirection.Ascending));

                targetAccountComboBox.ValueMember = Category.fNo;
                targetAccountComboBox.DisplayMember = Category.fName;
                targetAccountComboBox.DataSource = core.GetAccounts(EasyBase.Classes.SortOrder.Create(Account.fName, OrderDirection.Ascending));
            }
        }

        public DateTime TransactionDate
        {
            get { return new DateTime(dateTimePicker.Value.Year, dateTimePicker.Value.Month, dateTimePicker.Value.Day); }
            set { dateTimePicker.Value = value; }
        }

        public decimal Amount
        {
            get { return decimal.Parse(amountTextBox.Text.Trim()); }
            set { amountTextBox.Text = value.ToString(CurrentApplication.MoneyEditFormat); }
        }

        public int SourceAccountNo
        {
            get { return (int)sourceAccountComboBox.SelectedValue; }
            set { sourceAccountComboBox.SelectedValue = value; }
        }

        public int TargetAccountNo
        {
            get { return (int)targetAccountComboBox.SelectedValue; }
            set { targetAccountComboBox.SelectedValue = value; }
        }

        public string SourceNote
        {
            get { return sourceNoteTextBox.Text.Trim(); }
            set { sourceNoteTextBox.Text = value; }
        }

        public string targetNote
        {
            get { return targetNoteTextBox.Text.Trim(); }
            set { targetNoteTextBox.Text = value; }
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

        private void amountTextBox_Validating(object sender, CancelEventArgs e)
        {
            decimal tmp;

            if (!decimal.TryParse(amountTextBox.Text, out tmp)) {
                errorProvider.SetError(amountTextBox, "Felaktigt format. Värdet måste vara på formen \"" + CurrentApplication.MoneyEditFormat + "\".");
            }
            else {
                errorProvider.SetError(amountTextBox, "");
            }
        }
    }
}
