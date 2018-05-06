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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        public int UserNo
        {
            get { return (int)userComboBox.SelectedValue; }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            Text = CurrentApplication.Name;

            if (Environment.CommandLine.Contains(" /developer"))
            {
                Text += " - Debug";
            }

            using (var core = new StandardBusinessLayer()) {
                core.Connect();

                userComboBox.ValueMember = User.fNo;
                userComboBox.DisplayMember = User.fName;
                userComboBox.DataSource = core.GetUsers();
                userComboBox.SelectedValue = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (userComboBox.SelectedValue == null) {
                MessageBox.Show("Du har inte valt någon användare.", "Loginfel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
    }
}
