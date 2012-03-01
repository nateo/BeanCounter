using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace BeanCounter.BusinessLogic
{
    public class DatabaseProperties
    {

        internal static void SetPassword(string currentPassword, string newPassword, string confirmPassword)
        {
            //connectionString += @";Mode=Share Exclusive";
        }


        internal static bool PasswordProtected()
        {
            bool passwordProtected = true;
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString();
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(connectionString))
            {
                try
                {
                    myConnection.Open();
                }
                catch
                {
                    passwordProtected = false;
                }

            }
            return passwordProtected;
        }

        internal static bool PasswordIsCorrect(string password)
        {
            bool passwordIsCorrect = true;
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString();
            if (!string.IsNullOrEmpty(password))
                connectionString += @";Database Password = '" + password + "'";
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(connectionString))
            {
                try
                {
                    myConnection.Open();
                }
                catch
                {
                    passwordIsCorrect = false;
                }
            }
            return passwordIsCorrect;
        }
    }
}
