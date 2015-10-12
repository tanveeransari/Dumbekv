using System.Collections.Generic;

namespace CashiersLib
{
    public interface IStore
    {
        ISet<ICashier> Cashiers { get; }

        int EnqueueCustomers(List<Customer> customers);
    }
}