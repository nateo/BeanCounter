using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.OleDb;
using BeanCounter.BusinessLogic;

namespace BeanCounter
{
    public partial class FrmRegister : Form
    {
        int BankAccountID;
        string AccountName;

        public FrmRegister(int bankAccountID, string accountName)
        {
            InitializeComponent();
            BankAccountID = bankAccountID;
            AccountName = accountName;
            this.Text = accountName;
        }
        private void FrmRegister_Load(object sender, EventArgs e)
        {
            AddColumnsToGrid();
            AddTransactionSorted();
        }
        private void dgvRegister_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ComboBox combo = e.Control as ComboBox;
            if (combo != null)
            {
                combo.SelectedIndexChanged -= new EventHandler(cbCategory_SelectedIndexChanged);
                combo.SelectedIndexChanged += new EventHandler(cbCategory_SelectedIndexChanged);
            }
        }
        private void dgvRegister_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvRegister.Columns[e.ColumnIndex].Name == "Verified")
            {
                this.Validate();
                SaveRow();
                if (Convert.ToBoolean(dgvRegister.CurrentRow.Cells[e.ColumnIndex].Value.ToString()) == true &&
                    tsmiShowUnverifiedTransactionsOnly.Checked == true)
                    dgvRegister.Rows.Remove(dgvRegister.CurrentRow);
            }
        }
        private void dgvRegister_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.Validate();
            SaveRow();
        }
        private void dgvRegister_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            switch (dgvRegister.Columns[e.ColumnIndex].Name)
            {
                case "MerchantName":
                case "UserMemo":
                    this.Validate();
                    SaveRow();
                    break;
            }
        }
        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            string categoryName = ((ComboBox)sender).Text;
            string merchantName = dgvRegister.CurrentRow.Cells["MerchantName"].Value.ToString();
            Transaction.UpdateTransactionCategory(categoryName, Convert.ToInt32(
                dgvRegister.CurrentRow.Cells["OrginalTransactionID"].Value.ToString()));
            if (categoryName == "[Multiple Categories]")
            {
                Transaction transaction = new Transaction();
                transaction.OrginalTransactionID = Convert.ToInt32(dgvRegister.CurrentRow.Cells["OrginalTransactionID"].Value.ToString());
                transaction.MerchantName = merchantName;
                transaction.TransactionDate = Convert.ToDateTime(dgvRegister.CurrentRow.Cells["Date"].Value.ToString());
                transaction.TransactionAmount = Convert.ToDecimal(dgvRegister.CurrentRow.Cells["StaticAmount"].Value.ToString());
                transaction.BankMemo = dgvRegister.CurrentRow.Cells["BankMemo"].Value.ToString();
                FrmSplitTransaction frmSplitTransaction = new FrmSplitTransaction(transaction, BankAccountID);
                if (frmSplitTransaction.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    decimal totalAmount = 0;
                    for (int i = 0; i < frmSplitTransaction.dgvSplitTransaction.Rows.Count - 1; i++)
                    {
                        decimal transactionAmount = 0;
                        if (frmSplitTransaction.dgvSplitTransaction.Rows[i].Cells["TransactionAmount"].Value != null)
                            transactionAmount = Convert.ToDecimal(frmSplitTransaction.dgvSplitTransaction.Rows[i].Cells["TransactionAmount"].Value.ToString()
                               .Replace("($", "-").Replace("$", "").Replace(")", ""));
                        totalAmount += transactionAmount;
                    }
                    if (totalAmount == Convert.ToDecimal(dgvRegister.CurrentRow.Cells["StaticAmount"].Value.ToString()))
                    {
                        for (int i = 0; i < frmSplitTransaction.dgvSplitTransaction.Rows.Count - 1; i++)
                        {
                            string userMemo = "";
                            if (frmSplitTransaction.dgvSplitTransaction.Rows[i].Cells["UserMemo"].Value != null)
                                userMemo = frmSplitTransaction.dgvSplitTransaction.Rows[i].Cells["UserMemo"].Value.ToString();
                            if (frmSplitTransaction.dgvSplitTransaction.Rows[i].Cells["SplitTransactionID"].Value == null)
                                SplitTransaction.InsertTransaction(
                                    frmSplitTransaction.dgvSplitTransaction.Rows[i].Cells["CategoryName"].Value.ToString(),
                                    userMemo,
                                    Convert.ToDecimal(frmSplitTransaction.dgvSplitTransaction.Rows[i].Cells["TransactionAmount"].Value.ToString()),
                                    Convert.ToInt32(dgvRegister.CurrentRow.Cells["OrginalTransactionID"].Value.ToString()));
                            else
                                SplitTransaction.UpdateSplitTransaction(
                                    frmSplitTransaction.dgvSplitTransaction.Rows[i].Cells["CategoryName"].Value.ToString(),
                                    userMemo,
                                    Convert.ToDecimal(frmSplitTransaction.dgvSplitTransaction.Rows[i].Cells["TransactionAmount"].Value.ToString()
                                    .Replace("($", "-").Replace("$", "").Replace(")", "")),
                                    Convert.ToInt32(frmSplitTransaction.dgvSplitTransaction.Rows[i].Cells["SplitTransactionID"].Value.ToString()));
                        }
                    }
                    else
                        MessageBox.Show("Error", "Total split transactions amount must equal the total transaction amount (" +
                            dgvRegister.CurrentRow.Cells["StaticAmount"].Value.ToString());
                }
                else
                    MessageBox.Show("Error", "All categories and amounts need to be filled in properly");
            }
            else if (!string.IsNullOrEmpty(categoryName) && dgvRegister.CurrentRow.Cells["CheckNumber"].Value != null)
            {

                List<Merchant> nationalMerchants = Merchant.NationalMerchants();
                if (Merchant.NationalMerchant(merchantName, nationalMerchants) == "")
                {
                    if (Merchant.LocalMerchant(merchantName, false) == "" &&
                            !string.IsNullOrEmpty(merchantName) &&
                            merchantName != "?")
                    {

                        Merchant.InsertMerchant(merchantName, categoryName, true, true);
                        foreach (DataGridViewRow row in dgvRegister.Rows)
                        {
                            if (row.Cells["MerchantName"].Value.ToString() == merchantName)
                            {
                                row.Cells["CategoryName"].Value = categoryName;
                            }
                        }
                        dgvRegister.Refresh();
                    }
                }
            }
        }
        private DataGridViewColumn CheckBoxColumn(string name, string headerText)
        {
            DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
            column.Name = "Verified";
            column.HeaderText = "Verified";
            return column;
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
        private DataGridViewTextBoxColumn TextColumn(string columnName, bool visable, string headerText)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            switch (columnName)
            {
                case "StaticAmount":
                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                    style.Format = "C2";
                    style.NullValue = null;
                    column.DefaultCellStyle = style;
                    column.ReadOnly = true;
                    break;
                case "Date":
                case "TransactionType":
                case "CheckNumber":
                case "BankMemo":
                    column.ReadOnly = true;
                    break;
                default:
                    column.ReadOnly = false;
                    break;
            }
            column.Name = columnName;
            column.HeaderText = headerText;
            column.Visible = visable;
            return column;
        }
        private void AddTransactionSorted()
        {
            if (tsmiByCategoryMerchantName.Checked)
            {
                AddTransactions(ByCategoryMerchant());
                AddTransactions(ByChecks());
                AddTransactions(UnCategorized());
            }
            else if (tsmiByDate.Checked)
                AddTransactions(ByDate());
            if (dgvRegister.Rows.Count > 0)
                tsslTransactionCount.Text = "Number of transaction being displayed: " + dgvRegister.Rows.Count.ToString();

        }
        private string ByChecks()
        {
            string sql = string.Empty;
            if (tsmiYearToDateOnly.Checked)
            {
                sql += " and TransactionDate >= #01/01/" + DateTime.Now.Year + "#";
            }
            sql += " and (CategoryName is null) and (TransactionType = 'CHECK') order by TransactionDate DESC";
            return sql;
        }
        private string ByDate()
        {
            string sql = string.Empty;
            if (tsmiYearToDateOnly.Checked)
            {
                sql += " and tblOrginalTransaction.TransactionDate >= #01/01/" + DateTime.Now.Year + "#";
            } 
            sql += " ORDER BY tblOrginalTransaction.TransactionDate DESC, tblOrginalTransaction.TransactionAmount";
            return sql;
        }
        private string UnCategorized()
        {
            string sql = string.Empty;
            if (tsmiYearToDateOnly.Checked)
            {
                sql += " and TransactionDate >= #01/01/" + DateTime.Now.Year + "#";
            } 
            sql += " and (CategoryName is null) and (TransactionType <> 'CHECK') ORDER BY TransactionType, Merchant";
            return sql;
        }
        private string ByCategoryMerchant()
        {
            string sql = string.Empty;
            if (tsmiYearToDateOnly.Checked)
            {
                sql += " and TransactionDate >= #01/01/" + DateTime.Now.Year + "#";
            }
            sql += " and CategoryName is not null ORDER BY CategoryName, Merchant";
            return sql;
        }
        private void AddColumnsToGrid()
        {
            dgvRegister.Columns.Add(TextColumn("Date", true, "Date"));
            dgvRegister.Columns.Add(TextColumn("MerchantName", true, "Merchant Name"));
            dgvRegister.Columns.Add(ComboColumn("CategoryName", "Category", Category.CategoryNames()));
            dgvRegister.Columns.Add(CheckBoxColumn("Verified", "Verified"));
            dgvRegister.Columns.Add(TextColumn("StaticAmount", true, "TransactionAmount"));
            dgvRegister.Columns.Add(TextColumn("BankMemo", true, "BankMemo"));
            dgvRegister.Columns.Add(TextColumn("TransactionType", true, "Type"));
            dgvRegister.Columns.Add(TextColumn("UserMemo", true, "UserMemo"));
            dgvRegister.Columns.Add(TextColumn("CheckNumber", true, "Num"));
            dgvRegister.Columns.Add(TextColumn("OrginalTransactionID", false, ""));
        }
        private void SaveRow()
        {
            Transaction.UpdateTransaction(
                Convert.ToBoolean(dgvRegister.CurrentRow.Cells["Verified"].Value.ToString()),
                dgvRegister.CurrentRow.Cells["MerchantName"].Value.ToString(),
                dgvRegister.CurrentRow.Cells["UserMemo"].Value.ToString(),
                dgvRegister.CurrentRow.Cells["OrginalTransactionID"].Value.ToString());
        }
        private void AddTransactions(string orderBy)
        {
            bool verified = false;
            if (tsmiShowUnverifiedTransactionsOnly.Checked == true)
                verified = true;
            foreach (Transaction transaction in Transaction.Transactions(BankAccountID, verified, orderBy))
            {
                dgvRegister.Rows.Add(
                    transaction.TransactionDate.ToString("yyyy-MM-dd"),
                    transaction.MerchantName,
                    transaction.CategoryName,
                    transaction.Verified,
                    transaction.TransactionAmount,
                    transaction.BankMemo,
                    transaction.TransactionType,
                    transaction.UserMemo,
                    transaction.CheckNumber,
                    transaction.OrginalTransactionID);
            }
            if (dgvRegister.Rows.Count > 0)
                dgvRegister.UpdateRowHeightInfo(0, true);
            dgvRegister.Refresh();
        }
        private bool IsNumber(string text)
        {
            text = text.Trim();
            double number;
            bool isNumber = double.TryParse(text, out number);
            return isNumber;
        }
        private void tsmiShowAllTransactions_Click_1(object sender, EventArgs e)
        {
            tsmiShowAllTransactions.Checked = true;
            tsmiShowUnverifiedTransactionsOnly.Checked = false;
            dgvRegister.Rows.Clear();
            AddTransactionSorted();
        }
        private void tsmiByDate_Click(object sender, EventArgs e)
        {
            tsmiByDate.Checked = true;
            tsmiByCategoryMerchantName.Checked = false;
            dgvRegister.Rows.Clear();
            AddTransactions(ByDate());
        }
        private void tsmiByCategoryMerchantName_Click(object sender, EventArgs e)
        {
            tsmiByDate.Checked = false;
            tsmiByCategoryMerchantName.Checked = true;
            dgvRegister.Rows.Clear();
            AddTransactions(ByCategoryMerchant());
            AddTransactions(UnCategorized());
        }
        private void markCategorizedTransactionsAsVerifiedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Transaction.MarkVerified();
            if (tsmiShowUnverifiedTransactionsOnly.Checked)
            {
                foreach (DataGridViewRow row in dgvRegister.Rows)
                {
                    if (row.Cells["Categoryname"].Value != null &&
                        !string.IsNullOrEmpty(row.Cells["Categoryname"].Value.ToString()))
                        dgvRegister.Rows.Remove(row);
                }
            }
            dgvRegister.Rows.Clear();
            AddTransactionSorted();
            Cursor = Cursors.Default;

        }
        private void tsmiShowUnverifiedTransactionsOnly_Click(object sender, EventArgs e)
        {
            tsmiShowUnverifiedTransactionsOnly.Checked = true;
            tsmiShowAllTransactions.Checked = false;
            dgvRegister.Rows.Clear();
            AddTransactions(ByDate());
        }

        private void tsmiYearToDateOnly_Click(object sender, EventArgs e)
        {
            if (tsmiYearToDateOnly.Checked)
            {
                tsmiYearToDateOnly.Checked = false;
            }
            else
            {
                tsmiYearToDateOnly.Checked = true;
            }
            
            ConfigurationManager.AppSettings.Remove("YearToDateOnly");
            ConfigurationManager.AppSettings.Set("YearToDateOnly", tsmiYearToDateOnly.Checked.ToString());
            dgvRegister.Rows.Clear();
            AddTransactionSorted();
        }

    }
}
