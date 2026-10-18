using System;
using System.Collections.Generic;
using System.Text;

namespace Garage_App
{
    public class Vehicle
    {
        public string RegNr { get; }
        public string Color { get; }
        public int? ParkingSquaresNeeded { get; set; }
        public Vehicle(string regNr, string color, int? parkingSquaresNeeded = null)
        {
            RegNr = regNr;
            Color = color;
            ParkingSquaresNeeded = parkingSquaresNeeded;
        }
    }

    public class MotorCycle: Vehicle
    {
        public int NrWheels { get; set; }
        public string FuelType { get; set; }
        public MotorCycle(string regNr, string color, int nrWheels,  string fuelType, int? parkingSquaresNeeded = null) : base(regNr, color, parkingSquaresNeeded)
        {
            NrWheels = nrWheels;
            FuelType = fuelType;

        }
    }

}
