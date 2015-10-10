using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashiersLib
{
    public class Cashier : ICashier
    {
        private readonly bool _isTrainee;
        private readonly int _id;
        private List<ICustomer> _customersToServe;


        public Cashier(int id, bool isTrainee)
        {
            _id = id;
            _isTrainee = isTrainee;
            _customersToServe = new List<ICustomer>();
        }

        public bool IsTrainee
        {
            get
            {
                return _isTrainee;
            }
        }

        public int Id
        {
            get
            {
                return _id;
            }
        }

        public int GetFinalCompletionTime()
        {
            throw new NotImplementedException();
        }

        //Returns new completion time
        public int EnqueueCustomer(ICustomer customer)
        {
            _customersToServe.Add(customer);
            return calculateCurrentCompletionTime(customer.ArrivalTime);
        }

        private int calculateCurrentCompletionTime(int currentTime)
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
    }
}
