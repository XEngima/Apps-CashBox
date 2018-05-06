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
    public partial class CreateUsersForm : Form
    {
        public CreateUsersForm()
        {
            InitializeComponent();
        }

        private bool ValidateUser1TextBox()
        {
            TextBox ctrl = user1TextBox;

            if (ctrl.Text.Trim() == "")
            {
                errorProvider.SetError(ctrl, "Fältet får inte vara tomt.");
            }
            else
            {
                errorProvider.SetError(ctrl, "");
            }

            return errorProvider.GetError(ctrl) == "";
        }

        private bool ValidateUser2TextBox()
        {
            TextBox ctrl = user2TextBox;

            if (ctrl.Text.Trim() == user1TextBox.Text.Trim() && user1TextBox.Text.Trim() != "")
            {
                errorProvider.SetError(ctrl, "Det får inte finnas två användare med samma namn.");
            }
            else
            {
                errorProvider.SetError(ctrl, "");
            }

            return errorProvider.GetError(ctrl) == "";
        }

        private bool ValidateForm()
        {
            bool b1 = ValidateUser1TextBox();
            bool b2 = ValidateUser2TextBox();

            return b1 && b2;
        }

        private void createUsersButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                using (var core = new StandardBusinessLayer())
                {
                    core.Connect();

                    User user = new User(user1TextBox.Text.Trim(), user1TextBox.Text.Trim().ToLower() + "@cashbox.se", "", CurrentApplication.DateTimeNow);
                    core.Save(user);

                    string genitiveName = user.Name + (user.Name.ToLower().EndsWith("s") ? "" : "s");
                    Account account = new Account(user.No, AccountType.Asset, genitiveName + " lönekonto", 0, false, CurrentApplication.DateTimeNow, true);
                    core.Save(account);

                    if (user2TextBox.Text.Trim() != "")
                    {
                        user = new User(user2TextBox.Text.Trim(), user2TextBox.Text.Trim().ToLower() + "@cashbox.se", "", CurrentApplication.DateTimeNow);
                        core.Save(user);

                        genitiveName = user.Name + (user.Name.ToLower().EndsWith("s") ? "" : "s");
                        account = new Account(user.No, AccountType.Asset, genitiveName + " lönekonto", 0, false, CurrentApplication.DateTimeNow, true);
                        core.Save(account);
                    }
                }

                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
        }

        private void CreateUsersForm_Load(object sender, EventArgs e)
        {

        }
    }
}
