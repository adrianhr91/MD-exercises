using System;
using System.Linq;
using System.Data;

namespace _5._90DaysGraph
{
    public class UserAccount
    {
        public DataTable TransactionLog { get; set; }

        public decimal Balance { get; set; }

        public UserAccount()
        {
            TransactionLog = new DataTable("TransactionLog");
            TransactionLog.Columns.Add("TransactionId", typeof(long));
            TransactionLog.Columns.Add("Date", typeof(DateTime));
            TransactionLog.Columns.Add("Amount", typeof(decimal));
            TransactionLog.Columns.Add("IsDebit", typeof(bool));
            TransactionLog.Columns.Add("TransactionDescription", typeof(string));

            GenerateTransactionHistory();
        }

        private void GenerateTransactionHistory()
        {
            // fill with example data
            for (int i = 0; i < 50; i++)
            {
                TransactionLog.Rows.Add(
                    i,
                    DateTime.Now.AddMonths(i),
                    (decimal) new Random().Next(0, 600),
                    true,
                    "description" + i);
            }
        }
    }
}
