using System;
using System.Collections.Generic;
using System.Text;

namespace Garage_App
{
    public class Garage
    {
        public int Id { get; }
        public int Spaces { get; }
        private Vehicle[] CheckedInVehicles { get; }
        public string? Name { get; set; }
        public Garage(string? name, int spaces)
        {
            Spaces = spaces;
            Name = name;
            CheckedInVehicles = new Vehicle[spaces];
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine($"| Du har skapat ett garage med namn \"{Name}\" som innehåller {Spaces} parkeringsplatser. |");
            Console.WriteLine("-------------------------------------------------------------------------------\n");
        }

        public int GetAvailableSpaces(bool print = true)
        {
            int occupiedSpaces = CheckedInVehicles.Count(v => v != null);
            if (print)
                Console.WriteLine($"Det finns {Spaces - occupiedSpaces} av {Spaces} lediga parkeringsplatser. ");
            return Spaces - occupiedSpaces;
        }

        public void CheckInVehicle(Vehicle vehicle)
        {
            if (vehicle is null)
                throw new ArgumentException("Vehicle cannot be null.");
            //kolla att fordonet inte redan finns
            int index = -1;
            if (!SearchByRegNr(vehicle.RegNr, ref index)) //fordonet finns inte i garaget
            {
                int index2 = Array.IndexOf(CheckedInVehicles, null);
                CheckedInVehicles[index2] = vehicle;
                Console.WriteLine("-------------------------------------------------------------");
                Console.WriteLine($"| Din {vehicle.Type} är incheckad på parkeringsplats {index2}. |");
                Console.WriteLine("-------------------------------------------------------------\n");
            }
            else
                Console.WriteLine($"!! Det här fordonet är redan registrerat på plats {index}!!\n");
        }

        public void CheckOut()
        {
            if (GetAvailableSpaces(false) == Spaces)
                throw new ArgumentException($"Garaget {Name} är tomt!");
            //Get vehicle registration No.
            Console.WriteLine("Ange registreringsnumret på fordonet du vill checka ut:");
            string? regNo = Console.ReadLine().Trim().ToUpper().Replace(" ", "");
            while (String.IsNullOrEmpty(regNo))
            {
                Console.WriteLine("Ange ett giltigt registreringsnummer!");
                regNo = Console.ReadLine().Trim().ToUpper().Replace(" ", "");
            }
            //Checka ut fordonet
            int matchIndex = -1;
            if (SearchByRegNr(regNo, ref matchIndex))
            {
                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine($"| Din {CheckedInVehicles[matchIndex].Type} med regNr {CheckedInVehicles[matchIndex].RegNr} har blivit utcheckat från {Name} |");
                Console.WriteLine("-----------------------------------------------------------------\n");
                CheckedInVehicles[matchIndex] = null;
            }
            else
            {
                Console.WriteLine($"Det här fordonet finns inte i {Name}");
            }

        }
        public bool SearchByRegNr(string regNr, ref int index)
        {
            regNr = regNr.ToUpper();
            //Search for vehicle in garage
            bool match = false;
            for (int i = 0; i < Spaces; i++)
            {
                if (CheckedInVehicles[i] is not null)
                {
                    if (regNr == CheckedInVehicles[i].RegNr)
                    {
                        index = i;
                        match = true;
                        return match;
                    }
                }
            }
            return match;
        }
        public void ListParkedVehicles()
        {
            GetAvailableSpaces();
            int[] countVehicleTypes = new int[5];
            foreach (var vehicle in CheckedInVehicles) {
                if (vehicle is not null) {
                    switch (vehicle.Type)
                    {
                        case "Motorcykel":
                            countVehicleTypes[0]++;
                            break;
                        case "Flygplan":
                            countVehicleTypes[1]++;
                            break;
                        case "Bil":
                            countVehicleTypes[2]++;
                                break;
                        case "Buss":
                            countVehicleTypes[3]++;
                            break;
                        case "Båt":
                            countVehicleTypes[4]++;
                            break;
                        default:
                            break;

                    }
                }
            }
            Console.WriteLine($"{Name} innehåller:\nMotorcyklar: {countVehicleTypes[0]}\nFlygplan: {countVehicleTypes[1]}\nBilar: {countVehicleTypes[2]}\nBussar: {countVehicleTypes[3]}\nBåtar: {countVehicleTypes[4]}\n");


        }

        public Vehicle[] SearchInRegNr(string _regNr)
        {
            if (CheckedInVehicles.Count(v => v != null) == 0)
                throw new ArgumentException($"Det finns inga fordon i {Name}");
            // Kolla matchning
            var ResultList = new Vehicle[Spaces - GetAvailableSpaces(false)]; //largest possible array
            int index = 0;
            foreach (Vehicle v in CheckedInVehicles)
            {
                if (v is not null){
                    if (v.RegNr.Contains(_regNr))
                    {
                        ResultList[index] = v;
                        index++;
                    }
                }
            }
            return ResultList[0..index];
        }
        public Vehicle[] SearchColor(string userInput)
        {
            if (CheckedInVehicles.Count(v => v != null) == 0)
                throw new ArgumentException($"Det finns inga fordon i {Name}");
            
            // Kolla matchning
            var ResultList = new Vehicle[Spaces - GetAvailableSpaces(false)]; //largest possible array
            int index = 0;
            foreach (Vehicle v in CheckedInVehicles)
            {
                if (v is not null)
                {
                    if (v.Color.Contains(userInput))
                    {
                        ResultList[index] = v;
                        index++;
                    }
                }
            }
            return ResultList[0..index];
        }

    }
}
