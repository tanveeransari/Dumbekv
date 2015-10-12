using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CashiersLib;

namespace Cashiers
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length < 1 || args[0] == null || args[0].Length == 0)
            {
                Console.WriteLine("Usage: Cashiers <filepath>");
                return;
            }

            if (!File.Exists(args[0]))
            {
                Console.WriteLine("ERROR: File {0} does not exist.", args[0]);
                return;
            }

            int numCashiers;
            string custLine;
            List<Customer> customerList = new List<Customer>();
            try
            {
                using (var fStream = File.OpenRead(args[0]))
                {
                    using (var strRdr = new StreamReader(fStream))
                    {
                        string lineOne = strRdr.ReadLine();
                        if (string.IsNullOrEmpty(lineOne)) return;
                        if (!int.TryParse(lineOne, out numCashiers)) return;

                        while ((custLine = strRdr.ReadLine()) != null)
                        {
                            Customer cust = CustomerFactory.CreateCustomer(custLine);
                            if (cust != null) customerList.Add(cust);
                        }

                        var store = new Store(numCashiers);
                        Console.WriteLine(store.EnqueueCustomers(customerList));
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error reading {0} : {1} {2} {3}", args[0],
                    ex.Message, Environment.NewLine + "StackTrace:", ex.StackTrace);
            }

        }
    }
}
