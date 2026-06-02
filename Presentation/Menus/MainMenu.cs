using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Menus
{
    public class MainMenu
    {
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
                        AnimalMenu animalMenu = new AnimalMenu();
                        await animalMenu.StartAnimalMenu();
                        break;

                    case "2":
                        HabitatMenu habitatMenu = new HabitatMenu();
                        await habitatMenu.StartHabitatMenu();   
                        break;
                    case "3":
                        VisitorMenu visitorMenu = new VisitorMenu();
                        await visitorMenu.StartVisitorMenu();
                        break;
                    case "4":
                        VisitMenu visitMenu = new VisitMenu();
                        await visitMenu.StartVisitMenu();
                        break;
                    case "5":
                        StatisticsMenu statisticsMenu = new StatisticsMenu();
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
