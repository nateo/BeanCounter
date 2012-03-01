using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.OleDb;

namespace BeanCounter.BusinessLogic
{
    public class Transaction
    {

        public string TransactionType;
        public string TransactionID;
        public string MerchantName;
        public string BankMemo;
        public string CheckNumber;
        public string CategoryName;
        public string UserMemo;
        public DateTime TransactionDate;
        public decimal TransactionAmount;
        public int OrginalTransactionID;
        public bool Verified;

        public Transaction() { }

        public Transaction(
            bool verified,
            DateTime transactionDate,
            string merchantName,
            decimal transactionAmount,
            string categoryName,
            string bankMemo,
            int orginalTransactionID,
            string userMemo,
            string checkNumber,
            string transactionType)
        {
            Verified = verified;
            TransactionDate = transactionDate;
            MerchantName = merchantName;
            TransactionAmount = transactionAmount;
            CategoryName = categoryName;
            BankMemo = bankMemo;
            OrginalTransactionID = orginalTransactionID;
            UserMemo = userMemo;
            CheckNumber = checkNumber;
            TransactionType = transactionType;
        }

        public static IEnumerable<Transaction> Transactions(int BankAccountID, bool showOnlyUnverfied,
            string orderBy)
        {
            string cmdText =
                "SELECT Verified, TransactionDate, Merchant" +
                ", TransactionAmount, CategoryName, BankMemo" +
                ", CheckNumber, TransactionType" +
                ", OrginalTransactionID, UserMemo" +
                " FROM tblOrginalTransaction" +
                " WHERE (BankAccountId = " + Convert.ToString(BankAccountID) + ")";
            if (showOnlyUnverfied)
                cmdText += " AND (Verified = No)";
            cmdText += orderBy;
            List<Transaction> transactions = new List<Transaction>();
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(
                System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString()))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                {
                    using (OleDbDataReader myDataReader = myCommand.ExecuteReader())
                    {
                        while (myDataReader.Read())
                        {

                            Transaction transaction = new Transaction(
                            Convert.ToBoolean(myDataReader["Verified"].ToString()),
                            Convert.ToDateTime(myDataReader["TransactionDate"].ToString()),
                            myDataReader["Merchant"].ToString(),
                            Convert.ToDecimal(myDataReader["TransactionAmount"].ToString()),
                            myDataReader["CategoryName"].ToString(),
                            myDataReader["BankMemo"].ToString(),
                            Convert.ToInt32(myDataReader["OrginalTransactionID"].ToString()),
                            myDataReader["UserMemo"].ToString(),
                            myDataReader["CheckNumber"].ToString(),
                            myDataReader["TransactionType"].ToString()
                            );
                            transactions.Add(transaction);
                        }
                    }
                }
                return transactions;
            }

        }
        public static void UpdateTransactionCategory(string categoryName, int orginalTransactionID)
        {
            if (string.IsNullOrEmpty(categoryName))
                categoryName = "null";
            else
                categoryName = "'" + categoryName + "'";
            string cmdText = "update tblOrginalTransaction set CategoryName = " + categoryName +
                " where OrginalTransactionID = " + Convert.ToString(orginalTransactionID);
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(
                System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString()))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                    myCommand.ExecuteNonQuery();
            }
            if (categoryName == "[Multiple Categories]")
            { }
            else
            {
                cmdText = "update tblSplitTransaction set CategoryName = " + categoryName +
                " where OrginalTransactionID = " + Convert.ToString(orginalTransactionID);
                using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(
                    System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString()))
                {
                    myConnection.Open();
                    using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                        myCommand.ExecuteNonQuery();
                }
            }
        }
        public static void UpdateTransaction(bool verified, string merchantName, string userMemo, string orginalTransactionID)
        {
            string cmdText = "";
            cmdText = "update tblOrginalTransaction set"
                + " Verified = " + verified
                + ", Merchant = '" + merchantName.Replace(@"'", @"''") + "'"
                + ", UserMemo = '" + userMemo.Replace(@"'", @"''") + "'"
                + " where OrginalTransactionID = " + orginalTransactionID;
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(
                System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString()))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                    myCommand.ExecuteNonQuery();
            }
        }
        public static List<Transaction> TransactionsByCategory(string categoryName, string dateRange)
        {
            string cmdText = "select " +
                "tblOrginalTransaction.TransactionDate, tblOrginalTransaction.Merchant, tblSplitTransaction.TransactionAmount, " +
                "tblOrginalTransaction.BankMemo, tblOrginalTransaction.CheckNumber, tblOrginalTransaction.TransactionType, " +
                "tblOrginalTransaction.UserMemo " +
                "FROM tblOrginaltransaction left join tblSplitTransaction " +
                "on tblSplitTransaction.OrginalTransactionID = tblorginaltransaction.OrginalTransactionID " +
                "WHERE tblSplitTransaction.CategoryName = '" + categoryName + "'";
            cmdText += AddDateRange(dateRange);
            cmdText += "order by tblOrginalTransaction.TransactionDate desc";
            List<Transaction> transactions = new List<Transaction>();
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(
                System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString()))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                {
                    using (OleDbDataReader myDataReader = myCommand.ExecuteReader())
                    {
                        while (myDataReader.Read())
                        {
                            Transaction transaction = new Transaction();
                            transaction.TransactionDate = Convert.ToDateTime(myDataReader["TransactionDate"].ToString());
                            transaction.MerchantName = myDataReader["Merchant"].ToString();
                            transaction.TransactionAmount = Convert.ToDecimal(myDataReader["TransactionAmount"].ToString());
                            transaction.BankMemo = myDataReader["BankMemo"].ToString();
                            transaction.UserMemo = myDataReader["UserMemo"].ToString();
                            transaction.CheckNumber = myDataReader["CheckNumber"].ToString();
                            transaction.TransactionType = myDataReader["TransactionType"].ToString();
                            transactions.Add(transaction);
                        }
                    }
                }
                return transactions;
            }
        }
        public static decimal MonthlyAverage(decimal transactionsTotal, string dateRange)
        {
            DateRangeList dateRangeList = new DateRangeList(dateRange);
            int numberOfDays = dateRangeList.EndDate.Subtract(dateRangeList.StartDate).Days;
            decimal perDay = transactionsTotal / numberOfDays;
            decimal perYear = perDay * 365;
            decimal perMonth = perYear / 12;
            return Math.Round(perMonth);
        }
        public static DateTime OldestTransaction()
        {
            string cmdText = "select top 1 TransactionDate from tblOrginalTransaction order by OrginalTransactionID";
            DateTime oldestTransaction = new DateTime();
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(
                System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString()))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                {
                    using (OleDbDataReader myDataReader = myCommand.ExecuteReader())
                    {
                        if (myDataReader.Read())
                            if (!string.IsNullOrEmpty(myDataReader["TransactionDate"].ToString()) &&
                                IsDate(myDataReader["TransactionDate"].ToString()))
                                oldestTransaction = Convert.ToDateTime(myDataReader["TransactionDate"].ToString());
                    }
                }
                return oldestTransaction;
            }
        }
        private static bool IsDate(string date)
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
        public static Transaction CheckBankTricks(Transaction transaction, BankAccount bankAccount)
        {
            if (transaction.TransactionType.ToLower() == "check")
            {
                transaction.MerchantName = "?";
                transaction.BankMemo = "";
            }
            else
            {
                string merchantName = "";
                string bankMemo = "";
                if (bankAccount.ReverseFields)
                {
                    merchantName = transaction.BankMemo;
                    bankMemo = transaction.MerchantName;
                }
                else
                {
                    merchantName = transaction.MerchantName;
                    bankMemo = transaction.BankMemo;
                }
                transaction.MerchantName = merchantName;
                transaction.BankMemo = bankMemo;
                switch (bankAccount.RemoveFromMerchant)
                {
                    case "[Remove Merchant Name]":
                        transaction.MerchantName = RemovePartialMerchantName(transaction.MerchantName, transaction.BankMemo.Replace(bankAccount.RemoveFromBankMemo, "")).Trim();
                        break;
                    case "[Everything]":
                        transaction.MerchantName = "";
                        break;
                    default:
                        if (!string.IsNullOrEmpty(bankAccount.RemoveFromMerchant))
                            transaction.MerchantName = transaction.MerchantName.Replace(bankAccount.RemoveFromMerchant, "").Trim();
                        break;
                }
                switch (bankAccount.RemoveFromBankMemo)
                {
                    case "[Remove Merchant Name]":
                        transaction.BankMemo = RemovePartialMerchantName(transaction.BankMemo, transaction.MerchantName.Replace(bankAccount.RemoveFromMerchant, "")).Trim();
                        break;
                    case "[Everything]":
                        transaction.BankMemo = "";
                        break;
                    default:
                        if (!string.IsNullOrEmpty(bankAccount.RemoveFromBankMemo))
                            transaction.BankMemo = transaction.BankMemo.Replace(bankAccount.RemoveFromBankMemo, "").Trim();
                        break;
                }
            }
            return transaction;
        }
        public static string RemovePartialMerchantName(string firstString, string secondString)
        {
            string removeString = "";
            int start = firstString.Length;
            for (int i = 1; i < firstString.Length; i++)
            {
                if (secondString.Contains(firstString.Substring(start - 1, i)))
                    removeString = firstString.Substring(start - 1, i);
                start = start - 1;
            }
            if (removeString.Length > 4)
                return firstString.Replace(removeString, "");
            else
                return firstString;
        }
        public static void MarkVerified()
        {
            string cmdText = "update tblOrginalTransaction set Verified = true";
                cmdText += " where CategoryName is not null";
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(
                System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString()))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                    myCommand.ExecuteNonQuery();
            }
        }
        public static CategoryTotal CategoryTotal(string categoryName, string dateRange)
        {
            decimal transactionAmount = 0;
            int count = 0;
            string cmdText = "select sum(tblSplitTransaction.TransactionAmount) as total, " +
                    "count(tblSplitTransaction.TransactionAmount) as transctionCount" +
                    " from tblSplitTransaction left join tblorginalTransaction" +
                    " on tblSplitTransaction.OrginalTransactionID = tblOrginaltransaction.OrginalTransactionID" +
                    " where tblSplittransaction.CategoryName = '" + categoryName + "' ";
            cmdText += AddDateRange(dateRange);
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(
                System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString()))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                {
                    using (OleDbDataReader myDataReader = myCommand.ExecuteReader())
                    {
                        if (myDataReader.Read() && !string.IsNullOrEmpty(myDataReader["total"].ToString())
                                && IsNumber(myDataReader["total"].ToString()))
                        {
                            transactionAmount = Convert.ToDecimal(myDataReader["total"].ToString());
                            count = Convert.ToInt32(myDataReader["transctionCount"].ToString());
                        }

                    }
                }
            }
            return new CategoryTotal(count, transactionAmount);
        }
        private static string AddDateRange(string dateRange)
        {
            string cmdText = " and tblOrginalTransaction.TransactionDate between #";
            DateRangeList dateRangeNew = new DateRangeList(dateRange);
            cmdText += Convert.ToString(dateRangeNew.StartDate) + "# and #";
            cmdText += Convert.ToString(dateRangeNew.EndDate) + "#";
            return cmdText;
        }
        private static bool IsNumber(string text)
        {
            text = text.Trim();
            double number;
            bool isNumber = double.TryParse(text, out number);
            return isNumber;
        }

        public static List<string> Categories(string dateRange)
        {
            List<string> categories = new List<string>();
            string cmdText = "select distinct CategoryName from tblOrginalTransaction where CategoryName <> 'YoYo' ";
            cmdText += AddDateRange(dateRange);
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(
                System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString()))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                {
                    using (OleDbDataReader myDataReader = myCommand.ExecuteReader())
                    {
                        while (myDataReader.Read())
                            categories.Add(myDataReader["CategoryName"].ToString());
                    }
                }
            }
            return categories;
        }
    }

    public class DateRangeList
    {
        public DateTime StartDate = new DateTime();
        public DateTime EndDate;

        public DateRangeList(string dateRange)
        {
            switch (dateRange)
            {
                case "Month to date":
                    StartDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 01);
                    EndDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 01).AddMonths(1).AddDays(-1);
                    break;
                case "Last month":
                    StartDate = new DateTime(DateTime.Today.AddMonths(-1).Year, DateTime.Today.AddMonths(-1).Month, 01);
                    EndDate = new DateTime(DateTime.Today.AddMonths(-1).Year, DateTime.Today.AddMonths(-1).Month, 01).AddMonths(1).AddDays(-1);
                    break;
                case "Last 3 months":
                    StartDate = DateTime.Today.AddMonths(-3);
                    EndDate = DateTime.Today;
                    break;
                case "Last 6 months":
                    StartDate = DateTime.Today.AddMonths(-6);
                    EndDate = DateTime.Today;
                    break;
                case "Year to date":
                    StartDate = new DateTime(DateTime.Today.Year, 01, 01);
                    EndDate = DateTime.Today;
                    break;
            }
        }

    }
    public class CategoryTotal
    {
        public int Count;
        public decimal TransactionAmount;

        public CategoryTotal(int count, decimal transactionAmount)
        {
            Count = count;
            TransactionAmount = transactionAmount;
        }
    }

    public class SplitTransaction : Transaction
    {
        public int SplitTransactionID;

        public SplitTransaction() { }

        public SplitTransaction(decimal transactionAmount, string categoryName, string userMemo, int splitTransactionID)
        {
            TransactionAmount = transactionAmount;
            CategoryName = categoryName;
            UserMemo = userMemo;
            SplitTransactionID = splitTransactionID;
        }
        public static List<SplitTransaction> SplitTransactions(int orginalTransactionID)
        {
            string cmdText =
                "SELECT TransactionAmount, CategoryName, UserMemo, SplitTransactionID" +
                " FROM tblSplitTransaction" +
                " WHERE (OrginalTransactionID = " + Convert.ToString(orginalTransactionID) + ")";
            List<SplitTransaction> transactions = new List<SplitTransaction>();
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(
                System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString()))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                {
                    using (OleDbDataReader myDataReader = myCommand.ExecuteReader())
                    {
                        while (myDataReader.Read())
                        {

                            SplitTransaction transaction = new SplitTransaction(
                            Convert.ToDecimal(myDataReader["TransactionAmount"].ToString()),
                            myDataReader["CategoryName"].ToString(),
                            myDataReader["UserMemo"].ToString(),
                            Convert.ToInt32(myDataReader["SplitTransactionID"].ToString()));
                            transactions.Add(transaction);
                        }
                    }
                }
                return transactions;
            }
        }
        public static int InsertTransaction(string categoryName, string userMemo, decimal transactionAmount, int orginalTransactionID)
        {
            int splitTransactionID = 0;
            string cmdText = "insert into tblSplitTransaction(CategoryName, UserMemo, TransactionAmount, OrginalTransactionID) values(";
            if (string.IsNullOrEmpty(categoryName))
                cmdText += "null";
            else
                cmdText += "'" + categoryName + "'";
            cmdText += ", '" + userMemo + "', " + Convert.ToString(transactionAmount) +
                ", " + orginalTransactionID + ")";
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(
                System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString()))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                    myCommand.ExecuteNonQuery();
                string identity = "SELECT @@Identity";
                using (OleDbCommand myCommand = new OleDbCommand(identity, myConnection))
                    splitTransactionID = Convert.ToInt32(myCommand.ExecuteScalar().ToString());
            }
            return splitTransactionID;
        }
        public static void UpdateSplitTransaction(string categoryName, string userMemo, decimal transactionAmount, int splitTransactionID)
        {
            string cmdText = "update tblSplitTransaction set TransactionAmount = " + Convert.ToString(transactionAmount);
            if (string.IsNullOrEmpty(categoryName))
                cmdText += ", CategoryName = null";
            else
                cmdText += ", Categoryname = '" + categoryName + "'";
            if (!string.IsNullOrEmpty(userMemo))
                cmdText += ", UserMemo = '" + userMemo + "'";
            cmdText += " where SplitTransactionID = " + Convert.ToString(splitTransactionID);
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(
                System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString()))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                    myCommand.ExecuteNonQuery();
            }
        }
    }
}

