using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BeanCounter.BusinessLogic;

namespace BeanCounter
{
    public partial class FrmNewBankAccount : Form
    {
        public Basket Basket;

        public FrmNewBankAccount(Basket basket)
        {
            InitializeComponent();
            Basket = basket;
        }

        private void FrmNewBankAccount_Load(object sender, EventArgs e)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.HeaderText = "Column A";
            column.Name = "ColumnA";
            dgvTransactions.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.HeaderText = "Column B";
            column.Name = "ColumnB";
            dgvTransactions.Columns.Add(column);
            foreach (Transaction transaction in Basket.ofxFile.Transactions)
                dgvTransactions.Rows.Add(
                    transaction.MerchantName,
                    transaction.BankMemo);
            switch (Basket.ofxFile.BankAccount.BankName)
            {
                case "U.S. Bank":
                    if (Basket.BankAccount.AccountType.ToLower() == "checking" |
                        Basket.BankAccount.AccountType.ToLower() == "savings")
                    {
                        tbWebAddress.Text = "www.usbank.com";
                        rbColumnA.Checked = false;
                        rbColumnB.Checked = true;
                        cbRemoveFromColumnA.Text = "[Everything]";
                        cbRemoveFromColumnB.Text = "Download from usbank.com.";
                    }
                    else if (Basket.BankAccount.AccountType.ToLower() == "credit")
                    {
                        tbWebAddress.Text = "www.usbank.com";
                        rbColumnA.Checked = true;
                        rbColumnB.Checked = false;
                        cbRemoveFromColumnA.Text = "[Nothing]";
                        cbRemoveFromColumnB.Text = "[Everything]";
                    }
                    break;
            }
            CheckBold();
        }

        private void ResizeWindow()
        {
            this.Size = new System.Drawing.Size(
                this.MdiParent.Size.Width - 100,
                this.MdiParent.Size.Height - 230);
        }

        private bool FormCompleted()
        {
            if (!string.IsNullOrEmpty(tbNickname.Text) &&
                !string.IsNullOrEmpty(tbWebAddress.Text))
                return true;
            else
                return false;

        }

        private void tbWebAddress_Leave(object sender, EventArgs e)
        {
            CheckBold();
        }

        private void CheckBold()
        {
            if (!string.IsNullOrEmpty(tbWebAddress.Text))
                lblWebAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular);
            else
                lblWebAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            if (!string.IsNullOrEmpty(tbNickname.Text))
                lblNickName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular);
            else
                lblNickName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            if (rbColumnA.Checked || rbColumnB.Checked)
                lblColumnChoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular);
            else
                lblColumnChoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);

        }

        private void tbNickname_Leave(object sender, EventArgs e)
        {
            CheckBold();
        }




    }
}
