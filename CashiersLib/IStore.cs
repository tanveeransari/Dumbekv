using System.Collections.Generic;

namespace CashiersLib
{
    public interface IStore
    {
        int EnqueueCustomers(List<Customer> customers);
    }
}