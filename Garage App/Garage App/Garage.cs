using System;
using System.Collections.Generic;
using System.Text;

namespace Garage_App
{
    public class Garage
    {
        public int Id { get; }
        public int Spaces { get; }
        public string? Name { get; set; }
        public Garage (string? name, int spaces)
        {
            Spaces = spaces;
            Name = name;
            Console.WriteLine($"Du har skapat ett garage med namn \"{Name}\" som innehåller {Spaces} parkeringsplatser.\n");
        }
    }
}
