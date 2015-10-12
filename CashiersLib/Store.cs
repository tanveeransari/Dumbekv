﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CashiersLib
{
    public class Store : IStore
    {
        private readonly SortedSet<ICashier> _cashiers;

        private int _maxProcessingTimeFromStart;  //The answer to the big question

        private int _currentClockTime = 0;      // what minute are we in
        public Store(int cashierCount)
        {
            Debug.Assert(cashierCount > 0, "Invalid number of cashiers specified. Must be greater than zero");
            CashierIDGenerator.Reset();
            _cashiers = new SortedSet<ICashier>();
            for (int i = 0; i < cashierCount - 1; i++)
            {
                _cashiers.Add(new Cashier(CashierIDGenerator.NextCashierID));
            }
            _cashiers.Add(new CashierTrainee(CashierIDGenerator.NextCashierID));
        }

        public ISet<ICashier> Cashiers
        {
            get { return _cashiers; }
        }

        //returns time all customers are done processing
        public int EnqueueCustomers(List<Customer> custList)
        {
            if (custList == null || custList.Count == 0) return 0;

            int minuteIter = 0;
            var custByMinute = new List<Customer>();//new SortedSet<Customer>;

            var customers = custList.OrderBy(x => x.ArrivalTime).ToList();
            foreach (var customer in customers)
            {
                if (customer.ArrivalTime == minuteIter)
                {
                    custByMinute.Add(customer);
                }
                else
                {
                    if (custByMinute.Count > 0)
                    {
                        //Customer Sort handles customer arbitration for arrivals in the same minute
                        custByMinute.ForEach(x => EnqueueCustomer(x));
                        //foreach (var c in custByMinute)
                        //{
                        //    EnqueueCustomer(c);
                        //}
                        custByMinute.Clear();
                    }

                    custByMinute.Add(customer);
                    minuteIter = customer.ArrivalTime;
                }
                _currentClockTime = customer.ArrivalTime;
            }

            //Handle last minute customers
            if (custByMinute.Count > 0)
            {
                custByMinute.Sort();
                custByMinute.ForEach(x => EnqueueCustomer(x));
            }

            return _maxProcessingTimeFromStart;
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

        public override string ToString()
        {
            return string.Format("[Cashiers:{0}]  [Clock:{1}]  [MaxProcTime:{2}]",
                Cashiers.Count, _currentClockTime, _maxProcessingTimeFromStart);
        }

        static class CashierIDGenerator
        {
            private static int _cashierIDCtr = 0;
            internal static int NextCashierID
            {
                get
                {
                    return ++_cashierIDCtr;
                }
            }

            internal static void Reset()
            {
                _cashierIDCtr = 0;
            }
        }
    }
}