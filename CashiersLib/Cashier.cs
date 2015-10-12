using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CashiersLib
{
    public class Cashier : ICashier
    {
        private readonly LinkedList<Customer> _customersToServe;
        private readonly int _id;
        private int _lastCalculationTime;
        private int _numCustomers;

        public Cashier(int id)
        {
            _id = id;
            _customersToServe = new LinkedList<Customer>();
            RateOfWork = 2;
        }

        public int Id
        {
            get { return _id; }
        }

        /// <summary>
        /// Number of work items processed per minute. Each cart item is currently two work items.
        /// This field will be 2 for normal cashiers and 1 for trainee
        /// </summary>
        public int RateOfWork { get; protected set; }

        /// <summary>
        /// Enqueues customer in Cashier's queue
        /// </summary>
        /// <param name="customer">Customer to wait in Cashier Queue</param>
        /// <returns>Processing time remaining from time of customer arrival</returns>
        public int EnqueueCustomer(Customer customer)
        {
            if (customer.ArrivalTime != _lastCalculationTime)
            {
                MoveClockForward(customer.ArrivalTime - _lastCalculationTime);
            }
            _customersToServe.AddLast(customer);
            _numCustomers++;

            return CalculateCurrentCompletionTime();
        }

        public int CalculateQueueLength(int minute)
        {
            if (minute > _lastCalculationTime)
            {
                MoveClockForward(minute - _lastCalculationTime);
            }
            else if (minute < _lastCalculationTime)
            {
                throw new InvalidOperationException("Cannot move time backwards in CalculateQueueLength!");
            }

            return GetLineLength();
        }

        public int GetLastCustomerCartCount()
        {
            if (_customersToServe.Count == 0) return 0;
            return _customersToServe.Last.Value.CartCount;
        }

        /// <summary>
        /// Do cart items processing for specified number of minutes
        /// </summary>
        /// <param name="numMinutes">How many minutes to work for</param>
        private void MoveClockForward(int numMinutes)
        {
            int remainingMinutes = numMinutes;
            while (_customersToServe.First != null)
            {
                var cust = _customersToServe.First.Value;
                if (cust.WorkUnits <= RateOfWork*remainingMinutes)
                {
                    // customers whose items are done have to be removed from queue
                    _customersToServe.RemoveFirst();
                    remainingMinutes -= cust.WorkUnits/RateOfWork;
                    _numCustomers--;
                }
                else
                {
                    //customers partially processed have some items remaining
                    int workDoneInRemainingMinutes = RateOfWork*remainingMinutes;
                    Debug.Assert(workDoneInRemainingMinutes <= cust.WorkUnits, "Processing more items than exist in cart!");
                    remainingMinutes = 0;
                    cust.WorkUnits -= workDoneInRemainingMinutes;
                    Debug.Assert(remainingMinutes <= 0, "Remaining minutes are still left !");
                    break;
                }
            }

            _lastCalculationTime += numMinutes;
        }

        private int CalculateCurrentCompletionTime()
        {
            return _customersToServe.Sum(customer => customer.WorkUnits/RateOfWork);
        }

        private int GetLineLength()
        {
            return _numCustomers;
        }

        public override string ToString()
        {
            return string.Format("ID:{0} Customers:{1} LastArrival:{2}", _id, _numCustomers, _lastCalculationTime);
        }

        //sorts by queue/cashier id
        public int CompareTo(ICashier other)
        {
            return Compare(this, other);
        }

        public int Compare(ICashier x, ICashier y)
        {
            return x.Id.CompareTo(y.Id);
        }
    }
}