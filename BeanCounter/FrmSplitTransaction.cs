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
    public partial class FrmSplitTransaction : Form
    {
        Transaction Transaction;
        public int BankAccountID;
        public FrmSplitTransaction(Transaction transaction, int bankAccountID)
        {
            Transaction = transaction;
            BankAccountID = bankAccountID;
            InitializeComponent();
        }
        private void FrmSplitTransaction_Load(object sender, EventArgs e)
        {
            tbMerchantName.Text = Transaction.MerchantName;
            tbDate.Text = Convert.ToString(Transaction.TransactionDate);
            tbFullAmount.Text = Convert.ToString(Transaction.TransactionAmount);
            tbBankMemo.Text = Transaction.BankMemo;
            AddColumns();
            foreach (SplitTransaction transaction in
                    SplitTransaction.SplitTransactions(Transaction.OrginalTransactionID))
                dgvSplitTransaction.Rows.Add(
                    transaction.CategoryName,
                    transaction.TransactionAmount,
                    transaction.UserMemo,
                    transaction.SplitTransactionID);
        }
        private void AddColumns()
        {
            dgvSplitTransaction.Columns.Add(ComboColumn("CategoryName", "Category Name", Category.CategoryNames()));
            dgvSplitTransaction.Columns.Add(CurrencyColumn());
            dgvSplitTransaction.Columns.Add(TextColumn("UserMemo", "UserMemo", true));
            dgvSplitTransaction.Columns.Add(TextColumn("SplitTransactionID", "SplitTransactionID", false));
        }
        private DataGridViewColumn ComboColumn(string name, string headerText, IEnumerable<string> categoryNames)
        {
            DataGridViewComboBoxColumn categoryColumn = new DataGridViewComboBoxColumn();
            categoryColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
            categoryColumn.Items.Add("");
            foreach (string category in categoryNames)
                categoryColumn.Items.Add(category);
            categoryColumn.Name = name;
            categoryColumn.HeaderText = headerText;
            categoryColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            return categoryColumn;
        }
        private DataGridViewTextBoxColumn TextColumn(string columnName, string headerText, bool visible)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            if (columnName == "TransactionAmount")
            {
                DataGridViewCellStyle style = new DataGridViewCellStyle();
                style.Format = "C2";
                style.NullValue = null;
                column.DefaultCellStyle = style;
            }
            column.Name = columnName;
            column.HeaderText = headerText;
            column.Visible = visible;
            return column;
        }
        private DataGridViewTextBoxColumn CurrencyColumn()
        {
            DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            textColumn = TextColumn("TransactionAmount", "TransactionAmount", true);
            style = new DataGridViewCellStyle();
            style.Format = "C2";
            textColumn.DefaultCellStyle = style;
            return textColumn;
        }
    }
}
