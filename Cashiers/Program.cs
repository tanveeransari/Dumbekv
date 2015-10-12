using System;
using System.Collections.Generic;
using System.IO;
using CashiersLib;

namespace Cashiers
{
    internal class Program
    {
        private static void Main(string[] args)
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

            List<Customer> customerList = new List<Customer>();
            try
            {
                using (var fStream = File.OpenRead(args[0]))
                {
                    using (var strRdr = new StreamReader(fStream))
                    {
                        string lineOne = strRdr.ReadLine();
                        if (string.IsNullOrEmpty(lineOne)) return;
                        int numCashiers;
                        if (!int.TryParse(lineOne, out numCashiers)) return;

                        string custLine;
                        while ((custLine = strRdr.ReadLine()) != null)
                        {
                            Customer cust = CustomerFactory.CreateCustomer(custLine);
                            if (cust != null) customerList.Add(cust);
                        }

                        var store = new Store(numCashiers);
                        int finishTime = store.EnqueueCustomers(customerList);
                        Console.WriteLine("Finished at: t={0} minutes", finishTime);
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