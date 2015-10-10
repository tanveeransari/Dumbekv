using System;
using System.Collections.Generic;

namespace CashiersLib
{
    public interface ICashier : IComparable<ICashier>
    {
        int Id { get; }
        bool IsTrainee { get; }
        int GetLineLength();
        int GetCompletionTime();
        List<ICustomer> GetCustomersInLine();
        /// <summary></summary>
        /// <returns>New Completion Time</returns>
        int EnqueueCustomer(ICustomer customer);
    }
}