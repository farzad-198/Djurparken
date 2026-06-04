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
                Console.WriteLine("0. Back to main menu");
                Console.Write("Choose an option: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await AddVisitor();
                        Pause();
                        break;

                    case "2":
                        await ShowAllVisitors();
                        Pause();
                        break;

                    case "3":
                        await UpdateVisitor();
                        Pause();
                        break;

                    case "4":
                        await DeleteVisitor();
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
            var visitors = await _service.GetAllVisitorsAsync();

            if (visitors.Count == 0)
            {
                Console.WriteLine("No visitors found.");
                return null;
            }

            Console.WriteLine("Choose a visitor:");
            Console.WriteLine();

            for (int i = 0; i < visitors.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {visitors[i].FullName}");
                Console.WriteLine($"   Phone number: {visitors[i].PhoneNumber}");
                Console.WriteLine($"   Age: {visitors[i].Age}");
                Console.WriteLine();
            }

            Console.Write("Enter number: ");
            string? input = Console.ReadLine();

            bool isValidNumber = int.TryParse(input, out int selectedNumber);

            if (!isValidNumber || selectedNumber < 1 || selectedNumber > visitors.Count)
            {
                Console.WriteLine("Invalid number.");
                return null;
            }

            return visitors[selectedNumber - 1];
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
            string? ageInput = Console.ReadLine();

            bool isValidAge = int.TryParse(ageInput, out int age);

            if (!isValidAge)
            {
                Console.WriteLine("Invalid age.");
                return;
            }

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

            for (int i = 0; i < visitors.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {visitors[i].FullName}");
                Console.WriteLine($"Phone number: {visitors[i].PhoneNumber}");
                Console.WriteLine($"Age: {visitors[i].Age}");
                Console.WriteLine();
            }
        }

        private async Task UpdateVisitor()
        {
            Console.Clear();
            Console.WriteLine("===== UPDATE VISITOR =====");

            Visitor? selectedVisitor = await SelectVisitorFromNumber();

            if (selectedVisitor == null)
            {
                return;
            }

            Console.WriteLine();
            Console.WriteLine($"Selected visitor: {selectedVisitor.FullName}");
            Console.WriteLine();

            Console.Write("New full name: ");
            string fullName = Console.ReadLine() ?? string.Empty;

            Console.Write("New phone number: ");
            string phoneNumber = Console.ReadLine() ?? string.Empty;

            Console.Write("New age: ");
            string? ageInput = Console.ReadLine();

            bool isValidAge = int.TryParse(ageInput, out int age);

            if (!isValidAge)
            {
                Console.WriteLine("Invalid age.");
                return;
            }

            Visitor updatedVisitor = new Visitor
            {
                Id = selectedVisitor.Id,
                FullName = fullName,
                PhoneNumber = phoneNumber,
                Age = age
            };

            bool isUpdated = await _service.UpdateVisitorAsync(selectedVisitor.Id, updatedVisitor);

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

            Visitor? selectedVisitor = await SelectVisitorFromNumber();

            if (selectedVisitor == null)
            {
                return;
            }

            Console.WriteLine();
            Console.WriteLine($"Selected visitor: {selectedVisitor.FullName}");
            Console.Write("Are you sure you want to delete this visitor? (y/n): ");

            string? confirmation = Console.ReadLine();

            if (confirmation?.ToLower() != "y")
            {
                Console.WriteLine("Delete cancelled.");
                return;
            }

            bool isDeleted = await _service.DeleteVisitorAsync(selectedVisitor.Id);

            if (isDeleted)
            {
                Console.WriteLine("Visitor deleted successfully.");
            }
            else
            {
                Console.WriteLine("Failed to delete visitor.");
            }
        }

        private static void PrintVisitor(Visitor visitor)
        {
            Console.WriteLine($"Full name: {visitor.FullName}");
            Console.WriteLine($"Phone number: {visitor.PhoneNumber}");
            Console.WriteLine($"Age: {visitor.Age}");
            Console.WriteLine();
        }

        private static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}