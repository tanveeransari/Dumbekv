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
        private readonly LinkedList<Customer> _customersToServe;
        private int _numCustomers = 0;
        private int _lastArrivalMinute = 0;

        public Cashier(int id)
        {
            _id = id;
            _customersToServe = new LinkedList<Customer>();
            RateOfWork = 2;
        }

        public int Id
        {
            get
            {
                return _id;
            }
        }

        public int RateOfWork { get; protected set; }

        //Returns new completion time
        public int EnqueueCustomer(Customer customer)
        {
            _customersToServe.AddLast(customer);
            _numCustomers++;
            if (customer.ArrivalTime != _lastArrivalMinute)
            {
                MoveClockForward(customer.ArrivalTime - _lastArrivalMinute);
            }

            return CalculateCurrentCompletionTime(customer.ArrivalTime);
        }

        private void MoveClockForward(int numMinutes)
        {
            int remainingMinutes = numMinutes;
            while (_customersToServe.First != null)
            {
                var cust = _customersToServe.First.Value;
                if (cust.WorkUnits <= RateOfWork * remainingMinutes)
                {
                    //customers whose items are done have to be removed
                    _customersToServe.RemoveFirst();
                    remainingMinutes -= cust.WorkUnits / RateOfWork;
                    _numCustomers--;
                }
                else
                {
                    //customers partially processed have some items remaining
                    int workDoneInRemainingMinutes = RateOfWork * remainingMinutes;
                    remainingMinutes -= cust.WorkUnits / RateOfWork;
                    cust.WorkUnits -= workDoneInRemainingMinutes;
                    if (remainingMinutes > 0)
                    {
                        Debugger.Break();
                    }

                    Debug.Assert(remainingMinutes <= 0, "Remaining minutes are still left !");
                    break;
                }
                Debug.WriteLine("Remaining minutes {0}", remainingMinutes);
                //Debug.Assert(remainingMinutes >= 0, "Remaining minutes went negative !");
            }

            _lastArrivalMinute += numMinutes;
        }

        private int CalculateCurrentCompletionTime(int currentTime)
        {
            int remainingTime = 0;
            foreach (Customer customer in _customersToServe)
            {
                remainingTime += customer.WorkUnits / RateOfWork;
            }

            return remainingTime;
        }

        public int UpdateAndGetQueueLength(int minute)
        {
            if (minute > _lastArrivalMinute)
            {
                MoveClockForward(minute - _lastArrivalMinute);
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
            return string.Format("{0} Customers:{1} LastArrival:{2}", _id, _numCustomers, _lastArrivalMinute);
        }
    }
}
