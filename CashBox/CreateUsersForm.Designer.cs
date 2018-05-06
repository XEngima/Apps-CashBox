namespace CashBox
{
    partial class CreateUsersForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateUsersForm));
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.user2TextBox = new System.Windows.Forms.TextBox();
            this.user1TextBox = new System.Windows.Forms.TextBox();
            this.createUsersButton = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Namn användare 1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.user2TextBox);
            this.groupBox1.Controls.Add(this.user1TextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(302, 138);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Skapa användare";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Namn användare 2 (kan lämnas tom)";
            // 
            // user2TextBox
            // 
            this.errorProvider.SetIconPadding(this.user2TextBox, 3);
            this.user2TextBox.Location = new System.Drawing.Point(20, 96);
            this.user2TextBox.Name = "user2TextBox";
            this.user2TextBox.Size = new System.Drawing.Size(251, 20);
            this.user2TextBox.TabIndex = 3;
            // 
            // user1TextBox
            // 
            this.errorProvider.SetIconPadding(this.user1TextBox, 3);
            this.user1TextBox.Location = new System.Drawing.Point(20, 45);
            this.user1TextBox.Name = "user1TextBox";
            this.user1TextBox.Size = new System.Drawing.Size(251, 20);
            this.user1TextBox.TabIndex = 1;
            // 
            // createUsersButton
            // 
            this.createUsersButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.createUsersButton.Location = new System.Drawing.Point(206, 163);
            this.createUsersButton.Name = "createUsersButton";
            this.createUsersButton.Size = new System.Drawing.Size(108, 23);
            this.createUsersButton.TabIndex = 1;
            this.createUsersButton.Text = "Skapa användare";
            this.createUsersButton.UseVisualStyleBackColor = true;
            this.createUsersButton.Click += new System.EventHandler(this.createUsersButton_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // CreateUsersForm
            // 
            this.AcceptButton = this.createUsersButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 198);
            this.ControlBox = false;
            this.Controls.Add(this.createUsersButton);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateUsersForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CashBox";
            this.Load += new System.EventHandler(this.CreateUsersForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button createUsersButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox user2TextBox;
        private System.Windows.Forms.TextBox user1TextBox;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}