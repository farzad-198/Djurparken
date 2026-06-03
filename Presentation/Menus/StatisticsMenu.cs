using Application.Services;

namespace Presentation.Menus
{
    public class StatisticsMenu
    {
        private readonly AnimalService _animalService;
        private readonly HabitatService _habitatService;
        private readonly VisitorService _visitorService;
        private readonly VisitService _visitService;

        public StatisticsMenu(
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

        public async Task StartStatisticsMenu()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();

                Console.WriteLine("===== STATISTICS MENU =====");
                Console.WriteLine("1. Show total number of animals");
                Console.WriteLine("2. Show total number of habitats");
                Console.WriteLine("3. Show total number of visitors");
                Console.WriteLine("4. Show total number of visits");
                Console.WriteLine("5. Show paid and unpaid visits");
                Console.WriteLine("6. Show all statistics");
                Console.WriteLine("0. Back to main menu");
                Console.Write("Choose an option: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await ShowAnimalCount();
                        Pause();
                        break;

                    case "2":
                        await ShowHabitatCount();
                        Pause();
                        break;

                    case "3":
                        await ShowVisitorCount();
                        Pause();
                        break;

                    case "4":
                        await ShowVisitCount();
                        Pause();
                        break;

                    case "5":
                        await ShowPaidAndUnpaidVisits();
                        Pause();
                        break;

                    case "6":
                        await ShowAllStatistics();
                        Pause();
                        break;

                    case "0":
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        Pause();
                        break;
                }
            }
        }

        private async Task ShowAnimalCount()
        {
            Console.Clear();
            Console.WriteLine("===== ANIMAL STATISTICS =====");

            var animals = await _animalService.GetAllAnimalsAsync();

            Console.WriteLine($"Total animals: {animals.Count}");
        }

        private async Task ShowHabitatCount()
        {
            Console.Clear();
            Console.WriteLine("===== HABITAT STATISTICS =====");

            var habitats = await _habitatService.GetAllHabitatsAsync();

            Console.WriteLine($"Total habitats: {habitats.Count}");
        }

        private async Task ShowVisitorCount()
        {
            Console.Clear();
            Console.WriteLine("===== VISITOR STATISTICS =====");

            var visitors = await _visitorService.GetAllVisitorsAsync();

            Console.WriteLine($"Total visitors: {visitors.Count}");
        }

        private async Task ShowVisitCount()
        {
            Console.Clear();
            Console.WriteLine("===== VISIT STATISTICS =====");

            var visits = await _visitService.GetAllVisitsAsync();

            Console.WriteLine($"Total visits: {visits.Count}");
        }

        private async Task ShowPaidAndUnpaidVisits()
        {
            Console.Clear();
            Console.WriteLine("===== TICKET STATISTICS =====");

            var visits = await _visitService.GetAllVisitsAsync();

            int paidVisits = visits.Count(v => v.HasPaidTicket);
            int unpaidVisits = visits.Count(v => !v.HasPaidTicket);

            Console.WriteLine($"Paid visits: {paidVisits}");
            Console.WriteLine($"Unpaid visits: {unpaidVisits}");
        }

        private async Task ShowAllStatistics()
        {
            Console.Clear();
            Console.WriteLine("===== ALL STATISTICS =====");

            var animals = await _animalService.GetAllAnimalsAsync();
            var habitats = await _habitatService.GetAllHabitatsAsync();
            var visitors = await _visitorService.GetAllVisitorsAsync();
            var visits = await _visitService.GetAllVisitsAsync();

            int paidVisits = visits.Count(v => v.HasPaidTicket);
            int unpaidVisits = visits.Count(v => !v.HasPaidTicket);

            Console.WriteLine($"Total animals: {animals.Count}");
            Console.WriteLine($"Total habitats: {habitats.Count}");
            Console.WriteLine($"Total visitors: {visitors.Count}");
            Console.WriteLine($"Total visits: {visits.Count}");
            Console.WriteLine($"Paid visits: {paidVisits}");
            Console.WriteLine($"Unpaid visits: {unpaidVisits}");
        }

        private static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}