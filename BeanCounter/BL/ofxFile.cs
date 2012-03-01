using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeanCounter.BusinessLogic
{
    public class ofxFile
    {

        public BankAccount BankAccount;
        public List<Transaction> Transactions = new List<Transaction>();

        public ofxFile() { }

        public ofxFile(BankAccount bankAccount, List<Transaction> transaction)
        {
            BankAccount = bankAccount;
            Transactions = transaction;
        }

        public static ofxFile LoadOFXfile(string filePath)
        {
            string fileContents;
            using (System.IO.StreamReader streamReader = new System.IO.StreamReader(filePath))
                fileContents = streamReader.ReadToEnd();
            ofxFile ofxfile = new ofxFile(FindBankAccount(fileContents),
                GetTransactions(fileContents));
            return ofxfile;
        }

        internal static string ExtractText(string fileContents, string fieldName, int currentPosition)
        {
            string fieldData = "";
            int xStart = fileContents.IndexOf(fieldName, currentPosition);
            int xEnd = fileContents.Substring(xStart + fieldName.Length).IndexOf("<") + xStart;
            fieldData = fileContents.Substring(xStart + fieldName.Length, xEnd - xStart);
            if (fieldData.Contains(Environment.NewLine))
                fieldData = fieldData.Replace(Environment.NewLine, "");
            if (fieldData.Contains("\r"))
                fieldData = fieldData.Replace("\r", "");
            if (fieldData.Contains("\n"))
                fieldData = fieldData.Replace("\n", "");
            if (fieldData.Contains("\t"))
                fieldData = fieldData.Replace("\t", "");
            return fieldData;
        }

        private static List<Transaction> GetTransactions(string fileContents)
        {
            List<Transaction> transactions = new List<Transaction>();
            int currentPosition = fileContents.IndexOf("<TRNTYPE>") - 1;
            int endPosition = fileContents.LastIndexOf("<TRNTYPE>") - 1;
            int transactionNumber = 0;
            while (transactionNumber != NumberOfTransactions(fileContents))
            {
                Transaction transaction = new Transaction();
                transaction.TransactionType = ExtractText(fileContents, "<TRNTYPE>", currentPosition);
                if (transaction.TransactionType.ToUpper() == "CHECK")
                    transaction.CheckNumber = ExtractText(fileContents, "<CHECKNUM>", currentPosition);
                transaction.TransactionDate = ExtractDate(fileContents, "<DTPOSTED>", currentPosition);
                transaction.TransactionAmount = Convert.ToDecimal(ExtractText(fileContents, "<TRNAMT>", currentPosition));
                transaction.TransactionID = ExtractText(fileContents, "<FITID>", currentPosition);
                string merchantName = ExtractText(fileContents, "<NAME>", currentPosition);
                merchantName = merchantName.Replace("&amp;", "");
                transaction.MerchantName = merchantName.Trim();
                string bankMemo = "";
                if (fileContents.Contains("<MEMO>"))
                    bankMemo = ExtractText(fileContents, "<MEMO>", currentPosition).Replace("&amp;", "").Trim();
                transaction.BankMemo = bankMemo;
                currentPosition = fileContents.IndexOf("<NAME>", currentPosition);
                currentPosition = fileContents.IndexOf("<TRNTYPE>", currentPosition) - 1;
                transactions.Add(transaction);
                transactionNumber += 1;
            }

            return transactions;
        }

        private static DateTime ExtractDate(string fileContents, string fieldName, int currentPosition)
        {
            string fieldData = ExtractText(fileContents, fieldName, currentPosition);
            DateTime thisDate = Convert.ToDateTime(fieldData.Substring(0, 4) + "/" +
                fieldData.Substring(4, 2) + "/" +
                fieldData.Substring(6, 2));
            return thisDate;
        }

        private static int NumberOfTransactions(string text)
        {
            int count = 0;
            int i = 0;
            while ((i = text.IndexOf("<TRNTYPE>", i)) != -1)
            {
                i += "<TRNTYPE>".Length;
                count++;
            }
            return count;
        }

        internal static BankAccount FindBankAccount(string fileContents)
        {
            string accountNumber = ofxFile.ExtractText(fileContents, "<ACCTID>", 1);
            string bankName = ofxFile.ExtractText(fileContents, "<ORG>", 1).ToString().Replace(@",", " ");
            string bankFID = ofxFile.ExtractText(fileContents, "<FID>", 1);
            string accountType = "";
            if (fileContents.ToUpper().Contains("BANKACCTFROM"))
            {
                accountType = ofxFile.ExtractText(fileContents, "<ACCTTYPE>", 1);
            }
            else if (fileContents.ToUpper().Contains("</CCACCTFROM"))
                accountType = "CREDIT";
            decimal onlineBalance = Convert.ToDecimal(ofxFile.ExtractText(fileContents, "<BALAMT>", 1));
            BankAccount bankAccount = new BankAccount(
                accountNumber,
                bankName,
                bankFID,
                accountType,
                onlineBalance);
            return bankAccount;
        }




    }
}
