using System;
using System.Collections.Generic;

namespace CashiersLib
{
    public interface ICashier : IComparable<ICashier>, IComparer<ICashier>
    {
        int Id { get; }

        // returns New Completion Time beginning from this customer's arrival time 
        int EnqueueCustomer(Customer customer);

        int RateOfWork { get; }

        int UpdateAndGetQueueLength(int minute);

        int GetLastCustomerCartCount();
    }
}