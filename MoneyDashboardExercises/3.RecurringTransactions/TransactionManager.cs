using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3.RecurringTransactions
{
    public class TransactionManager
    {
        private const int RECURRING_TRANSACTION_NUMBER = 2;
        public List<Transaction> GetRecurringTransaction(List<Transaction> transactions)
        {
            List<Transaction> recurringTransactions = new List<Transaction>();

            // get the transactions from the latest month
            var latestMonthTransactions =
                transactions
                .Where(tran => tran.TransactionDate.Month == DateTime.Now.Month)
                .ToList();

            var latestMonthUniqueTransactions = RemoveMatching(latestMonthTransactions);

            // get the transactions from months other than the last
            var otherMonthTransactions =
                transactions
                .Where(tran => tran.TransactionDate.Month != DateTime.Now.Month)
                .ToList();

            foreach (Transaction transaction in latestMonthUniqueTransactions)
            {
                int recurringCount = otherMonthTransactions.Count(other => other.IsMatching(transaction));

                if (recurringCount >= RECURRING_TRANSACTION_NUMBER)
                {
                    recurringTransactions.Add(transaction);
                }

                // remove transactions matching the current one 
                // because no other transaction would match them.
                otherMonthTransactions.RemoveAll(other => other.IsMatching(transaction));
            }

            return recurringTransactions;
        }

        private List<Transaction> RemoveMatching(List<Transaction> transactions)
        {
            // Order the transaction by date so we get the latest first
            // this is done in order to remove any of their matching transactions
            // and prevent removing the latest of the matching transactions
            List<Transaction> uniqueTransactions = transactions
                .OrderBy(tran => tran.TransactionDate)
                .ToList();

            foreach (Transaction transaction in transactions)
            {
                uniqueTransactions.RemoveAll(
                    unique => unique.IsMatching(transaction) &&
                    unique.TransactionId != transaction.TransactionId &&
                    unique.IsLatestInMonth == false);

                transaction.IsLatestInMonth = true;
            }

            return uniqueTransactions.ToList();
        }
    }
}
