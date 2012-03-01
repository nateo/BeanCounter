using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Configuration;


namespace BeanCounter.BusinessLogic
{
    public class ofxFileImporter
    {
        public static void UpdateBalance(Basket basket)
        {
            string cmdText = "UPDATE tblBankAccount SET OnlineBalance = "
                + Convert.ToString(basket.ofxFile.BankAccount.OnlineBalance) +
                " WHERE (BankFID = '" + Convert.ToString(basket.ofxFile.BankAccount.BankFID) + "')";
            if (!string.IsNullOrEmpty(basket.ofxFile.BankAccount.AccountNumber))
                cmdText += " AND (AccountNumber = '" +
                Convert.ToString(basket.ofxFile.BankAccount.AccountNumber) + "')";
            using (OleDbConnection myConnection = new OleDbConnection(
                    ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString()))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                    myCommand.ExecuteNonQuery();
            }

        }
        public static bool TransactionDoesNotExist(Transaction transaction, int bankAccountID)
        {
            bool transactionDoesNotExist = false;
            string cmdText = "select * from tblOrginalTransaction where TransactionID = '" +
                transaction.TransactionID + "' and BankAccountId = " + Convert.ToString(bankAccountID) +
                " and TransactionAmount = " + Convert.ToString(transaction.TransactionAmount);
            using (OleDbConnection myConnection = new OleDbConnection(
                ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString()))
            {
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                {
                    myConnection.Open();
                    using (OleDbDataReader myDataReader = myCommand.ExecuteReader())
                    {
                        if (!myDataReader.Read())
                            transactionDoesNotExist = true;
                    }
                }
            }
            return transactionDoesNotExist;
        }
        public static int InsertIntoOringalTransaction(BankAccount bankAccount, Transaction transaction, string categoryName)
        {
            string cmdText = "INSERT INTO tblOrginalTransaction(";
            cmdText += "Verified, TransactionID, TransactionDate, TransactionAmount, Merchant, BankMemo, BankAccountId, TransactionType";
            if (!string.IsNullOrEmpty(transaction.CheckNumber))
                cmdText += ", CheckNumber";
            if (categoryName != "")
                cmdText += ", CategoryName";
            cmdText += ") Values(";
            cmdText += "false";
            cmdText += ", '" + transaction.TransactionID + "'";
            cmdText += ", #" + Convert.ToString(transaction.TransactionDate) + "#";
            cmdText += ", " + Convert.ToString(transaction.TransactionAmount);
            cmdText += ", '" + transaction.MerchantName.Replace(@"'", "''") + "'";
            cmdText += ", '" + transaction.BankMemo.Replace(@"'", "''") + "'";
            cmdText += ", " + Convert.ToString(bankAccount.BankAccountID);
            cmdText += ", '" + transaction.TransactionType + "'";
            if (!string.IsNullOrEmpty(transaction.CheckNumber))
                cmdText += ", '" + transaction.CheckNumber + "'";
            if (categoryName != "")
                cmdText += ", '" + categoryName + "'";
            cmdText += ")";
            string identity = " SELECT @@Identity";
            int orginalTransactionID;
            using (OleDbConnection myConnection = new OleDbConnection(
            ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString()))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                    myCommand.ExecuteNonQuery();
                using (OleDbCommand myCommand = new OleDbCommand(identity, myConnection))
                    orginalTransactionID = Convert.ToInt32(myCommand.ExecuteScalar().ToString());
            }
            return orginalTransactionID;
        }
        public static void InsertIntoSplitTransction(string categoryName, int transactionID, Transaction transaction)
        {
            string cmdText = "INSERT INTO tblSplitTransaction(";
            cmdText += "OrginalTransactionID, TransactionAmount";
            if (!string.IsNullOrEmpty(categoryName))
                cmdText += ", CategoryName";
            cmdText += ") Values(" + transactionID;

            cmdText += ", " + Convert.ToString(transaction.TransactionAmount);
            if (!string.IsNullOrEmpty(categoryName))
                cmdText += ", '" + categoryName + "'";
            cmdText += ")";
            using (OleDbConnection myConnection = new OleDbConnection(
            ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString()))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                    myCommand.ExecuteNonQuery().ToString();
            }
        }
        public static string FindCategory(Transaction transaction, List<Merchant> merchants)
        {
            string categoryName = "";
            if (transaction.MerchantName != "?")
            {
                categoryName = Merchant.NationalMerchant(transaction.MerchantName,merchants);
                if (categoryName == "")
                    categoryName = Merchant.LocalMerchant(transaction.MerchantName, true);
                if (categoryName != "")
                    categoryName = CheckSpecials(categoryName, transaction.TransactionAmount);
            }
            return categoryName;
        }
        private static string CheckSpecials(string categoryName, decimal transactionAmount)
        {
            if (!string.IsNullOrEmpty(categoryName))
            {
                if (categoryName.ToLower().StartsWith("auto: fuel") &&
                    transactionAmount > -10)
                    categoryName = "";
            }
            return categoryName;
        }
    }
}
