using System.Collections.Generic;
using System.Threading;

namespace CashiersLib
{
    public abstract class Customer : ICustomer
    {
        private const int UNITS_OF_WORK_PER_CART_ITEM = 2;

        // maintain a count so we don't have to an O(N) operation to get queue length
        private static int _customerIdCounter;

        private readonly int _arrivalTime;
        private readonly int _id;

        private int _workUnits;

        protected Customer(int arrivalTime, int cartCount)
        {
            _id = Interlocked.Increment(ref _customerIdCounter);
            _arrivalTime = arrivalTime;
            // Divide each cart item into two work units - to avoid fractional work units for trainee
            _workUnits = cartCount*UNITS_OF_WORK_PER_CART_ITEM;
        }

        public int WorkUnits
        {
            get { return _workUnits; }
            set { _workUnits = value; }
        }


        public int ArrivalTime
        {
            get { return _arrivalTime; }
        }

        public int CartCount
        {
            get { return _workUnits/UNITS_OF_WORK_PER_CART_ITEM; }
        }

        public abstract CustomerType CustomerType { get; }

        public int Id
        {
            get { return _id; }
        }


        public abstract ICashier ChooseCashier(ISet<ICashier> cashiers);

        public int CompareTo(ICustomer other)
        {
            //those with fewer items choose registers before those with more, and if they have the same number of items then type A's choose before type B's.
            return Compare(this, other);
        }

        public int Compare(ICustomer x, ICustomer y)
        {
            if (x.ArrivalTime != y.ArrivalTime) return x.ArrivalTime.CompareTo(y.ArrivalTime);
            if (x.CartCount != y.CartCount) return x.CartCount.CompareTo(y.CartCount);

            return x.CustomerType.CompareTo(y.CustomerType);
        }

        public override string ToString()
        {
            return string.Format("Time:{2} {1} Cart:{3}  ID:{0}",
                _id, CustomerType, ArrivalTime, CartCount);
        }
    }
}