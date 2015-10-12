using System;
using System.Diagnostics;

namespace CashiersLib
{
    public class CashierTrainee:Cashier
    {
        public CashierTrainee(int id):base(id)
        {
            RateOfWork = 1;   
        }
    }
}