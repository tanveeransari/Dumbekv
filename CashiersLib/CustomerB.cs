using System;
using System.Collections.Generic;

namespace CashiersLib
{
    public class CustomerB : Customer
    {
        public CustomerB(int arrivalTime, int cartCount)
            : base(arrivalTime, cartCount)
        { }

        public override CustomerType CustomerType { get { return CustomerType.B; } }

        public override ICashier ChooseCashier(ISet<ICashier> cashiers)
        {
            int minCustomerCartCount = int.MaxValue;
            ICashier selectedCashier = null;
            foreach (var cashier in cashiers)
            {
                if (cashier.CalculateQueueLength(ArrivalTime) == 0) return cashier;
                int cartCount = cashier.GetLastCustomerCartCount();
                if (cartCount < minCustomerCartCount)
                {
                    minCustomerCartCount = cartCount;
                    selectedCashier = cashier;
                }
            }

            return selectedCashier;
        }
    }
}