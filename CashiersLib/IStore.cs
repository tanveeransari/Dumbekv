using System.Collections.Generic;

namespace CashiersLib
{
    public interface IStore
    {
        ISet<Cashier> Cashiers { get; }

        int EnqueueCustomers(List<ICustomer> customers);
    }
}