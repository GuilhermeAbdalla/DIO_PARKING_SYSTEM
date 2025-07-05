using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingSystem
{
    public class Vehicle
    {
        public int idVehicle;
        public string nameCustomer;
        public string licensePlate;
        public string maker;
        public string model;
        public string color;

        public override string ToString()
        {
            return $"{idVehicle}\t\t{nameCustomer}\t\t{licensePlate}\t\t{maker}\t\t{model}\t\t{color}";
        }
    }
}