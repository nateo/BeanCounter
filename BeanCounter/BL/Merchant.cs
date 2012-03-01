using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Configuration;

namespace BeanCounter.BusinessLogic
{
    public class Merchant
    {

        public string MerchantName;
        public string CategoryName;
        public bool AutoCategorize;
        public bool IsLocalMerchant;
        public int MerchantID;


        public Merchant() { }
        public Merchant(string merchantName, string categoryName, bool autoCategorize,
            bool isLocalMerchant, int merchantid)
        {
            MerchantName = merchantName;
            CategoryName = categoryName;
            AutoCategorize = autoCategorize;
            IsLocalMerchant = isLocalMerchant;
            MerchantID = merchantid;
        }
        public static string NationalMerchant(string merchantName, List<Merchant> merchants)
        {
            string categoryName = "";
            foreach (Merchant merchant in merchants)
            {
                if (merchantName.Contains(merchant.MerchantName))
                    categoryName = merchant.CategoryName;
            }
            return categoryName;
        }
        public static List<Merchant> NationalMerchants()
        {
            List<Merchant> merchants = new List<Merchant>();
            string cmdText = "SELECT MerchantName, CategoryName FROM tblMerchant where LocalMerchant = false and AutoCategorize = true order by MerchantName";
            using (OleDbConnection myConnection = new OleDbConnection(
                ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString()))
            {
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                {
                    myConnection.Open();
                    using (OleDbDataReader myDataReader = myCommand.ExecuteReader())
                    {
                        while (myDataReader.Read())
                        {
                            //if (merchant.Contains(myDataReader["MerchantName"].ToString()))
                            //{
                            //    categoryName = myDataReader["CategoryName"].ToString();
                            //    break;
                            //}
                            Merchant merchant = new Merchant();
                            merchant.MerchantName = myDataReader["MerchantName"].ToString();
                            merchant.CategoryName = myDataReader["CategoryName"].ToString();
                            merchants.Add(merchant);
                        }
                    }
                }
            }
            return merchants;
        }
        public static string LocalMerchant(string merchant, bool autoCategorizeOnly)
        {
            string categoryName = "";
            string cmdText = "SELECT CategoryName, MerchantName FROM tblMerchant" +
            " WHERE LocalMerchant = true and MerchantName = '" + merchant.Replace(@"'", "''") + "'";
            if (autoCategorizeOnly)
                cmdText += " and AutoCategorize = true";
            using (OleDbConnection myConnection = new OleDbConnection(
                ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString()))
            {
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                {
                    myConnection.Open();
                    using (OleDbDataReader myDataReader = myCommand.ExecuteReader())
                    {
                        if (myDataReader.Read())
                            categoryName = myDataReader["CategoryName"].ToString();
                    }
                }
            }
            return categoryName;
        }
        public static List<Merchant> Merchants(string merchantType)
        {
            List<Merchant> merchants = new List<Merchant>();
            string cmdText = "select * from tblMerchant";
            switch (merchantType)
            {
                case "Local Merchants *":
                    cmdText += " where LocalMerchant = true";
                    break;
                case "National Merchants *":
                    cmdText += " where LocalMerchant = false";
                    break;
            }
            cmdText += " order by MerchantName";
            using (OleDbConnection myConnection = new OleDbConnection(
                ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString()))
            {
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                {
                    myConnection.Open();
                    using (OleDbDataReader myDataReader = myCommand.ExecuteReader())
                    {
                        while (myDataReader.Read())
                        {

                            Merchant merchant = new Merchant(
                                myDataReader["MerchantName"].ToString(),
                                myDataReader["CategoryName"].ToString(),
                                Convert.ToBoolean(myDataReader["AutoCategorize"].ToString()),
                                Convert.ToBoolean(myDataReader["LocalMerchant"].ToString()),
                                Convert.ToInt32(myDataReader["MerchantID"].ToString()));
                            merchants.Add(merchant);
                        }
                    }
                }
            }
            return merchants;
        }
        public static int InsertMerchant(string merchantName, string categoryName, bool autoCategorize, bool localMerchant)
        {
            if (string.IsNullOrEmpty(categoryName))
                categoryName = "null";
            else
                categoryName = "'" + categoryName + "'";
            string cmdText = "insert into tblMerchant(MerchantName, CategoryName, AutoCategorize, LocalMerchant) values(";
            cmdText += "'" + merchantName.Replace(@"'", "''") + "', " + categoryName + ", " +
                Convert.ToString(autoCategorize) + ", " + Convert.ToString(localMerchant) + ")";
            int merchantID = 0;
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(
                System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString()))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                    myCommand.ExecuteNonQuery();
                string identity = "SELECT @@Identity";
                using (OleDbCommand myCommand = new OleDbCommand(identity, myConnection))
                    merchantID = Convert.ToInt32(myCommand.ExecuteScalar().ToString());
            }
            return merchantID;
        }
        public static void UpdateMerchant(int merchantID, string merchantName, string categoryName, bool autoCategorize, bool localMerchant)
        {
            string cmdText = "update tblMerchant set MerchantName = '" + merchantName.Replace(@"'", @"''") +
                "', AutoCategorize = " + Convert.ToString(autoCategorize) +
                    ", LocalMerchant = " + Convert.ToString(localMerchant);
            if (string.IsNullOrEmpty(categoryName))
                cmdText += ", CategoryName = null";
            else
                cmdText += ", CategoryName = '" + categoryName + "'";
            cmdText += " where MerchantID = " + Convert.ToString(merchantID);
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(
                System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString()))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                    myCommand.ExecuteNonQuery();
            }
            if (autoCategorize)
            {
                cmdText = "update tblOrginalTransaction set CategoryName = '" +
                    categoryName + "' where Merchant = '" + merchantName + "'";
                using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(
                    System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString()))
                {
                    myConnection.Open();
                    using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                        myCommand.ExecuteNonQuery();
                }
                UpdateTransactionCategories(merchantName, categoryName);
            }


        }

        internal static void UpdateTransactionCategories(string merchantName, string categoryName)
        {
            string cmdText = "update tblSplitTransaction set categoryname = '" + categoryName +
                    "' where OrginalTransactionID in (select OrginalTransactionID from tblOrginalTransaction where Merchant = '" + merchantName + "')";
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(
                System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString()))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                    myCommand.ExecuteNonQuery();
            }
        }
        public static void DeleteMerchant(int merchantID)
        {
            string cmdText = "delete from tblMerchant where MerchantID = " + Convert.ToString(merchantID);
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(
                    System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString()))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                    myCommand.ExecuteNonQuery();
            }
        }

        internal static void UpdateMerchants(Category category, string oldCategoryName)
        {
            string cmdText = "update tblMerchant set CategoryName = '" + category.CategoryName + "'" +
                " where CategoryName = '" + oldCategoryName + "'";
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
