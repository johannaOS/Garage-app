using Garage_App;
using System.ComponentModel.Design;
var MainMenu = new Main()
{
    WelcomeMessage = "******* Välkommen till Epic Garage App! ********\n",
    NrMenuOptions = 3,
    MenuOptions = new List<string> { "0: avsluta programmet\t", "1: Skapa ett nytt garage\t", "2: Hantera ett av mina garage\n" }
};

var myGarages = new List<Garage>();
bool programOn = true;
while (programOn)
{
    Console.WriteLine(MainMenu.WelcomeMessage);
    MainMenu.DisplayOptions();

    switch (Console.ReadLine())
    {
        case "0":
            Console.WriteLine("Avslutar programmet...");
            programOn = false; break;
        case "1":
            { // the braces create a block scope, meaning the newGarage instance will only excist here
                Garage? newGarage = MainMenu.ExecuteCreateNewGarageSelection();
                if (newGarage != null)
                    myGarages.Add(newGarage);
                break;
            }

        case "2":
            if (myGarages.Count == 0) { Console.WriteLine("Du har inga garage än!"); break; }
            var selectedGarage = SelectGarage(myGarages);
            RunGarageMenu(selectedGarage);
            break;
        default:
            Console.WriteLine("Ange ett giltigt val!");
            break;
    }
}

static Garage SelectGarage(List<Garage> myGarages)
{
    Console.WriteLine("Vilka av dina garage vill du hantera?");
    foreach (var garage in myGarages)
        Console.Write($"{myGarages.IndexOf(garage)}: {garage.Name}\t");
    Console.WriteLine();
    int userSelection = 0;
    bool UserInputSuccess = false;
    while (!UserInputSuccess)
    {
        try
        {
            if (int.TryParse(Console.ReadLine(), out userSelection) && userSelection >= 0 && userSelection < myGarages.Count)
                return myGarages[userSelection];
            else
                throw new ArgumentException("Du måste ange ett giltigt val!\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    return myGarages[userSelection];
}

static void RunGarageMenu(Garage selectedGarage)
{
    var garageMenu = new Menu()
    {
        WelcomeMessage = $"******* Hantera ditt garage \"{selectedGarage.Name}\"! *******\n",
        NrMenuOptions = 4,
        MenuOptions = new List<string> { "0: Gå tillbaka\t", "1: Checka in\t", "2: Checka ut\t", "3: Sök bland dina fordon\n" }
    };

    bool MenuOn = true;
    while (MenuOn)
    {
        Console.WriteLine(garageMenu.WelcomeMessage);
        garageMenu.DisplayOptions();

        switch (Console.ReadLine())
        {
            case "0":
                MenuOn = false;
                break;
            case "1":
                CheckIn(selectedGarage);
                break;
            case "2":
                CheckOut(selectedGarage);
                break;
            case "3":
                Console.WriteLine("Du angav 3");
                break;
            default:
                Console.WriteLine("Ange ett giltigt alternativ!");
                break;
        }
    }

}

static void CheckIn(Garage garage)
{
    int availableSpaces = garage.GetAvailableSpaces();
    if (availableSpaces > 0)
    {
        Console.WriteLine("Vilket typ av fordon vill du checka in?\n0: Motorcycle\t9: Exit"); //fyll på här när nya Vehicleklasser skapats
        bool userAnswerSuccess = true;
        while (userAnswerSuccess)
        {
            switch (Console.ReadLine())
            {
                case "0":
                    Console.WriteLine("Låt oss checka in din mc!");
                    garage.CheckInVehicle(
                        new MotorCycle("abc123", "blue", 2, "gas")
                        );
                    userAnswerSuccess = false;
                    break;
                case "9":
                    userAnswerSuccess = false;
                    break;
                default:
                    Console.WriteLine("Ange ett giltigt val!");
                    break;
            }
        }
    }

}

static void CheckOut(Garage garage)
{
    bool requireAnswer = true;
    while (requireAnswer)
    {
        try {
            garage.CheckOut();
            requireAnswer = false;
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
        }
    }
}


