using Garage_App;
var MainMenu = new Main()
{
    WelcomeMessage = "******* Välkommen till Epic Garage App! Vad vill du göra? ********\n",
    NrMenuOptions = 3,
    MenuOptions = new List<string> { "0: avsluta programmet\t", "1: Skapa ett nytt garage\t", "2: Hantera ett av mina garage" }
};
/*var SubMenuToOpt1inMainMenu = new Menu()
{
    WelcomeMessage = "Vad vill du göra nu?",
    NrMenuOptions = 2,
    MenuOptions = new List<string> { "0: tillbaka till huvudmenyn\t", "1: Hantera ditt garage\t", "2: Skapa ett nytt garage"}
};*/

var myGarages = new List<Garage>();
bool programOn = true;
while (programOn)
{
    Console.WriteLine(MainMenu.WelcomeMessage);
    MainMenu.DisplayOptions();
    int userSelection = MainMenu.GetUserSelection();
    
    switch (userSelection)
    {
        case 0:
            Console.WriteLine("Avslutar programmet...");
            programOn = false; break;
        case 1:
            { // the braces create a block scope, meaning the newGarage instance will only excist here
                Garage? newGarage = MainMenu.ExecuteCreateNewGarageSelection();
                if (newGarage != null)
                    myGarages.Add(newGarage);
                break;
            }

        case 2:
            if (myGarages.Count == 0) {Console.WriteLine("Du har inga garage än!"); break; }
            userSelection = SelectGarage(ref myGarages);
            Console.WriteLine($"Lås oss hantera {myGarages[userSelection].Name}");
//            RunGarageMenu(ref myGarages[userSelection]);
            break;
        default:
            //programOn = false;
            break;
    }
    
     static int SelectGarage(ref List<Garage> myGarages)
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
                if (int.TryParse(Console.ReadLine(), out userSelection) && userSelection >= 0 && userSelection <= myGarages.Count)
                    return userSelection;
                else
                    throw new ArgumentException("Du måste ange ett giltigt val!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        return userSelection;

    }

    static void RunGarageMenu(ref Garage garage)
    {
        var garageMenu = new Menu()
        {
            WelcomeMessage = "Hantera ditt Garage!\n",
            NrMenuOptions = 4,
            MenuOptions = new List<string> { "0: Gå tillbaka\t", "1: Checka in\t", "2: Checka ut\t", "3: Sök bland dina fordon"}
        };
        bool MenuOn = true;
        while (MenuOn)
        {
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
}



