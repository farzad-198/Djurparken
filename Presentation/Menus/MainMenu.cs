using Application.Services;

namespace Presentation.Menus
{
    public class MainMenu
    {
        private readonly AnimalService _animalService;
        private readonly HabitatService _habitatService;
        private readonly VisitorService _visitorService;
        private readonly VisitService _visitService;

        public MainMenu(
            AnimalService animalService,
            HabitatService habitatService,
            VisitorService visitorService,
            VisitService visitService)
        {
            _animalService = animalService;
            _habitatService = habitatService;
            _visitorService = visitorService;
            _visitService = visitService;
        }


        public async Task StartMainMenu()
        { 
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();

                Console.WriteLine("===== DJURPARKEN MAIN MENU =====");
                Console.WriteLine("1. Manage animals");
                Console.WriteLine("2. Manage habitats");
                Console.WriteLine("3. Manage visitors");
                Console.WriteLine("4. Manage visits");
                Console.WriteLine("5. Show statistics");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AnimalMenu animalMenu = new AnimalMenu(_animalService, _habitatService);
                        await animalMenu.StartAnimalMenu();
                        break;

                    case "2":
                        HabitatMenu habitatMenu = new HabitatMenu(_habitatService);
                        await habitatMenu.StartHabitatMenu();   
                        break;
                    case "3":
                        VisitorMenu visitorMenu = new VisitorMenu(_visitorService);
                        await visitorMenu.StartVisitorMenu();
                        break;
                    case "4":
                        VisitMenu visitMenu = new VisitMenu(_visitService);
                        await visitMenu.StartVisitMenu();
                        break;
                    case "5":
                        StatisticsMenu statisticsMenu = new StatisticsMenu(_animalService, _habitatService, _visitorService, _visitService);
                        await statisticsMenu.StartStatisticsMenu();
                        break;
                   case "0":
                        isRunning = false;
                        Console.WriteLine("Exiting the application. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

            }

        }
    }
}
