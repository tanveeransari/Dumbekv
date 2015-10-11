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

        public Store(int cashierCount)
        {
            Debug.Assert(cashierCount > 0, "Invalid number of cashiers specified. Must be greater than zero");
            _cashiers = new SortedSet<Cashier>();
            for (int i = 0; i < cashierCount-1; i++)
            {
                _cashiers.Add(new Cashier(i));
            }
            _cashiers.Add(new TraineeCashier(cashierCount-1));

        }

        public SortedSet<Cashier> Cashiers
        {
            get { return _cashiers; }
        }

        public int CompletionTime
        {
            get { return _latestCompletionTime; }
        }

        private int _latestCompletionTime;
        public bool EnqueueCustomers(List<Customer> customers)
        {
            if (customers == null || customers.Count == 0) return false;
            
            customers = customers.OrderBy(x => x.ArrivalTime).ToList();
            int currMinute = customers.First().ArrivalTime;

            var customersInSameMinute = new SortedSet<Customer>();
            foreach (var customer in customers)
            {
                if (customer.ArrivalTime == currMinute)
                {
                    customersInSameMinute.Add(customer);
                }
                else
                {
                    //Customer sort takes care of which customer gets to choose first
                    foreach (var c in customersInSameMinute)
                    {
                        EnqueueCustomer(c);
                    }
                    customersInSameMinute.Clear();

                    customersInSameMinute.Add(customer);
                    currMinute = customer.ArrivalTime;
                }
            }

            foreach (var c in customersInSameMinute)
            {
                EnqueueCustomer(c);
            }

            //for each customer in orderedcustomerschoose

            return true;
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