using System.Collections.Generic;
using System.Linq;
using CashiersLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CashierTests
{
    [TestClass]
    public class CustomerTests
    {
        [TestInitialize]
        public void Initialize()
        {
            //            _store = new Store(3);
        }

        [TestMethod]
        public void TestSortingByTime()
        {
            var cList = new List<Customer>
            {
                new CustomerA(10, 2),
                new CustomerA(1, 22),
                new CustomerB(5, 13)
            };

            cList.Sort();
            Assert.AreEqual(22, cList.First().CartCount);
            Assert.AreEqual(2, cList.Last().CartCount);
        }

        [TestMethod]
        public void TestSortingByCartCount()
        {
            var custs = new List<Customer>
            {
                new CustomerB(5, 12),
                new CustomerB(5, 1),
                new CustomerA(5, 55),
                new CustomerA(5, 7)
            };

            custs.Sort();
            Assert.AreEqual(1, custs.First().CartCount);

            Assert.AreEqual(55, custs.Last().CartCount);
        }

        [TestMethod]
        public void TestSortingByType()
        {
            var custs = new List<Customer>
            {
                new CustomerB(5, 12),
                new CustomerA(5, 12)
            };

            custs.Sort();

            Assert.IsTrue(custs.First() is CustomerA);
            Assert.IsTrue(custs.Last() is CustomerB);
        }

        [TestMethod]
        public void TestNullCashiers()
        {
            var noCashiers = new HashSet<ICashier>();

            var custA = new CustomerA(1, 10);
            var chosenA = custA.ChooseCashier(noCashiers);
            Assert.IsNull(chosenA);

            var custB = new CustomerA(1, 10);
            var chosenB = custB.ChooseCashier(noCashiers);
            Assert.IsNull(chosenB);
        }

        [TestMethod]
        public void TestAChoosingShortestLine()
        {
            var c1 = new Cashier(1);
            c1.EnqueueCustomer(new CustomerA(1, 10));
            c1.EnqueueCustomer(new CustomerB(2, 100));
            c1.EnqueueCustomer(new CustomerA(3, 8));

            var c2 = new Cashier(2);
            c2.EnqueueCustomer(new CustomerB(2, 10));
            c2.EnqueueCustomer(new CustomerB(2, 15));

            var c3 = new CashierTrainee(3);
            c3.EnqueueCustomer(new CustomerA(1, 9999));

            var cashiers = new HashSet<ICashier> {c1, c2, c3};

            var c = new CustomerA(3, 1);
            var chosenCashier = c.ChooseCashier(cashiers);
            Assert.AreEqual(c3, chosenCashier);
        }

        [TestMethod]
        public void TestAChoosingLowerNumberedLine()
        {
            var c1 = new Cashier(1);
            c1.EnqueueCustomer(new CustomerA(1, 10));
            c1.EnqueueCustomer(new CustomerB(2, 10));

            var c2 = new Cashier(2);
            c2.EnqueueCustomer(new CustomerB(1, 10));
            c2.EnqueueCustomer(new CustomerB(2, 10));

            var c3 = new CashierTrainee(3);
            c3.EnqueueCustomer(new CustomerA(1, 10));
            c3.EnqueueCustomer(new CustomerA(2, 10));

            var cashiers = new HashSet<ICashier> {c1, c2, c3};

            var c = new CustomerA(3, 1);
            var chosenCashier = c.ChooseCashier(cashiers);
            Assert.AreEqual(c1, chosenCashier);
        }

        [TestMethod]
        public void TestBChoosingEmptyLine()
        {
            var c1 = new Cashier(1);
            c1.EnqueueCustomer(new CustomerA(0, 10));

            var c2 = new Cashier(2);
            c2.EnqueueCustomer(new CustomerB(0, 2));

            var t = new CashierTrainee(3);

            var cashiers = new HashSet<ICashier> {c1, c2, t};

            var b = new CustomerB(1, 10);
            var chosenCashier = b.ChooseCashier(cashiers);
            ;
            Assert.AreEqual(t, chosenCashier);
        }

        [TestMethod]
        public void TestBChoosingLineWithFewestItemsLeftForLastPerson()
        {
            var c1 = new CashierTrainee(1);
            c1.EnqueueCustomer(new CustomerA(1, 10));
            c1.EnqueueCustomer(new CustomerB(2, 100));
            c1.EnqueueCustomer(new CustomerA(3, 8));

            var c2 = new Cashier(2);
            c2.EnqueueCustomer(new CustomerB(2, 10));
            c2.EnqueueCustomer(new CustomerB(2, 15));

            var c3 = new Cashier(3);
            c3.EnqueueCustomer(new CustomerA(1, 18));

            var cashiers = new HashSet<ICashier> {c1, c2, c3};

            var c = new CustomerB(4, 999);
            var chosenCashier = c.ChooseCashier(cashiers);
            Assert.AreEqual(c1, chosenCashier);
        }

    }
}