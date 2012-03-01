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
    public partial class FrmMain : Form
    {

        public string[] Args;

        public FrmMain()
        {
            InitializeComponent();
        }

        delegate void MethodCallback(string[] args);

        public delegate void ProcessParametersDelegate(object sender, string[] args);

        public void ProcessParameters(object sender, string[] args)
        {
            if (args != null && args.Length != 0)
            {
                LoadOFXfile(args);
            }

        }

        private void LoadOFXfile(string[] args)
        {
            tslMain.Text = "Loading OFX file...";
            bwLoadOFXfile.RunWorkerAsync(args[0]);
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (this.Args != null)
            {
                ProcessParameters(null, this.Args);
                this.Args = null;
            }
            AddColumns();
            LoadGrids();

        }

        private void LoadBalances()
        {

            decimal cashTotal = BankAccount.AccountBalance("CASH");
            decimal creditTotal = BankAccount.AccountBalance("CREDIT");
            decimal networth = 0;
            if (cashTotal != 0)
                networth = cashTotal;
            if (creditTotal != 0)
                networth += creditTotal;
            dgvBalances.Rows.Clear();
            dgvBalances.Rows.Add("Total Cash: ", cashTotal.ToString("c"));
            dgvBalances.Rows.Add("Total Debt: ", creditTotal.ToString("c"));
            dgvBalances.Rows.Add("Networth: ", networth.ToString("c"));
            foreach (DataGridViewRow row in dgvBalances.Rows)
            {
                if (double.Parse(row.Cells[1].Value.ToString(), System.Globalization.NumberStyles.Currency) < 0)
                    row.DefaultCellStyle.ForeColor = Color.Red;
            }
            dgvBalances.Rows[2].DefaultCellStyle.Font = new Font(dgvBalances.DefaultCellStyle.Font.FontFamily, dgvBalances.DefaultCellStyle.Font.Size, FontStyle.Bold);
            foreach (DataGridViewRow selectedRow in dgvBalances.SelectedRows)
                selectedRow.Selected = false;
        }

        private void LoadGrids()
        {
            if (DatabaseProperties.PasswordProtected())
            {

            }
            LoadCashAccounts();
            LoadCreditAccounts();
            LoadBalances();
            foreach (DataGridViewRow row in dgvCashAccounts.SelectedRows)
                row.Selected = false;
            foreach (DataGridViewRow row in dgvCreditAccounts.SelectedRows)
                row.Selected = false;
            foreach (DataGridViewRow row in dgvBalances.SelectedRows)
                row.Selected = false;
        }

        private void LoadCreditAccounts()
        {
            int index = 0;
            foreach (BankAccount bankAccount in BankAccount.AccountSummary("CREDIT"))
            {
                dgvCreditAccounts.Rows.Add(
                   bankAccount.AccountName,
                   bankAccount.OnlineBalance.ToString("c"),
                   Convert.ToString(bankAccount.BankAccountID),
                   bankAccount.WebAddress);
                if (bankAccount.OnlineBalance < 0)
                {
                    DataGridViewRow row = dgvCreditAccounts.Rows[index];
                    row.DefaultCellStyle.ForeColor = Color.Red;
                }
                index++;
            }
        }

        private void LoadCashAccounts()
        {
            foreach (BankAccount bankAccount in BankAccount.AccountSummary("CASH"))
            {
                int index = 0;
                dgvCashAccounts.Rows.Add(bankAccount.AccountName
                    , bankAccount.OnlineBalance.ToString("c")
                    , Convert.ToString(bankAccount.BankAccountID)
                    , bankAccount.WebAddress);
                if (bankAccount.OnlineBalance < 0)
                {
                    DataGridViewRow row = dgvCashAccounts.Rows[index];
                    row.DefaultCellStyle.ForeColor = Color.Red;
                }
                index++;
            }
        }

        private void AddColumns()
        {
            dgvCashAccounts.Rows.Clear();
            dgvCreditAccounts.Rows.Clear();
            dgvCashAccounts.Columns.Clear();
            dgvCreditAccounts.Columns.Clear();
            dgvBalances.Columns.Clear();
            dgvCashAccounts.Columns.Add(Column("AccountName", true, "Account"));
            dgvCreditAccounts.Columns.Add(Column("AccountName", true, "Account"));
            dgvCashAccounts.Columns.Add(Column("OnlineBalance", true, "Balance"));
            dgvCreditAccounts.Columns.Add(Column("OnlineBalance", true, "Balance"));
            dgvCashAccounts.Columns.Add(Column("BankAccountID", false, ""));
            dgvCreditAccounts.Columns.Add(Column("BankAccountID", false, ""));
            dgvCashAccounts.Columns.Add(Column("WebAddress", false, ""));
            dgvCreditAccounts.Columns.Add(Column("WebAddress", false, ""));
            dgvBalances.Columns.Add(Column("label", true, ""));
            dgvBalances.Columns.Add(Column("StaticAmount", true, ""));
        }

        private DataGridViewTextBoxColumn Column(string name, bool visable, string headerText)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.Name = name;
            column.HeaderText = headerText;
            column.Visible = visable;
            return column;
        }

        private void bwLoadOFXfile_DoWork(object sender, DoWorkEventArgs e)
        {
            ofxFile ofxfile = ofxFile.LoadOFXfile(e.Argument as string);
            BankAccount bankAccount = BankAccount.FindAccountSettings(ofxfile);
            e.Result = new Basket(ofxfile, bankAccount);
        }

        private void bwLoadOFXfile_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bool cancelOperation = false;
            Basket basket = e.Result as Basket;
            if (basket.BankAccount.BankAccountID == 0)
            {
                FrmNewBankAccount fNewBankAccount = new FrmNewBankAccount(basket);
                fNewBankAccount.WindowState = FormWindowState.Maximized;
                switch (fNewBankAccount.ShowDialog())
                {
                    case DialogResult.OK:
                        basket.BankAccount.AccountName = fNewBankAccount.tbNickname.Text;
                        basket.BankAccount.WebAddress = fNewBankAccount.tbWebAddress.Text;
                        if (basket.BankAccount.WebAddress.Contains("http://") == false)
                            basket.BankAccount.WebAddress = "http://" + basket.BankAccount.WebAddress;
                        if (fNewBankAccount.rbColumnA.Checked)
                            basket.BankAccount.ReverseFields = false;
                        else if (fNewBankAccount.rbColumnB.Checked)
                            basket.BankAccount.ReverseFields = true;
                        if (!string.IsNullOrEmpty(fNewBankAccount.cbRemoveFromColumnA.Text))
                            if (basket.BankAccount.ReverseFields == true)
                                basket.BankAccount.RemoveFromMerchant = fNewBankAccount.cbRemoveFromColumnB.Text;
                            else
                                basket.BankAccount.RemoveFromMerchant = fNewBankAccount.cbRemoveFromColumnA.Text;
                        if (!string.IsNullOrEmpty(fNewBankAccount.cbRemoveFromColumnB.Text))
                            if (basket.BankAccount.ReverseFields == true)
                                basket.BankAccount.RemoveFromBankMemo = fNewBankAccount.cbRemoveFromColumnA.Text;
                            else
                                basket.BankAccount.RemoveFromBankMemo = fNewBankAccount.cbRemoveFromColumnB.Text;
                        basket.BankAccount.BankAccountID = BankAccount.InsertAccountSettings(basket.BankAccount);
                        break;
                    case DialogResult.Cancel:
                        cancelOperation = true;
                        break;
                    default:
                        cancelOperation = true;
                        break;
                }
            }
            if (!cancelOperation)
            {
                tslMain.Text = "Importing Transctions...";
                tspbMain.Value = 0;
                tspbMain.Visible = true;
                bwImportTransactions.RunWorkerAsync(basket);
            }


        }

        private void bwImportTransactions_DoWork(object sender, DoWorkEventArgs e)
        {
            Basket basket = e.Argument as Basket;
            ofxFileImporter.UpdateBalance(basket);
            decimal numberOfTransactions = basket.ofxFile.Transactions.Count;
            decimal currentPosition = 0;
            List<Merchant> nationalMerchants = Merchant.NationalMerchants();
            foreach (Transaction Transaction in basket.ofxFile.Transactions)
            {
                if (ofxFileImporter.TransactionDoesNotExist(Transaction, basket.BankAccount.BankAccountID))
                {
                    Transaction transaction = Transaction.CheckBankTricks(Transaction, basket.BankAccount);
                    string categoryName = "";
                    if (transaction.TransactionType == "ATM" && transaction.BankMemo != "STAMP PURCHASE")
                        categoryName = "Miscellaneous: Cash";
                    if (categoryName == "")
                        categoryName = ofxFileImporter.FindCategory(transaction, nationalMerchants);
                    int orginalTransactionID = ofxFileImporter.InsertIntoOringalTransaction(basket.BankAccount,
                        transaction, categoryName);
                    ofxFileImporter.InsertIntoSplitTransction(categoryName, orginalTransactionID, transaction);
                }
                currentPosition++;
                decimal percent = currentPosition / numberOfTransactions;
                decimal percentnew = percent * 100;
                bwImportTransactions.ReportProgress((int)percentnew);
            }
        }

        private void bwImportTransactions_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            tslMain.Text = "Finished importing transactions!";
            tspbMain.Value = 0;
            tspbMain.Visible = false;
            RefreshMainForm();
        }

        private void dgvCashAccounts_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvCreditAccounts.SelectedRows)
                row.Selected = false;
            if (dgvCashAccounts.CurrentRow.Cells["BankAccountID"].Value != null
                    && IsNumber(dgvCashAccounts.CurrentRow.Cells["BankAccountID"].Value.ToString()))
            {
                int bankAccountID = Convert.ToInt32(dgvCashAccounts.CurrentRow.Cells["BankAccountID"].Value.ToString());
                string accountName = "";
                if (dgvCashAccounts.CurrentRow.Cells["AccountName"].Value != null)
                    accountName = dgvCashAccounts.CurrentRow.Cells["AccountName"].Value.ToString();
                LoadBankAccount(bankAccountID, accountName);
            }
        }

        private static bool IsNumber(string text)
        {
            text = text.Trim();
            double number;
            bool isNumber = double.TryParse(text, out number);
            return isNumber;
        }

        private void LoadBankAccount(int bankAccountID, string accountName)
        {
            tslMain.Text = "";
            FrmRegister frmRegister = new FrmRegister(bankAccountID, accountName);
            if (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["YearToDateOnly"].ToString()))
            {
                frmRegister.tsmiYearToDateOnly.Checked = true;
            }
            else
            {
                frmRegister.tsmiYearToDateOnly.Checked = false;
            }
            if (Transaction.Transactions(bankAccountID, true, "").Count() < 16)
            {
                frmRegister.tsmiByDate.Checked = true;
                frmRegister.tsmiByCategoryMerchantName.Checked = false;
            }
            else
            {
                frmRegister.tsmiByDate.Checked = false;
                frmRegister.tsmiByCategoryMerchantName.Checked = true;
            }
            frmRegister.WindowState = FormWindowState.Maximized;
            frmRegister.MdiParent = this;
            frmRegister.Show();
            tsbDownload.Enabled = true;
        }

        private void tsbCategories_Click(object sender, EventArgs e)
        {
            tslMain.Text = "";
            FrmCategories frmCategories = new FrmCategories();
            frmCategories.WindowState = FormWindowState.Maximized;
            frmCategories.MdiParent = this;
            frmCategories.Show();

        }

        private void tsbReports_Click(object sender, EventArgs e)
        {
            tslMain.Text = "";
            //FrmReports frmRegister = new FrmReports(
            //    Convert.ToInt32(dgvCashAccounts.CurrentRow.Cells["BankAccountID"].Value.ToString()),
            //    dgvCashAccounts.CurrentRow.Cells["AccountName"].Value.ToString());
            FrmReports frmRegister = new FrmReports();
            frmRegister.WindowState = FormWindowState.Maximized;
            frmRegister.MdiParent = this;
            frmRegister.Show();
            tsbDownload.Enabled = true;
        }

        private void tsblMerchants_Click(object sender, EventArgs e)
        {
            tslMain.Text = "";
            FrmMerchants frmMerchants = new FrmMerchants();
            frmMerchants.WindowState = FormWindowState.Maximized;
            frmMerchants.MdiParent = this;
            frmMerchants.Show();

        }

        private void bwImportTransactions_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            tspbMain.Value = e.ProgressPercentage;
        }

        private void tsbDownload_Click(object sender, EventArgs e)
        {
            tslMain.Text = "";
            FrmWebDownload frmWebDownload = new FrmWebDownload();
            string uriText = "";
            if (dgvCashAccounts.SelectedRows.Count > 0)
                uriText = BankAccount.DownloadUri(Convert.ToInt32(
                    dgvCashAccounts.CurrentRow.Cells["BankAccountID"].Value.ToString()));
            if (dgvCreditAccounts.SelectedRows.Count > 0)
                uriText = BankAccount.DownloadUri(Convert.ToInt32(
                    dgvCreditAccounts.CurrentRow.Cells["BankAccountID"].Value.ToString()));
            Uri uri = new Uri(uriText);
            frmWebDownload.wbWebDownload.Url = uri;
            frmWebDownload.WindowState = FormWindowState.Maximized;
            frmWebDownload.MdiParent = this;
            frmWebDownload.Show();
        }

        private void dgvCreditAccounts_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvCashAccounts.SelectedRows)
                row.Selected = false;
            tslMain.Text = "";
            if (dgvCreditAccounts.CurrentRow.Cells["BankAccountID"].Value != null
                && IsNumber(dgvCreditAccounts.CurrentRow.Cells["BankAccountID"].Value.ToString()))
            {
                int bankAccountID = Convert.ToInt32(dgvCreditAccounts.CurrentRow.Cells["BankAccountID"].Value.ToString());
                string accountName = "";
                if (dgvCreditAccounts.CurrentRow.Cells["AccountName"].Value != null)
                    accountName = dgvCreditAccounts.CurrentRow.Cells["AccountName"].Value.ToString();
                LoadBankAccount(bankAccountID, accountName);
            }
        }


        private void RefreshMainForm()
        {
            Cursor = Cursors.WaitCursor;
            dgvBalances.Rows.Clear();
            dgvCashAccounts.Rows.Clear();
            dgvCreditAccounts.Rows.Clear();
            LoadGrids();
            Cursor = Cursors.Default;
        }

        private void tsmiNewAccount_Click(object sender, EventArgs e)
        {
            NewAccount();
        }

        private void NewAccount()
        {
            tslMain.Text = "";
            Uri uri;
            frmWebAddress _frmWebAddress = new frmWebAddress();
            if (_frmWebAddress.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    uri = new Uri(_frmWebAddress.tbWebsiteAddress.Text);
                    FrmWebDownload frmWebDownload = new FrmWebDownload();
                    frmWebDownload.wbWebDownload.Url = uri;
                    frmWebDownload.WindowState = FormWindowState.Maximized;
                    frmWebDownload.MdiParent = this;
                    frmWebDownload.Show();
                }
                catch
                {
                    MessageBox.Show("invalid url format");
                }
            }
        }

        private void donateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore.exe", "www.personalsoftwaresolutions.com/donate.aspx");
        }

        private void tsbNewAccount_Click(object sender, EventArgs e)
        {
            NewAccount();
        }

        private void tsmiSetPassword_Click(object sender, EventArgs e)
        {
            FrmSetPassword frmSetPassword = new FrmSetPassword();
            if (frmSetPassword.ShowDialog() == DialogResult.OK)
            {
                DatabaseProperties.SetPassword(frmSetPassword.tbCurrentPassword.Text, frmSetPassword.tbNewPassword.Text, frmSetPassword.tbConfirmPassword.Text);
            }
        }

    }
}
