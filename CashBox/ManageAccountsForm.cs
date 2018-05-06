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
    public partial class ManageAccountsForm : Form
    {
        public ManageAccountsForm(DataCache dataCache)
        {
            InitializeComponent();
            DataCache = dataCache;
            IsDirty = false;
            IgnoreAccountDropDownChange = false;
            IgnoreUserDropDownChange = false;
            NewAccount = false;
        }

        private DataCache DataCache
        {
            get; set;
        }

        private bool NewAccount
        {
            get;
            set;
        }

        private void EnableDisableControls()
        {
            settingsGroupBox.Enabled = (accountComboBox.SelectedValue != null && (int)accountComboBox.SelectedValue > 0) || NewAccount;
            saveButton.Enabled = IsDirty;
            deleteAccountButton.Enabled = accountComboBox.SelectedValue != null && (int)accountComboBox.SelectedValue > 0;
        }

        private Account CurrentAccount
        {
            get;
            set;
        }

        private void UpdateFormFromProperty()
        {
            if (CurrentAccount != null)
            {
                typeAssetRadioButton.Checked = CurrentAccount.Type == AccountType.Asset;
                typeDebtRadioButton.Checked = CurrentAccount.Type == AccountType.Debt;
                nameTextBox.Text = CurrentAccount.Name;
                userComboBox.SelectedValue = CurrentAccount.UserNo;
                balanceBroughtForwardAmountTextBox.Text = CurrentAccount.BalanceBroughtForwardAmount.ToString(CurrentApplication.MoneyEditFormat);
                showInDiagramCheckBox.Checked = CurrentAccount.ShowInDiagram;
                isArchivedCheckBox.Checked = CurrentAccount.IsArchived;
            }
            else
            {
                typeAssetRadioButton.Checked = false;
                typeDebtRadioButton.Checked = false;
                userComboBox.SelectedValue = 0;
                nameTextBox.Text = "";
                balanceBroughtForwardAmountTextBox.Text = "";
                showInDiagramCheckBox.Checked = true;
                isArchivedCheckBox.Checked = false;
            }

            IsDirty = false;
        }

        private bool _isDirty;
        private bool IsDirty
        {
            get { return _isDirty; }
            set
            {
                _isDirty = value;
                EnableDisableControls();
            }
        }

        private void accountComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IgnoreAccountDropDownChange)
            {
                int accountNo = (int)accountComboBox.SelectedValue;

                if (accountNo == 0)
                {
                    CurrentAccount = null;
                    UpdateFormFromProperty();
                }
                else
                {
                    using (var core = new StandardBusinessLayer(DataCache))
                    {
                        core.Connect();
                        CurrentAccount = core.GetAccount(accountNo);
                        UpdateFormFromProperty();
                    }
                }
            }

            EnableDisableControls();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            DialogResult result = System.Windows.Forms.DialogResult.Yes;

            if (IsDirty)
            {
                result = MessageBox.Show("Den valda kontot är ändrat, men inte sparat. Om du stänger dialogrutan kommer dina ändringar gå förlorade. Vill du stänga dialogrutan?", CurrentApplication.Name, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            }

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
                Close();
            }
        }

        private void userComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            IsDirty = true;
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
        }

        private void typeIncomeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            IsDirty = true;
        }

        private void typeExpenseRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            IsDirty = true;
        }

        private void isArchivedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            IsDirty = true;
        }

        private bool ValidateUserComboBox()
        {
            ComboBox ctrl = userComboBox;

            if (ctrl.SelectedValue == null || (int)ctrl.SelectedValue <= 0) {
                errorProvider.SetError(ctrl, "Du har inte valt någon ägare.");
            }
            else
            {
                errorProvider.SetError(ctrl, "");
            }

            return errorProvider.GetError(ctrl) == "";
        }

        private bool ValidateNameTextBox()
        {
            TextBox ctrl = nameTextBox;

            if (ctrl.Text.Trim() == "") {
                errorProvider.SetError(ctrl, "Fältet får inte vara tomt.");
            }
            else {
                errorProvider.SetError(ctrl, "");
            }

            return errorProvider.GetError(ctrl) == "";
        }

        private bool ValidateIsArchivedCheckBox()
        {
            CheckBox ctrl = isArchivedCheckBox;
            string errorMessage = "";

            if (ctrl.Checked)
            {
                using (var core = new StandardBusinessLayer(DataCache))
                {
                    core.Connect();
                    //decimal amount = core.CalculateAccountBalance((int)accountComboBox.SelectedValue);
                    decimal amount = DataCache.CalculateAccountBalance((int)accountComboBox.SelectedValue);
                    if (decimal.Compare(amount, 0) != 0) {
                        errorMessage = "Ett konto kan bara arkiveras om dess saldo är noll.";
                    }
                }
            }

            if (errorMessage == "") {
                errorProvider.SetError(ctrl, "");
                return true;
            }
            else {
                errorProvider.SetError(ctrl, errorMessage);
                return false;
            }
        }

        private bool ValidateInitialBalanceAmountTextBox()
        {
            TextBox ctrl = balanceBroughtForwardAmountTextBox;
            string errorMessage = "";

            if (ctrl.Enabled)
            {
                decimal tmp;

                if (ctrl.Text == "")
                {
                    errorMessage = "Fältet får inte vara tomt.";
                }
                else if (!decimal.TryParse(ctrl.Text, out tmp))
                {
                    errorMessage = "Felaktigt format. Värdet måste vara på formen \"" + CurrentApplication.MoneyEditFormat + "\".";
                }
            }

            if (errorMessage == "")
            {
                errorProvider.SetError(ctrl, "");
            }
            else
            {
                errorProvider.SetError(ctrl, errorMessage);
            }

            return errorProvider.GetError(ctrl) == "";
        }

        private bool ValidateForm()
        {
            bool b1 = ValidateUserComboBox();
            bool b2 = ValidateNameTextBox();
            bool b3 = ValidateIsArchivedCheckBox();
            bool b4 = ValidateInitialBalanceAmountTextBox();

            return b1 && b2 && b3 && b4;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            if (ValidateForm()) {
                using (var core = new StandardBusinessLayer(DataCache)) {
                    core.Connect();
                    Account account;

                    string name = nameTextBox.Text.Trim();
                    int userNo = (int)userComboBox.SelectedValue;
                    bool showInDiagram = showInDiagramCheckBox.Checked;
                    bool isArchived = isArchivedCheckBox.Checked;
                    AccountType accountType = typeDebtRadioButton.Checked ? AccountType.Debt : AccountType.Asset;

                    if (NewAccount) {
                        account = new Account(userNo, accountType, name, decimal.Parse(balanceBroughtForwardAmountTextBox.Text), isArchived, CurrentApplication.DateTimeNow, true);
                    }
                    else {
                        account = core.GetAccount((int)accountComboBox.SelectedValue);
                        account.Name = name;
                        account.UserNo = userNo;
                        account.Type = accountType;
                        account.ShowInDiagram = showInDiagram;
                        account.IsArchived = isArchived;

                        if (balanceBroughtForwardAmountTextBox.Text.Trim() == "") {
                            account.BalanceBroughtForwardAmount = 0;
                        }
                        else {
                            account.BalanceBroughtForwardAmount = decimal.Parse(balanceBroughtForwardAmountTextBox.Text);
                        }
                    }

                    core.Save(account);
                    NewAccount = false;
                    CurrentAccount = account;
                }

                ReloadForm();
            }
        }

        private bool IgnoreAccountDropDownChange
        {
            get;
            set;
        }

        private bool IgnoreUserDropDownChange
        {
            get;
            set;
        }

        private void ReloadForm()
        {
            using (var core = new StandardBusinessLayer(DataCache))
            {
                core.Connect();

                int currentAccountNo = CurrentAccount != null ? CurrentAccount.No : 0;

                IgnoreAccountDropDownChange = true;

                var accounts = (from a in core.GetAccountsByIsArchived(false, new EasyBase.Classes.SortOrder(Account.fName, OrderDirection.Ascending))
                                select new DictionaryItem<int, string>() { 
                                    Key = a.No, 
                                    Value = a.Name
                                }).ToList();

                accountComboBox.DataSource = null;
                accounts.Insert(0, new DictionaryItem<int, string>() { Key = 0, Value = "*** Välj ett konto ***" });

                var archivedAccounts = (from a in core.GetAccountsByIsArchived(true, new EasyBase.Classes.SortOrder(Account.fName, OrderDirection.Ascending))
                                        select new DictionaryItem<int, string>()
                                        {
                                            Key = a.No,
                                            Value = a.Name
                                        }).ToList();

                if (archivedAccounts.Count > 0)
                {
                    accounts.Add(new DictionaryItem<int, string>() { Key = -1, Value = "*** Arkiverade konton ***" });
                    accounts.AddRange(archivedAccounts);
                }

                accountComboBox.DisplayMember = "Value";
                accountComboBox.ValueMember = "Key";
                accountComboBox.DataSource = accounts;
                accountComboBox.SelectedValue = 0;

                userComboBox.DataSource = null;
                userComboBox.ValueMember = User.fNo;
                userComboBox.DisplayMember = User.fName;
                userComboBox.DataSource = core.GetUsers();
                userComboBox.SelectedValue = 0;

                CurrentAccount = core.GetAccount(currentAccountNo);
                UpdateFormFromProperty();

                if (CurrentAccount != null)
                {
                    accountComboBox.SelectedValue = CurrentAccount.No;
                    userComboBox.SelectedValue = CurrentAccount.UserNo;
                }

                IgnoreAccountDropDownChange = false;
            }

            IsDirty = false;
        }

        private void deleteAccountButton_Click(object sender, EventArgs e)
        {
            int accountNo = (int)accountComboBox.SelectedValue;
            bool accountDeleted = false;

            using (var core = new StandardBusinessLayer(DataCache)) {
                core.Connect();

                int accountTransactionsCount = core.CountAccountTransactionsByAccountNo(accountNo);
                if (accountTransactionsCount > 0) {
                    MessageBox.Show("Kontot kan inte tas bort eftersom den har registrerade kontotransaktioner.", CurrentApplication.Name, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                Account account = core.GetAccount(accountNo);

                DialogResult result = MessageBox.Show("Är du säker på att du vill ta bort kontot '" + account.Name + "'?", CurrentApplication.Name, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes) {
                    core.DeleteAccount(accountNo);
                    accountDeleted = true;
                }
            }

            if (accountDeleted) {
                ReloadForm();
            }
        }

        private void addAccountButton_Click(object sender, EventArgs e)
        {
            if (IsDirty) {
                DialogResult result = MessageBox.Show("Det valda kontot är ändrat. Vill du spara ändringarna innan du skapar ett nytt konto?", CurrentApplication.Name, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3);

                if (result == DialogResult.Yes) {
                    Save();
                }
                else if (result == DialogResult.Cancel) {
                    return;
                }
            }

            accountComboBox.SelectedValue = 0;
            NewAccount = true;
            nameTextBox.Text = "Nytt konto";
            nameTextBox.SelectionStart = 0;
            nameTextBox.SelectionLength = nameTextBox.Text.Length;
            nameTextBox.Focus();
            IsDirty = true;
        }

        private void ManageAccountsForm_Load(object sender, EventArgs e)
        {
            ReloadForm();
            EnableDisableControls();
        }

        private void initialBalanceCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            IsDirty = true;
        }

        private void initialBalanceDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            IsDirty = true;
        }

        private void initialBalanceAmountTextBox_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
        }

        private void typeAssetRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            IsDirty = true;
        }

        private void typeDebtRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            IsDirty = true;
        }

        private void showInDiagramCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            IsDirty = true;
        }
    }
}
