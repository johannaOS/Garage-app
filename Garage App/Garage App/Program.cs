using Garage_App;
var MainMenu = new Main()
{
    WelcomeMessage = "******* Välkommen till Epic Garage App! Vad vill du göra? ********\n",
    NrMenuOptions = 3,
    MenuOptions = new List<string> { "0: avsluta programmet\t", "1: Skapa ett nytt garage\t", "2: Hantera ett av mina garage\n" }
};

var myGarages = new List<Garage>();
bool programOn = true;
Console.WriteLine(MainMenu.WelcomeMessage);
while (programOn)
{
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
    Console.WriteLine(garageMenu.WelcomeMessage);
    while (MenuOn)
    {
        garageMenu.DisplayOptions();

        switch (Console.ReadLine())
        {
            case "0":
                MenuOn = false;
                break;
            case "1":
                Console.WriteLine("Du angav 1.");
                break;
            case "2":
                Console.WriteLine("Du angav 2");
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




