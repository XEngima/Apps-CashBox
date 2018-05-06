namespace CashBox
{
    partial class ManageAccountsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageAccountsForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.deleteAccountButton = new System.Windows.Forms.Button();
            this.addAccountButton = new System.Windows.Forms.Button();
            this.accountComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.settingsGroupBox = new System.Windows.Forms.GroupBox();
            this.typeDebtRadioButton = new System.Windows.Forms.RadioButton();
            this.typeAssetRadioButton = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.balanceBroughtForwardAmountLabel = new System.Windows.Forms.Label();
            this.balanceBroughtForwardAmountTextBox = new System.Windows.Forms.TextBox();
            this.userComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.isArchivedCheckBox = new System.Windows.Forms.CheckBox();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.showInDiagramCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.settingsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.deleteAccountButton);
            this.groupBox1.Controls.Add(this.addAccountButton);
            this.groupBox1.Controls.Add(this.accountComboBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(16, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(659, 86);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Konto";
            // 
            // deleteAccountButton
            // 
            this.deleteAccountButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteAccountButton.Location = new System.Drawing.Point(559, 31);
            this.deleteAccountButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.deleteAccountButton.Name = "deleteAccountButton";
            this.deleteAccountButton.Size = new System.Drawing.Size(48, 28);
            this.deleteAccountButton.TabIndex = 3;
            this.deleteAccountButton.Text = "-";
            this.deleteAccountButton.UseVisualStyleBackColor = true;
            this.deleteAccountButton.Click += new System.EventHandler(this.deleteAccountButton_Click);
            // 
            // addAccountButton
            // 
            this.addAccountButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addAccountButton.Location = new System.Drawing.Point(503, 31);
            this.addAccountButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.addAccountButton.Name = "addAccountButton";
            this.addAccountButton.Size = new System.Drawing.Size(48, 28);
            this.addAccountButton.TabIndex = 2;
            this.addAccountButton.Text = "+";
            this.addAccountButton.UseVisualStyleBackColor = true;
            this.addAccountButton.Click += new System.EventHandler(this.addAccountButton_Click);
            // 
            // accountComboBox
            // 
            this.accountComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.accountComboBox.FormattingEnabled = true;
            this.accountComboBox.Location = new System.Drawing.Point(141, 31);
            this.accountComboBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.accountComboBox.Name = "accountComboBox";
            this.accountComboBox.Size = new System.Drawing.Size(352, 24);
            this.accountComboBox.TabIndex = 1;
            this.accountComboBox.SelectedIndexChanged += new System.EventHandler(this.accountComboBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 34);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Konto";
            // 
            // settingsGroupBox
            // 
            this.settingsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.settingsGroupBox.Controls.Add(this.showInDiagramCheckBox);
            this.settingsGroupBox.Controls.Add(this.typeDebtRadioButton);
            this.settingsGroupBox.Controls.Add(this.typeAssetRadioButton);
            this.settingsGroupBox.Controls.Add(this.label6);
            this.settingsGroupBox.Controls.Add(this.balanceBroughtForwardAmountLabel);
            this.settingsGroupBox.Controls.Add(this.balanceBroughtForwardAmountTextBox);
            this.settingsGroupBox.Controls.Add(this.userComboBox);
            this.settingsGroupBox.Controls.Add(this.label5);
            this.settingsGroupBox.Controls.Add(this.saveButton);
            this.settingsGroupBox.Controls.Add(this.isArchivedCheckBox);
            this.settingsGroupBox.Controls.Add(this.nameTextBox);
            this.settingsGroupBox.Controls.Add(this.label2);
            this.settingsGroupBox.Location = new System.Drawing.Point(16, 108);
            this.settingsGroupBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.settingsGroupBox.Name = "settingsGroupBox";
            this.settingsGroupBox.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.settingsGroupBox.Size = new System.Drawing.Size(659, 242);
            this.settingsGroupBox.TabIndex = 1;
            this.settingsGroupBox.TabStop = false;
            this.settingsGroupBox.Text = "Inställningar";
            // 
            // typeDebtRadioButton
            // 
            this.typeDebtRadioButton.AutoSize = true;
            this.errorProvider.SetIconPadding(this.typeDebtRadioButton, 3);
            this.typeDebtRadioButton.Location = new System.Drawing.Point(289, 127);
            this.typeDebtRadioButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.typeDebtRadioButton.Name = "typeDebtRadioButton";
            this.typeDebtRadioButton.Size = new System.Drawing.Size(99, 21);
            this.typeDebtRadioButton.TabIndex = 9;
            this.typeDebtRadioButton.TabStop = true;
            this.typeDebtRadioButton.Text = "Skuldkonto";
            this.typeDebtRadioButton.UseVisualStyleBackColor = true;
            this.typeDebtRadioButton.CheckedChanged += new System.EventHandler(this.typeDebtRadioButton_CheckedChanged);
            // 
            // typeAssetRadioButton
            // 
            this.typeAssetRadioButton.AutoSize = true;
            this.typeAssetRadioButton.Location = new System.Drawing.Point(141, 127);
            this.typeAssetRadioButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.typeAssetRadioButton.Name = "typeAssetRadioButton";
            this.typeAssetRadioButton.Size = new System.Drawing.Size(121, 21);
            this.typeAssetRadioButton.TabIndex = 8;
            this.typeAssetRadioButton.TabStop = true;
            this.typeAssetRadioButton.Text = "Tillgångskonto";
            this.typeAssetRadioButton.UseVisualStyleBackColor = true;
            this.typeAssetRadioButton.CheckedChanged += new System.EventHandler(this.typeAssetRadioButton_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 129);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 17);
            this.label6.TabIndex = 7;
            this.label6.Text = "Kontotyp";
            // 
            // balanceBroughtForwardAmountLabel
            // 
            this.balanceBroughtForwardAmountLabel.AutoSize = true;
            this.balanceBroughtForwardAmountLabel.Location = new System.Drawing.Point(24, 94);
            this.balanceBroughtForwardAmountLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.balanceBroughtForwardAmountLabel.Name = "balanceBroughtForwardAmountLabel";
            this.balanceBroughtForwardAmountLabel.Size = new System.Drawing.Size(105, 17);
            this.balanceBroughtForwardAmountLabel.TabIndex = 3;
            this.balanceBroughtForwardAmountLabel.Text = "Ingående saldo";
            // 
            // balanceBroughtForwardAmountTextBox
            // 
            this.errorProvider.SetIconPadding(this.balanceBroughtForwardAmountTextBox, 3);
            this.balanceBroughtForwardAmountTextBox.Location = new System.Drawing.Point(141, 91);
            this.balanceBroughtForwardAmountTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.balanceBroughtForwardAmountTextBox.Name = "balanceBroughtForwardAmountTextBox";
            this.balanceBroughtForwardAmountTextBox.Size = new System.Drawing.Size(137, 22);
            this.balanceBroughtForwardAmountTextBox.TabIndex = 4;
            this.balanceBroughtForwardAmountTextBox.TextChanged += new System.EventHandler(this.initialBalanceAmountTextBox_TextChanged);
            // 
            // userComboBox
            // 
            this.userComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.userComboBox.FormattingEnabled = true;
            this.errorProvider.SetIconPadding(this.userComboBox, 3);
            this.userComboBox.Location = new System.Drawing.Point(141, 59);
            this.userComboBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.userComboBox.Name = "userComboBox";
            this.userComboBox.Size = new System.Drawing.Size(464, 24);
            this.userComboBox.TabIndex = 3;
            this.userComboBox.SelectedIndexChanged += new System.EventHandler(this.userComboBox_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 63);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "Ägare";
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Location = new System.Drawing.Point(441, 198);
            this.saveButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(165, 28);
            this.saveButton.TabIndex = 6;
            this.saveButton.Text = "Spara konto";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // isArchivedCheckBox
            // 
            this.isArchivedCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.isArchivedCheckBox.AutoSize = true;
            this.errorProvider.SetIconPadding(this.isArchivedCheckBox, 3);
            this.isArchivedCheckBox.Location = new System.Drawing.Point(28, 203);
            this.isArchivedCheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.isArchivedCheckBox.Name = "isArchivedCheckBox";
            this.isArchivedCheckBox.Size = new System.Drawing.Size(86, 21);
            this.isArchivedCheckBox.TabIndex = 5;
            this.isArchivedCheckBox.Text = "Arkiverat";
            this.isArchivedCheckBox.UseVisualStyleBackColor = true;
            this.isArchivedCheckBox.CheckedChanged += new System.EventHandler(this.isArchivedCheckBox_CheckedChanged);
            // 
            // nameTextBox
            // 
            this.errorProvider.SetIconPadding(this.nameTextBox, 3);
            this.nameTextBox.Location = new System.Drawing.Point(141, 27);
            this.nameTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(464, 22);
            this.nameTextBox.TabIndex = 1;
            this.nameTextBox.TextChanged += new System.EventHandler(this.nameTextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 31);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Namn";
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.Location = new System.Drawing.Point(575, 364);
            this.closeButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(100, 28);
            this.closeButton.TabIndex = 2;
            this.closeButton.Text = "St&äng";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // showInDiagramCheckBox
            // 
            this.showInDiagramCheckBox.AutoSize = true;
            this.errorProvider.SetIconPadding(this.showInDiagramCheckBox, 3);
            this.showInDiagramCheckBox.Location = new System.Drawing.Point(28, 166);
            this.showInDiagramCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.showInDiagramCheckBox.Name = "showInDiagramCheckBox";
            this.showInDiagramCheckBox.Size = new System.Drawing.Size(119, 21);
            this.showInDiagramCheckBox.TabIndex = 10;
            this.showInDiagramCheckBox.Text = "Visa i diagram";
            this.showInDiagramCheckBox.UseVisualStyleBackColor = true;
            this.showInDiagramCheckBox.CheckedChanged += new System.EventHandler(this.showInDiagramCheckBox_CheckedChanged);
            // 
            // ManageAccountsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 407);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.settingsGroupBox);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ManageAccountsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hantera konton";
            this.Load += new System.EventHandler(this.ManageAccountsForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.settingsGroupBox.ResumeLayout(false);
            this.settingsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox settingsGroupBox;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox accountComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button addAccountButton;
        private System.Windows.Forms.Button deleteAccountButton;
        private System.Windows.Forms.CheckBox isArchivedCheckBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Label balanceBroughtForwardAmountLabel;
        private System.Windows.Forms.TextBox balanceBroughtForwardAmountTextBox;
        private System.Windows.Forms.ComboBox userComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton typeDebtRadioButton;
        private System.Windows.Forms.RadioButton typeAssetRadioButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox showInDiagramCheckBox;
    }
}