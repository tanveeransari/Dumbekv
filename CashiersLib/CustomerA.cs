using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CashiersLib
{
    public abstract class Customer : ICustomer
    {
        public int ArrivalTime
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int CartCount
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public abstract CustomerType CustomerType
        {
            get;
        }

        public int Id
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        int ICustomer.ArrivalTime
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public ICashier ChooseCashier(SortedSet<Cashier> cashiers)
        {
            throw new NotImplementedException();
        }
    }
}