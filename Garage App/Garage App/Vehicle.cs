using System;
using System.Collections.Generic;
using System.Text;

namespace Garage_App
{
    public abstract class Vehicle
    {
        public string? Type;
        public string RegNr { get; }
        public string Color { get; set; }
        public int? ParkingSquaresNeeded { get; set; }
        public string? FuelType { get; set;}
        public Vehicle(string regNr, string color, int? parkingSquaresNeeded = null, string? fuelType = null)
        {
            string _regNr = regNr.Trim().Replace(" ", "").ToUpper();
            if (String.IsNullOrEmpty(_regNr) || _regNr.Length > 12)
                throw new ArgumentException("Du måste ange ett giltigt registreringsnummer!");
            //Registration No. can only be set once
            if (String.IsNullOrEmpty(RegNr))
            {
                RegNr = _regNr;
            }
            else
                throw new ArgumentException("Det här fordonet har redan ett registreringsnummer!");
            if (String.IsNullOrEmpty(color))
                throw new ArgumentException("Du måste ange färg på fordonet!");
            Color = color.Trim().ToLower();
            ParkingSquaresNeeded = parkingSquaresNeeded;
            FuelType = fuelType;
        }
    }

    public class MotorCycle: Vehicle
    {
        public int NrWheels { get; set; }
        public MotorCycle(string regNr, string color, int? nrWheels = null , int? parkingSquaresNeeded = null, string? fuelType = null) : base(regNr, color, parkingSquaresNeeded, fuelType)
        {
            int _nrWheels = nrWheels ?? 2;
            if (_nrWheels < 2 || _nrWheels > 6)
                throw new ArgumentException("Amount of wheels needs to be between 2 and 6!");
            NrWheels = _nrWheels;
            Type = "Motorcycle";
        }
    }

    public class Airplane : Vehicle
    {
        public double LengthInMeters { get; set; }
        public Airplane(string regNr, string color, int length, int? parkingSquaresNeeded = null, string? fuelType = null) : base(regNr, color, parkingSquaresNeeded, fuelType)
        {
            LengthInMeters = length;
            Type = "Airplane";
        }
    }

    public class Car : Vehicle
    {
        public int NoOfSeats { get; set; }
        public Car(string regNr, string color, int seats = 5, int? parkingSquaresNeeded = null, string? fuelType = null) : base(regNr, color, parkingSquaresNeeded, fuelType)
        {
            NoOfSeats = seats;
            Type = "Car";
        }
    }
    public class Bus : Vehicle
    {
        public int NoOfSeats { get; set; }
        public Bus(string regNr, string color, int seats = 20, int? parkingSquaresNeeded = null, string? fuelType = null) : base(regNr, color, parkingSquaresNeeded, fuelType)
        {
            NoOfSeats = seats;
            Type = "Bus";
        }
    }

    public class Boat : Vehicle
    {
        public int NoOfCylinders { get; set; }
        public Boat(string regNr, string color, int noOfCylinders = 1, int? parkingSquaresNeeded = null, string? fuelType = null) : base(regNr, color, parkingSquaresNeeded, fuelType)
        {
            NoOfCylinders = noOfCylinders;
            Type = "Boat";
        }
    }
}
