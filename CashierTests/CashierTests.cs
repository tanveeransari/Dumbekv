using System;
using CashiersLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CashierTests
{
    [TestClass]
    public class CashierTests
    {
        [TestMethod]
        public void TestNonTraineeProcessingTime()
        {
            var cashier = new Cashier(0);
            int completionTime = cashier.EnqueueCustomer(new CustomerA(0, 1));
            Assert.AreEqual(1, completionTime);
        }

        [TestMethod]
        public void TestTraineeProcessingTime()
        {
            var cashier = new CashierTrainee(0);
            int completionTime = cashier.EnqueueCustomer(new CustomerA(0, 1));
            Assert.AreEqual(2, completionTime);
        }

        [TestMethod]
        public void TestGetLineLength()
        {
        }

        [TestMethod]
        public void GetNumberOfCustomersInLine()
        {
        }

        [TestMethod]
        public void TestSorting()
        {
        }
    }
}
