using System;
using System.Diagnostics;

namespace CashiersLib
{
    public class CashierTrainee:Cashier
    {
        public CashierTrainee(int id):base(id)
        {
            _itemsPerMinute = 1;   
        }

        //protected override void MoveTimeForward(int numMinutes)
        //{
        //    int remainingMinutes = numMinutes;
        //    while (_customersToServe.First != null)
        //    {
        //        var cust = _customersToServe.First.Value;
        //        if (cust.CartCount < remainingMinutes)
        //        {
        //            //customers whose items are done have to be removed
        //            remainingMinutes -= cust.CartCount;
        //            _customersToServe.RemoveFirst();
        //            _numCustomers--;
        //        }
        //        else
        //        {
        //            //customers in process have some items removed and some left
        //            cust.CartCount -= remainingMinutes;
        //            remainingMinutes -= cust.CartCount;
        //            Debug.Assert(remainingMinutes == 0, "Remaining minutes are still left !");
        //            //Ignore remaining customers
        //            break;
        //        }
        //        Debug.Assert(remainingMinutes >= 0, "Remaining minutes went negative !");
        //    }

        //    _lastArrivalMinute += numMinutes;
        //}

        //protected override int CalculateCurrentCompletionTime(int currentTime)
        //{
        //    int remainingTime = 0;
        //    foreach (ICustomer customer in _customersToServe)
        //    {
        //        remainingTime += customer.CartCount;
        //    }

        //    return remainingTime;
        //}
    }
}