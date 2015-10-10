using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CashiersLib
{
    public abstract class Customer : ICustomer
    {
        private static int _customerIdCounter = 0;

        private readonly int _id;
        private readonly int _arrivalTime;
        private int _cartCount;

        public Customer(int arrivalTime, int cartCount)
        {
            _id = Interlocked.Increment(ref _customerIdCounter);
            _arrivalTime = arrivalTime;
            _cartCount = cartCount;
        }

        #region Properties
        public int ArrivalTime
        {
            get
            {
                return _arrivalTime;
            }
        }

        public int CartCount
        {
            get
            {
                return _cartCount;
            }
            set
            {
                _cartCount = value;
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
                return _id;
            }
        }
        #endregion

        public abstract ICashier ChooseCashier(SortedSet<Cashier> cashiers);

        #region IComparable<ICustomer> Members

        public int CompareTo(ICustomer other)
        {
            //those with fewer items choose registers before those with more, and if they have the same number of items then type A's choose before type B's.
            if (CartCount != other.CartCount)
            {
                return _cartCount.CompareTo(other.CartCount);
            }
            else
            {
                return this.CustomerType.CompareTo(other.CustomerType);
            }
        }

        #endregion
    }

    public class CustomerA : Customer
    {
        public CustomerA(int arrivalTime, int cartCount)
            : base(arrivalTime, cartCount)
        { }
        public override CustomerType CustomerType { get { return CashiersLib.CustomerType.A; } }

        public override ICashier ChooseCashier(SortedSet<Cashier> cashiers)
        {
            //var shortestLines = from c in cashiers orderby c.GetLineLength select c;
            throw new NotImplementedException();
        }
    }

    public class CustomerB : Customer
    {
        public CustomerB(int arrivalTime, int cartCount)
            : base(arrivalTime, cartCount)
        { }

        public override CustomerType CustomerType { get { return CashiersLib.CustomerType.B; } }

        public override ICashier ChooseCashier(SortedSet<Cashier> cashiers)
        {
            throw new NotImplementedException();
        }
    }
}