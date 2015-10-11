using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CashiersLib
{
    public class Cashier : ICashier
    {
        private readonly int _id;

        protected readonly LinkedList<ICustomer> _customersToServe;
        protected int _numCustomers = 0;

        private int _lastArrivalMinute = 0;

        public Cashier(int id)
        {
            _id = id;
            _customersToServe = new LinkedList<ICustomer>();
        }


        public int Id
        {
            get
            {
                return _id;
            }
        }

        //Returns new completion time
        public int EnqueueCustomer(ICustomer customer)
        {
            _customersToServe.AddLast(customer);
            _numCustomers++;
            if (customer.ArrivalTime != _lastArrivalMinute)
            {
                MoveTimeForward(customer.ArrivalTime - _lastArrivalMinute);
            }

            return CalculateCurrentCompletionTime(customer.ArrivalTime);
        }

        protected virtual void MoveTimeForward(int numMinutes)
        {
            int remainingMinutes = numMinutes;
            while (_customersToServe.First != null)
            {
                var cust = _customersToServe.First.Value;
                if (cust.CartCount < remainingMinutes)
                {
                    //customers whose items are done have to be removed
                    remainingMinutes -= cust.CartCount;
                    _customersToServe.RemoveFirst();
                    _numCustomers--;
                }
                else
                {
                    //customers in process have some items removed and some left
                    cust.CartCount -= remainingMinutes;
                    remainingMinutes -= cust.CartCount;
                    Debug.Assert(remainingMinutes == 0, "Remaining minutes are still left !");
                    //Ignore remaining customers
                    break;
                }
                Debug.Assert(remainingMinutes >= 0, "Remaining minutes went negative !");
            }

            _lastArrivalMinute += numMinutes;
        }

        protected virtual int CalculateCurrentCompletionTime(int currentTime)
        {
            int remainingTime = 0;
            foreach (ICustomer customer in _customersToServe)
            {
                remainingTime += customer.CartCount;
            }

            return remainingTime;
        }

        public int UpdateAndGetQueueLength(int minute)
        {
            if (minute > _lastArrivalMinute)
            {
                MoveTimeForward(minute - _lastArrivalMinute);
            }
            else if (minute < _lastArrivalMinute) throw new Exception("Moving time backwards in UpdateAndGetQueueLength!");

            return GetLineLength();         
        }

        private int GetLineLength()
        {
            return _numCustomers;
        }

        public int GetLastCustomerCartCount()
        {
            if (_customersToServe.Count == 0) return 0;
            return _customersToServe.Last.Value.CartCount;
        }

        #region IComparable, IComparer
        //sorts by queue/cashier id
        public int CompareTo(ICashier other)
        {
            return Compare(this, other);
        }

        public int Compare(ICashier x, ICashier y)
        {
            return x.Id.CompareTo(y.Id);
        }
        #endregion
    }
}
