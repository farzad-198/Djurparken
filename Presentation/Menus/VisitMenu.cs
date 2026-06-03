using Application.Services;
using Domain.Entities;

namespace Presentation.Menus
{
    public class VisitMenu
    {
        private readonly VisitService _service;

        public VisitMenu(VisitService service)
        {
            _service = service;
        }

        public async Task StartVisitMenu()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();

                Console.WriteLine("===== VISIT MENU =====");
                Console.WriteLine("1. Add visit");
                Console.WriteLine("2. Show all visits");
                Console.WriteLine("3. Update visit");
                Console.WriteLine("4. Delete visit");
                Console.WriteLine("5. Search visit");
                Console.WriteLine("0. Back to main menu");
                Console.Write("Choose an option: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Add visit:");
                        await AddVisit();
                        Pause();
                        break;

                    case "2":
                        Console.WriteLine("Show all visits:");
                        await ShowAllVisits();
                        Pause();
                        break;

                    case "3":
                        Console.WriteLine("Update visit:");
                        await UpdateVisit();
                        Pause();
                        break;

                    case "4":
                        Console.WriteLine("Delete visit:");
                        await DeleteVisit();
                        Pause();
                        break;

                    case "5":
                        Console.WriteLine("Search visit:");
                        await SearchVisit();
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

        private async Task AddVisit()
        {
            Console.Clear();
            Console.WriteLine("===== ADD VISIT =====");

            Console.Write("Visitor ID: ");
            string? visitorInput = Console.ReadLine();

            bool isValidVisitorId = Guid.TryParse(visitorInput, out Guid visitorId);

            if (!isValidVisitorId)
            {
                Console.WriteLine("Invalid visitor ID.");
                return;
            }

            Console.Write("Visit date (yyyy-mm-dd): ");
            DateTime visitDate = DateTime.Parse(Console.ReadLine() ?? DateTime.Now.ToString("yyyy-MM-dd"));

            Console.Write("Has paid ticket? (yes/no): ");
            string paidInput = Console.ReadLine() ?? string.Empty;

            bool hasPaidTicket = paidInput.ToLower() == "yes" || paidInput.ToLower() == "y";

            Visit visit = new Visit
            {
                Id = Guid.NewGuid(),
                VisitorId = visitorId,
                VisitDate = visitDate,
                HasPaidTicket = hasPaidTicket
            };

            var newVisit = await _service.AddVisitAsync(visit);

            if (newVisit != null)
            {
                Console.WriteLine("Visit added successfully.");
            }
            else
            {
                Console.WriteLine("Failed to add visit.");
            }
        }

        private async Task ShowAllVisits()
        {
            Console.Clear();
            Console.WriteLine("===== ALL VISITS =====");

            var visits = await _service.GetAllVisitsAsync();

            if (visits.Count == 0)
            {
                Console.WriteLine("No visits found.");
                return;
            }

            foreach (var visit in visits)
            {
                Console.WriteLine($"ID: {visit.Id}");
                Console.WriteLine($"Visitor ID: {visit.VisitorId}");
                Console.WriteLine($"Visit date: {visit.VisitDate:yyyy-MM-dd}");
                Console.WriteLine($"Has paid ticket: {visit.HasPaidTicket}");
                Console.WriteLine();
            }
        }

        private async Task UpdateVisit()
        {
            Console.Clear();
            Console.WriteLine("===== UPDATE VISIT =====");

            Console.Write("Enter visit ID: ");
            string? idInput = Console.ReadLine();

            bool isValidId = Guid.TryParse(idInput, out Guid visitId);

            if (!isValidId)
            {
                Console.WriteLine("Invalid visit ID.");
                return;
            }

            var visit = await _service.GetVisitByIdAsync(visitId);

            if (visit == null)
            {
                Console.WriteLine("Visit not found.");
                return;
            }

            Console.Write("Visit date (yyyy-mm-dd): ");
            DateTime visitDate = DateTime.Parse(Console.ReadLine() ?? DateTime.Now.ToString("yyyy-MM-dd"));

            Console.Write("Has paid ticket? (yes/no): ");
            string paidInput = Console.ReadLine() ?? string.Empty;

            bool hasPaidTicket = paidInput.ToLower() == "yes" || paidInput.ToLower() == "y";

            bool isUpdated = await _service.UpdateVisitAsync(visitId, new Visit
            {
                Id = visit.Id,
                VisitorId = visit.VisitorId,
                VisitDate = visitDate,
                HasPaidTicket = hasPaidTicket
            });

            if (isUpdated)
            {
                Console.WriteLine("Visit updated successfully.");
            }
            else
            {
                Console.WriteLine("Failed to update visit.");
            }
        }

        private async Task DeleteVisit()
        {
            Console.Clear();
            Console.WriteLine("===== DELETE VISIT =====");

            Console.Write("Enter visit ID: ");
            string? idInput = Console.ReadLine();

            bool isValidId = Guid.TryParse(idInput, out Guid visitId);

            if (!isValidId)
            {
                Console.WriteLine("Invalid visit ID.");
                return;
            }

            bool isDeleted = await _service.DeleteVisitAsync(visitId);

            if (isDeleted)
            {
                Console.WriteLine("Visit deleted successfully.");
            }
            else
            {
                Console.WriteLine("Failed to delete visit.");
            }
        }

        private async Task SearchVisit()
        {
            Console.Clear();
            Console.WriteLine("===== SEARCH VISIT =====");

            Console.Write("Enter visit ID: ");
            string? idInput = Console.ReadLine();

            bool isValidId = Guid.TryParse(idInput, out Guid visitId);

            if (!isValidId)
            {
                Console.WriteLine("Invalid visit ID.");
                return;
            }

            var visit = await _service.GetVisitByIdAsync(visitId);

            if (visit == null)
            {
                Console.WriteLine("Visit not found.");
                return;
            }

            Console.WriteLine($"ID: {visit.Id}");
            Console.WriteLine($"Visitor ID: {visit.VisitorId}");
            Console.WriteLine($"Visit date: {visit.VisitDate:yyyy-MM-dd}");
            Console.WriteLine($"Has paid ticket: {visit.HasPaidTicket}");
        }

        private static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}