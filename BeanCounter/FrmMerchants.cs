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
    public partial class FrmMerchants : Form
    {
        public FrmMerchants()
        {
            InitializeComponent();
        }

        private void Merchants_Load(object sender, EventArgs e)
        {
            dgvMerchants.Columns.Add(TextColumn("MerchantID", "MerchantID", false));
            dgvMerchants.Columns.Add(TextColumn("MerchantName", "Merchant name", true));
            dgvMerchants.Columns.Add(ComboColumn("CategoryName", "Category Name", Category.CategoryNames()));
            dgvMerchants.Columns.Add(CheckboxColumn("AutoCategorize", "Auto Categorize"));
            LocalMerchants();

        }
        private DataGridViewColumn ComboColumn(string name, string headerText, IEnumerable<string> categoryNames)
        {
            DataGridViewComboBoxColumn comboColumn = new DataGridViewComboBoxColumn();
            comboColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
            comboColumn.Items.Add("");
            foreach (string item in categoryNames)
                comboColumn.Items.Add(item);
            comboColumn.Name = name;
            comboColumn.HeaderText = headerText;
            comboColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            return comboColumn;
        }
        private static DataGridViewTextBoxColumn TextColumn(string name, string headerText, bool visable)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.Name = name;
            column.HeaderText = headerText;
            column.Visible = visable;
            return column;
        }
        private DataGridViewCheckBoxColumn CheckboxColumn(string columnName, string headerText)
        {
            DataGridViewCheckBoxColumn comboColumn = new DataGridViewCheckBoxColumn();
            comboColumn.Name = columnName;
            comboColumn.HeaderText = headerText;
            return comboColumn;
        }
        private void dgvMerchants_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (dgvMerchants.Columns[e.ColumnIndex].Name)
            {
                case "AutoCategorize":
                case "LocalMerchant":
                    this.Validate();
                    SaveRow();
                    break;
            }
        }
        private void SaveRow()
        {
            if (dgvMerchants.CurrentRow != null)
                if (dgvMerchants.CurrentRow.Cells["MerchantName"].Value != null)
                    SaveRowData(dgvMerchants.CurrentRow.Cells["CategoryName"].Value.ToString());
                else
                    MessageBox.Show("Error", "You must enter the merchant name & select the category");
        }
        private void SaveRowData(string categoryName)
        {
            bool localMerchant = false;
            switch (cbMerchantType.Text)
            {
                case "Local Merchants *":
                    localMerchant = true;
                    break;
                case "National Merchants *":
                    localMerchant = false;
                    break;
            }
            bool autoCategorize = false;
            if (dgvMerchants.CurrentRow.Cells["AutoCategorize"].Value != null)
                autoCategorize = Convert.ToBoolean(dgvMerchants.CurrentRow.Cells["AutoCategorize"].Value.ToString());
            if (dgvMerchants.CurrentRow.Cells["MerchantID"].Value != null)
                Merchant.UpdateMerchant(
                    Convert.ToInt32(dgvMerchants.CurrentRow.Cells["MerchantID"].Value.ToString()),
                    dgvMerchants.CurrentRow.Cells["MerchantName"].Value.ToString(),
                    categoryName, autoCategorize, localMerchant);
            else
                dgvMerchants.CurrentRow.Cells["MerchantID"].Value =
                    Merchant.InsertMerchant(dgvMerchants.CurrentRow.Cells["MerchantName"].Value.ToString(),
                        categoryName, autoCategorize, localMerchant);
        }
        private void dgvMerchants_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.Validate();
            SaveRow();
        }
        private void cbMerchantType_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvMerchants.Rows.Clear();
            switch (cbMerchantType.Text)
            {
                case "Local Merchants *":
                    lblMerchantType.Text = "* Needs to be a exact match in order to automatically categorize";
                    LocalMerchants();
                    break;
                case "National Merchants *":
                    lblMerchantType.Text = "* Uses keywords to automatically categoize";
                    NationalMerchants();
                    break;
            }

        }
        private void LocalMerchants()
        {
            foreach (Merchant merchant in Merchant.Merchants(cbMerchantType.Text))
                dgvMerchants.Rows.Add(
                    merchant.MerchantID,
                    merchant.MerchantName,
                    merchant.CategoryName,
                    merchant.AutoCategorize);
        }
        private void NationalMerchants()
        {
            foreach (Merchant merchant in Merchant.Merchants(cbMerchantType.Text))
                dgvMerchants.Rows.Add(
                    merchant.MerchantID,
                    merchant.MerchantName,
                    merchant.CategoryName,
                    merchant.AutoCategorize);
        }
        private void dgvMerchants_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            dgvMerchants.Rows[e.RowIndex].Selected = true;
        }
        private void dgvMerchants_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.Button == MouseButtons.Right)
            {
                if (dgvMerchants.CurrentRow.Cells["MerchantID"].Value != null)
                {
                    this.Validate();
                    dgvMerchants.EndEdit();
                    dgvMerchants.CurrentRow.Selected = false;
                    dgvMerchants.CurrentCell = dgvMerchants[e.ColumnIndex, e.RowIndex];
                    dgvMerchants.Rows[e.RowIndex].Selected = true;
                    Rectangle r = dgvMerchants.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    cmsDelete.Show((Control)sender, r.Left + e.X, r.Top + e.Y);
                }
            }
        }
        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            int test = dgvMerchants.SelectedRows.Count;
            if (dgvMerchants.CurrentRow.Cells["MerchantID"].Value != null)
                if (MessageBox.Show("Are you sure you want to delete this merchant?", "Confirmation", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    Merchant.DeleteMerchant(Convert.ToInt32(dgvMerchants.CurrentRow.Cells["MerchantID"].Value.ToString()));
                    dgvMerchants.Rows.Remove(dgvMerchants.CurrentRow);
                }
        }
        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dgvMerchants.CurrentRow.Cells["MerchantName"].Value != null)
            {
                string categoryName = ((ComboBox)sender).Text;
                SaveRowData(categoryName);
            }

        }
        private void dgvMerchants_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ComboBox combo = e.Control as ComboBox;
            if (combo != null)
            {
                combo.SelectedIndexChanged -= new EventHandler(cbCategory_SelectedIndexChanged);
                combo.SelectedIndexChanged += new EventHandler(cbCategory_SelectedIndexChanged);
            }
        }
    }
}
