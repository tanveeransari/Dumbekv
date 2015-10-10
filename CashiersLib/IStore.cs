using System.Collections.Generic;

namespace CashiersLib
{
    public interface IStore
    {
        SortedSet<Cashier> Cashiers { get; }

        bool EnqueueCustomer(ICustomer customer);

        int GetCompletionTime();
    }
}