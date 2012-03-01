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
    public partial class FrmCategories : Form
    {
        bool FinishedLoading = false;

        public FrmCategories()
        {
            InitializeComponent();
        }
        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CategoryType()) && !string.IsNullOrEmpty(Frequency()))
                LoadCategories();
        }
        private void cbFrequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CategoryType()) && !string.IsNullOrEmpty(Frequency()))
                LoadCategories();
        }
        private void cbDayOfMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveRow(Convert.ToInt32(((ComboBox)sender).Text));
            //dgvCategories.EndEdit();
        }
        private string Frequency()
        {
            string frequency = "";
            if (rbAnnually.Checked)
                frequency = "Annually";
            if (rbAnytime.Checked)
                frequency = "Anytime";
            if (rbBiWeekly.Checked)
                frequency = "Bi-Weekly";
            if (rbMonthly.Checked)
                frequency = "Monthly";
            if (rbOneTime.Checked)
                frequency = "One Time";
            if (rbWeekly.Checked)
                frequency = "Weekly";
            return frequency;
        }
        private void LoadCategories()
        {
            ClearMonthlyValues();
            gbMonthlyAmounts.Enabled = false;
            FinishedLoading = false;
            dgvCategories.Rows.Clear();
            dgvCategories.Columns.Clear();
            dgvCategories.Columns.Add(TextColumn("CategoryID", "CategoryID", false));
            dgvCategories.Columns.Add(TextColumn("CategoryName", "Category Name", true));
            switch (Frequency())
            {
                case "Annually":
                case "One Time":
                    dgvCategories.Columns.Add(StaticAmountColumn());
                    dgvCategories.Columns.Add(TextColumn("SpecificDate", "Date", true));
                    dgvCategories.Columns.Add(CheckboxColumn("ExcludeFromBudget", "Exclude from budget?"));
                    foreach (Category category in Category.Categories(CategoryType(), Frequency(), chkByDate.Checked))
                        dgvCategories.Rows.Add(category.CategoryID, category.CategoryName, category.StaticAmount,
                            category.SpecificDate.ToString("MM/dd/yyyy"), category.ExcludeFromBudget);
                    break;
                case "Anytime":
                    gbMonthlyAmounts.Enabled = true;
                    dgvCategories.Columns.Add(CheckboxColumn("ExcludeFromBudget", "Exclude from budget?"));
                    foreach (Category category in Category.Categories(CategoryType(), Frequency(), chkByDate.Checked))
                        dgvCategories.Rows.Add(category.CategoryID, category.CategoryName, category.ExcludeFromBudget);
                    break;
                case "Bi-Weekly":
                case "Weekly":
                    dgvCategories.Columns.Add(StaticAmountColumn());
                    dgvCategories.Columns.Add(TextColumn("NextOccurance", "Next Occurance", true));
                    dgvCategories.Columns.Add(TextColumn("EndDate", "End Date (Optional)", true));
                    dgvCategories.Columns.Add(CheckboxColumn("ExcludeFromBudget", "Exclude from budget?"));
                    foreach (Category category in Category.Categories(CategoryType(), Frequency(), chkByDate.Checked))
                    {
                        string endDate = "";
                        if (category.EndDate != new DateTime())
                            endDate = Convert.ToString(category.EndDate.ToString("MM/dd/yyy"));
                        dgvCategories.Rows.Add(category.CategoryID, category.CategoryName, category.StaticAmount,
                               category.NextOccurance.ToString("MM/dd/yyy"), endDate, category.ExcludeFromBudget);
                    }
                    break;
                case "Monthly":
                    gbMonthlyAmounts.Enabled = true;
                    dgvCategories.Columns.Add(ComboColumn("DayOfMonth", "Day", DaysOfMonth()));
                    dgvCategories.Columns.Add(CheckboxColumn("ExcludeFromBudget", "Exclude from budget?"));
                    foreach (Category category in Category.Categories(CategoryType(), Frequency(), chkByDate.Checked))
                        dgvCategories.Rows.Add(category.CategoryID, category.CategoryName,
                            Convert.ToString(category.DayOfMonth), category.ExcludeFromBudget);
                    break;
            }
            FinishedLoading = true;
            if (Frequency() == "Monthly" | Frequency() == "Anytime")
            {
                if (dgvCategories.Rows[0].Cells["CategoryID"].Value != null)
                {
                    Category category = Category.FindCategory(Convert.ToInt32(
                        dgvCategories.Rows[0].Cells["CategoryID"].Value.ToString()));
                    LoadMonthlyValues(category);
                }
            }
            if (dgvCategories.CurrentRow != null)
            {
                if (dgvCategories.CurrentRow.Cells["Categoryname"].Value != null)
                    tsmiDeleteCategory.Enabled = true;
                else
                    tsmiDeleteCategory.Enabled = false;
            }
        }
        private string CategoryType()
        {
            string categoryType = "";
            if (rbSpending.Checked)
                categoryType = "Expenses";
            if (rbIncome.Checked)
                categoryType = "Income";
            return categoryType;
        }
        private void dgvCategories_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.Button == MouseButtons.Right)
            {
                if (dgvCategories.CurrentRow.Cells["CategoryName"].Value != null)
                {
                    this.Validate();
                    dgvCategories.EndEdit();
                    dgvCategories.CurrentRow.Selected = false;
                    dgvCategories.CurrentCell = dgvCategories[e.ColumnIndex, e.RowIndex];
                    dgvCategories.Rows[e.RowIndex].Selected = true;
                    Rectangle r = dgvCategories.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    cmsDelete.Show((Control)sender, r.Left + e.X, r.Top + e.Y);
                }
            }
        }
        private void dgvCategories_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCategories.Columns[e.ColumnIndex].Name == "ExcludeFromBudget")
            {
                this.Validate();
                SaveRow();
            }
        }
        private void dgvCategories_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.Validate();
                SaveRow();
            }
        }
        private void dgvCategories_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            ClearMonthlyAmounts();
        }
        private void dgvCategories_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            dgvCategories.Rows[e.RowIndex].Selected = true;
            if (dgvCategories.Rows[e.RowIndex].Cells["CategoryName"].Value != null)
                LoadRow(Convert.ToInt32(dgvCategories.Rows[e.RowIndex].Cells["CategoryID"].Value.ToString()));
        }
        private void dgvCategories_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ComboBox combo = e.Control as ComboBox;
            if (combo != null)
            {
                combo.SelectedIndexChanged -= new EventHandler(cbDayOfMonth_SelectedIndexChanged);
                combo.SelectedIndexChanged += new EventHandler(cbDayOfMonth_SelectedIndexChanged);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            this.Validate();
            SaveRow();
            btnSave.Enabled = true;
        }
        private void btnFill_Click(object sender, EventArgs e)
        {
            tbJan.Text = tbFill.Text;
            tbFeb.Text = tbFill.Text;
            tbMar.Text = tbFill.Text;
            tbApr.Text = tbFill.Text;
            tbMar.Text = tbFill.Text;
            tbMay.Text = tbFill.Text;
            tbJun.Text = tbFill.Text;
            tbJul.Text = tbFill.Text;
            tbAug.Text = tbFill.Text;
            tbSep.Text = tbFill.Text;
            tbOct.Text = tbFill.Text;
            tbNov.Text = tbFill.Text;
            tbDec.Text = tbFill.Text;
            tbFill.Text = "";
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearMonthlyAmounts();
        }
        private void tsmiDeleteCategory_Click(object sender, EventArgs e)
        {
            int test = dgvCategories.CurrentRow.Index;
            if (dgvCategories.CurrentRow.Cells["Categoryname"].Value != null)
                if (MessageBox.Show("Are you sure you want to delete this category?", "Confirmation", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (Category.IsUsed(dgvCategories.CurrentRow.Cells["CategoryName"].Value.ToString()) &&
                        Category.Categories(dgvCategories.CurrentRow.Cells["CategoryName"].Value.ToString()).Count < 2)
                        MessageBox.Show("The current category is in use.  You must assign a new category to the transaction(s) first");
                    else
                    {
                        Category.DeleteCategory(
                            Convert.ToInt32(dgvCategories.CurrentRow.Cells["CategoryID"].Value.ToString()));
                        dgvCategories.Rows.Remove(dgvCategories.CurrentRow);
                    }
                }
        }
        private void ClearMonthlyAmounts()
        {
            tbJan.Text = "";
            tbFeb.Text = "";
            tbMar.Text = "";
            tbApr.Text = "";
            tbMay.Text = "";
            tbJun.Text = "";
            tbJul.Text = "";
            tbAug.Text = "";
            tbSep.Text = "";
            tbOct.Text = "";
            tbNov.Text = "";
            tbDec.Text = "";
        }
        private void LoadRow(int categoryID)
        {
            Category category;
            if (FinishedLoading)
            {
                if (dgvCategories.CurrentRow != null && dgvCategories.CurrentRow.Cells["CategoryName"].Value != null)
                {
                    switch (Frequency())
                    {
                        case "Anytime":
                            //category = Category.CategoryLookup(categoryName);
                            category = Category.FindCategory(categoryID);
                            LoadMonthlyValues(category);
                            break;
                        case "Monthly":
                            category = Category.FindCategory(categoryID);
                            LoadMonthlyValues(category);
                            break;
                    }
                }
            }
        }
        private void SaveRow(int dayOfMonth = 0)
        {
            if (dgvCategories.CurrentRow != null)
                if (dgvCategories.CurrentRow.Cells["CategoryName"].Value != null)
                {
                    Category category = new Category();
                    category.CategoryName = dgvCategories.CurrentRow.Cells["CategoryName"].Value.ToString();
                    if (dgvCategories.CurrentRow.Cells["CategoryID"].Value != null)
                        category.CategoryID = Convert.ToInt32(dgvCategories.CurrentRow.Cells["CategoryID"].Value.ToString());
                    bool cancelUpdate = false;
                    switch (Frequency())
                    {
                        case "Annually":
                        case "One Time":
                            if (dgvCategories.CurrentRow.Cells["StaticAmount"].Value == null |
                                    !IsNumber(dgvCategories.CurrentRow.Cells["StaticAmount"].Value.ToString().Replace("$", "")) |
                                    dgvCategories.CurrentRow.Cells["SpecificDate"].Value == null |
                                    !IsDate(dgvCategories.CurrentRow.Cells["SpecificDate"].Value.ToString()))
                            {
                                cancelUpdate = true;
                                MessageBox.Show("Please enter a valid date and transactionAmount", "Error");
                            }
                            else
                            {
                                category.StaticAmount = Convert.ToDecimal(dgvCategories.CurrentRow.Cells["StaticAmount"].Value.ToString().Replace("$", ""));
                                category.SpecificDate = Convert.ToDateTime(dgvCategories.CurrentRow.Cells["SpecificDate"].Value.ToString());
                                if (Frequency() == "Annually")
                                {
                                    if (Convert.ToDateTime(dgvCategories.CurrentRow.Cells["SpecificDate"].Value.ToString()).Month
                                            < DateTime.Now.Month &&
                                        category.SpecificDate.Year <= DateTime.Now.Year)
                                    {
                                        dgvCategories.CurrentRow.Cells["TransactionDate"].Value = category.SpecificDate.AddYears(1).ToShortDateString();
                                        category.SpecificDate = Convert.ToDateTime(dgvCategories.CurrentRow.Cells["SpecificDate"].Value.ToString());
                                    }
                                }
                            }
                            break;
                        case "Anytime":
                            category = AddMonthlyAmounts(category);
                            break;
                        case "Bi-Weekly":
                        case "Weekly":
                            if (dgvCategories.CurrentRow.Cells["StaticAmount"].Value != null &&
                                    IsNumber(dgvCategories.CurrentRow.Cells["StaticAmount"].Value.ToString().Replace("$", "")) &&
                                    dgvCategories.CurrentRow.Cells["NextOccurance"].Value != null &&
                                    IsDate(dgvCategories.CurrentRow.Cells["NextOccurance"].Value.ToString()))
                            {
                                category.StaticAmount = Convert.ToDecimal(dgvCategories.CurrentRow.Cells["StaticAmount"].Value.ToString().Replace("$", ""));
                                category.NextOccurance = Convert.ToDateTime(dgvCategories.CurrentRow.Cells["NextOccurance"].Value.ToString());
                                if (dgvCategories.CurrentRow.Cells["EndDate"].Value != null &&
                                        IsDate(dgvCategories.CurrentRow.Cells["EndDate"].Value.ToString()))
                                    category.EndDate = Convert.ToDateTime(dgvCategories.CurrentRow.Cells["EndDate"].Value.ToString());

                            }
                            else
                            {
                                cancelUpdate = true;
                                MessageBox.Show("Please enter a valid transaction amount & date of next occurance", "Error");
                            }
                            break;
                        case "Monthly":
                            category = AddMonthlyAmounts(category);
                            if (dayOfMonth == 0 && dgvCategories.CurrentRow.Cells["DayOfMonth"].Value == null)
                            {
                                cancelUpdate = true;
                                MessageBox.Show("Please enter a valid day of the month", "Error");
                            }
                            else
                                if (dayOfMonth == 0)
                                    category.DayOfMonth = Convert.ToInt32(dgvCategories.CurrentRow.Cells["DayOfMonth"].Value.ToString());
                                else
                                    category.DayOfMonth = dayOfMonth;

                            break;
                    }
                    if (!cancelUpdate)
                    {
                        if (dgvCategories.CurrentRow.Cells["ExcludeFromBudget"].Value != null)
                            category.ExcludeFromBudget = Convert.ToBoolean(dgvCategories.CurrentRow.Cells["ExcludeFromBudget"].Value.ToString());
                        category.Type = CategoryType();
                        category.Frequency = Frequency();
                        if (dgvCategories.CurrentRow.Cells["CategoryID"].Value == null)
                            dgvCategories.CurrentRow.Cells["CategoryID"].Value = Category.InsertCategory(category);
                        else
                            Category.UpdateCategory(category);
                    }
                }
        }
        private void LoadMonthlyValues(Category category)
        {
            tbJan.Text = category.C1.ToString();
            tbFeb.Text = category.C2.ToString();
            tbMar.Text = category.C3.ToString();
            tbApr.Text = category.C4.ToString();
            tbMay.Text = category.C5.ToString();
            tbJun.Text = category.C6.ToString();
            tbJul.Text = category.C7.ToString();
            tbAug.Text = category.C8.ToString();
            tbSep.Text = category.C9.ToString();
            tbOct.Text = category.C10.ToString();
            tbNov.Text = category.C11.ToString();
            tbDec.Text = category.C12.ToString();
            if (tbJan.Text == "0.0000")
                tbJan.Text = "0";
            if (tbFeb.Text == "0.0000")
                tbFeb.Text = "0";
            if (tbMar.Text == "0.0000")
                tbMar.Text = "0";
            if (tbApr.Text == "0.0000")
                tbApr.Text = "0";
            if (tbMay.Text == "0.0000")
                tbMay.Text = "0";
            if (tbJun.Text == "0.0000")
                tbJun.Text = "0";
            if (tbJul.Text == "0.0000")
                tbJul.Text = "0";
            if (tbAug.Text == "0.0000")
                tbAug.Text = "0";
            if (tbSep.Text == "0.0000")
                tbSep.Text = "0";
            if (tbOct.Text == "0.0000")
                tbOct.Text = "0";
            if (tbNov.Text == "0.0000")
                tbNov.Text = "0";
            if (tbDec.Text == "0.0000")
                tbDec.Text = "0";
        }
        private void ClearMonthlyValues()
        {
            tbJan.Text = "";
            tbFeb.Text = "";
            tbMar.Text = "";
            tbApr.Text = "";
            tbMay.Text = "";
            tbJun.Text = "";
            tbJul.Text = "";
            tbAug.Text = "";
            tbSep.Text = "";
            tbOct.Text = "";
            tbNov.Text = "";
            tbDec.Text = "";
        }
        private DataGridViewColumn ComboColumn(string name, string headerText, IEnumerable<string> categorynames)
        {
            DataGridViewComboBoxColumn comboColumn = new DataGridViewComboBoxColumn();
            comboColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
            foreach (string item in categorynames)
                comboColumn.Items.Add(item);
            comboColumn.Name = name;
            comboColumn.HeaderText = headerText;
            comboColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            return comboColumn;
        }
        private DataGridViewTextBoxColumn TextColumn(string name, string headerText, bool visable)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.Name = name;
            column.HeaderText = headerText;
            column.Visible = visable;
            //column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            return column;
        }
        private DataGridViewTextBoxColumn StaticAmountColumn()
        {
            DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            textColumn = TextColumn("StaticAmount", "Transaction Amount", true);
            style = new DataGridViewCellStyle();
            style.Format = "C2";
            textColumn.DefaultCellStyle = style;
            //textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            return textColumn;
        }
        private DataGridViewTextBoxCell textCell(string text)
        {
            DataGridViewTextBoxCell cell = new DataGridViewTextBoxCell();
            cell.Value = text;
            return cell;
        }
        private DataGridViewCheckBoxColumn CheckboxColumn(string columnName, string headerText)
        {
            DataGridViewCheckBoxColumn comboColumn = new DataGridViewCheckBoxColumn();
            comboColumn.Name = columnName;
            comboColumn.HeaderText = headerText;
            return comboColumn;
        }
        private Category AddMonthlyAmounts(Category category)
        {
            if (!string.IsNullOrEmpty(tbJan.Text) && IsNumber(tbJan.Text))
                category.C1 = Convert.ToDecimal(tbJan.Text);
            if (!string.IsNullOrEmpty(tbFeb.Text) && IsNumber(tbFeb.Text))
                category.C2 = Convert.ToDecimal(tbFeb.Text);
            if (!string.IsNullOrEmpty(tbMar.Text) && IsNumber(tbMar.Text))
                category.C3 = Convert.ToDecimal(tbMar.Text);
            if (!string.IsNullOrEmpty(tbApr.Text) && IsNumber(tbApr.Text))
                category.C4 = Convert.ToDecimal(tbApr.Text);
            if (!string.IsNullOrEmpty(tbMay.Text) && IsNumber(tbMay.Text))
                category.C5 = Convert.ToDecimal(tbMay.Text);
            if (!string.IsNullOrEmpty(tbJun.Text) && IsNumber(tbJun.Text))
                category.C6 = Convert.ToDecimal(tbJun.Text);
            if (!string.IsNullOrEmpty(tbJul.Text) && IsNumber(tbJul.Text))
                category.C7 = Convert.ToDecimal(tbJul.Text);
            if (!string.IsNullOrEmpty(tbAug.Text) && IsNumber(tbAug.Text))
                category.C8 = Convert.ToDecimal(tbAug.Text);
            if (!string.IsNullOrEmpty(tbSep.Text) && IsNumber(tbSep.Text))
                category.C9 = Convert.ToDecimal(tbSep.Text);
            if (!string.IsNullOrEmpty(tbOct.Text) && IsNumber(tbOct.Text))
                category.C10 = Convert.ToDecimal(tbOct.Text);
            if (!string.IsNullOrEmpty(tbNov.Text) && IsNumber(tbNov.Text))
                category.C11 = Convert.ToDecimal(tbNov.Text);
            if (!string.IsNullOrEmpty(tbDec.Text) && IsNumber(tbDec.Text))
                category.C12 = Convert.ToDecimal(tbDec.Text);
            return category;
        }
        private List<string> DaysOfMonth()
        {
            List<string> daysOfMonth = new List<string>();
            for (int i = 1; i < 32; i++)
                daysOfMonth.Add(Convert.ToString(i));
            return daysOfMonth;
        }
        private bool IsNumber(string text)
        {
            text = text.Trim();
            double number;
            bool isNumber = double.TryParse(text, out number);
            return isNumber;
        }
        private bool IsDate(string date)
        {
            try
            {
                DateTime dt;
                DateTime.TryParse(date, out dt);
                if (dt != DateTime.MinValue && dt != DateTime.MaxValue)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
        private void rbAnnually_CheckedChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CategoryType()) && !string.IsNullOrEmpty(Frequency()))
                LoadCategories();
        }
        private void rbAnytime_CheckedChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CategoryType()) && !string.IsNullOrEmpty(Frequency()))
                LoadCategories();
        }
        private void rbBiWeekly_CheckedChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CategoryType()) && !string.IsNullOrEmpty(Frequency()))
                LoadCategories();
        }
        private void rbMonthly_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMonthly.Checked)
                chkByDate.Visible = true;
            else
                chkByDate.Visible = false;
            if (!string.IsNullOrEmpty(CategoryType()) && !string.IsNullOrEmpty(Frequency()))
                LoadCategories();
        }
        private void rbOneTime_CheckedChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CategoryType()) && !string.IsNullOrEmpty(Frequency()))
                LoadCategories();
        }
        private void rbWeekly_CheckedChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CategoryType()) && !string.IsNullOrEmpty(Frequency()))
                LoadCategories();
        }
        private void rbIncome_CheckedChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CategoryType()) && !string.IsNullOrEmpty(Frequency()))
                LoadCategories();
        }

        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CategoryType()) && !string.IsNullOrEmpty(Frequency()))
                LoadCategories();
        }

        private void rbSpending_CheckedChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CategoryType()) && !string.IsNullOrEmpty(Frequency()))
                LoadCategories();
        }

    }
}
