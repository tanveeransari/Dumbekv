using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Cashiers
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length < 1 || args[0] == null || args[0].Length == 0)
            {
                Console.WriteLine("Usage Cashiers <filepath>");
                return;
            }


            if (!File.Exists(args[0]))
            {
                Console.WriteLine("ERROR: File {0} does not exist.", args[0]);
            }

            

        }
    }
}
