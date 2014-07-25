using _3.RecurringTransactions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5._90DaysGraph
{
    public static class TransactionParser
    {
        public static List<Transaction> ParseTransactions(DataTable transactionLog)
        {
            List<Transaction> transactions = new List<Transaction>();

            foreach (DataRow transactionRow in transactionLog.Rows)
            {
                Transaction tran = ParseTransaction(transactionRow);
                transactions.Add(tran);
            }

            return transactions;
        }

        public static Transaction ParseTransaction(DataRow transactionRow)
        {
            Transaction tran = new Transaction();

            if (transactionRow["TransactionId"] != null)
            {
                long id;
                long.TryParse(transactionRow["TransactionId"].ToString(), out id);
                tran.TransactionId = id;
            }

            if (transactionRow["Date"] != null)
            {
                DateTime date;
                DateTime.TryParse(transactionRow["Date"].ToString(), out date);
                tran.TransactionDate = date;
            }

            if (transactionRow["Amount"] != null)
            {
                decimal amount;
                decimal.TryParse(transactionRow["Amount"].ToString(), out amount);
                tran.Amount = amount;
            }

            if (transactionRow["IsDebit"] != null)
            {
                bool isDebit;
                bool.TryParse(transactionRow["IsDebit"].ToString(), out isDebit);
                tran.IsDebit = isDebit;
            }

            if (transactionRow["TransactionDescription"] != null)
            {
                tran.Description = transactionRow["TransactionDescription"] as string;
            }

            return tran;
        }
    }

}
