using System;

namespace CashiersLib
{
    public class CashierTrainee:Cashier
    {
        public CashierTrainee(int id):base(id)
        {
           
        }

        protected override void MoveTimeForward(int numMinutes)
        {
            int remainingMinutes = numMinutes; //Trainee takes twice the time
            while (_customersToServe.First != null)
            {
                
            }
        }

        protected override int CalculateCurrentCompletionTime(int currentTime)
        {
            throw new NotImplementedException();
            return 2 * base.CalculateCurrentCompletionTime(currentTime);
        }
    }
}