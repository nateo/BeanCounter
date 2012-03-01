using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace BeanCounter.BusinessLogic
{
    public class CashForecast
    {
        public decimal C1 = 0, C2 = 0, C3 = 0, C4 = 0, C5 = 0, C6 = 0;
        public decimal C7 = 0, C8 = 0, C9 = 0, C10 = 0, C11 = 0, C12 = 0;
        public decimal C13 = 0, Total = 0;
        public decimal Average = 0;
        public IncomeForecast _IncomeForecast;
        public SpendingForecast _SpendingForecast;
        public BalanceForecast _BalanceForecast;
        public GainForecast _GainForecast;
        public CashForecast() { }
        public static decimal CheckingBalance()
        {
            decimal checkingBalance = 0;
            string cmdText = "SELECT SUM(OnlineBalance) as balance from tblBankAccount WHERE AccountType = 'CHECKING'";
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString();
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(connectionString))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                using (OleDbDataReader myDataReader = myCommand.ExecuteReader())
                    if (myDataReader.Read())
                    {
                        if (myDataReader["balance"].ToString() != null &&
                                IsNumber(myDataReader["balance"].ToString()))
                            checkingBalance = Convert.ToDecimal(myDataReader["balance"].ToString());
                    }
            }
            return checkingBalance;
        }
        public static int FindYear(int year, int month)
        {
            if (Convert.ToInt32(DateTime.Today.ToString("MM")) > month)
                year++;
            return year;
        }
        private static bool IsNumber(string text)
        {
            text = text.Trim();
            double number;
            bool isNumber = double.TryParse(text, out number);
            return isNumber;
        }
        public static decimal BudgetAmount(int month, int year, string type, Options options)
        {
            decimal transactionAmount = 0;
            transactionAmount += AnnualAmounts(month, FindYear(year, month), type);
            transactionAmount += StaticBudgetAmounts(month, FindYear(year, month), type, "Weekly");
            transactionAmount += StaticBudgetAmounts(month, FindYear(year, month), type, "Bi-Weekly");
            transactionAmount += AnytimeAmounts(month, FindYear(year, month), type, options);
            transactionAmount += OneTimeBudgetAmount(month, FindYear(year, month), type);
            transactionAmount += MonthlyCmdText(month, FindYear(year, month), type);
            return Math.Round(transactionAmount, 0);
        }
        private static decimal AnnualAmounts(int month, int year, string type)
        {
            decimal budgetAmount = 0;
            foreach (Category category in Category.StaticAmountCategories(type, "Annually"))
            {
                if (category.SpecificDate.Day == 1 && category.SpecificDate.AddMonths(-1).Month == month)
                    budgetAmount += category.StaticAmount;
                if (category.SpecificDate.Day > 1 && category.SpecificDate.Month == month)
                    budgetAmount += category.StaticAmount;
                //if (category.SpecificDate.Month < month && category.SpecificDate.Year <= year)
                if (category.SpecificDate < DateTime.Today)
                    Category.UpdateSpecificDate(category.SpecificDate.AddYears(1), category.CategoryID);
            }
            return budgetAmount;
        }
        private static decimal StaticBudgetAmounts(int month, int year, string type, string frequency)
        {
            decimal StaticAmounts = 0;
            int length = 0;
            if (frequency == "Weekly")
                length = 7;
            else if (frequency == "Bi-Weekly")
                length = 14;
            List<Category> categories = Category.StaticAmountCategories(type, frequency);
            DateTime nextOccurance = new DateTime();
            if (categories.Count > 0)
            {
                foreach (Category category in categories)
                {
                    nextOccurance = Category.NextOccuranceLookup(category.CategoryID);
                    if (nextOccurance < DateTime.Today)
                    {
                        DateTime nextOccuranceUpdate = nextOccurance;
                        while (nextOccuranceUpdate < DateTime.Today)
                        {
                            switch (frequency)
                            {
                                case "Bi-Weekly":
                                    nextOccuranceUpdate = nextOccuranceUpdate.AddDays(14);
                                    break;
                                case "Weekly":
                                    nextOccuranceUpdate = nextOccuranceUpdate.AddDays(7);
                                    break;
                            }
                        }
                        Category.UpdateNextOccurance(category.CategoryID, nextOccuranceUpdate);
                    }

                    int numberOfOccurances = 0;
                    if (nextOccurance != new DateTime())
                    {
                        DateTime tempDate = nextOccurance;
                        while (tempDate <= new DateTime(year, month, 01))
                            tempDate = tempDate.AddDays(length);

                        while (tempDate <= new DateTime(year, month, 02).AddMonths(1).AddDays(-2))
                        {

                            numberOfOccurances++;
                            if (category.EndDate != new DateTime() && category.EndDate <= tempDate)
                                numberOfOccurances--;
                            tempDate = tempDate.AddDays(length);
                        }
                        if (month < 12 && tempDate == new DateTime(year, month + 1, 01))
                            numberOfOccurances++;
                        StaticAmounts += (numberOfOccurances * category.StaticAmount);
                    }
                }
            }
            return StaticAmounts;
        }
        private static decimal AnytimeAmounts(int month, int year, string type, Options options)
        {
            string cmdText = "select sum(c" + Convert.ToString(month) + ") as transactionAmounts " +
                "from tblCategory where Type = '" + type + "' " +
                "and Frequency = 'Anytime' " +
                "and ExcludeFromBudget = No";
            decimal budgetAmount = MonthlyAmounts(month, cmdText);
            if (options.AddOverages)
                budgetAmount += budgetAmount * options.Overages / 100;
            if (month == Convert.ToInt32(DateTime.Today.ToString("MM")))
            {
                decimal daysInMonth = new DateTime(year, month, 01).AddMonths(1).AddDays(-1).Day;
                decimal day = daysInMonth - DateTime.Today.Day;
                decimal percent = day / daysInMonth;
                if (budgetAmount != 0)
                    budgetAmount = budgetAmount * percent;
            }
            return Math.Round(budgetAmount, 0);
        }
        private static decimal OneTimeBudgetAmount(int month, int year, string type)
        {
            decimal budgetAmount = 0;
            foreach (Category category in Category.StaticAmountCategories(type, "One Time"))
            {
                if (category.SpecificDate.Day == 1 &&
                        category.SpecificDate.AddMonths(-1).Month == month &&
                        category.SpecificDate.Year <= DateTime.Today.Year)
                    budgetAmount += category.StaticAmount;
                if (category.SpecificDate.Day > 1 && category.SpecificDate.Month == month
                        && category.SpecificDate.Year <= DateTime.Today.Year)
                    budgetAmount += category.StaticAmount;
                if (category.SpecificDate < DateTime.Today)
                    Category.UpdateSpecificDate(category.SpecificDate.AddYears(1), category.CategoryID);
            }
            return budgetAmount;
        }
        private static decimal MonthlyCmdText(int month, int year, string type)
        {
            string cmdText = "select sum(c" + Convert.ToString(month) + ") as transactionAmounts " +
                "from tblCategory where Type = '" + type + "' " +
                "and Frequency = 'Monthly' and DayOfMonth > 1 " +
                "and ExcludeFromBudget = No";
            if (month == DateTime.Today.Month)
                cmdText += " and DayOfMonth > " + DateTime.Today.Day;
            decimal budgetAmount = MonthlyAmounts(month, cmdText);
            if (month == 12)
                month = 1;
            else if (month < 12)
                month++;
            cmdText = "select sum(c" + Convert.ToString(month) + ") as transactionAmounts " +
                "from tblCategory where Type = '" + type + "' " +
                "and Frequency = 'Monthly' and DayOfMonth = 1 " +
                "and ExcludeFromBudget = No"; ;
            budgetAmount += MonthlyAmounts(month, cmdText);
            return budgetAmount;
        }
        private static decimal MonthlyAmounts(int month, string cmdText)
        {
            decimal monthlyBudgetAmounts = 0;
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString();
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(connectionString))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                {
                    using (OleDbDataReader myDataReader = myCommand.ExecuteReader())
                    {
                        if (myDataReader.Read())
                        {
                            if (myDataReader["transactionAmounts"].ToString() != null &&
                                    IsNumber(myDataReader["transactionAmounts"].ToString()))
                                monthlyBudgetAmounts = Convert.ToDecimal(myDataReader["transactionAmounts"].ToString());
                        }
                    }
                }
            }
            return monthlyBudgetAmounts;
        }
        public static CashForecast BuildCashForecast(Options options)
        {
            CashForecast cashForecast = new CashForecast();
            decimal checkingBalance = 0;
            if (options.OverrideBalance)
                checkingBalance = options.Balance;
            else
                checkingBalance = CheckingBalance();
            cashForecast._IncomeForecast = new IncomeForecast();
            cashForecast._SpendingForecast = new SpendingForecast(options);
            cashForecast._GainForecast = new GainForecast(cashForecast);
            cashForecast._BalanceForecast = new BalanceForecast(checkingBalance, cashForecast);
            InsertForecast(cashForecast._BalanceForecast, "tblBalanceForecast", options);
            return cashForecast;
        }
        private static void InsertForecast(CashForecast cashForecast, string forecastType, Options options)
        {
            string cmdText;
            cmdText = "insert into " + forecastType + "(C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, C12, DateCalculated";
            if (forecastType != "tblBalanceForecast")
                cmdText += ", Average";
            if (options.AddOverages)
                cmdText += ", AddOverages, Overages";
            cmdText += ") values(" +
            cashForecast.C1 + ", " + cashForecast.C2 + ", " + cashForecast.C3 + ", " + cashForecast.C4 + ", " +
            cashForecast.C5 + ", " + cashForecast.C6 + ", " + cashForecast.C7 + ", " + cashForecast.C8 + ", " +
            cashForecast.C9 + ", " + cashForecast.C10 + ", " + cashForecast.C11 + ", " + cashForecast.C12 + ", " +
            "'" + DateTime.Today + "'";
            if (forecastType != "tblBalanceForecast")
                cmdText += ", " + cashForecast.Average;
            if (options.AddOverages)
                cmdText += "," + Convert.ToString(options.AddOverages) + "," + Convert.ToString(options.Overages);
            cmdText += ")";
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString();
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(connectionString))
            {
                myConnection.Open();
                using (OleDbCommand myCommand = new OleDbCommand(cmdText, myConnection))
                    myCommand.ExecuteNonQuery();
            }
        }
    }
    public class BalanceForecast : CashForecast
    {
        public BalanceForecast() { }
        public BalanceForecast(decimal currentBalance, CashForecast cashForecast)
        {
            for (int month = DateTime.Today.Month + 1; month < 13; month++)
            {
                currentBalance = CalculateBalance(currentBalance, month, cashForecast);
                UpdateBalance(currentBalance, month);
            }
            for (int month = 1; month < DateTime.Today.Month + 1; month++)
            {
                currentBalance = CalculateBalance(currentBalance, month, cashForecast);
                UpdateBalance(currentBalance, month);
            }

        }
        private decimal CalculateBalance(decimal currentBalance, int month, CashForecast cashForecast)
        {
            GainForecast gainForecast = cashForecast._GainForecast;
            switch (month)
            {
                case 1:
                    currentBalance = currentBalance + gainForecast.C12;
                    break;
                case 2:
                    currentBalance = currentBalance + gainForecast.C1;
                    break;
                case 3:
                    currentBalance = currentBalance + gainForecast.C2;
                    break;
                case 4:
                    currentBalance = currentBalance + gainForecast.C3;
                    break;
                case 5:
                    currentBalance = currentBalance + gainForecast.C4;
                    break;
                case 6:
                    currentBalance = currentBalance + gainForecast.C5;
                    break;
                case 7:
                    currentBalance = currentBalance + gainForecast.C6;
                    break;
                case 8:
                    currentBalance = currentBalance + gainForecast.C7;
                    break;
                case 9:
                    currentBalance = currentBalance + gainForecast.C8;
                    break;
                case 10:
                    currentBalance = currentBalance + gainForecast.C9;
                    break;
                case 11:
                    currentBalance = currentBalance + gainForecast.C10;
                    break;
                case 12:
                    currentBalance = currentBalance + gainForecast.C11;
                    break;
            }
            return Math.Round(currentBalance, 0);

        }
        private void UpdateBalance(decimal currentBalance, int month)
        {
            switch (month)
            {
                case 1:
                    C1 = currentBalance;
                    break;
                case 2:
                    C2 = currentBalance;
                    break;
                case 3:
                    C3 = currentBalance;
                    break;
                case 4:
                    C4 = currentBalance;
                    break;
                case 5:
                    C5 = currentBalance;
                    break;
                case 6:
                    C6 = currentBalance;
                    break;
                case 7:
                    C7 = currentBalance;
                    break;
                case 8:
                    C8 = currentBalance;
                    break;
                case 9:
                    C9 = currentBalance;
                    break;
                case 10:
                    C10 = currentBalance;
                    break;
                case 11:
                    C11 = currentBalance;
                    break;
                case 12:
                    C12 = currentBalance;
                    break;
            }
        }
    }
    public class IncomeForecast : CashForecast
    {
        public IncomeForecast()
        {
            int year = Convert.ToInt32(DateTime.Today.ToString("yyyy"));
            C1 = BudgetAmount(01, year, "Income", new Options());
            C2 = BudgetAmount(02, year, "Income", new Options());
            C3 = BudgetAmount(03, year, "Income", new Options());
            C4 = BudgetAmount(04, year, "Income", new Options());
            C5 = BudgetAmount(05, year, "Income", new Options());
            C6 = BudgetAmount(06, year, "Income", new Options());
            C7 = BudgetAmount(07, year, "Income", new Options());
            C8 = BudgetAmount(08, year, "Income", new Options());
            C9 = BudgetAmount(09, year, "Income", new Options());
            C10 = BudgetAmount(10, year, "Income", new Options());
            C11 = BudgetAmount(11, year, "Income", new Options());
            C12 = BudgetAmount(12, year, "Income", new Options());
            C13 = BudgetAmount(DateTime.Today.Month, year + 1, "Income", new Options());
            Total = C1 + C2 + C3 + C4 + C5 + C6 + C7 + C8 + C9 + C10 + C11 + C12 + C13;
            Total = SubtractPartialMonth(Total);
            Average = Math.Round(Total / 12);
        }

        private decimal SubtractPartialMonth(decimal total)
        {
            if (DateTime.Today.Month == 1)
                total -= C1;
            if (DateTime.Today.Month == 2)
                total -= C2;
            if (DateTime.Today.Month == 3)
                total -= C3;
            if (DateTime.Today.Month == 4)
                total -= C4;
            if (DateTime.Today.Month == 5)
                total -= C5;
            if (DateTime.Today.Month == 6)
                total -= C6;
            if (DateTime.Today.Month == 7)
                total -= C7;
            if (DateTime.Today.Month == 8)
                total -= C8;
            if (DateTime.Today.Month == 9)
                total -= C9;
            if (DateTime.Today.Month == 10)
                total -= C10;
            if (DateTime.Today.Month == 11)
                total -= C11;
            if (DateTime.Today.Month == 12)
                total -= C12;
            return total;
        }
    }
    public class SpendingForecast : CashForecast
    {
        public SpendingForecast(Options options)
        {
            int year = Convert.ToInt32(DateTime.Today.ToString("yyyy"));
            C1 = BudgetAmount(01, year, "Expenses", options);
            C2 = BudgetAmount(02, year, "Expenses", options);
            C3 = BudgetAmount(03, year, "Expenses", options);
            C4 = BudgetAmount(04, year, "Expenses", options);
            C5 = BudgetAmount(05, year, "Expenses", options);
            C6 = BudgetAmount(06, year, "Expenses", options);
            C7 = BudgetAmount(07, year, "Expenses", options);
            C8 = BudgetAmount(08, year, "Expenses", options);
            C9 = BudgetAmount(09, year, "Expenses", options);
            C10 = BudgetAmount(10, year, "Expenses", options);
            C11 = BudgetAmount(11, year, "Expenses", options);
            C12 = BudgetAmount(12, year, "Expenses", options);
            C13 = BudgetAmount(DateTime.Today.Month, year + 1, "Expenses", options);
            Total = C1 + C2 + C3 + C4 + C5 + C6 + C7 + C8 + C9 + C10 + C11 + C12 + C13;
            Total = SubtractPartialMonth(Total);
            Average = Math.Round(Total / 12);
        }

        private decimal SubtractPartialMonth(decimal total)
        {
            if (DateTime.Today.Month == 1)
                total -= C1;
            if (DateTime.Today.Month == 2)
                total -= C2;
            if (DateTime.Today.Month == 3)
                total -= C3;
            if (DateTime.Today.Month == 4)
                total -= C4;
            if (DateTime.Today.Month == 5)
                total -= C5;
            if (DateTime.Today.Month == 6)
                total -= C6;
            if (DateTime.Today.Month == 7)
                total -= C7;
            if (DateTime.Today.Month == 8)
                total -= C8;
            if (DateTime.Today.Month == 9)
                total -= C9;
            if (DateTime.Today.Month == 10)
                total -= C10;
            if (DateTime.Today.Month == 11)
                total -= C11;
            if (DateTime.Today.Month == 12)
                total -= C12;
            return total;
        }

    }
    public class GainForecast : CashForecast
    {
        public GainForecast(CashForecast cashForecast)
        {
            C1 = cashForecast._IncomeForecast.C1 - cashForecast._SpendingForecast.C1;
            C2 = cashForecast._IncomeForecast.C2 - cashForecast._SpendingForecast.C2;
            C3 = cashForecast._IncomeForecast.C3 - cashForecast._SpendingForecast.C3;
            C4 = cashForecast._IncomeForecast.C4 - cashForecast._SpendingForecast.C4;
            C5 = cashForecast._IncomeForecast.C5 - cashForecast._SpendingForecast.C5;
            C6 = cashForecast._IncomeForecast.C6 - cashForecast._SpendingForecast.C6;
            C7 = cashForecast._IncomeForecast.C7 - cashForecast._SpendingForecast.C7;
            C8 = cashForecast._IncomeForecast.C8 - cashForecast._SpendingForecast.C8;
            C9 = cashForecast._IncomeForecast.C9 - cashForecast._SpendingForecast.C9;
            C10 = cashForecast._IncomeForecast.C10 - cashForecast._SpendingForecast.C10;
            C11 = cashForecast._IncomeForecast.C11 - cashForecast._SpendingForecast.C11;
            C12 = cashForecast._IncomeForecast.C12 - cashForecast._SpendingForecast.C12;
            C13 = cashForecast._IncomeForecast.C13 - cashForecast._SpendingForecast.C13;
            Total = C1 + C2 + C3 + C4 + C5 + C6 + C7 + C8 + C9 + C10 + C11 + C12 + C13;
            Total = SubtractPartialMonth(Total);
            Average = Math.Round(Total / 12);
        }

        private decimal SubtractPartialMonth(decimal total)
        {
            if (DateTime.Today.Month == 1)
                total -= C1;
            if (DateTime.Today.Month == 2)
                total -= C2;
            if (DateTime.Today.Month == 3)
                total -= C3;
            if (DateTime.Today.Month == 4)
                total -= C4;
            if (DateTime.Today.Month == 5)
                total -= C5;
            if (DateTime.Today.Month == 6)
                total -= C6;
            if (DateTime.Today.Month == 7)
                total -= C7;
            if (DateTime.Today.Month == 8)
                total -= C8;
            if (DateTime.Today.Month == 9)
                total -= C9;
            if (DateTime.Today.Month == 10)
                total -= C10;
            if (DateTime.Today.Month == 11)
                total -= C11;
            if (DateTime.Today.Month == 12)
                total -= C12;
            return total;
        }

    }
}
public class Options
{
    public bool OverrideBalance;
    public decimal Balance;
    public bool AddOverages;
    public int Overages;

    public Options(
        bool overrideBalance,
        decimal balance,
        bool addOverages,
        int overages)
    {
        OverrideBalance = overrideBalance;
        Balance = balance;
        AddOverages = addOverages;
        Overages = overages;
    }

    public Options() { }
}