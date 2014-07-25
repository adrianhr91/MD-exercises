using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5._90DaysGraph;
using _3.RecurringTransactions;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class NinetyDaysGraph
    {
        [TestMethod]
        public void ParseTransactionLog_ParseNotFailing_Test()
        {
            // Arrange
            UserAccount acc = new UserAccount();

            // Act
            List<Transaction> transactions = TransactionParser.ParseTransactions(acc.TransactionLog);

            //Assert
            Assert.AreEqual(50, transactions.Count);
        }
    }
}
