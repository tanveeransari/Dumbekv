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

        public Cashier(int id, bool isTrainee)
        {
            _id = id;
            _isTrainee = isTrainee;
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

        public int GetLineLength()
        {
            throw new NotImplementedException();
        }

        public int GetCompletionTime()
        {
            throw new NotImplementedException();
        }

        public List<ICustomer> GetCustomersInLine()
        {
            throw new NotImplementedException();
        }

        public int EnqueueCustomer(ICustomer customer)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(ICashier other)
        {
            throw new NotImplementedException();
        }
    }
}
