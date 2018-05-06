namespace CashBox
{
    partial class ManageCategoriesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageCategoriesForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.deleteCategoryButton = new System.Windows.Forms.Button();
            this.addCategoryButton = new System.Windows.Forms.Button();
            this.categoryComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.settingsGroupBox = new System.Windows.Forms.GroupBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.isArchivedCheckBox = new System.Windows.Forms.CheckBox();
            this.typeExpenseRadioButton = new System.Windows.Forms.RadioButton();
            this.typeIncomeRadioButton = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.totalValueLabel = new System.Windows.Forms.Label();
            this.noOfPostsLabel = new System.Windows.Forms.Label();
            this.moveTransactionsButton = new System.Windows.Forms.Button();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.parentComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
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
            this.groupBox1.Controls.Add(this.deleteCategoryButton);
            this.groupBox1.Controls.Add(this.addCategoryButton);
            this.groupBox1.Controls.Add(this.categoryComboBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(16, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(659, 86);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Kategori";
            // 
            // deleteCategoryButton
            // 
            this.deleteCategoryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteCategoryButton.Location = new System.Drawing.Point(559, 28);
            this.deleteCategoryButton.Margin = new System.Windows.Forms.Padding(4);
            this.deleteCategoryButton.Name = "deleteCategoryButton";
            this.deleteCategoryButton.Size = new System.Drawing.Size(48, 28);
            this.deleteCategoryButton.TabIndex = 3;
            this.deleteCategoryButton.Text = "-";
            this.deleteCategoryButton.UseVisualStyleBackColor = true;
            this.deleteCategoryButton.Click += new System.EventHandler(this.deleteCategoryButton_Click);
            // 
            // addCategoryButton
            // 
            this.addCategoryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addCategoryButton.Location = new System.Drawing.Point(503, 28);
            this.addCategoryButton.Margin = new System.Windows.Forms.Padding(4);
            this.addCategoryButton.Name = "addCategoryButton";
            this.addCategoryButton.Size = new System.Drawing.Size(48, 28);
            this.addCategoryButton.TabIndex = 2;
            this.addCategoryButton.Text = "+";
            this.addCategoryButton.UseVisualStyleBackColor = true;
            this.addCategoryButton.Click += new System.EventHandler(this.addCategoryButton_Click);
            // 
            // categoryComboBox
            // 
            this.categoryComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.categoryComboBox.FormattingEnabled = true;
            this.categoryComboBox.Location = new System.Drawing.Point(125, 31);
            this.categoryComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.categoryComboBox.Name = "categoryComboBox";
            this.categoryComboBox.Size = new System.Drawing.Size(368, 24);
            this.categoryComboBox.TabIndex = 1;
            this.categoryComboBox.SelectedIndexChanged += new System.EventHandler(this.categoryComboBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 34);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Kategori";
            // 
            // settingsGroupBox
            // 
            this.settingsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.settingsGroupBox.Controls.Add(this.showInDiagramCheckBox);
            this.settingsGroupBox.Controls.Add(this.saveButton);
            this.settingsGroupBox.Controls.Add(this.isArchivedCheckBox);
            this.settingsGroupBox.Controls.Add(this.typeExpenseRadioButton);
            this.settingsGroupBox.Controls.Add(this.typeIncomeRadioButton);
            this.settingsGroupBox.Controls.Add(this.label6);
            this.settingsGroupBox.Controls.Add(this.moveTransactionsButton);
            this.settingsGroupBox.Controls.Add(this.nameTextBox);
            this.settingsGroupBox.Controls.Add(this.label2);
            this.settingsGroupBox.Controls.Add(this.parentComboBox);
            this.settingsGroupBox.Controls.Add(this.label1);
            this.settingsGroupBox.Location = new System.Drawing.Point(16, 108);
            this.settingsGroupBox.Margin = new System.Windows.Forms.Padding(4);
            this.settingsGroupBox.Name = "settingsGroupBox";
            this.settingsGroupBox.Padding = new System.Windows.Forms.Padding(4);
            this.settingsGroupBox.Size = new System.Drawing.Size(659, 218);
            this.settingsGroupBox.TabIndex = 1;
            this.settingsGroupBox.TabStop = false;
            this.settingsGroupBox.Text = "Inställningar";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(453, 165);
            this.saveButton.Margin = new System.Windows.Forms.Padding(4);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(165, 28);
            this.saveButton.TabIndex = 11;
            this.saveButton.Text = "Spara kategori";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // isArchivedCheckBox
            // 
            this.isArchivedCheckBox.AutoSize = true;
            this.isArchivedCheckBox.Location = new System.Drawing.Point(27, 170);
            this.isArchivedCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.isArchivedCheckBox.Name = "isArchivedCheckBox";
            this.isArchivedCheckBox.Size = new System.Drawing.Size(90, 21);
            this.isArchivedCheckBox.TabIndex = 9;
            this.isArchivedCheckBox.Text = "Arkiverad";
            this.isArchivedCheckBox.UseVisualStyleBackColor = true;
            this.isArchivedCheckBox.CheckedChanged += new System.EventHandler(this.isArchivedCheckBox_CheckedChanged);
            // 
            // typeExpenseRadioButton
            // 
            this.typeExpenseRadioButton.AutoSize = true;
            this.errorProvider.SetIconPadding(this.typeExpenseRadioButton, 3);
            this.typeExpenseRadioButton.Location = new System.Drawing.Point(289, 95);
            this.typeExpenseRadioButton.Margin = new System.Windows.Forms.Padding(4);
            this.typeExpenseRadioButton.Name = "typeExpenseRadioButton";
            this.typeExpenseRadioButton.Size = new System.Drawing.Size(120, 21);
            this.typeExpenseRadioButton.TabIndex = 6;
            this.typeExpenseRadioButton.TabStop = true;
            this.typeExpenseRadioButton.Text = "Utgiftskategori";
            this.typeExpenseRadioButton.UseVisualStyleBackColor = true;
            this.typeExpenseRadioButton.CheckedChanged += new System.EventHandler(this.typeExpenseRadioButton_CheckedChanged);
            // 
            // typeIncomeRadioButton
            // 
            this.typeIncomeRadioButton.AutoSize = true;
            this.typeIncomeRadioButton.Location = new System.Drawing.Point(125, 95);
            this.typeIncomeRadioButton.Margin = new System.Windows.Forms.Padding(4);
            this.typeIncomeRadioButton.Name = "typeIncomeRadioButton";
            this.typeIncomeRadioButton.Size = new System.Drawing.Size(128, 21);
            this.typeIncomeRadioButton.TabIndex = 5;
            this.typeIncomeRadioButton.TabStop = true;
            this.typeIncomeRadioButton.Text = "Inkomstkategori";
            this.typeIncomeRadioButton.UseVisualStyleBackColor = true;
            this.typeIncomeRadioButton.CheckedChanged += new System.EventHandler(this.typeIncomeRadioButton_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 97);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 17);
            this.label6.TabIndex = 4;
            this.label6.Text = "Kategorityp";
            // 
            // totalValueLabel
            // 
            this.totalValueLabel.AutoSize = true;
            this.totalValueLabel.Location = new System.Drawing.Point(32, 345);
            this.totalValueLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.totalValueLabel.Name = "totalValueLabel";
            this.totalValueLabel.Size = new System.Drawing.Size(101, 17);
            this.totalValueLabel.TabIndex = 8;
            this.totalValueLabel.Text = "Totalt värde: X";
            this.totalValueLabel.Visible = false;
            // 
            // noOfPostsLabel
            // 
            this.noOfPostsLabel.AutoSize = true;
            this.noOfPostsLabel.Location = new System.Drawing.Point(192, 345);
            this.noOfPostsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.noOfPostsLabel.Name = "noOfPostsLabel";
            this.noOfPostsLabel.Size = new System.Drawing.Size(101, 17);
            this.noOfPostsLabel.TabIndex = 7;
            this.noOfPostsLabel.Text = "Antal poster: X";
            this.noOfPostsLabel.Visible = false;
            // 
            // moveTransactionsButton
            // 
            this.moveTransactionsButton.Enabled = false;
            this.moveTransactionsButton.Location = new System.Drawing.Point(453, 122);
            this.moveTransactionsButton.Margin = new System.Windows.Forms.Padding(4);
            this.moveTransactionsButton.Name = "moveTransactionsButton";
            this.moveTransactionsButton.Size = new System.Drawing.Size(165, 28);
            this.moveTransactionsButton.TabIndex = 10;
            this.moveTransactionsButton.Text = "Flytta kassahändelser";
            this.moveTransactionsButton.UseVisualStyleBackColor = true;
            this.moveTransactionsButton.Visible = false;
            // 
            // nameTextBox
            // 
            this.errorProvider.SetIconPadding(this.nameTextBox, 3);
            this.nameTextBox.Location = new System.Drawing.Point(125, 63);
            this.nameTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(480, 22);
            this.nameTextBox.TabIndex = 3;
            this.nameTextBox.TextChanged += new System.EventHandler(this.nameTextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 66);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Namn";
            // 
            // parentComboBox
            // 
            this.parentComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.parentComboBox.FormattingEnabled = true;
            this.errorProvider.SetIconPadding(this.parentComboBox, 3);
            this.parentComboBox.Location = new System.Drawing.Point(125, 30);
            this.parentComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.parentComboBox.Name = "parentComboBox";
            this.parentComboBox.Size = new System.Drawing.Size(480, 24);
            this.parentComboBox.TabIndex = 1;
            this.parentComboBox.SelectedIndexChanged += new System.EventHandler(this.parentComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 33);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Förälder";
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.Location = new System.Drawing.Point(575, 345);
            this.closeButton.Margin = new System.Windows.Forms.Padding(4);
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
            this.showInDiagramCheckBox.Location = new System.Drawing.Point(27, 133);
            this.showInDiagramCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.showInDiagramCheckBox.Name = "showInDiagramCheckBox";
            this.showInDiagramCheckBox.Size = new System.Drawing.Size(119, 21);
            this.showInDiagramCheckBox.TabIndex = 12;
            this.showInDiagramCheckBox.Text = "Visa i diagram";
            this.showInDiagramCheckBox.UseVisualStyleBackColor = true;
            this.showInDiagramCheckBox.CheckedChanged += new System.EventHandler(this.showInDiagramCheckBox_CheckedChanged);
            // 
            // ManageCategoriesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 388);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.settingsGroupBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.totalValueLabel);
            this.Controls.Add(this.noOfPostsLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ManageCategoriesForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hantera kategorier";
            this.Load += new System.EventHandler(this.ManageCategoriesForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.settingsGroupBox.ResumeLayout(false);
            this.settingsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox settingsGroupBox;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.ComboBox parentComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox categoryComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button addCategoryButton;
        private System.Windows.Forms.Button deleteCategoryButton;
        private System.Windows.Forms.Button moveTransactionsButton;
        private System.Windows.Forms.Label totalValueLabel;
        private System.Windows.Forms.Label noOfPostsLabel;
        private System.Windows.Forms.CheckBox isArchivedCheckBox;
        private System.Windows.Forms.RadioButton typeExpenseRadioButton;
        private System.Windows.Forms.RadioButton typeIncomeRadioButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.CheckBox showInDiagramCheckBox;
    }
}