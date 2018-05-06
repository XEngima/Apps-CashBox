using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EasyBase.BusinessLayer;
using EasyBase.Classes;

namespace CashBox
{
    public partial class SettingsForm : Form
    {
        private int _fromAccountingYear;

        public SettingsForm(DataCache dataCache)
        {
            InitializeComponent();
        }

        private DataCache DataCache { get; set; }

        private void cancelButton_Click(object sender, EventArgs e)
        {

        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            using (var core = new StandardBusinessLayer(DataCache))
            {
                core.Connect();

                _fromAccountingYear = core.GetCashBoxSettings(CashBoxSettingsNo.CurrentApplicationNo).AccountingYear;
                accountingYearTextBox.Text = _fromAccountingYear.ToString();
            }
        }

        private bool ValidateAccountingYearTextBox()
        {
            TextBox ctrl = accountingYearTextBox;
            int accountingYear;
            bool successfulConversion = int.TryParse(ctrl.Text.Trim(), out accountingYear);


            if (ctrl.Text.Trim() == "")
            {
                errorProvider.SetError(ctrl, "Fältet får inte vara tomt.");
            }
            else if (!successfulConversion)
            {
                errorProvider.SetError(ctrl, "Fältet måste innehålla ett årtal på formen XXXX");
            }
            else if (accountingYear < 1900 || accountingYear >= 10000)
            {
                errorProvider.SetError(ctrl, "Året måste vara mellan 1900 och 10000.");
            }

            return errorProvider.GetError(ctrl) == "";
        }

        private bool ValidateForm()
        {
            bool b1 = ValidateAccountingYearTextBox();
            return b1;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                int accountingYear = int.Parse(accountingYearTextBox.Text.Trim());

                using (var core = new StandardBusinessLayer(DataCache))
                {
                    core.Connect();

                    CashBoxSettings settings = core.GetCashBoxSettings(CashBoxSettingsNo.CurrentApplicationNo);
                    settings.AccountingYear = accountingYear;

                    core.Save(settings);

                    //CurrentApplication.CurrentVerification = core.GetFullVerification(core.GetLastVerification(accountingYear).No);
                    //CurrentApplication.CurrentVerification = DataCache.GetVerification(DataCache.GetLastVerification().No);
                }

                if (accountingYear != _fromAccountingYear)
                {
                    var settings = DataCache.Settings;
                    settings.AccountingYear = accountingYear;
                    DataCache.Save(settings);

                    ApplicationEvents.OnAccountingYearChanged(_fromAccountingYear, accountingYear);
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Formuläret innehåller fel.", "Valideringsfel", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
