using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CashiersLib
{
    public class Cashier : ICashier
    {
        private readonly int _id;
        private readonly LinkedList<Customer> _customersToServe;
        private int _numCustomers = 0;
        private int _lastCalculationTime = 0;

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
            //_customersToServe.AddLast(customer);
            //_numCustomers++;
            if (customer.ArrivalTime != _lastCalculationTime)
            {
                MoveClockForward(customer.ArrivalTime - _lastCalculationTime);
            }
            _customersToServe.AddLast(customer);
            _numCustomers++;

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
                    Debug.Assert(workDoneInRemainingMinutes > cust.WorkUnits, "Processing more items than exist in cart!");
                    remainingMinutes = 0;
                    cust.WorkUnits -= workDoneInRemainingMinutes;
                    Debug.Assert(remainingMinutes <= 0, "Remaining minutes are still left !");
                    break;
                }
            }

            _lastCalculationTime += numMinutes;
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

        public int CalculateQueueLength(int minute)
        {
            if (minute > _lastCalculationTime)
            {
                MoveClockForward(minute - _lastCalculationTime);
            }
            else if (minute < _lastCalculationTime)
            {
                throw new InvalidOperationException("Cannot move time backwards in UpdateAndGetQueueLength!");
            }

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
            return string.Format("ID:{0} Customers:{1} LastArrival:{2}", _id, _numCustomers, _lastCalculationTime);
        }
    }
}
