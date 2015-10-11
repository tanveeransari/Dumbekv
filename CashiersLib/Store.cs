using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CashiersLib
{
    public class Store : IStore
    {
        private readonly SortedSet<Cashier> _cashiers;
        private int _latestCompletionTime;  //The answer to the big question
        private int _currentClockTime = 0;      // what minute are we in
        public Store(int cashierCount)
        {
            Debug.Assert(cashierCount > 0, "Invalid number of cashiers specified. Must be greater than zero");
            _cashiers = new SortedSet<Cashier>();
            for (int i = 0; i < cashierCount - 1; i++)
            {
                _cashiers.Add(new Cashier(i));
            }
            _cashiers.Add(new CashierTrainee(cashierCount - 1));
        }

        public ISet<Cashier> Cashiers
        {
            get { return _cashiers; }
        }

        //returns time all customers are done processing
        public int EnqueueCustomers(List<ICustomer> custList)
        {
            if (custList == null || custList.Count == 0) return 0;

            int minuteIter = 0;
            var custByMinute = new SortedSet<ICustomer>();

            var customers = custList.OrderBy(x => x.ArrivalTime).ToList();
            foreach (var customer in customers)
            {
                if (customer.ArrivalTime == minuteIter)
                {
                    custByMinute.Add(customer);
                }
                else
                {
                    //Customer sort takes care of which customer gets to choose first
                    foreach (var c in custByMinute)
                    {
                        EnqueueCustomer(c);
                    }
                    custByMinute.Clear();

                    custByMinute.Add(customer);
                    minuteIter = customer.ArrivalTime;
                }
            }
            
            //Handle last minute customers
            foreach (var c in custByMinute)
            {
                EnqueueCustomer(c);
            }

            Debug.WriteLine("Last customer arrived at {0} = currentClockTime. Latest Completion Time {1}",
                _currentClockTime, _latestCompletionTime);

            return _currentClockTime + _latestCompletionTime;
        }

        private bool EnqueueCustomer(ICustomer customer)
        {
            ICashier selectedCashier = customer.ChooseCashier(_cashiers);
            if (selectedCashier == null) return false;

            int cashierNewCompletionTime = selectedCashier.EnqueueCustomer(customer);
            if (cashierNewCompletionTime > _latestCompletionTime)
            {
                _latestCompletionTime = cashierNewCompletionTime;
            }
            return true;
        }

    }
}