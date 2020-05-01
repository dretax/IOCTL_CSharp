using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModeApplication
{
    public class Program
    {
        public static DriverComms Comms;

        public static void Main(string[] args)
        {
            Comms = new DriverComms("\\\\.\\dretaxioctlsample");

            if (Comms.HasValidHandle())
            {
                Console.WriteLine("Attemting read & write tests.");
                Comms.ReadVirtualMemory(0, 0, sizeof(int));
                Console.WriteLine("Sent!");
            }
            else
            {
                Console.WriteLine("Failure at Driver handle!");
            }

            Console.ReadKey();
        }
    }
}
