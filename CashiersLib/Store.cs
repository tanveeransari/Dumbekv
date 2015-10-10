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

        public SortedSet<Cashier> Cashiers { get { return _cashiers; } }

        public int GetCompletionTime()
        {
            //Must calculate max completiontime
            int completionTime = 0;
            foreach (var cshr in _cashiers)
            {
                completionTime = Math.Max(0, cshr.GetFinalCompletionTime());
            }
            return completionTime;
        }

        public bool EnqueueCustomer(ICustomer customer)
        {
            throw new System.NotImplementedException();
            //needs to decide which customer to ask to choose first

            //for each customer in orderedcustomerschoose
        }
    }
}