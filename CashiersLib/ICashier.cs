using System;
using System.Collections.Generic;

namespace CashiersLib
{
    public interface ICashier : IComparable<ICashier>,IComparer<ICashier>
    {
        int Id { get; }
        //bool IsTrainee { get; }
        
        // returns New Completion Time 
        int EnqueueCustomer(ICustomer customer);
        int UpdateAndGetQueueLength(int minute);

        int GetLastCustomerCartCount();
    }
}