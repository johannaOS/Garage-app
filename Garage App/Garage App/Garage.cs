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
            Console.WriteLine($"Du har skapat ett garage med namn \"{Name}\" som innehåller {Spaces} parkeringsplatser.\n");
        }

        public int GetAvailableSpaces()
        {
            int occupiedSpaces = CheckedInVehicles.Count(v => v != null);
            Console.Write($"Det finns {Spaces - occupiedSpaces} av {Spaces} lediga parkeringsplatser.");
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
                Console.WriteLine($"Din {vehicle.Type} är incheckad på parkeringsplats {index2}.\n");
            }
            else 
                Console.WriteLine($"Det här fordonet är redan registrerat på plats {index}.\n");
        }

        public void CheckOut()
        {
            //Get vehicle registration No.
            Console.WriteLine("Ange registreringsnumret på fordonet du vill checka ut:");
            string? regNo = Console.ReadLine().Trim().ToUpper().Replace(" ", "");
            if (String.IsNullOrEmpty(regNo))
                throw new ArgumentException("Ange ett giltigt registreringsnummer!"); //hantera while.. i anropet
            //Search for vehicle in garage
            int match = -1;
            for (int i = 0; i < Spaces; i++) {
                if (CheckedInVehicles[i] is not null)
                {
                    if (regNo == CheckedInVehicles[i].RegNr.ToUpper())
                    { match = i; break; }
                }
            }
            if (match >= 0)
            {
                Console.WriteLine($"Your vehicle {regNo} is in this garage on spot {match}\n");
            }
            else
            {
                Console.WriteLine($"Your vehicle {regNo} is not in this garage!\n");
            }

        }

    }
}
