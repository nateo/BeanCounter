using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace BeanCounter.BusinessLogic
{
    public class BankAccount
    {

        public string AccountNumber, BankName, BankFID, AccountType;
        public decimal OnlineBalance, CreditLimit;
        public string AccountName, WebAddress, RemoveFromMerchant, RemoveFromBankMemo;
        public Boolean ReverseFields;
        public int BankAccountID;

        public BankAccount() { }
        public BankAccount(string accountNumber, string bankName, string bankFID, string accountType, decimal onlineBalance)
        {
            AccountNumber = accountNumber;
            BankName = bankName;
            BankFID = bankFID;
            AccountType = accountType;
            OnlineBalance = onlineBalance;
        }
        public static int InsertAccountSettings(BankAccount bankAccount)
        {
            int bankAccountID = 0;
            string cmdText = "INSERT INTO tblBankAccount  " +
                "(AccountName, WebAddress, AccountNumber, BankName, BankFID, AccountType, " +
                "RemoveFromMerchant, RemoveFromBankMemo, ReverseFields, OnlineBalance, ExcludeFromBalances, Inactive)";
            if (bankAccount.CreditLimit != 0)
                cmdText += ", CreditLimit";
            cmdText += "VALUES (" +
                "'" + bankAccount.AccountName + "'" +
                ", '" + bankAccount.WebAddress + "'" +
                ", '" + bankAccount.AccountNumber + "'" +
                ", '" + bankAccount.BankName + "'" +
                ", '" + bankAccount.BankFID + "'" +
                ", '" + bankAccount.AccountType + "'" +
                ", '" + bankAccount.RemoveFromMerchant + "'" +
                ", '" + bankAccount.RemoveFromBankMemo + "'" +
                ", " + bankAccount.ReverseFields +
                ", " + bankAccount.OnlineBalance + "0, 0";
            if (bankAccount.CreditLimit != 0)
                cmdText += ", " + bankAccount.CreditLimit;
            cmdText += ")";

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString();
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(connectionString))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                    myCommand.ExecuteNonQuery();
                string identity = "SELECT @@Identity";
                using (OleDbCommand myCommand = new OleDbCommand(identity, myConnection))
                    bankAccountID = Convert.ToInt32(myCommand.ExecuteScalar().ToString());
            }
            return bankAccountID;
        }
        public static IEnumerable<BankAccount> AccountSummary(string accountType)
        {
            string cmdText = "SELECT AccountName, OnlineBalance, BankAccountId, WebAddress FROM tblBankAccount WHERE ";
            if (accountType.ToUpper() == "CREDIT")
                cmdText += "(AccountType = 'CREDIT' or AccountType = 'CREDITLINE')";
            else if (accountType.ToUpper() == "CASH")
                cmdText += "(AccountType = 'CHECKING' or AccountType = 'SAVINGS') ";
            cmdText += "  and (inactive <> 1)";
            cmdText += "ORDER BY BankAccountId";
            List<BankAccount> bankAccounts = new List<BankAccount>();
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString();
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(connectionString))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                using (OleDbDataReader myDataReader = myCommand.ExecuteReader())
                    while (myDataReader.Read())
                    {
                        BankAccount bankAccount = new BankAccount();
                        bankAccount.AccountName = myDataReader["AccountName"].ToString();
                        bankAccount.OnlineBalance = Convert.ToDecimal(myDataReader["OnlineBalance"].ToString());
                        bankAccount.BankAccountID = Convert.ToInt32(myDataReader["BankAccountId"].ToString());
                        bankAccount.WebAddress = myDataReader["WebAddress"].ToString();
                        bankAccounts.Add(bankAccount);
                    }
                return bankAccounts;
            }
        }
        public static BankAccount FindAccountSettings(ofxFile ofxfile)
        {
            BankAccount bankAccount = ofxfile.BankAccount;
            string cmdText = "SELECT BankAccountId, AccountName, WebAddress, RemoveFromMerchant, " +
            "RemoveFromBankMemo, ReverseFields from tblBankAccount WHERE BankFID = '"
                + ofxfile.BankAccount.BankFID + "'";
            if (!string.IsNullOrEmpty(ofxfile.BankAccount.AccountNumber))
                cmdText += " AND AccountNumber = '" + ofxfile.BankAccount.AccountNumber + "'";
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString();
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(connectionString))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                using (OleDbDataReader myDataReader = myCommand.ExecuteReader())
                    if (myDataReader.Read())
                    {
                        bankAccount.BankAccountID = Convert.ToInt32(myDataReader["BankAccountId"].ToString());
                        bankAccount.AccountName = myDataReader["AccountName"].ToString();
                        bankAccount.WebAddress = myDataReader["WebAddress"].ToString();
                        bankAccount.RemoveFromMerchant = myDataReader["RemoveFromMerchant"].ToString();
                        bankAccount.RemoveFromBankMemo = myDataReader["RemoveFromBankMemo"].ToString();
                        bankAccount.ReverseFields = Convert.ToBoolean(myDataReader["ReverseFields"].ToString());
                    }
            }

            return bankAccount;
        }
        public static decimal AccountBalance(string accountType)
        {
            decimal cashTotal = 0;
            string cmdText = "SELECT SUM(OnlineBalance) as cBalance from tblBankAccount " +
                 "WHERE ";
            if (accountType.ToUpper() == "CREDIT")
                cmdText += "(AccountType = 'CREDIT' or AccountType = 'CREDITLINE')";
            else if (accountType.ToUpper() == "CASH")
                cmdText += "(AccountType = 'CHECKING' or AccountType = 'SAVINGS')";
            cmdText += " and (ExcludeFromBalances <> 1) and (InActive <> 1)";
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString();
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(connectionString))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                using (OleDbDataReader myDataReader = myCommand.ExecuteReader())
                    if (myDataReader.Read())
                    {
                        if (!string.IsNullOrEmpty(myDataReader["cBalance"].ToString()))
                            cashTotal = Convert.ToDecimal(myDataReader["cBalance"].ToString());
                    }
            }
            return cashTotal;
        }


        public static string DownloadUri(int bankAccountID)
        {
            string downloadUri = "";
            string cmdText = "select WebAddress from tblBankAccount where BankAccountId = " +
                Convert.ToString(bankAccountID);
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString();
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(connectionString))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                using (OleDbDataReader myDataReader = myCommand.ExecuteReader())
                    if (myDataReader.Read())
                        downloadUri = myDataReader["WebAddress"].ToString();
            }
            return downloadUri;
        }
    }
}
