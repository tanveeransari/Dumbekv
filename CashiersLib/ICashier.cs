using System;
using System.Collections.Generic;

namespace CashiersLib
{
    public interface ICashier : IComparable<ICashier>
    {
        int Id { get; }
        //bool IsTrainee { get; }
        
        // returns New Completion Time 
        int EnqueueCustomer(ICustomer customer);
        int GetLineLength(int minute);
        int GetNumberOfCustomersInLine(int minute);
        //List<ICustomer> GetCustomersInLine();
        int GetLastCustomerCartCount();
    }
}