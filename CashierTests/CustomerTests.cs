using System;
using CashiersLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CashierTests
{
    [TestClass]
    public class CustomerTests
    {
        private ICustomer _customer;

        [TestInitialize]
        public void Initialize()
        { 
        
        }

        [TestMethod]
        public void TestBChoosingEmptyLine()
        {
        }

        [TestMethod]
        public void TestBChoosingLineWithFewestItemsLeftForLastPerson()
        {
        }

        [TestMethod]
        public void TestAChoosingShortestLine()
        {
        }

        [TestMethod]
        public void TestSorting()
        {
            //this ought to be for both A and B
        }
    }
}
