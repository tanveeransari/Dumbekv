﻿using System.Collections.Generic;
using System.Diagnostics;

namespace CashiersLib
{
    public class CustomerA : Customer
    {
        public CustomerA(int arrivalTime, int cartCount)
            : base(arrivalTime, cartCount)
        { }
        public override CustomerType CustomerType { get { return CustomerType.A; } }

        public override ICashier ChooseCashier(SortedSet<Cashier> cashiers)
        {
            if (cashiers == null || cashiers.Count == 0) return null;
            Cashier selectedCashier = null;
            int minLineLength = int.MaxValue;
            foreach (var cashier in cashiers)
            {
                int cashierLineLength = cashier.GetLineLength(ArrivalTime);
                if (cashierLineLength < minLineLength)
                {
                    minLineLength = cashierLineLength;
                    selectedCashier = cashier;
                }
            }

            Debug.Assert(selectedCashier != null, "no cashier selected by A");
            return selectedCashier;
        }
    }
}