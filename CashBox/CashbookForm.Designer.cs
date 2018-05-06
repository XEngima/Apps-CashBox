namespace CashBox
{
    partial class CashBookForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CashBookForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.newCashBookItemButton = new System.Windows.Forms.ToolStripButton();
            this.deleteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.monthToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.dateTypeComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.userToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.categoryToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.transactionsDataGridView = new System.Windows.Forms.DataGridView();
            this.transactionContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.totalToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
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
            this.newCashBookItemButton,
            this.deleteToolStripButton,
            this.monthToolStripComboBox,
            this.dateTypeComboBox,
            this.userToolStripComboBox,
            this.categoryToolStripComboBox});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(721, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // newCashBookItemButton
            // 
            this.newCashBookItemButton.Image = ((System.Drawing.Image)(resources.GetObject("newCashBookItemButton.Image")));
            this.newCashBookItemButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newCashBookItemButton.Name = "newCashBookItemButton";
            this.newCashBookItemButton.Size = new System.Drawing.Size(42, 22);
            this.newCashBookItemButton.Text = "Ny";
            this.newCashBookItemButton.Click += new System.EventHandler(this.newCashBookItemButton_Click);
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
            // monthToolStripComboBox
            // 
            this.monthToolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.monthToolStripComboBox.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.monthToolStripComboBox.Name = "monthToolStripComboBox";
            this.monthToolStripComboBox.Size = new System.Drawing.Size(75, 25);
            this.monthToolStripComboBox.SelectedIndexChanged += new System.EventHandler(this.monthToolStripComboBox_SelectedIndexChanged);
            // 
            // dateTypeComboBox
            // 
            this.dateTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dateTypeComboBox.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.dateTypeComboBox.Name = "dateTypeComboBox";
            this.dateTypeComboBox.Size = new System.Drawing.Size(85, 25);
            this.dateTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.dateTypeComboBox_SelectedIndexChanged);
            // 
            // userToolStripComboBox
            // 
            this.userToolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.userToolStripComboBox.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.userToolStripComboBox.Name = "userToolStripComboBox";
            this.userToolStripComboBox.Size = new System.Drawing.Size(125, 25);
            this.userToolStripComboBox.SelectedIndexChanged += new System.EventHandler(this.userToolStripComboBox_SelectedIndexChanged);
            // 
            // categoryToolStripComboBox
            // 
            this.categoryToolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.categoryToolStripComboBox.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.categoryToolStripComboBox.Name = "categoryToolStripComboBox";
            this.categoryToolStripComboBox.Size = new System.Drawing.Size(200, 25);
            this.categoryToolStripComboBox.SelectedIndexChanged += new System.EventHandler(this.categoryToolStripComboBox_SelectedIndexChanged);
            // 
            // transactionsDataGridView
            // 
            this.transactionsDataGridView.AllowUserToAddRows = false;
            this.transactionsDataGridView.AllowUserToDeleteRows = false;
            this.transactionsDataGridView.AllowUserToResizeRows = false;
            this.transactionsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.transactionsDataGridView.ContextMenuStrip = this.transactionContextMenuStrip;
            this.transactionsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.transactionsDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
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
            // 
            // transactionContextMenuStrip
            // 
            this.transactionContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
            this.transactionContextMenuStrip.Name = "transactionContextMenuStrip";
            this.transactionContextMenuStrip.Size = new System.Drawing.Size(113, 26);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteToolStripMenuItem.Image")));
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.deleteToolStripMenuItem.Text = "&Ta bort";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.totalToolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 628);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(721, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // totalToolStripStatusLabel
            // 
            this.totalToolStripStatusLabel.Name = "totalToolStripStatusLabel";
            this.totalToolStripStatusLabel.Size = new System.Drawing.Size(61, 17);
            this.totalToolStripStatusLabel.Text = "Summa: X";
            // 
            // CashBookForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 650);
            this.Controls.Add(this.transactionsDataGridView);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CashBookForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kassabok";
            this.Activated += new System.EventHandler(this.CashBookForm_Activated);
            this.Load += new System.EventHandler(this.CashBookForm_Load);
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
        private System.Windows.Forms.DataGridView transactionsDataGridView;
        private System.Windows.Forms.ToolStripButton newCashBookItemButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel totalToolStripStatusLabel;
        private System.Windows.Forms.ToolStripComboBox categoryToolStripComboBox;
        private System.Windows.Forms.ToolStripComboBox userToolStripComboBox;
        private System.Windows.Forms.ToolStripComboBox monthToolStripComboBox;
        private System.Windows.Forms.ToolStripButton deleteToolStripButton;
        private System.Windows.Forms.ContextMenuStrip transactionContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox dateTypeComboBox;
    }
}