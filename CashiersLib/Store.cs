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
            for (int i = 0; i < cashierCount; i++)
            {
                _cashiers.Add(new Cashier(i, i == cashierCount - 1));
            }
        }

        public SortedSet<Cashier> Cashiers
        {
            get
            {
                return _cashiers;
            }
        }

        public int GetCompletionTime()
        {
            throw new System.NotImplementedException();
        }

        public bool EnqueueCustomer(ICustomer customer)
        {
            throw new System.NotImplementedException();
        }
    }
}