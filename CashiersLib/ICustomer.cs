using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CashiersLib
{
    public interface ICustomer:IComparable<ICustomer>, IComparer<ICustomer>
    {
        int ArrivalTime { get;}
        //TODO: if going with the doubled solution change name to DoubleCartCount or something
        int CartCount { get;}
        CustomerType CustomerType { get; }
        int Id { get;}

        ICashier ChooseCashier(ISet<ICashier> cashiers);
    }
}