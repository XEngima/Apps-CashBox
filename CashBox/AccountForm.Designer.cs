namespace CashBox
{
    partial class AccountForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.accountComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.depositWithdrawalButton = new System.Windows.Forms.ToolStripButton();
            this.transactionToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.deleteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.transactionsDataGridView = new System.Windows.Forms.DataGridView();
            this.transactionContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createCashBookItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createAccountTransactionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.balanceToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.transactionsDataGridView)).BeginInit();
            this.transactionContextMenuStrip.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.accountComboBox,
            this.depositWithdrawalButton,
            this.transactionToolStripButton,
            this.deleteToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(721, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(39, 22);
            this.toolStripLabel1.Text = "Konto";
            // 
            // accountComboBox
            // 
            this.accountComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.accountComboBox.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.accountComboBox.Name = "accountComboBox";
            this.accountComboBox.Size = new System.Drawing.Size(250, 25);
            this.accountComboBox.SelectedIndexChanged += new System.EventHandler(this.accountComboBox_SelectedIndexChanged);
            this.accountComboBox.Click += new System.EventHandler(this.accountComboBox_Click);
            // 
            // depositWithdrawalButton
            // 
            this.depositWithdrawalButton.Image = ((System.Drawing.Image)(resources.GetObject("depositWithdrawalButton.Image")));
            this.depositWithdrawalButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.depositWithdrawalButton.Name = "depositWithdrawalButton";
            this.depositWithdrawalButton.Size = new System.Drawing.Size(114, 22);
            this.depositWithdrawalButton.Text = "Insättning/Uttag";
            this.depositWithdrawalButton.Click += new System.EventHandler(this.depositWithdrawalButton_Click);
            // 
            // transactionToolStripButton
            // 
            this.transactionToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("transactionToolStripButton.Image")));
            this.transactionToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.transactionToolStripButton.Name = "transactionToolStripButton";
            this.transactionToolStripButton.Size = new System.Drawing.Size(114, 22);
            this.transactionToolStripButton.Text = "Kontoöverföring";
            this.transactionToolStripButton.Visible = false;
            this.transactionToolStripButton.Click += new System.EventHandler(this.transactionToolStripButton_Click);
            // 
            // deleteToolStripButton
            // 
            this.deleteToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteToolStripButton.Image")));
            this.deleteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteToolStripButton.Name = "deleteToolStripButton";
            this.deleteToolStripButton.Size = new System.Drawing.Size(65, 22);
            this.deleteToolStripButton.Text = "Ta bort";
            this.deleteToolStripButton.Click += new System.EventHandler(this.deleteToolStripButton_Click);
            // 
            // transactionsDataGridView
            // 
            this.transactionsDataGridView.AllowUserToAddRows = false;
            this.transactionsDataGridView.AllowUserToDeleteRows = false;
            this.transactionsDataGridView.AllowUserToResizeRows = false;
            this.transactionsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.transactionsDataGridView.ContextMenuStrip = this.transactionContextMenuStrip;
            this.transactionsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.transactionsDataGridView.Location = new System.Drawing.Point(0, 25);
            this.transactionsDataGridView.MultiSelect = false;
            this.transactionsDataGridView.Name = "transactionsDataGridView";
            this.transactionsDataGridView.ReadOnly = true;
            this.transactionsDataGridView.RowHeadersVisible = false;
            this.transactionsDataGridView.RowTemplate.Height = 24;
            this.transactionsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.transactionsDataGridView.Size = new System.Drawing.Size(721, 603);
            this.transactionsDataGridView.TabIndex = 1;
            this.transactionsDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.transactionsDataGridView_CellClick);
            this.transactionsDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.transactionsDataGridView_CellDoubleClick);
            this.transactionsDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.transactionsDataGridView_CellFormatting);
            this.transactionsDataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.transactionsDataGridView_CellMouseDown);
            this.transactionsDataGridView.SelectionChanged += new System.EventHandler(this.transactionsDataGridView_SelectionChanged);
            this.transactionsDataGridView.Sorted += new System.EventHandler(this.transactionsDataGridView_Sorted);
            // 
            // transactionContextMenuStrip
            // 
            this.transactionContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createCashBookItemToolStripMenuItem,
            this.createAccountTransactionToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.transactionContextMenuStrip.Name = "transactionContextMenuStrip";
            this.transactionContextMenuStrip.Size = new System.Drawing.Size(195, 70);
            // 
            // createCashBookItemToolStripMenuItem
            // 
            this.createCashBookItemToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("createCashBookItemToolStripMenuItem.Image")));
            this.createCashBookItemToolStripMenuItem.Name = "createCashBookItemToolStripMenuItem";
            this.createCashBookItemToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.createCashBookItemToolStripMenuItem.Text = "Skapa &kassahändelse";
            this.createCashBookItemToolStripMenuItem.Click += new System.EventHandler(this.createCashBookItemToolStripMenuItem_Click);
            // 
            // createAccountTransactionToolStripMenuItem
            // 
            this.createAccountTransactionToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("createAccountTransactionToolStripMenuItem.Image")));
            this.createAccountTransactionToolStripMenuItem.Name = "createAccountTransactionToolStripMenuItem";
            this.createAccountTransactionToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.createAccountTransactionToolStripMenuItem.Text = "Skapa konto&överföring";
            this.createAccountTransactionToolStripMenuItem.Visible = false;
            this.createAccountTransactionToolStripMenuItem.Click += new System.EventHandler(this.createAccountTransactionToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteToolStripMenuItem.Image")));
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.deleteToolStripMenuItem.Text = "&Ta bort";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.balanceToolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 628);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(721, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // balanceToolStripStatusLabel
            // 
            this.balanceToolStripStatusLabel.Name = "balanceToolStripStatusLabel";
            this.balanceToolStripStatusLabel.Size = new System.Drawing.Size(49, 17);
            this.balanceToolStripStatusLabel.Text = "Saldo: X";
            // 
            // AccountForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 650);
            this.Controls.Add(this.transactionsDataGridView);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AccountForm";
            this.Text = "Konto";
            this.Load += new System.EventHandler(this.AccountForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.transactionsDataGridView)).EndInit();
            this.transactionContextMenuStrip.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox accountComboBox;
        private System.Windows.Forms.DataGridView transactionsDataGridView;
        private System.Windows.Forms.ToolStripButton depositWithdrawalButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel balanceToolStripStatusLabel;
        private System.Windows.Forms.ToolStripButton transactionToolStripButton;
        private System.Windows.Forms.ToolStripButton deleteToolStripButton;
        private System.Windows.Forms.ContextMenuStrip transactionContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem createCashBookItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createAccountTransactionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
    }
}