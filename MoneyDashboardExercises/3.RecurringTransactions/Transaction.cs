using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3.RecurringTransactions
{
    public class Transaction
    {
        private const int DATE_RANGE = 3;

        public long TransactionId { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }

        public bool IsDebit { get; set; }

        public DateTime TransactionDate { get; set; }

        public int? TagId { get; set; }

        public bool IsLatestInMonth { get; set; }

        public bool IsMatching(Transaction anotherTransaction)
        {
            bool isMatching = false;

            bool isDescriptionMatch = IsDescriptionMatching(anotherTransaction);
            bool isDateInRange = IsDateInRange(this.TransactionDate, anotherTransaction.TransactionDate);

            if (isDescriptionMatch && isDateInRange)
            {
                isMatching = true;
            }

            return isMatching;
        }
  
        private bool IsDescriptionMatching(Transaction anotherTransaction)
        {
            bool isDescriptionMatch = this.Description.Equals(anotherTransaction.Description);

            return isDescriptionMatch;
        }

        private bool IsDateInRange(DateTime transDate1, DateTime transDate2)
        {
            bool isInRange = false;

            var trans1Range = GetDateRange(transDate1);
            var trans2Range = GetDateRange(transDate2);

            // check if any of the two dates' days of month are in the "range"
            // of the other one.
            if (trans1Range.Any(d => d.Day == transDate2.Day))
            {
                isInRange = true;
            }
            else if (trans2Range.Any(d => d.Day == transDate1.Day))
            {
                isInRange = true;
            }

            return isInRange;
        }

        private List<DateTime> GetDateRange(DateTime date)
        {
            List<DateTime> range = new List<DateTime> { date };

            for (int i = 1; i <= DATE_RANGE; i++)
            {
                DateTime adjacentDayAfter = date.AddDays(i);
                DateTime adjacentDayBefore = date.AddDays(-i);
                range.Add(adjacentDayAfter);
                range.Add(adjacentDayBefore);
            }

            return range;
        }
    }
}
