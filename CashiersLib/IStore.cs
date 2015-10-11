using System.Collections.Generic;

namespace CashiersLib
{
    public interface IStore
    {
        SortedSet<Cashier> Cashiers { get; }

        bool EnqueueCustomers(List<Customer> customers);

        int CompletionTime { get; }
    }
}