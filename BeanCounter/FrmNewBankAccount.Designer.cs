namespace BeanCounter
{
    partial class FrmNewBankAccount
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
            if (disposing && (components != null))
            {
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
            this.grpNewAccount = new System.Windows.Forms.GroupBox();
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbRemoveFromColumnB = new System.Windows.Forms.ComboBox();
            this.cbRemoveFromColumnA = new System.Windows.Forms.ComboBox();
            this.tbWebAddress = new System.Windows.Forms.TextBox();
            this.rbColumnB = new System.Windows.Forms.RadioButton();
            this.lblNickName = new System.Windows.Forms.Label();
            this.rbColumnA = new System.Windows.Forms.RadioButton();
            this.lblWebAddress = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tbNickname = new System.Windows.Forms.TextBox();
            this.lblColumnChoice = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvTransactions = new System.Windows.Forms.DataGridView();
            this.grpNewAccount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactions)).BeginInit();
            this.SuspendLayout();
            // 
            // grpNewAccount
            // 
            this.grpNewAccount.Controls.Add(this.scMain);
            this.grpNewAccount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpNewAccount.Location = new System.Drawing.Point(0, 0);
            this.grpNewAccount.Name = "grpNewAccount";
            this.grpNewAccount.Size = new System.Drawing.Size(641, 340);
            this.grpNewAccount.TabIndex = 0;
            this.grpNewAccount.TabStop = false;
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scMain.Location = new System.Drawing.Point(3, 16);
            this.scMain.Name = "scMain";
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.groupBox1);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.dgvTransactions);
            this.scMain.Size = new System.Drawing.Size(635, 321);
            this.scMain.SplitterDistance = 297;
            this.scMain.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbRemoveFromColumnB);
            this.groupBox1.Controls.Add(this.cbRemoveFromColumnA);
            this.groupBox1.Controls.Add(this.tbWebAddress);
            this.groupBox1.Controls.Add(this.rbColumnB);
            this.groupBox1.Controls.Add(this.lblNickName);
            this.groupBox1.Controls.Add(this.rbColumnA);
            this.groupBox1.Controls.Add(this.lblWebAddress);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.tbNickname);
            this.groupBox1.Controls.Add(this.lblColumnChoice);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnOk);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(9, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(282, 315);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // cbRemoveFromColumnB
            // 
            this.cbRemoveFromColumnB.FormattingEnabled = true;
            this.cbRemoveFromColumnB.Items.AddRange(new object[] {
            "[Everything]",
            "[Nothing]",
            "[Remove Merchant Name]"});
            this.cbRemoveFromColumnB.Location = new System.Drawing.Point(6, 181);
            this.cbRemoveFromColumnB.Name = "cbRemoveFromColumnB";
            this.cbRemoveFromColumnB.Size = new System.Drawing.Size(270, 21);
            this.cbRemoveFromColumnB.TabIndex = 7;
            // 
            // cbRemoveFromColumnA
            // 
            this.cbRemoveFromColumnA.FormattingEnabled = true;
            this.cbRemoveFromColumnA.Items.AddRange(new object[] {
            "[Everything]",
            "[Nothing]",
            "[Remove Merchant Name]"});
            this.cbRemoveFromColumnA.Location = new System.Drawing.Point(6, 141);
            this.cbRemoveFromColumnA.Name = "cbRemoveFromColumnA";
            this.cbRemoveFromColumnA.Size = new System.Drawing.Size(270, 21);
            this.cbRemoveFromColumnA.TabIndex = 5;
            // 
            // tbWebAddress
            // 
            this.tbWebAddress.Location = new System.Drawing.Point(6, 102);
            this.tbWebAddress.Name = "tbWebAddress";
            this.tbWebAddress.Size = new System.Drawing.Size(270, 20);
            this.tbWebAddress.TabIndex = 3;
            this.tbWebAddress.Leave += new System.EventHandler(this.tbWebAddress_Leave);
            // 
            // rbColumnB
            // 
            this.rbColumnB.AutoSize = true;
            this.rbColumnB.Location = new System.Drawing.Point(194, 235);
            this.rbColumnB.Name = "rbColumnB";
            this.rbColumnB.Size = new System.Drawing.Size(70, 17);
            this.rbColumnB.TabIndex = 10;
            this.rbColumnB.TabStop = true;
            this.rbColumnB.Text = "Column B";
            this.rbColumnB.UseVisualStyleBackColor = true;
            // 
            // lblNickName
            // 
            this.lblNickName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNickName.Location = new System.Drawing.Point(6, 18);
            this.lblNickName.Name = "lblNickName";
            this.lblNickName.Size = new System.Drawing.Size(189, 28);
            this.lblNickName.TabIndex = 0;
            this.lblNickName.Text = "Please enter account Nickname (e.g. Checking)";
            // 
            // rbColumnA
            // 
            this.rbColumnA.AutoSize = true;
            this.rbColumnA.Location = new System.Drawing.Point(8, 235);
            this.rbColumnA.Name = "rbColumnA";
            this.rbColumnA.Size = new System.Drawing.Size(70, 17);
            this.rbColumnA.TabIndex = 9;
            this.rbColumnA.TabStop = true;
            this.rbColumnA.Text = "Column A";
            this.rbColumnA.UseVisualStyleBackColor = true;
            // 
            // lblWebAddress
            // 
            this.lblWebAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWebAddress.Location = new System.Drawing.Point(6, 72);
            this.lblWebAddress.Name = "lblWebAddress";
            this.lblWebAddress.Size = new System.Drawing.Size(200, 27);
            this.lblWebAddress.TabIndex = 2;
            this.lblWebAddress.Text = "Please enter the website address (e.g. www.bank.com): ";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(113, 282);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // tbNickname
            // 
            this.tbNickname.Location = new System.Drawing.Point(6, 49);
            this.tbNickname.Name = "tbNickname";
            this.tbNickname.Size = new System.Drawing.Size(270, 20);
            this.tbNickname.TabIndex = 1;
            this.tbNickname.Leave += new System.EventHandler(this.tbNickname_Leave);
            // 
            // lblColumnChoice
            // 
            this.lblColumnChoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColumnChoice.Location = new System.Drawing.Point(5, 205);
            this.lblColumnChoice.Name = "lblColumnChoice";
            this.lblColumnChoice.Size = new System.Drawing.Size(259, 27);
            this.lblColumnChoice.TabIndex = 8;
            this.lblColumnChoice.Text = "Choose the column that best describes the merchant\'s name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(261, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Any text you wish to remove from all cells in Column A:";
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(194, 282);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 11;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 165);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(261, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Any text you wish to remove from all cells in Column B:";
            // 
            // dgvTransactions
            // 
            this.dgvTransactions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTransactions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTransactions.Location = new System.Drawing.Point(0, 0);
            this.dgvTransactions.Name = "dgvTransactions";
            this.dgvTransactions.Size = new System.Drawing.Size(334, 321);
            this.dgvTransactions.TabIndex = 0;
            // 
            // FrmNewBankAccount
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 340);
            this.Controls.Add(this.grpNewAccount);
            this.MinimizeBox = false;
            this.Name = "FrmNewBankAccount";
            this.Text = "New Bank Account";
            this.Load += new System.EventHandler(this.FrmNewBankAccount_Load);
            this.grpNewAccount.ResumeLayout(false);
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpNewAccount;
        private System.Windows.Forms.SplitContainer scMain;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox tbNickname;
        public System.Windows.Forms.TextBox tbWebAddress;
        private System.Windows.Forms.Label lblWebAddress;
        private System.Windows.Forms.Label lblNickName;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.DataGridView dgvTransactions;
        private System.Windows.Forms.Label lblColumnChoice;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.RadioButton rbColumnB;
        public System.Windows.Forms.RadioButton rbColumnA;
        public System.Windows.Forms.ComboBox cbRemoveFromColumnB;
        public System.Windows.Forms.ComboBox cbRemoveFromColumnA;
    }
}