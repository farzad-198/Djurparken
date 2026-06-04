using Application.Services;
using Domain.Entities;

namespace Presentation.Menus
{
    public class VisitMenu
    {
        private readonly VisitService _visitService;
        private readonly VisitorService _visitorService;

        public VisitMenu(VisitService visitService, VisitorService visitorService)
        {
            _visitService = visitService;
            _visitorService = visitorService;
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
                Console.WriteLine("0. Back to main menu");
                Console.Write("Choose an option: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await AddVisit();
                        Pause();
                        break;

                    case "2":
                        await ShowAllVisits();
                        Pause();
                        break;

                    case "3":
                        await UpdateVisit();
                        Pause();
                        break;

                    case "4":
                        await DeleteVisit();
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

        private async Task<Visitor?> SelectVisitorFromNumber()
        {
            var visitors = await _visitorService.GetAllVisitorsAsync();

            if (visitors.Count == 0)
            {
                Console.WriteLine("No visitors found.");
                Console.WriteLine("You must add a visitor before adding a visit.");
                return null;
            }

            Console.WriteLine("Choose a visitor:");
            Console.WriteLine();

            for (int i = 0; i < visitors.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {visitors[i].FullName}");
                Console.WriteLine($"   Phone: {visitors[i].PhoneNumber}");
                Console.WriteLine($"   Age: {visitors[i].Age}");
                Console.WriteLine();
            }

            Console.Write("Enter visitor number: ");
            string? input = Console.ReadLine();

            bool isValidNumber = int.TryParse(input, out int selectedNumber);

            if (!isValidNumber || selectedNumber < 1 || selectedNumber > visitors.Count)
            {
                Console.WriteLine("Invalid visitor number.");
                return null;
            }

            return visitors[selectedNumber - 1];
        }

        private async Task<Visit?> SelectVisitFromNumber()
        {
            var visits = await _visitService.GetAllVisitsAsync();
            var visitors = await _visitorService.GetAllVisitorsAsync();

            if (visits.Count == 0)
            {
                Console.WriteLine("No visits found.");
                return null;
            }

            Console.WriteLine("Choose a visit:");
            Console.WriteLine();

            for (int i = 0; i < visits.Count; i++)
            {
                Visitor? visitor = visitors.FirstOrDefault(v => v.Id == visits[i].VisitorId);
                string visitorName = visitor != null ? visitor.FullName : "Unknown visitor";

                Console.WriteLine($"{i + 1}. {visitorName}");
                Console.WriteLine($"   Visit date: {visits[i].VisitDate:yyyy-MM-dd}");
                Console.WriteLine($"   Has paid ticket: {visits[i].HasPaidTicket}");
                Console.WriteLine();
            }

            Console.Write("Enter visit number: ");
            string? input = Console.ReadLine();

            bool isValidNumber = int.TryParse(input, out int selectedNumber);

            if (!isValidNumber || selectedNumber < 1 || selectedNumber > visits.Count)
            {
                Console.WriteLine("Invalid visit number.");
                return null;
            }

            return visits[selectedNumber - 1];
        }

        private async Task AddVisit()
        {
            Console.Clear();
            Console.WriteLine("===== ADD VISIT =====");

            Visitor? selectedVisitor = await SelectVisitorFromNumber();

            if (selectedVisitor == null)
            {
                return;
            }

            Console.WriteLine();
            Console.WriteLine($"Selected visitor: {selectedVisitor.FullName}");
            Console.WriteLine();

            Console.Write("Visit date (yyyy-mm-dd): ");
            string? visitDateInput = Console.ReadLine();

            bool isValidVisitDate = DateTime.TryParse(visitDateInput, out DateTime visitDate);

            if (!isValidVisitDate)
            {
                Console.WriteLine("Invalid visit date.");
                return;
            }

            Console.Write("Has paid ticket? (yes/no): ");
            string paidInput = Console.ReadLine() ?? string.Empty;

            bool hasPaidTicket = paidInput.ToLower() == "yes" || paidInput.ToLower() == "y";

            Visit visit = new Visit
            {
                Id = Guid.NewGuid(),
                VisitorId = selectedVisitor.Id,
                VisitDate = visitDate,
                HasPaidTicket = hasPaidTicket
            };

            var newVisit = await _visitService.AddVisitAsync(visit);

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

            var visits = await _visitService.GetAllVisitsAsync();
            var visitors = await _visitorService.GetAllVisitorsAsync();

            if (visits.Count == 0)
            {
                Console.WriteLine("No visits found.");
                return;
            }

            for (int i = 0; i < visits.Count; i++)
            {
                Visitor? visitor = visitors.FirstOrDefault(v => v.Id == visits[i].VisitorId);
                string visitorName = visitor != null ? visitor.FullName : "Unknown visitor";

                Console.WriteLine($"{i + 1}. {visitorName}");
                Console.WriteLine($"Visit date: {visits[i].VisitDate:yyyy-MM-dd}");
                Console.WriteLine($"Has paid ticket: {visits[i].HasPaidTicket}");
                Console.WriteLine();
            }
        }

        private async Task UpdateVisit()
        {
            Console.Clear();
            Console.WriteLine("===== UPDATE VISIT =====");

            Visit? selectedVisit = await SelectVisitFromNumber();

            if (selectedVisit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Choose new visitor:");
            Visitor? selectedVisitor = await SelectVisitorFromNumber();

            if (selectedVisitor == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New visit date (yyyy-mm-dd): ");
            string? visitDateInput = Console.ReadLine();

            bool isValidVisitDate = DateTime.TryParse(visitDateInput, out DateTime visitDate);

            if (!isValidVisitDate)
            {
                Console.WriteLine("Invalid visit date.");
                return;
            }

            Console.Write("Has paid ticket? (yes/no): ");
            string paidInput = Console.ReadLine() ?? string.Empty;

            bool hasPaidTicket = paidInput.ToLower() == "yes" || paidInput.ToLower() == "y";

            Visit updatedVisit = new Visit
            {
                Id = selectedVisit.Id,
                VisitorId = selectedVisitor.Id,
                VisitDate = visitDate,
                HasPaidTicket = hasPaidTicket
            };

            bool isUpdated = await _visitService.UpdateVisitAsync(selectedVisit.Id, updatedVisit);

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

            Visit? selectedVisit = await SelectVisitFromNumber();

            if (selectedVisit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("Are you sure you want to delete this visit? (y/n): ");

            string? confirmation = Console.ReadLine();

            if (confirmation?.ToLower() != "y")
            {
                Console.WriteLine("Delete cancelled.");
                return;
            }

            bool isDeleted = await _visitService.DeleteVisitAsync(selectedVisit.Id);

            if (isDeleted)
            {
                Console.WriteLine("Visit deleted successfully.");
            }
            else
            {
                Console.WriteLine("Failed to delete visit.");
            }
        }


        private static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}