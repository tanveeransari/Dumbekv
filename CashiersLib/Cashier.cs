using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashiersLib
{
    public class Cashier : ICashier
    {
        private readonly int _id;
        private Queue<ICustomer> _customersToServe;
        private readonly double _rateOfWork;

        private int _currentMinute = 0;

        public Cashier(int id)
        {
            _id = id;
            _customersToServe = new Queue<ICustomer>();
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
            lock (_customersToServe)
            {
                _customersToServe.Enqueue(customer);
                if (customer.ArrivalTime != _currentMinute)
                {
                    MoveTimeForward(customer.ArrivalTime - _currentMinute);
                }
            }

            return calculateCurrentCompletionTime(customer.ArrivalTime);
        }

        protected virtual void MoveTimeForward(int numMinutes)
        {
            for (int i = 0; i < _customersToServe.Count ; i++)
            {
                var customer = _customersToServe.Peek();
                int customerProcessingTime = customer.CartCount;
                if (customer.CartCount <= numMinutes)
                {
                    _customersToServe.Dequeue();
                }
                else
                {
                    customer.CartCount -= numMinutes;
                    //TODO: What about trainees
                    break;
                }
            }
            _currentMinute += numMinutes;
        }

        protected virtual int calculateCurrentCompletionTime(int currentTime)
        {
            //customers whose items are done have to be removed
            //customers in process have some items removed and some left
            //customers waiting have all items left
            throw new NotImplementedException();
        }

        public int GetLineLength(int minute)
        {
            throw new NotImplementedException();
        }

        public int GetNumberOfCustomersInLine(int minute)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(ICashier other)
        {
            return Id.CompareTo(other.Id);
        }

        public int GetLastCustomerCartCount()
        {
            throw new NotImplementedException();
        }
    }

    public class TraineeCashier:Cashier
    {
        public TraineeCashier(int id):base(id)
        {
           
        }

        protected override void MoveTimeForward(int numMinutes)
        {
            throw new NotImplementedException();
        }

        protected override int calculateCurrentCompletionTime(int currentTime)
        {
            throw new NotImplementedException();
            return 2 * base.calculateCurrentCompletionTime(currentTime);
        }
    }
}
