using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CashiersLib
{
    public class Store : IStore
    {
        private readonly SortedSet<ICashier> _cashiers;

        private int _maxProcessingTimeFromStart; //The answer to the big question

        public Store(int cashierCount)
        {
            Debug.Assert(cashierCount > 0, "Invalid number of cashiers specified. Must be greater than zero");
            CashierIdGenerator.Reset();
            _cashiers = new SortedSet<ICashier>();
            for (int i = 0; i < cashierCount - 1; i++)
            {
                _cashiers.Add(new Cashier(CashierIdGenerator.NextCashierId));
            }
            _cashiers.Add(new CashierTrainee(CashierIdGenerator.NextCashierId));
        }

        /// <summary>
        /// Main worker function
        /// </summary>
        /// <param name="custList">List of customers</param>
        /// <returns>Time at which last customer processing completes</returns>
        public int EnqueueCustomers(List<Customer> custList)
        {
            if (custList == null || custList.Count == 0) return 0;

            int arrivalTimeIter = 0;
            var custByMinute = new List<Customer>();

            var customers = custList.OrderBy(x => x.ArrivalTime).ToList();

            // Could do next block and above line with LINQ in one statement - but lets do this longer version for readability
            foreach (var customer in customers)
            {
                if (customer.ArrivalTime == arrivalTimeIter)
                {
                    custByMinute.Add(customer);
                }
                else
                {
                    EnqueueCustomersArrivingInTheSameMinute(custByMinute);

                    custByMinute.Add(customer);
                    arrivalTimeIter = customer.ArrivalTime;
                }
            }

            // Handle last minute customers
            EnqueueCustomersArrivingInTheSameMinute(custByMinute);

            return _maxProcessingTimeFromStart;
        }

        private void EnqueueCustomersArrivingInTheSameMinute(List<Customer> custByMinute)
        {
            if (custByMinute.Any())
            {
                // Customer Sort handles customer arbitration for arrivals in the same minute
                custByMinute.Sort();
                custByMinute.ForEach(x => EnqueueCustomer(x));
                custByMinute.Clear();
            }
        }

        private bool EnqueueCustomer(Customer customer)
        {
            ICashier selectedCashier = customer.ChooseCashier(_cashiers);
            if (selectedCashier == null) return false;

            int cashierRemainingProcessingTime = selectedCashier.EnqueueCustomer(customer);
            if (_maxProcessingTimeFromStart == 0)
            {
                _maxProcessingTimeFromStart = customer.ArrivalTime + cashierRemainingProcessingTime;
            }
            else
            {
                if ((customer.ArrivalTime + cashierRemainingProcessingTime) > _maxProcessingTimeFromStart)
                {
                    _maxProcessingTimeFromStart = customer.ArrivalTime + cashierRemainingProcessingTime;
                }
            }
            return true;
        }


        //public ISet<ICashier> Cashiers
        //{
        //    get { return _cashiers; }
        //}

        private static class CashierIdGenerator
        {
            private static int _cashierIdCtr;

            internal static int NextCashierId
            {
                get { return ++_cashierIdCtr; }
            }

            internal static void Reset()
            {
                _cashierIdCtr = 0;
            }
        }
    }
}