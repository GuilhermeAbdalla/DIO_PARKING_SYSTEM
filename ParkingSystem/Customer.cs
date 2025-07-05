using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingSystem
{
    public class Customer
    {
        public int idCustomer;
        public string? nameCustomer;
        public string? docCustomer;
        public string? phoneCustomer;
        public string? emailCustomer;
        public int activeSituation;

        public override string ToString()
        {
            string? activeSituation;
            if (this.activeSituation == 1)
            {
                activeSituation = "Active";
            }
            else
            {
                activeSituation = "Inactive";
            }
            return $"{idCustomer}\t\t{nameCustomer}\t\t{docCustomer}\t\t{phoneCustomer}\t\t{emailCustomer}\t\t{activeSituation}";
        }
    }
}