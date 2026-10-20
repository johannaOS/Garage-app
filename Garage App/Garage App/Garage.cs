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
        public Garage (string? name, int spaces)
        {
            Spaces = spaces;
            Name = name;
            CheckedInVehicles = new Vehicle[spaces];
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine($"| Du har skapat ett garage med namn \"{Name}\" som innehåller {Spaces} parkeringsplatser. |");
            Console.WriteLine("-------------------------------------------------------------------------------\n");
        }

        public int GetAvailableSpaces()
        {
            int occupiedSpaces = CheckedInVehicles.Count(v => v != null);
            Console.Write($"Det finns {Spaces - occupiedSpaces} av {Spaces} lediga parkeringsplatser. ");
            return Spaces - occupiedSpaces;
        }

        public void CheckInVehicle(Vehicle vehicle)
        {
            //check that vehicle isn't already in since all vehicle registration numbers are unique
            bool isUnique = true;
            int index = -1;
            for (int i = 0; i < Spaces; i++)
            {
                if (CheckedInVehicles[i] is not null)
                {
                    if (CheckedInVehicles[i].RegNr == vehicle.RegNr)
                    {
                        isUnique = false;
                        index = i;
                        break;
                    }
                }
            }
            
            if (isUnique)
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
            //Get vehicle registration No.
            Console.WriteLine("Ange registreringsnumret på fordonet du vill checka ut:");
            string? regNo = Console.ReadLine().Trim().ToUpper().Replace(" ", "");
            if (String.IsNullOrEmpty(regNo))
                throw new ArgumentException("Ange ett giltigt registreringsnummer!"); //hantera while.. i anropet
            //Search for vehicle in garage
            bool match = false;
            for (int i = 0; i < Spaces; i++) {
                if (CheckedInVehicles[i] is not null)
                {
                    if (regNo == CheckedInVehicles[i].RegNr.ToUpper())
                    { 
                        match = true;
                        Console.WriteLine("-----------------------------------------------------------------");
                        Console.WriteLine($"| Din {CheckedInVehicles[i].Type} med regNr {regNo} har blivit utcheckat från {Name} |");
                        Console.WriteLine("-----------------------------------------------------------------\n");
                        CheckedInVehicles[i] = null;
                        break; }
                }
            }
            if (!match)
            {
                Console.WriteLine($"!! Ditt fordon {regNo} finns inte i det här garaget!!\n");
            }

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

    }
}
