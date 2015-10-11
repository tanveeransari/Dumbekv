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

        
        public int CompareTo(ICustomer other)
        {
            //those with fewer items choose registers before those with more, and if they have the same number of items then type A's choose before type B's.
            return Compare(this, other);
        }

        
        public int Compare(ICustomer x, ICustomer y)
        {
            if (x.CartCount != y.CartCount) return x.CartCount.CompareTo(y.CartCount);

            return x.CustomerType.CompareTo(y.CustomerType);
        }
    }
}