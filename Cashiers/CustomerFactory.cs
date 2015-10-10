using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CashiersLib;

namespace Cashiers
{
    public static class CustomerFactory
    {
        private static readonly char[] splitChars = { ' ' };
        public static ICustomer CreateCustomer(string customerLine)
        {
            if (string.IsNullOrWhiteSpace(customerLine)) return null;
            string[] tokens = customerLine.Split(splitChars);
            if (tokens.Length < 3) return null;
            int arrivalTime;
            if (!int.TryParse(tokens[1], out arrivalTime)) return null;
            int cartCount;
            if (!int.TryParse(tokens[2], out cartCount)) return null;

            ICustomer customer;
            if (tokens[0] == "A")
            {
                customer = new CustomerA(arrivalTime, cartCount);
            }
            else if (tokens[0] == "B")
            {
                customer = new CustomerB(arrivalTime, cartCount);
            }
            else return null;
            
            return customer;
        }
    }
}
