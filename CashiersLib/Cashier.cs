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

        protected readonly LinkedList<Customer> _customersToServe;
        protected int _numCustomers = 0;
        protected int _itemsPerMinute = 2;
        protected int _lastArrivalMinute = 0;

        public Cashier(int id)
        {
            _id = id;
            _customersToServe = new LinkedList<Customer>();
        }


        public int Id
        {
            get
            {
                return _id;
            }
        }

        //Returns new completion time
        public int EnqueueCustomer(Customer customer)
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
                if (cust.CartCountTimesTwo <= _itemsPerMinute * remainingMinutes)
                {
                    //customers whose items are done have to be removed
                    _customersToServe.RemoveFirst();
                    Debug.Assert(cust.CartCountTimesTwo % 2 == 0, "MoveTimeForward rounding error inevitable");
                    remainingMinutes -= cust.CartCountTimesTwo/_itemsPerMinute;
                    _numCustomers--;
                }
                else
                {
                    //customers in process have some items removed and some left
                    cust.CartCountTimesTwo-= _itemsPerMinute *  remainingMinutes;
                    remainingMinutes -= cust.CartCountTimesTwo/_itemsPerMinute;
                    //Debug.Assert(remainingMinutes <= 0, "Remaining minutes are still left !");
                    if (remainingMinutes > 0)
                    {
                        Debugger.Break();
                    }
                    //Ignore remaining customers
                    break;
                }
                Debug.WriteLine("Remaining minutes {0}", remainingMinutes);
                //Debug.Assert(remainingMinutes >= 0, "Remaining minutes went negative !");
            }

            _lastArrivalMinute += numMinutes;
        }

        protected virtual int CalculateCurrentCompletionTime(int currentTime)
        {
            int remainingTime = 0;
            foreach (Customer customer in _customersToServe)
            {
                remainingTime += customer.CartCountTimesTwo/_itemsPerMinute;
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

        public override string ToString()
        {
            return string.Format("{0} Rate:{1} Customers:{2} LastArrival:{3}", _id, _itemsPerMinute, _numCustomers, _lastArrivalMinute);
        }
    }
}
