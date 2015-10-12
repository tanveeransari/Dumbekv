using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CashiersLib;
using System.IO;
using System.Collections.Generic;

namespace CashierTests
{
    [TestClass]
    public class StoreTests
    {
        [TestMethod]
        public void Test1()
        {
            string inputText = @"1
A 1 2
A 2 1";

            int completionTime = ProcessTextInput(inputText);
            Assert.AreEqual(7, completionTime);
        }

        [TestMethod]
        public void Test2()
        {
            string inputText = @"2
A 1 5
B 2 1
A 3 5
B 5 3
A 8 2";
            int completionTime = ProcessTextInput(inputText);
            Assert.AreEqual(13, completionTime);
        }

        [TestMethod]
        public void Test3()
        {
            string inputText = @"2
A 1 2
A 1 2
A 2 1
A 3 2";
            int completionTime = ProcessTextInput(inputText);
            Assert.AreEqual(6, completionTime);
        }

        [TestMethod]
        public void Test4()
        {
            string inputText = @"2
A 1 2
A 1 3
A 2 1
A 2 1";
            int completionTime = ProcessTextInput(inputText);
            Assert.AreEqual(9, completionTime);
        }

        [TestMethod]
        public void Test5()
        {
            string inputText = @"2
A 1 3
A 1 5
A 3 1
B 4 1
A 4 1";
            int completionTime = ProcessTextInput(inputText);
            Assert.AreEqual(11, completionTime);
        }
        private int ProcessTextInput(string inputText)
        {
            if (string.IsNullOrWhiteSpace(inputText))
            {
                return 0;
            }

            int numCashier;
            var customers = new List<Customer>();
            using (var s = new StringReader(inputText))
            {
                numCashier = int.Parse(s.ReadLine());
                string custLine;
                while ((custLine = s.ReadLine()) != null)
                {
                    Customer cust = CustomerFactory.CreateCustomer(custLine);
                    if (cust != null) customers.Add(cust);
                }
            }

            var store = new Store(numCashier);
            return store.EnqueueCustomers(customers);
        }

    }
}
