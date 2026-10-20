using Garage_App;
using System.ComponentModel.Design;
using System.Xml.Linq;
var MainMenu = new Main()
{
    WelcomeMessage = "************** Välkommen till Epic Garage App! ***************\n",
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
                Console.WriteLine("Vill du populera garaget direkt? (y/n)");
                string userResponse = Console.ReadLine().Trim().ToLower();
                while (userResponse != "y" && userResponse != "n")
                {
                    Console.WriteLine("Du måste ange 'y' eller 'n'!");
                    userResponse = Console.ReadLine().Trim().ToLower();
                }
                if (userResponse == "n")
                    break;
                else
                {
                    Console.WriteLine($"Hur många fordon vill du populera garaget med? Max: {newGarage.Spaces}!");
                    int initPop;
                    while (!int.TryParse(Console.ReadLine().Trim(), out initPop) || initPop <= 0 || initPop > newGarage.Spaces)
                        Console.WriteLine("Du måste ange ett giltigt antal!");
                    if (newGarage != null)
                    {
                        for (int i=0; i<initPop; i++)
                            CheckIn(newGarage);
                    }
                    Console.WriteLine("Då har du populerat klart ditt garage!");
                }
                break;
            }

        case "2":
            if (myGarages.Count == 0) { Console.WriteLine("!! Du har inga garage än! !!"); break; }
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
    int userSelection = -1;
    while (true)
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
}

static void RunGarageMenu(Garage selectedGarage)
{
    var garageMenu = new Menu()
    {
        WelcomeMessage = $"************** Hantera ditt garage \"{selectedGarage.Name}\"! **************\n",
        NrMenuOptions = 4,
        MenuOptions = new List<string> { "0: Gå tillbaka\t", "1: Checka in\t", "2: Checka ut\t", "3: Lista alla fordon\t", "4: Sök bland dina fordon\n" }
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
                selectedGarage.CheckOut();
                break;
            case "3":
                selectedGarage.ListParkedVehicles();
                break;
            case "4":
                RunSearchMenu(selectedGarage);
                break;
            default:
                Console.WriteLine("Ange ett giltigt alternativ!");
                break;
        }
    }

}

static void RunSearchMenu(Garage selectedGarage)
{
    var searchMenu = new Menu()
    {
        WelcomeMessage = $"************** Vad vill du söka efter i \"{selectedGarage.Name}\"? **************\n",
        NrMenuOptions = 3,
        MenuOptions = new List<string> { "0: Gå tillbaka\t", "1: Sök på registreringsskylt\t", "2: Fri söktext\n" }
    };

    bool MenuOn = true;
    while (MenuOn)
    {
        Console.WriteLine(searchMenu.WelcomeMessage);
        searchMenu.DisplayOptions();

        switch (Console.ReadLine())
        {
            case "0":
                MenuOn = false;
                break;
            case "1":
                break;
  /*          case "2":
                CheckOut(selectedGarage);
                break;
            case "3":
                selectedGarage.ListParkedVehicles();
                break;
            case "4":
                RunSearchMenu();
                break;*/
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
        Console.WriteLine("Vilket typ av fordon vill du checka in?\n1: Motorcykel\t2: Flygplan\t3: Bil\t4: Buss\t5: Båt\t0: Avbryt"); //fyll på här när nya Vehicleklasser skapats
        bool userAnswerSuccess = false;
        string userChoice;
        while (!userAnswerSuccess)
        {
            userChoice = Console.ReadLine().Trim();
            switch (userChoice)
            {
                case "0":
                    userAnswerSuccess = true;
                    break;
                case "1":
                    Console.WriteLine("Låt oss checka in din mc!");
                    garage.CheckInVehicle(
                        CreateVehicle(userChoice)
                        );
                    userAnswerSuccess = true;
                    break;
                case "2":
                    Console.WriteLine("Låt oss checka in ditt flygplan!");
                    garage.CheckInVehicle(
                        CreateVehicle(userChoice)
                        );
                    userAnswerSuccess = true;
                    break;
                case "3":
                    Console.WriteLine("Låt oss checka in din bil!");
                    garage.CheckInVehicle(
                        CreateVehicle(userChoice)
                        );
                    userAnswerSuccess = true;
                    break;
                case "4":
                    Console.WriteLine("Låt oss checka in din buss!");
                    garage.CheckInVehicle(
                        CreateVehicle(userChoice)
                        );
                    userAnswerSuccess = true;
                    break;
                case "5":
                    Console.WriteLine("Låt oss checka in din båt!");
                    garage.CheckInVehicle(
                        CreateVehicle(userChoice)
                        );
                    userAnswerSuccess = true;
                    break;
                default:
                    Console.WriteLine("Ange ett giltigt val!");
                    break;
            }
        }
    }
}

static Vehicle CreateVehicle(string choice)
{
    switch (choice)
    {
        case "1": //motorcykel
            {
                string regNr = ValidateRegNr();
                string color = ValidateColor();
                int? nrOfWheels = ValidateNrOfWheels();

                //instanciera en ny motorcykel
                try
                {
                    var motorcycle = new MotorCycle(regNr, color, nrOfWheels);
                    return motorcycle;
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
                break;
            }
        case "2": //flygplan
            {
                string regNr = ValidateRegNr();
                string color = ValidateColor();
                double length = ValidateLength();

                try
                {
                    var airplane = new Airplane(regNr, color, length);
                    return airplane;
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
                break;
            }
        case "3": //bil
            {
                string regNr = ValidateRegNr();
                string color = ValidateColor();

                try
                {
                    var car = new Car(regNr, color);
                    return car;
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
                    break;
            }
        case "4": //buss
            {
                string regNr = ValidateRegNr();
                string color = ValidateColor();

                try
                {
                    var bus = new Bus(regNr, color);
                    return bus;
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
                break;
            }
        case "5": //båt
            {
                string regNr = ValidateRegNr();
                string color = ValidateColor();

                try
                {
                    var boat = new Boat(regNr, color);
                    return boat;
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
                break;
            }
        default:
            break;
    }
    return null;
}

static string ValidateRegNr()
{
    Console.Write("Ange registreringsnummer (obligatoriskt): ");
    while (true)
    {
        string _regNr = Console.ReadLine().Trim().Replace(" ", "").ToUpper();
        try
        {
            if (String.IsNullOrEmpty(_regNr) || _regNr.Length > 12)
                throw new ArgumentException("Du måste ange ett giltigt registreringsnummer!");
            return _regNr;
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
    }
}

static string ValidateColor()
{
    Console.Write("Ange färg (obligatoriskt): ");
    while (true)
    {
        string color = Console.ReadLine().Trim().Replace(" ", "").ToLower();
        try
        {
            if (String.IsNullOrEmpty(color) || color.Length > 200)
                throw new ArgumentException("Du måste ange en giltig färg!");
            return color;
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
    }
}
static int? ValidateNrOfWheels()
{
    Console.Write("Ange antal hjul (tryck 0 för att hoppa över denna): ");
    int nrOfwheels;
    while (true)
    {
        string wheels = Console.ReadLine().Trim().Replace(" ", "");
        if (wheels == "0")
            return null;
        if (int.TryParse(wheels, out nrOfwheels) && nrOfwheels > 1 && nrOfwheels < 11)
            return nrOfwheels;
        else
            Console.WriteLine("Du måste ange ett giltigt antal hjul!");  
    }
}
static double ValidateLength()
{
    Console.Write("Ange längd på fordonet (obligatorisk): ");
    double length;
    while (true)
    {
        string input = Console.ReadLine().Trim().Replace(" ", "");
        if (double.TryParse(input, out length) && length > 1 && length <100 )
            return length;
        else
            Console.WriteLine("Du måste ange en giltig längd!");
    }
}



