using Application.Services;
using Domain.Entities;

namespace Presentation.Menus
{
    public class VisitorMenu
    {
        private readonly VisitorService _service;

        public VisitorMenu(VisitorService service)
        {
            _service = service;
        }

        public async Task StartVisitorMenu()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();

                Console.WriteLine("===== VISITOR MENU =====");
                Console.WriteLine("1. Add visitor");
                Console.WriteLine("2. Show all visitors");
                Console.WriteLine("3. Update visitor");
                Console.WriteLine("4. Delete visitor");
                Console.WriteLine("5. Search visitor");
                Console.WriteLine("0. Back to main menu");
                Console.Write("Choose an option: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Add visitor:");
                        await AddVisitor();
                        Pause();
                        break;

                    case "2":
                        Console.WriteLine("Show all visitors:");
                        await ShowAllVisitors();
                        Pause();
                        break;

                    case "3":
                        Console.WriteLine("Update visitor:");
                        await UpdateVisitor();
                        Pause();
                        break;

                    case "4":
                        Console.WriteLine("Delete visitor:");
                        await DeleteVisitor();
                        Pause();
                        break;

                    case "5":
                        Console.WriteLine("Search visitor:");
                        await SearchVisitor();
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

        private async Task AddVisitor()
        {
            Console.Clear();
            Console.WriteLine("===== ADD VISITOR =====");

            Console.Write("Full name: ");
            string fullName = Console.ReadLine() ?? string.Empty;

            Console.Write("Phone number: ");
            string phoneNumber = Console.ReadLine() ?? string.Empty;

            Console.Write("Age: ");
            int age = int.Parse(Console.ReadLine() ?? "0");

            Visitor visitor = new Visitor
            {
                Id = Guid.NewGuid(),
                FullName = fullName,
                PhoneNumber = phoneNumber,
                Age = age
            };

            var newVisitor = await _service.AddVisitorAsync(visitor);

            if (newVisitor != null)
            {
                Console.WriteLine("Visitor added successfully.");
            }
            else
            {
                Console.WriteLine("Failed to add visitor.");
            }
        }

        private async Task ShowAllVisitors()
        {
            Console.Clear();
            Console.WriteLine("===== ALL VISITORS =====");

            var visitors = await _service.GetAllVisitorsAsync();

            if (visitors.Count == 0)
            {
                Console.WriteLine("No visitors found.");
                return;
            }

            foreach (var visitor in visitors)
            {
                Console.WriteLine($"ID: {visitor.Id}");
                Console.WriteLine($"Full name: {visitor.FullName}");
                Console.WriteLine($"Phone number: {visitor.PhoneNumber}");
                Console.WriteLine($"Age: {visitor.Age}");
                Console.WriteLine();
            }
        }

        private async Task UpdateVisitor()
        {
            Console.Clear();
            Console.WriteLine("===== UPDATE VISITOR =====");

            Console.Write("Enter visitor ID: ");
            string? idInput = Console.ReadLine();

            bool isValidId = Guid.TryParse(idInput, out Guid visitorId);

            if (!isValidId)
            {
                Console.WriteLine("Invalid visitor ID.");
                return;
            }

            var visitor = await _service.GetVisitorByIdAsync(visitorId);

            if (visitor == null)
            {
                Console.WriteLine("Visitor not found.");
                return;
            }

            Console.Write("Full name: ");
            string fullName = Console.ReadLine() ?? string.Empty;

            Console.Write("Phone number: ");
            string phoneNumber = Console.ReadLine() ?? string.Empty;

            Console.Write("Age: ");
            int age = int.Parse(Console.ReadLine() ?? "0");

            bool isUpdated = await _service.UpdateVisitorAsync(visitorId, new Visitor
            {
                Id = visitor.Id,
                FullName = fullName,
                PhoneNumber = phoneNumber,
                Age = age
            });

            if (isUpdated)
            {
                Console.WriteLine("Visitor updated successfully.");
            }
            else
            {
                Console.WriteLine("Failed to update visitor.");
            }
        }

        private async Task DeleteVisitor()
        {
            Console.Clear();
            Console.WriteLine("===== DELETE VISITOR =====");

            Console.Write("Enter visitor ID: ");
            string? idInput = Console.ReadLine();

            bool isValidId = Guid.TryParse(idInput, out Guid visitorId);

            if (!isValidId)
            {
                Console.WriteLine("Invalid visitor ID.");
                return;
            }

            bool isDeleted = await _service.DeleteVisitorAsync(visitorId);

            if (isDeleted)
            {
                Console.WriteLine("Visitor deleted successfully.");
            }
            else
            {
                Console.WriteLine("Failed to delete visitor.");
            }
        }

        private async Task SearchVisitor()
        {
            Console.Clear();
            Console.WriteLine("===== SEARCH VISITOR =====");

            Console.Write("Enter visitor ID: ");
            string? idInput = Console.ReadLine();

            bool isValidId = Guid.TryParse(idInput, out Guid visitorId);

            if (!isValidId)
            {
                Console.WriteLine("Invalid visitor ID.");
                return;
            }

            var visitor = await _service.GetVisitorByIdAsync(visitorId);

            if (visitor == null)
            {
                Console.WriteLine("Visitor not found.");
                return;
            }

            Console.WriteLine($"ID: {visitor.Id}");
            Console.WriteLine($"Full name: {visitor.FullName}");
            Console.WriteLine($"Phone number: {visitor.PhoneNumber}");
            Console.WriteLine($"Age: {visitor.Age}");
        }

        private static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}