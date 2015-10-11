using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CashiersLib
{
    public interface ICustomer:IComparable<ICustomer>, IComparer<ICustomer>
    {
        int ArrivalTime { get;}
        int CartCount { get; set;}
        CustomerType CustomerType { get; }
        int Id { get;}

        ICashier ChooseCashier(SortedSet<Cashier> cashiers);
    }
}