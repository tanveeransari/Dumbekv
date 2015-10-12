using System.Collections.Generic;
using System.IO;
using CashiersLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CashierTests
{
    [TestClass]
    public class StoreTests
    {
        [TestMethod]
        public void Test1()
        {
            const string inputText = @"1
A 1 2
A 2 1";

            var completionTime = ProcessTextInput(inputText);
            Assert.AreEqual(7, completionTime);
        }

        [TestMethod]
        public void Test2()
        {
            const string inputText = @"2
A 1 5
B 2 1
A 3 5
B 5 3
A 8 2";
            var completionTime = ProcessTextInput(inputText);
            Assert.AreEqual(13, completionTime);
        }

        [TestMethod]
        public void Test3()
        {
            const string inputText = @"2
A 1 2
A 1 2
A 2 1
A 3 2";
            var completionTime = ProcessTextInput(inputText);
            Assert.AreEqual(6, completionTime);
        }

        [TestMethod]
        public void Test4()
        {
            const string inputText = @"2
A 1 2
A 1 3
A 2 1
A 2 1";
            var completionTime = ProcessTextInput(inputText);
            Assert.AreEqual(9, completionTime);
        }

        [TestMethod]
        public void Test5()
        {
            const string inputText = @"2
A 1 3
A 1 5
A 3 1
B 4 1
A 4 1";
            var completionTime = ProcessTextInput(inputText);
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
                    var cust = CustomerFactory.CreateCustomer(custLine);
                    if (cust != null) customers.Add(cust);
                }
            }

            var store = new Store(numCashier);
            return store.EnqueueCustomers(customers);
        }

        [TestMethod]
        public void TestNoCustomers()
        {

            var store = new Store(4);
            int completionTime = store.EnqueueCustomers(new List<Customer>());
            Assert.AreEqual(0, completionTime);
        }
    }
}