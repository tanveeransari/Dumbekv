using System;
using System.Collections.Generic;

namespace CashiersLib
{
    public interface ICashier : IComparable<ICashier>, IComparer<ICashier>
    {
        int Id { get; }

        int RateOfWork { get; }

        int EnqueueCustomer(Customer customer);

        int CalculateQueueLength(int minute);

        int GetLastCustomerCartCount();
    }
}