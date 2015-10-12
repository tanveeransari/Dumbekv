using System;
using CashiersLib;
using System.Linq;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CashierTests
{
    [TestClass]
    public class CashierTests
    {
        [TestMethod]
        public void TestNonTraineeProcessingTime()
        {
            var cashier = new Cashier(0);
            int completionTime = cashier.EnqueueCustomer(new CustomerA(0, 2));
            Assert.AreEqual(2, completionTime);

            completionTime = cashier.EnqueueCustomer(new CustomerB(1, 5));
            Assert.AreEqual(6, completionTime);
        }

        [TestMethod]
        public void TestTraineeProcessingTime()
        {
            var cashier = new CashierTrainee(0);
            int completionTime = cashier.EnqueueCustomer(new CustomerA(0, 2));
            Assert.AreEqual(4, completionTime);

            cashier.EnqueueCustomer(new CustomerB(1, 3));
            cashier.EnqueueCustomer(new CustomerA(1, 1));
            completionTime = cashier.EnqueueCustomer(new CustomerB(3, 10));
            Assert.AreEqual(29, completionTime);
        }

        [TestMethod]
        public void TestGetLineLength()
        {
            var cashier = new Cashier(0);
            cashier.EnqueueCustomer(new CustomerA(0, 10));
            cashier.EnqueueCustomer(new CustomerA(2, 5));
            cashier.EnqueueCustomer(new CustomerB(2, 3));
            int lineLength = cashier.UpdateAndGetQueueLength(2);
            Assert.AreEqual(3, lineLength);

            //lineLength = cashier.UpdateAndGetQueueLength(25);
            //Assert.AreNotEqual(0, lineLength);

            lineLength = cashier.UpdateAndGetQueueLength(36);
            Assert.AreEqual(0, lineLength);
        }



    }
}
