namespace CashBox
{
    partial class DepositWithdrawalForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DepositWithdrawalForm));
            this.noteTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.amountTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tagComboBox = new System.Windows.Forms.ComboBox();
            this.tagLabel = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.verificationComboBox = new System.Windows.Forms.ComboBox();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.accountingDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.suggestionsGroupBox = new System.Windows.Forms.GroupBox();
            this.suggestion3CheckBox = new System.Windows.Forms.CheckBox();
            this.suggestion2CheckBox = new System.Windows.Forms.CheckBox();
            this.suggestion1CheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.suggestionsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // noteTextBox
            // 
            this.noteTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.errorProvider.SetIconPadding(this.noteTextBox, 3);
            this.noteTextBox.Location = new System.Drawing.Point(23, 100);
            this.noteTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.noteTextBox.Name = "noteTextBox";
            this.noteTextBox.Size = new System.Drawing.Size(401, 22);
            this.noteTextBox.TabIndex = 3;
            this.noteTextBox.TextChanged += new System.EventHandler(this.noteTextBox_TextChanged);
            this.noteTextBox.Leave += new System.EventHandler(this.noteTextBox_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 30);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Belopp";
            // 
            // amountTextBox
            // 
            this.errorProvider.SetIconPadding(this.amountTextBox, 3);
            this.amountTextBox.Location = new System.Drawing.Point(23, 50);
            this.amountTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.amountTextBox.Name = "amountTextBox";
            this.amountTextBox.Size = new System.Drawing.Size(163, 22);
            this.amountTextBox.TabIndex = 1;
            this.amountTextBox.TextChanged += new System.EventHandler(this.amountTextBox_TextChanged);
            this.amountTextBox.Leave += new System.EventHandler(this.amountTextBox_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 80);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Notering (inköpsställe)";
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(384, 503);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 28);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "Avbryt";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(276, 503);
            this.okButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(100, 28);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tagComboBox);
            this.groupBox1.Controls.Add(this.tagLabel);
            this.groupBox1.Controls.Add(this.noteTextBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.amountTextBox);
            this.groupBox1.Location = new System.Drawing.Point(16, 166);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(468, 197);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Insättning/Uttag";
            // 
            // tagComboBox
            // 
            this.tagComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tagComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tagComboBox.FormattingEnabled = true;
            this.errorProvider.SetIconPadding(this.tagComboBox, 3);
            this.tagComboBox.Location = new System.Drawing.Point(23, 150);
            this.tagComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tagComboBox.Name = "tagComboBox";
            this.tagComboBox.Size = new System.Drawing.Size(401, 24);
            this.tagComboBox.TabIndex = 5;
            // 
            // tagLabel
            // 
            this.tagLabel.AutoSize = true;
            this.tagLabel.Location = new System.Drawing.Point(19, 130);
            this.tagLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.tagLabel.Name = "tagLabel";
            this.tagLabel.Size = new System.Drawing.Size(66, 17);
            this.tagLabel.TabIndex = 4;
            this.tagLabel.Text = "Märkning";
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // verificationComboBox
            // 
            this.verificationComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.verificationComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.verificationComboBox.FormattingEnabled = true;
            this.errorProvider.SetIconPadding(this.verificationComboBox, 3);
            this.verificationComboBox.Location = new System.Drawing.Point(23, 50);
            this.verificationComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.verificationComboBox.Name = "verificationComboBox";
            this.verificationComboBox.Size = new System.Drawing.Size(164, 24);
            this.verificationComboBox.TabIndex = 1;
            this.verificationComboBox.SelectedIndexChanged += new System.EventHandler(this.verificationComboBox_SelectedIndexChanged);
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.errorProvider.SetIconPadding(this.dateTimePicker, 3);
            this.dateTimePicker.Location = new System.Drawing.Point(23, 101);
            this.dateTimePicker.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(164, 22);
            this.dateTimePicker.TabIndex = 3;
            this.dateTimePicker.ValueChanged += new System.EventHandler(this.dateTimePicker_ValueChanged);
            // 
            // accountingDateTimePicker
            // 
            this.accountingDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.errorProvider.SetIconPadding(this.accountingDateTimePicker, 3);
            this.accountingDateTimePicker.Location = new System.Drawing.Point(232, 101);
            this.accountingDateTimePicker.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.accountingDateTimePicker.Name = "accountingDateTimePicker";
            this.accountingDateTimePicker.Size = new System.Drawing.Size(164, 22);
            this.accountingDateTimePicker.TabIndex = 5;
            this.accountingDateTimePicker.ValueChanged += new System.EventHandler(this.accountingDateTimePicker_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.accountingDateTimePicker);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.verificationComboBox);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.dateTimePicker);
            this.groupBox2.Location = new System.Drawing.Point(15, 14);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(471, 146);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Affärshändelse";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(229, 80);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "Bokföringsdatum";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 30);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Nummer";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 80);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Kassadatum";
            // 
            // suggestionsGroupBox
            // 
            this.suggestionsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.suggestionsGroupBox.Controls.Add(this.suggestion3CheckBox);
            this.suggestionsGroupBox.Controls.Add(this.suggestion2CheckBox);
            this.suggestionsGroupBox.Controls.Add(this.suggestion1CheckBox);
            this.suggestionsGroupBox.Location = new System.Drawing.Point(16, 370);
            this.suggestionsGroupBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.suggestionsGroupBox.Name = "suggestionsGroupBox";
            this.suggestionsGroupBox.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.suggestionsGroupBox.Size = new System.Drawing.Size(468, 126);
            this.suggestionsGroupBox.TabIndex = 5;
            this.suggestionsGroupBox.TabStop = false;
            this.suggestionsGroupBox.Text = "Förslag";
            // 
            // suggestion3CheckBox
            // 
            this.suggestion3CheckBox.AutoSize = true;
            this.suggestion3CheckBox.Location = new System.Drawing.Point(21, 91);
            this.suggestion3CheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.suggestion3CheckBox.Name = "suggestion3CheckBox";
            this.suggestion3CheckBox.Size = new System.Drawing.Size(98, 21);
            this.suggestion3CheckBox.TabIndex = 11;
            this.suggestion3CheckBox.Text = "checkBox3";
            this.suggestion3CheckBox.UseVisualStyleBackColor = true;
            this.suggestion3CheckBox.CheckedChanged += new System.EventHandler(this.suggestion3CheckBox_CheckedChanged);
            // 
            // suggestion2CheckBox
            // 
            this.suggestion2CheckBox.AutoSize = true;
            this.suggestion2CheckBox.Location = new System.Drawing.Point(21, 59);
            this.suggestion2CheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.suggestion2CheckBox.Name = "suggestion2CheckBox";
            this.suggestion2CheckBox.Size = new System.Drawing.Size(98, 21);
            this.suggestion2CheckBox.TabIndex = 10;
            this.suggestion2CheckBox.Text = "checkBox2";
            this.suggestion2CheckBox.UseVisualStyleBackColor = true;
            this.suggestion2CheckBox.CheckedChanged += new System.EventHandler(this.suggestion2CheckBox_CheckedChanged);
            // 
            // suggestion1CheckBox
            // 
            this.suggestion1CheckBox.AutoSize = true;
            this.suggestion1CheckBox.Location = new System.Drawing.Point(21, 28);
            this.suggestion1CheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.suggestion1CheckBox.Name = "suggestion1CheckBox";
            this.suggestion1CheckBox.Size = new System.Drawing.Size(98, 21);
            this.suggestion1CheckBox.TabIndex = 9;
            this.suggestion1CheckBox.Text = "checkBox1";
            this.suggestion1CheckBox.UseVisualStyleBackColor = true;
            this.suggestion1CheckBox.CheckedChanged += new System.EventHandler(this.suggestion1CheckBox_CheckedChanged);
            // 
            // DepositWithdrawalForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(500, 545);
            this.Controls.Add(this.suggestionsGroupBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DepositWithdrawalForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Insättning/Uttag";
            this.Load += new System.EventHandler(this.AccountTransactionForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.suggestionsGroupBox.ResumeLayout(false);
            this.suggestionsGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox noteTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox amountTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox verificationComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.ComboBox tagComboBox;
        private System.Windows.Forms.Label tagLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker accountingDateTimePicker;
        private System.Windows.Forms.GroupBox suggestionsGroupBox;
        private System.Windows.Forms.CheckBox suggestion3CheckBox;
        private System.Windows.Forms.CheckBox suggestion2CheckBox;
        private System.Windows.Forms.CheckBox suggestion1CheckBox;
    }
}