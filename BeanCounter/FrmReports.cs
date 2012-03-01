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
    public partial class FrmReports : Form
    {
        public FrmReports()
        {
            InitializeComponent();
        }

        private void btnLoadReport_Click(object sender, EventArgs e)
        {
            tsslAverage.Text = "";
            tsslMonthly.Text = "";
            tsslRows.Text = "";
            tsslTotal.Text = "";
            EnableForm(false);
            dgvReports.Columns.Clear();
            switch (cbReportType.Text)
            {
                case "All Categories":
                    LoadAllCategories();
                    EnableForm(true);
                    break;
                case "By Category":
                    LoadByCategory();
                    EnableForm(true);
                    break;
                case "Cash Forecast":
                    LoadCashForecast();
                    break;
                case "Items Due":
                    break;
            }

        }
        private void LoadCashForecast()
        {
            decimal balance = 0;
            if (!string.IsNullOrEmpty(tbOverrideBalance.Text) &&
                    IsNumber(tbOverrideBalance.Text))
                balance = Convert.ToDecimal(tbOverrideBalance.Text);
            bwBuildCashForecast.RunWorkerAsync(new Options(
                cbOverrideBalance.Checked,
                balance,
                cbOverages.Checked,
                Convert.ToInt32(nudOverages.Value)));
        }
        private void LoadCategories()
        {
            cbCategoryName.Items.Clear();
            cbCategoryName.Items.Add("");
            foreach (string categoryName in Category.CategoryNames())
                cbCategoryName.Items.Add(categoryName);
        }
        private void LoadByCategory()
        {
            List<Transaction> transactions = Transaction.TransactionsByCategory(
                cbCategoryName.Text, cbDateRange.Text);
            AddByCategoryColumns();
            decimal transactionsTotal = 0;
            foreach (Transaction transaction in transactions)
            {
                dgvReports.Rows.Add(
                    transaction.TransactionDate.ToString("MM/dd/yyyy"),
                    transaction.MerchantName,
                    transaction.TransactionAmount,
                    transaction.BankMemo,
                    transaction.UserMemo,
                    transaction.CheckNumber,
                    transaction.TransactionType);
                transactionsTotal += transaction.TransactionAmount;
            }
            tsslRows.Text = "Number of transactions: " + Convert.ToString(transactions.Count);
            decimal average = 0;
            if (transactionsTotal != 0 && transactions.Count > 0)
                average = Math.Round(transactionsTotal / transactions.Count);
            tsslAverage.Text = "Average amount per transaction: $" + Convert.ToString(average);
            tsslMonthly.Text = "Monthly average: $" + Transaction.MonthlyAverage(transactionsTotal, cbDateRange.Text);
            tsslTotal.Text = "Total: $" + Convert.ToString(transactionsTotal);

        }
        private void LoadAllCategories()
        {
            AddAllCategoriesColumns();
            //foreach (Category category in Category.Categories())
            foreach (string categoryName in Transaction.Categories(cbDateRange.Text))
            {
                CategoryTotal categoryTotal = Transaction.CategoryTotal(categoryName, cbDateRange.Text);
                if (categoryTotal.Count > 0)
                {
                    dgvReports.Rows.Add(
                        categoryName,
                        categoryTotal.TransactionAmount,
                        Math.Round(categoryTotal.TransactionAmount / categoryTotal.Count),
                        Transaction.MonthlyAverage(categoryTotal.TransactionAmount, cbDateRange.Text),
                        categoryTotal.Count);
                }
            }
        }
        private void AddCashForecastColumns()
        {
            dgvReports.Columns.Clear();
            //dgvReports.Columns.Add(TextColumn("RowType", "Type", true));
            //dgvReports.Columns.Add(CurrencyColumn("Jan", "Jan"));
            //dgvReports.Columns.Add(CurrencyColumn("Feb", "Feb"));
            //dgvReports.Columns.Add(CurrencyColumn("Mar", "Mar"));
            //dgvReports.Columns.Add(CurrencyColumn("Apr", "Apr"));
            //dgvReports.Columns.Add(CurrencyColumn("May", "May"));
            //dgvReports.Columns.Add(CurrencyColumn("Jun", "Jun"));
            //dgvReports.Columns.Add(CurrencyColumn("Jul", "Jul"));
            //dgvReports.Columns.Add(CurrencyColumn("Aug", "Aug"));
            //dgvReports.Columns.Add(CurrencyColumn("Sep", "Sep"));
            //dgvReports.Columns.Add(CurrencyColumn("Oct", "Oct"));
            //dgvReports.Columns.Add(CurrencyColumn("Nov", "Nov"));
            //dgvReports.Columns.Add(CurrencyColumn("Dec", "Dec"));
            //dgvReports.Columns.Add(CurrencyColumn("Average", "Average"));
            dgvReports.Columns.Add(TextColumn("Date", "Date", true));
            dgvReports.Columns.Add(TextColumn("Income", "Income", true));
            dgvReports.Columns.Add(TextColumn("Spending", "Spending", true));
            dgvReports.Columns.Add(TextColumn("Gain", "Gain", true));
            dgvReports.Columns.Add(TextColumn("Balance", "Balance on the 1st", true));

        }
        private void AddAllCategoriesColumns()
        {
            dgvReports.Columns.Add(TextColumn("CategoryName", "Category Name", true));
            dgvReports.Columns.Add(CurrencyColumn("Total", "Total", true));
            dgvReports.Columns.Add(CurrencyColumn("Average", "Average amount per transaction", true));
            dgvReports.Columns.Add(CurrencyColumn("Monthly Average", "Monthly average", true));
            dgvReports.Columns.Add(TextColumn("Count", "Number of transactions", true));
        }
        private void AddByCategoryColumns()
        {
            dgvReports.Columns.Add(TextColumn("TransactionDate", "Date", true));
            dgvReports.Columns.Add(TextColumn("Merchant", "Merchant", true));
            dgvReports.Columns.Add(CurrencyColumn("StaticAmount", "TransactionAmount", false));
            dgvReports.Columns.Add(TextColumn("BankMemo", "BankMemo", true));
            dgvReports.Columns.Add(TextColumn("UserMemo", "UserMemo", true));
            dgvReports.Columns.Add(TextColumn("CheckNumber", "CheckNumber", true));
            dgvReports.Columns.Add(TextColumn("TransactionType", "TransactionType", true));
        }
        private static bool IsNumber(string text)
        {
            text = text.Trim();
            double number;
            bool isNumber = double.TryParse(text, out number);
            return isNumber;
        }
        private void EnableForm(bool enabled)
        {
            btnLoadReport.Enabled = enabled;
            scMain.Enabled = enabled;
            gbForecaseOptions.Enabled = enabled;
        }
        private DataGridViewRow CashForecastList(string type, CashForecast cashforecast)
        {
            DataGridViewRow row = new DataGridViewRow();
            DataGridViewTextBoxCell cell = new DataGridViewTextBoxCell();
            cell.Value = type;
            row.Cells.Add(cell);
            cell = new DataGridViewTextBoxCell();
            cell.Value = cashforecast.C1;
            row.Cells.Add(cell);
            cell = new DataGridViewTextBoxCell();
            cell.Value = cashforecast.C2;
            row.Cells.Add(cell);
            cell = new DataGridViewTextBoxCell();
            cell.Value = cashforecast.C3;
            row.Cells.Add(cell);
            cell = new DataGridViewTextBoxCell();
            cell.Value = cashforecast.C4;
            row.Cells.Add(cell);
            cell = new DataGridViewTextBoxCell();
            cell.Value = cashforecast.C5;
            row.Cells.Add(cell);
            cell = new DataGridViewTextBoxCell();
            cell.Value = cashforecast.C6;
            row.Cells.Add(cell);
            cell = new DataGridViewTextBoxCell();
            cell.Value = cashforecast.C7;
            row.Cells.Add(cell);
            cell = new DataGridViewTextBoxCell();
            cell.Value = cashforecast.C8;
            row.Cells.Add(cell);
            cell = new DataGridViewTextBoxCell();
            cell.Value = cashforecast.C9;
            row.Cells.Add(cell);
            cell = new DataGridViewTextBoxCell();
            cell.Value = cashforecast.C10;
            row.Cells.Add(cell);
            cell = new DataGridViewTextBoxCell();
            cell.Value = cashforecast.C11;
            row.Cells.Add(cell);
            cell = new DataGridViewTextBoxCell();
            cell.Value = cashforecast.C12;
            row.Cells.Add(cell);
            if (type != "Balance")
            {
                cell = new DataGridViewTextBoxCell();
                cell.Value = cashforecast.Average;
                row.Cells.Add(cell);
            }
            return row;
        }
        private DataGridViewTextBoxColumn CurrencyColumn(string name, string headerText, bool roundOff)
        {
            DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            textColumn = TextColumn(name, headerText, true);
            style = new DataGridViewCellStyle();
            if (roundOff)
                style.Format = "C0";
            else
                style.Format = "C";
            textColumn.DefaultCellStyle = style;
            return textColumn;
        }
        private DataGridViewTextBoxColumn TextColumn(string name, string headerText, bool visable)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.Name = name;
            column.HeaderText = headerText;
            column.Visible = visable;
            return column;
        }
        private void bwBuildCashForecast_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = CashForecast.BuildCashForecast(e.Argument as Options);
        }
        private void bwBuildCashForecast_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            AddCashForecastColumns();
            CashForecast cashForecast = e.Result as CashForecast;
            int year = DateTime.Today.Year;
            bool yearchanged = false;
            if (DateTime.Today.Month == 1)
            {
                dgvReports.Rows.Add("Jan " + year.ToString(), cashForecast._IncomeForecast.C1.ToString("$#,#"), cashForecast._SpendingForecast.C1.ToString("$#,#"), cashForecast._GainForecast.C1.ToString("$#,#"));
            }
            else
            {
                year++;
                dgvReports.Rows.Add("Jan " + year.ToString(), cashForecast._IncomeForecast.C1.ToString("$#,#"), cashForecast._SpendingForecast.C1.ToString("$#,#"), cashForecast._GainForecast.C1.ToString("$#,#"), cashForecast._BalanceForecast.C1.ToString("$#,#"));
            }
            if (DateTime.Today.Month <= 2 && yearchanged == false)
            {
                year--;
                yearchanged = true;
            }
            dgvReports.Rows.Add("Feb " + year.ToString(), cashForecast._IncomeForecast.C2.ToString("$#,#"), cashForecast._SpendingForecast.C2.ToString("$#,#"), cashForecast._GainForecast.C2.ToString("$#,#"), cashForecast._BalanceForecast.C2.ToString("$#,#"));
            if (DateTime.Today.Month <= 3 && yearchanged == false)
            {
                year--;
                yearchanged = true;
            }
            dgvReports.Rows.Add("Mar " + year.ToString(), cashForecast._IncomeForecast.C3.ToString("$#,#"), cashForecast._SpendingForecast.C3.ToString("$#,#"), cashForecast._GainForecast.C3.ToString("$#,#"), cashForecast._BalanceForecast.C3.ToString("$#,#"));
            if (DateTime.Today.Month <= 4 && yearchanged == false)
            {
                year--;
                yearchanged = true;
            }
            dgvReports.Rows.Add("Apr " + year.ToString(), cashForecast._IncomeForecast.C4.ToString("$#,#"), cashForecast._SpendingForecast.C4.ToString("$#,#"), cashForecast._GainForecast.C4.ToString("$#,#"), cashForecast._BalanceForecast.C4.ToString("$#,#"));
            if (DateTime.Today.Month <= 5 && yearchanged == false)
            {
                year--;
                yearchanged = true;
            }
            dgvReports.Rows.Add("May " + year.ToString(), cashForecast._IncomeForecast.C5.ToString("$#,#"), cashForecast._SpendingForecast.C5.ToString("$#,#"), cashForecast._GainForecast.C5.ToString("$#,#"), cashForecast._BalanceForecast.C5.ToString("$#,#"));
            if (DateTime.Today.Month <= 6 && yearchanged == false)
            {
                year--;
                yearchanged = true;
            }
            dgvReports.Rows.Add("Jun " + year.ToString(), cashForecast._IncomeForecast.C6.ToString("$#,#"), cashForecast._SpendingForecast.C6.ToString("$#,#"), cashForecast._GainForecast.C6.ToString("$#,#"), cashForecast._BalanceForecast.C6.ToString("$#,#"));
            if (DateTime.Today.Month <= 7 && yearchanged == false)
            {
                year--;
                yearchanged = true;
            }
            dgvReports.Rows.Add("Jul " + year.ToString(), cashForecast._IncomeForecast.C7.ToString("$#,#"), cashForecast._SpendingForecast.C7.ToString("$#,#"), cashForecast._GainForecast.C7.ToString("$#,#"), cashForecast._BalanceForecast.C7.ToString("$#,#"));
            if (DateTime.Today.Month <= 8 && yearchanged == false)
            {
                year--;
                yearchanged = true;
            }

            dgvReports.Rows.Add("Aug " + year.ToString(), cashForecast._IncomeForecast.C8.ToString("$#,#"), cashForecast._SpendingForecast.C8.ToString("$#,#"), cashForecast._GainForecast.C8.ToString("$#,#"), cashForecast._BalanceForecast.C8.ToString("$#,#"));
            if (DateTime.Today.Month <= 9 && yearchanged == false)
            {
                year--;
                yearchanged = true;
            }
            dgvReports.Rows.Add("Sep " + year.ToString(), cashForecast._IncomeForecast.C9.ToString("$#,#"), cashForecast._SpendingForecast.C9.ToString("$#,#"), cashForecast._GainForecast.C9.ToString("$#,#"), cashForecast._BalanceForecast.C9.ToString("$#,#"));
            if (DateTime.Today.Month <= 10 && yearchanged == false)
            {
                year--;
                yearchanged = true;
            }
            dgvReports.Rows.Add("Oct " + year.ToString(), cashForecast._IncomeForecast.C10.ToString("$#,#"), cashForecast._SpendingForecast.C10.ToString("$#,#"), cashForecast._GainForecast.C10.ToString("$#,#"), cashForecast._BalanceForecast.C10.ToString("$#,#"));
            if (DateTime.Today.Month <= 11 && yearchanged == false)
            {
                year--;
                yearchanged = true;
            }

            dgvReports.Rows.Add("Nov " + year.ToString(), cashForecast._IncomeForecast.C11.ToString("$#,#"), cashForecast._SpendingForecast.C11.ToString("$#,#"), cashForecast._GainForecast.C11.ToString("$#,#"), cashForecast._BalanceForecast.C11.ToString("$#,#"));
            if (DateTime.Today.Month <= 12 && yearchanged == false)
            {
                year--;
                yearchanged = true;
            }
            dgvReports.Rows.Add("Dec " + year.ToString(), cashForecast._IncomeForecast.C12.ToString("$#,#"), cashForecast._SpendingForecast.C12.ToString("$#,#"), cashForecast._GainForecast.C12.ToString("$#,#"), cashForecast._BalanceForecast.C12.ToString("$#,#"));
            dgvReports.Rows.Add();
            dgvReports.Rows.Add("Totals", cashForecast._IncomeForecast.Total.ToString("$#,#"), cashForecast._SpendingForecast.Total.ToString("$#,#"), cashForecast._GainForecast.Total.ToString("$#,#"));
            dgvReports.Rows.Add("Average", cashForecast._IncomeForecast.Average.ToString("$#,#"), cashForecast._SpendingForecast.Average.ToString("$#,#"), cashForecast._GainForecast.Average.ToString("$#,#"));
            EnableForm(true);
        }


        private void cbDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cbDateRange.Text) && !string.IsNullOrEmpty(cbCategoryName.Text))
                btnLoadReport.Enabled = true;
            if (!string.IsNullOrEmpty(cbDateRange.Text) && cbReportType.Text == "All Categories")
                btnLoadReport.Enabled = true;
        }
        private void cbCategoryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cbDateRange.Text) && !string.IsNullOrEmpty(cbCategoryName.Text))
                btnLoadReport.Enabled = true;
        }
        private void cbOverrideBalance_CheckedChanged(object sender, EventArgs e)
        {
            if (cbOverrideBalance.Checked)
                tbOverrideBalance.Enabled = true;
            else
                tbOverrideBalance.Enabled = false;
        }
        private void cbOverages_CheckedChanged(object sender, EventArgs e)
        {
            if (cbOverages.Checked)
                nudOverages.Enabled = false;
            else
                nudOverages.Enabled = true;
        }
        private void cbReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbDateRange.Text = "";
            cbCategoryName.Text = "";
            btnLoadReport.Enabled = false;
            cbDateRange.Enabled = false;
            cbCategoryName.Enabled = false;
            gbForecaseOptions.Enabled = false;
            switch (cbReportType.Text)
            {
                case "All Categories":
                    cbDateRange.Enabled = true;
                    break;
                case "By Category":
                    if (!string.IsNullOrEmpty(cbReportType.Text))
                    {
                        cbDateRange.Enabled = true;
                        cbCategoryName.Enabled = true;
                        LoadCategories();
                    }
                    break;
                case "Cash Forecast":
                    if (!string.IsNullOrEmpty(cbReportType.Text))
                    {
                        btnLoadReport.Enabled = true;
                        gbForecaseOptions.Enabled = true;
                    }
                    break;
                case "Items Due":
                    break;
            }
        }
    }
}
