using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Configuration;

namespace BeanCounter.BusinessLogic
{
    public class Category
    {

        public string CategoryName;
        public string Type;
        public string Frequency;
        public int DayOfMonth = 1;
        public int CategoryID = 0;
        public DateTime SpecificDate;
        public DateTime NextOccurance;
        public DateTime EndDate;
        public decimal StaticAmount = 0;
        public decimal C1 = 0, C2 = 0, C3 = 0, C4 = 0, C5 = 0, C6 = 0;
        public decimal C7 = 0, C8 = 0, C9 = 0, C10 = 0, C11 = 0, C12 = 0;
        public bool ExcludeFromBudget = false;

        public Category() { }

        public static IEnumerable<string> CategoryNames()
        {
            List<string> categories = new List<string>();
            string cmdText = "select distinct CategoryName from tblCategory order by CategoryName";
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString();
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(connectionString))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                using (OleDbDataReader myDataReader = myCommand.ExecuteReader())
                    while (myDataReader.Read())
                    {
                        string category = myDataReader["CategoryName"].ToString();
                        categories.Add(category);
                    }
                return categories;
            }
        }

        public static List<Category> Categories(string type, string frequency, bool upcoming)
        {
            List<Category> categories = new List<Category>();
            string cmdText = "select * from tblCategory where"
            + " Type = '" + type + "'"
            + " and Frequency = '" + frequency + "'";
            switch (frequency)
            {
                case "Annually":
                case "One Time":
                    cmdText += " order by ExcludeFromBudget desc, SpecificDate, CategoryName, CategoryID";
                    break;
                case "Monthly":
                    if (upcoming)
                        cmdText += " order by ExcludeFromBudget desc, DayOfMonth";
                    else
                        cmdText += " order by ExcludeFromBudget desc, CategoryName, CategoryID";
                    break;
                default:
                    cmdText += " order by ExcludeFromBudget desc, CategoryName, CategoryID";
                    break;
            }

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString();
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(connectionString))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                using (OleDbDataReader myDataReader = myCommand.ExecuteReader())
                    while (myDataReader.Read())
                    {
                        Category category = new Category();
                        category.CategoryName = myDataReader["CategoryName"].ToString();
                        category.CategoryID = Convert.ToInt32(myDataReader["CategoryID"].ToString());
                        switch (frequency)
                        {
                            case "Annually":
                            case "One Time":
                                
                                if (!string.IsNullOrEmpty(myDataReader["SpecificDate"].ToString()))
                                    category.SpecificDate = Convert.ToDateTime(myDataReader["SpecificDate"].ToString());
                                if (!string.IsNullOrEmpty(myDataReader["StaticAmount"].ToString()))
                                    category.StaticAmount = Convert.ToDecimal(myDataReader["StaticAmount"].ToString());
                                break;
                            case "Anytime":
                                category = AddMonthlyAmounts(category, myDataReader);
                                break;
                            case "Bi-Weekly":
                            case "Weekly":
                                if (!string.IsNullOrEmpty(myDataReader["StaticAmount"].ToString()))
                                    category.StaticAmount = Convert.ToDecimal(myDataReader["StaticAmount"].ToString());
                                if (!string.IsNullOrEmpty(myDataReader["NextOccurance"].ToString()) &&
                                        IsDate(myDataReader["NextOccurance"].ToString()))
                                    category.NextOccurance = Convert.ToDateTime(myDataReader["NextOccurance"].ToString());
                                if (!string.IsNullOrEmpty(myDataReader["EndDate"].ToString()) &&
                                        IsDate(myDataReader["EndDate"].ToString()))
                                    category.EndDate = Convert.ToDateTime(myDataReader["EndDate"].ToString());
                                if (category.NextOccurance < DateTime.Today)
                                {
                                    while (category.NextOccurance < DateTime.Today)
                                    {
                                        switch (frequency)
                                        {
                                            case "Bi-Weekly":
                                                category.NextOccurance = category.NextOccurance.AddDays(14);
                                                break;
                                            case "Weekly":
                                                category.NextOccurance = category.NextOccurance.AddDays(7);
                                                break;
                                        }
                                    }
                                    Category.UpdateNextOccurance(category.CategoryID, category.NextOccurance);
                                }
                                break;
                            case "Monthly":
                                if (!string.IsNullOrEmpty(myDataReader["DayOfMonth"].ToString()))
                                    category.DayOfMonth = Convert.ToInt32(myDataReader["DayOfMonth"].ToString());
                                category = AddMonthlyAmounts(category, myDataReader);
                                break;
                        }
                        category.ExcludeFromBudget = Convert.ToBoolean(myDataReader["ExcludeFromBudget"].ToString());
                        categories.Add(category);
                    }
                return categories;
            }
        }

        private static Category AddMonthlyAmounts(Category category, OleDbDataReader myDataReader)
        {
            if (!string.IsNullOrEmpty(myDataReader["c1"].ToString()))
                category.C1 = Convert.ToDecimal(myDataReader["c1"].ToString());
            if (!string.IsNullOrEmpty(myDataReader["c2"].ToString()))
                category.C2 = Convert.ToDecimal(myDataReader["c2"].ToString());
            if (!string.IsNullOrEmpty(myDataReader["c3"].ToString()))
                category.C3 = Convert.ToDecimal(myDataReader["c3"].ToString());
            if (!string.IsNullOrEmpty(myDataReader["c4"].ToString()))
                category.C4 = Convert.ToDecimal(myDataReader["c4"].ToString());
            if (!string.IsNullOrEmpty(myDataReader["c5"].ToString()))
                category.C5 = Convert.ToDecimal(myDataReader["c5"].ToString());
            if (!string.IsNullOrEmpty(myDataReader["c6"].ToString()))
                category.C6 = Convert.ToDecimal(myDataReader["c6"].ToString());
            if (!string.IsNullOrEmpty(myDataReader["c7"].ToString()))
                category.C7 = Convert.ToDecimal(myDataReader["c7"].ToString());
            if (!string.IsNullOrEmpty(myDataReader["c8"].ToString()))
                category.C8 = Convert.ToDecimal(myDataReader["c8"].ToString());
            if (!string.IsNullOrEmpty(myDataReader["c9"].ToString()))
                category.C9 = Convert.ToDecimal(myDataReader["c9"].ToString());
            if (!string.IsNullOrEmpty(myDataReader["c10"].ToString()))
                category.C10 = Convert.ToDecimal(myDataReader["c10"].ToString());
            if (!string.IsNullOrEmpty(myDataReader["c11"].ToString()))
                category.C11 = Convert.ToDecimal(myDataReader["c11"].ToString());
            if (!string.IsNullOrEmpty(myDataReader["c12"].ToString()))
                category.C12 = Convert.ToDecimal(myDataReader["c12"].ToString());
            return category;
        }

        public static void UpdateCategory(Category category)
        {
            string oldCategoryName = FindCategoryName(category.CategoryID);
            UpdateCategoryTable(category);
            if (oldCategoryName != category.CategoryName)
            {
                UpdateOrginalTransactions(category, oldCategoryName);
                UpdateSplitTransactions(category, oldCategoryName);
                Merchant.UpdateMerchants(category, oldCategoryName);
            }
        }

        private static string FindCategoryName(int categoryID)
        {
            string categoryName = "";
            string cmdText = "select CategoryName from tblCategory where CategoryID = " + Convert.ToString(categoryID);
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString();
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(connectionString))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                using (OleDbDataReader myDataReader = myCommand.ExecuteReader())
                    if (myDataReader.Read())
                        categoryName = myDataReader["CategoryName"].ToString();
                return categoryName;
            }
        }

        private static void UpdateCategoryTable(Category category)
        {
            string cmdText = "update tblCategory set CategoryName = '" + category.CategoryName + "'";
            switch (category.Frequency)
            {
                case "Annually":
                case "One Time":
                    cmdText += ", StaticAmount = " + category.StaticAmount;
                    if (category.SpecificDate != new DateTime(00001, 01, 01))
                        cmdText += ", SpecificDate = '" + category.SpecificDate + "'";
                    break;
                case "Anytime":
                    cmdText += AddMonthsUpdate(category);
                    break;
                case "Bi-Weekly":
                case "Weekly":
                    cmdText += ", StaticAmount = " + category.StaticAmount;
                    cmdText += ", NextOccurance = #" + category.NextOccurance + "#";
                    if (category.NextOccurance != new DateTime() && category.EndDate != new DateTime())
                        cmdText += ", EndDate = #" + category.EndDate + "#";
                    break;
                case "Monthly":
                    cmdText += AddMonthsUpdate(category);
                    cmdText += ", DayOfMonth = " + category.DayOfMonth;
                    break;
            }
            cmdText += ", ExcludeFromBudget = " + Convert.ToString(category.ExcludeFromBudget);
            cmdText += " where CategoryID = " + Convert.ToString(category.CategoryID);
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString();
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(connectionString))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                    myCommand.ExecuteNonQuery();
            }
        }

        private static void UpdateSplitTransactions(Category category, string oldCategoryName)
        {
            string cmdText = "update tblSplitTransaction set CategoryName = '" + category.CategoryName + "'" +
                " where CategoryName = '" + oldCategoryName + "'";
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString();
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(connectionString))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                    myCommand.ExecuteNonQuery();
            }
        }

        private static void UpdateOrginalTransactions(Category category, string oldCategoryName)
        {
            string cmdText = "update tblOrginalTransaction set CategoryName = '" + category.CategoryName + "'" +
                " where CategoryName = '" + oldCategoryName + "'";
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString();
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(connectionString))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                    myCommand.ExecuteNonQuery();
            }
        }

        public static int InsertCategory(Category category)
        {
            int categoryID = 0;
            string cmdText = "";
            if (category.CategoryName != null)
            {
                cmdText = "insert into tblCategory (CategoryName";
                switch (category.Frequency)
                {
                    case "Annually":
                    case "One Time":
                        cmdText += ", StaticAmount";
                        cmdText += ", SpecificDate";
                        break;
                    case "Anytime":
                        cmdText += AddMonthlyColumns(category);
                        break;
                    case "Bi-Weekly":
                    case "Weekly":
                        cmdText += ", StaticAmount, NextOccurance";
                        if (category.EndDate != new DateTime())
                            cmdText += ", EndDate";
                        break;
                    case "Monthly":
                        cmdText += AddMonthlyColumns(category);
                        cmdText += ", DayOfMonth";
                        break;
                }
                cmdText += ", Type, Frequency) values('" + category.CategoryName + "'";
                switch (category.Frequency)
                {
                    case "Annually":
                    case "One Time":
                        cmdText += ", " + category.StaticAmount;
                        cmdText += ", '" + category.SpecificDate + "'";
                        break;
                    case "Anytime":
                        cmdText += AddMonthlyValues(category);
                        break;
                    case "Bi-Weekly":
                    case "Weekly":
                        cmdText += ", " + category.StaticAmount + ", '" + category.NextOccurance + "'";
                        if (category.EndDate != new DateTime())
                            cmdText += ", #" + category.EndDate + "#";
                        break;
                    case "Monthly":
                        cmdText += AddMonthlyValues(category);
                        cmdText += ", " + category.DayOfMonth;
                        break;
                }
                cmdText += ", '" + category.Type + "', '" + category.Frequency + "')";
            }
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString();
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(connectionString))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                    myCommand.ExecuteNonQuery();
                string identity = "SELECT @@Identity";
                using (OleDbCommand myCommand = new OleDbCommand(identity, myConnection))
                    categoryID = Convert.ToInt32(myCommand.ExecuteScalar().ToString());

            }
            return categoryID;
        }

        private static string AddMonthsUpdate(Category category)
        {
            string cmdText = "";
            cmdText += ", c1 = " + Convert.ToString(category.C1);
            cmdText += ", c2 = " + Convert.ToString(category.C2);
            cmdText += ", c3 = " + Convert.ToString(category.C3);
            cmdText += ", c4 = " + Convert.ToString(category.C4);
            cmdText += ", c5 = " + Convert.ToString(category.C5);
            cmdText += ", c6 = " + Convert.ToString(category.C6);
            cmdText += ", c7 = " + Convert.ToString(category.C7);
            cmdText += ", c8 = " + Convert.ToString(category.C8);
            cmdText += ", c9 = " + Convert.ToString(category.C9);
            cmdText += ", c10 = " + Convert.ToString(category.C10);
            cmdText += ", c11 = " + Convert.ToString(category.C11);
            cmdText += ", c12 = " + Convert.ToString(category.C12);
            return cmdText;
        }

        private static string AddMonthlyValues(Category category)
        {
            string cmdText = "";
            cmdText += ", " + Convert.ToString(category.C1);
            cmdText += ", " + Convert.ToString(category.C2);
            cmdText += ", " + Convert.ToString(category.C3);
            cmdText += ", " + Convert.ToString(category.C4);
            cmdText += ", " + Convert.ToString(category.C5);
            cmdText += ", " + Convert.ToString(category.C6);
            cmdText += ", " + Convert.ToString(category.C7);
            cmdText += ", " + Convert.ToString(category.C8);
            cmdText += ", " + Convert.ToString(category.C9);
            cmdText += ", " + Convert.ToString(category.C10);
            cmdText += ", " + Convert.ToString(category.C11);
            cmdText += ", " + Convert.ToString(category.C12);
            return cmdText;
        }

        private static string AddMonthlyColumns(Category category)
        {
            string cmdText = "";
            cmdText += ", c1";
            cmdText += ", c2";
            cmdText += ", c3";
            cmdText += ", c4";
            cmdText += ", c5";
            cmdText += ", c6";
            cmdText += ", c7";
            cmdText += ", c8";
            cmdText += ", c9";
            cmdText += ", c10";
            cmdText += ", c11";
            cmdText += ", c12";
            return cmdText;
        }

        public static List<Category> Categories(string categoryName)
        {
            string cmdText = "select * from tblCategory where CategoryName = '" + categoryName + "' order by CategoryName, CategoryID";
            List<Category> categories = new List<Category>();
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString();
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(connectionString))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                using (OleDbDataReader myDataReader = myCommand.ExecuteReader())
                    while (myDataReader.Read())
                    {
                        Category category = new Category();
                        if (myDataReader["c1"].ToString() != null && !string.IsNullOrEmpty(myDataReader["c1"].ToString()))
                            category.C1 = Convert.ToDecimal(myDataReader["c1"].ToString());
                        if (myDataReader["c2"].ToString() != null && !string.IsNullOrEmpty(myDataReader["c2"].ToString()))
                            category.C2 = Convert.ToDecimal(myDataReader["c2"].ToString());
                        if (myDataReader["c3"].ToString() != null && !string.IsNullOrEmpty(myDataReader["c3"].ToString()))
                            category.C3 = Convert.ToDecimal(myDataReader["c3"].ToString());
                        if (myDataReader["c4"].ToString() != null && !string.IsNullOrEmpty(myDataReader["c4"].ToString()))
                            category.C4 = Convert.ToDecimal(myDataReader["c4"].ToString());
                        if (myDataReader["c5"].ToString() != null && !string.IsNullOrEmpty(myDataReader["c5"].ToString()))
                            category.C5 = Convert.ToDecimal(myDataReader["c5"].ToString());
                        if (myDataReader["c6"].ToString() != null && !string.IsNullOrEmpty(myDataReader["c6"].ToString()))
                            category.C6 = Convert.ToDecimal(myDataReader["c6"].ToString());
                        if (myDataReader["c7"].ToString() != null && !string.IsNullOrEmpty(myDataReader["c7"].ToString()))
                            category.C7 = Convert.ToDecimal(myDataReader["c7"].ToString());
                        if (myDataReader["c8"].ToString() != null && !string.IsNullOrEmpty(myDataReader["c8"].ToString()))
                            category.C8 = Convert.ToDecimal(myDataReader["c8"].ToString());
                        if (myDataReader["c9"].ToString() != null && !string.IsNullOrEmpty(myDataReader["c9"].ToString()))
                            category.C9 = Convert.ToDecimal(myDataReader["c9"].ToString());
                        if (myDataReader["c10"].ToString() != null && !string.IsNullOrEmpty(myDataReader["c10"].ToString()))
                            category.C10 = Convert.ToDecimal(myDataReader["c10"].ToString());
                        if (myDataReader["c11"].ToString() != null && !string.IsNullOrEmpty(myDataReader["c11"].ToString()))
                            category.C11 = Convert.ToDecimal(myDataReader["c11"].ToString());
                        if (myDataReader["c12"].ToString() != null && !string.IsNullOrEmpty(myDataReader["c12"].ToString()))
                            category.C12 = Convert.ToDecimal(myDataReader["c12"].ToString());
                        categories.Add(category);
                    }
                return categories;
            }
        }

        public static bool IsUsed(string categoryName)
        {
            bool isUsed = false;
            string cmdText = "select * from tblSplitTransaction where CategoryName = '" + categoryName + "'";
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString();
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(connectionString))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                using (OleDbDataReader myDataReader = myCommand.ExecuteReader())
                    if (myDataReader.Read())
                        isUsed = true;
            }
            return isUsed;
        }

        public static void DeleteCategory(int categoryID)
        {
            string cmdText = "delete from tblCategory where CategoryID = " + Convert.ToInt32(categoryID);
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString();
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(connectionString))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                    myCommand.ExecuteNonQuery();
            }
        }

        internal static List<Category> StaticAmountCategories(string type, string frequency)
        {
            string cmdText = "select CategoryID, CategoryName, StaticAmount, SpecificDate, NextOccurance, EndDate " +
                "from tblCategory where Type = '" + type + "' " + " and Frequency = '" + frequency + "'" + 
                " and ExcludeFromBudget = 0";
            List<Category> categories = new List<Category>();
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString();
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(connectionString))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                using (OleDbDataReader myDataReader = myCommand.ExecuteReader())
                    while (myDataReader.Read())
                    {
                        Category category = new Category();
                        category.CategoryName = myDataReader["CategoryName"].ToString();
                        category.CategoryID = Convert.ToInt32(myDataReader["CategoryID"].ToString());
                        if (myDataReader["StaticAmount"].ToString() != null &&
                                IsNumber(myDataReader["StaticAmount"].ToString()))
                            category.StaticAmount = Convert.ToDecimal(myDataReader["StaticAmount"].ToString());
                        if (myDataReader["SpecificDate"].ToString() != null && IsDate(myDataReader["SpecificDate"].ToString()))
                            category.SpecificDate = Convert.ToDateTime(myDataReader["SpecificDate"].ToString());
                        if (myDataReader["NextOccurance"].ToString() != null && IsDate(myDataReader["NextOccurance"].ToString()))
                            category.NextOccurance = Convert.ToDateTime(myDataReader["NextOccurance"].ToString());
                        if (myDataReader["EndDate"].ToString() != null && IsDate(myDataReader["EndDate"].ToString()))
                            category.EndDate = Convert.ToDateTime(myDataReader["EndDate"].ToString());
                        categories.Add(category);
                    }
            }
            return categories;
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

        private static bool IsNumber(string text)
        {
            text = text.Trim();
            double number;
            bool isNumber = double.TryParse(text, out number);
            return isNumber;
        }

        public static void UpdateSpecificDate(DateTime transactionDate, int categoryID)
        {
            string cmdText = "update tblCategory set SpecificDate = '" + transactionDate + "' where CategoryID = " +
                Convert.ToString(categoryID);
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString();
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(connectionString))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                    myCommand.ExecuteNonQuery();
            }
        }

        internal static DateTime NextOccuranceLookup(int categoryID)
        {
            string cmdText = "select NextOccurance from tblCategory where CategoryID = " + categoryID +
                " order by NextOccurance desc";
            DateTime nextOccurance = new DateTime();
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString();
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(connectionString))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                using (OleDbDataReader myDataReader = myCommand.ExecuteReader())
                    if (myDataReader.Read())
                        if (myDataReader["NextOccurance"].ToString() != null)
                            nextOccurance = Convert.ToDateTime(myDataReader["NextOccurance"].ToString());
            }
            return nextOccurance;
        }

        internal static void UpdateNextOccurance(int categoryID, DateTime nextOccuranceUpdate)
        {
            string cmdText = "update tblCategory set NextOccurance = '" + nextOccuranceUpdate + "'";
            cmdText += " where CategoryID = " + Convert.ToString(categoryID);
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString();
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(connectionString))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                    myCommand.ExecuteNonQuery();
            }
        }

        public static bool CategoryExists(int categoryID)
        {
            bool categoryExists = false;
            string cmdText = "select CategoryID from tblCategory where CategoryID = " + Convert.ToString(categoryID);
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString();
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(connectionString))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                {
                    using (OleDbDataReader myDataReader = myCommand.ExecuteReader())
                    {
                        if (myDataReader.Read())
                            categoryExists = true;
                    }
                }
            }
            return categoryExists;
        }

        public static Category FindCategory(int categoryID)
        {
            string cmdText = "select * from tblCategory where CategoryID = " + categoryID;
            Category category = new Category();
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString();
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(connectionString))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                using (OleDbDataReader myDataReader = myCommand.ExecuteReader())
                    if (myDataReader.Read())
                    {
                        if (myDataReader["c1"].ToString() != null && !string.IsNullOrEmpty(myDataReader["c1"].ToString()))
                            category.C1 = Convert.ToDecimal(myDataReader["c1"].ToString());
                        if (myDataReader["c2"].ToString() != null && !string.IsNullOrEmpty(myDataReader["c2"].ToString()))
                            category.C2 = Convert.ToDecimal(myDataReader["c2"].ToString());
                        if (myDataReader["c3"].ToString() != null && !string.IsNullOrEmpty(myDataReader["c3"].ToString()))
                            category.C3 = Convert.ToDecimal(myDataReader["c3"].ToString());
                        if (myDataReader["c4"].ToString() != null && !string.IsNullOrEmpty(myDataReader["c4"].ToString()))
                            category.C4 = Convert.ToDecimal(myDataReader["c4"].ToString());
                        if (myDataReader["c5"].ToString() != null && !string.IsNullOrEmpty(myDataReader["c5"].ToString()))
                            category.C5 = Convert.ToDecimal(myDataReader["c5"].ToString());
                        if (myDataReader["c6"].ToString() != null && !string.IsNullOrEmpty(myDataReader["c6"].ToString()))
                            category.C6 = Convert.ToDecimal(myDataReader["c6"].ToString());
                        if (myDataReader["c7"].ToString() != null && !string.IsNullOrEmpty(myDataReader["c7"].ToString()))
                            category.C7 = Convert.ToDecimal(myDataReader["c7"].ToString());
                        if (myDataReader["c8"].ToString() != null && !string.IsNullOrEmpty(myDataReader["c8"].ToString()))
                            category.C8 = Convert.ToDecimal(myDataReader["c8"].ToString());
                        if (myDataReader["c9"].ToString() != null && !string.IsNullOrEmpty(myDataReader["c9"].ToString()))
                            category.C9 = Convert.ToDecimal(myDataReader["c9"].ToString());
                        if (myDataReader["c10"].ToString() != null && !string.IsNullOrEmpty(myDataReader["c10"].ToString()))
                            category.C10 = Convert.ToDecimal(myDataReader["c10"].ToString());
                        if (myDataReader["c11"].ToString() != null && !string.IsNullOrEmpty(myDataReader["c11"].ToString()))
                            category.C11 = Convert.ToDecimal(myDataReader["c11"].ToString());
                        if (myDataReader["c12"].ToString() != null && !string.IsNullOrEmpty(myDataReader["c12"].ToString()))
                            category.C12 = Convert.ToDecimal(myDataReader["c12"].ToString());
                    }
                return category;
            }

        }
    }
}
