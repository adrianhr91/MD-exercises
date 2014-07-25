using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _3.RecurringTransactions;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class RecurringTransactionsTests
    {
        private Transaction tran1; 
        private Transaction tran2;
        private Transaction tran3;
        private List<Transaction> transactions;

        [TestInitialize]
        public void SetUp()
        {
            tran1 = new Transaction();
            tran2 = new Transaction();
            tran3 = new Transaction();

            transactions = new List<Transaction> { tran1, tran2, tran3 };

            tran1.TransactionId = 1;
            tran2.TransactionId = 2;
            tran3.TransactionId = 3;
        }

        [TestMethod]
        public void OneRecurringTransaction_Test()
        {
            // Arrange
            tran1.TransactionDate = DateTime.Now;
            tran2.TransactionDate = DateTime.Now.AddMonths(-1);
            tran3.TransactionDate = DateTime.Now.AddMonths(-2);
            AddDescriptionToTransactions();
            TransactionManager manager = new TransactionManager();

            // Act
            var reccuring = manager.GetRecurringTransaction(transactions);

            // Assert
            Assert.AreEqual(1, reccuring.Count);
        }

        [TestMethod]
        public void NonConsecutiveButMatching_Test()
        {
            // Arrange
            tran1.TransactionDate = DateTime.Now;
            tran2.TransactionDate = DateTime.Now.AddMonths(-1);
            tran3.TransactionDate = DateTime.Now.AddMonths(-3);
            AddDescriptionToTransactions();
            TransactionManager manager = new TransactionManager();

            // Act
            var reccuring = manager.GetRecurringTransaction(transactions);

            // Assert
            Assert.AreEqual(0, reccuring.Count);
        }
  
        [TestMethod]
        public void OneRecurring2InLatestMonth_Test()
        {
            // Arrange
            tran1.TransactionDate = DateTime.Now;
            tran2.TransactionDate = DateTime.Now.AddMonths(-1);
            tran3.TransactionDate = DateTime.Now.AddMonths(-2);

            Transaction latestMonthOlderTran = new Transaction()
            {
                TransactionDate = DateTime.Now.AddDays(-1)
            };

            transactions.Add(latestMonthOlderTran);
            AddDescriptionToTransactions();
            TransactionManager manager = new TransactionManager();
            
            // Act 
            var reccuring = manager.GetRecurringTransaction(transactions);

            // Assert
            Assert.AreEqual(tran1.TransactionId, reccuring[0].TransactionId);
        }

        [TestMethod]
        public void TwoTransactionsMatching_Test()
        {
            // Arrange
            tran1.TransactionDate = DateTime.Now;
            tran2.TransactionDate = DateTime.Now;

            AddDescriptionToTransactions();

            // Assert
            Assert.IsTrue(tran1.IsMatching(tran2));
        }

        [TestMethod]
        public void TwoTransactionsMatching1DayRange_Test()
        {
            // Arrange
            tran1.TransactionDate = DateTime.Now;
            tran2.TransactionDate = DateTime.Now.AddDays(-1);

            AddDescriptionToTransactions();

            // Assert
            Assert.IsTrue(tran1.IsMatching(tran2));
        }

        [TestMethod]
        public void TwoTransactionsNotMatchingDates_Test()
        {
            // Arrange
            tran1.TransactionDate = DateTime.Now;
            tran2.TransactionDate = DateTime.Now.AddDays(5);

            AddDescriptionToTransactions();

            // Assert
            Assert.IsFalse(tran1.IsMatching(tran2));
        }

        private void AddDescriptionToTransactions()
        {
            foreach (var tran in transactions)
            {
                tran.Description = "description";
            }
        }
    }
}
