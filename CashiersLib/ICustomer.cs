using System;
using System.Collections.Generic;

namespace CashiersLib
{
    public interface ICustomer : IComparable<ICustomer>, IComparer<ICustomer>
    {
        int ArrivalTime { get; }

        int CartCount { get; }
        CustomerType CustomerType { get; }
        int Id { get; }

        ICashier ChooseCashier(ISet<ICashier> cashiers);
    }
}