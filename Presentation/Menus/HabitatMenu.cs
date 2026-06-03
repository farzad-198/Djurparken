using Application.Services;
using Domain.Entities;

namespace Presentation.Menus
{
    public class HabitatMenu
    {
        private readonly HabitatService _service;

        public HabitatMenu(HabitatService service)
        {
            _service = service;
        }

        public async Task StartHabitatMenu()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();

                Console.WriteLine("===== HABITAT MENU =====");
                Console.WriteLine("1. Add habitat");
                Console.WriteLine("2. Show all habitats");
                Console.WriteLine("3. Update habitat");
                Console.WriteLine("4. Delete habitat");
                Console.WriteLine("5. Search habitat");
                Console.WriteLine("0. Back to main menu");
                Console.Write("Choose an option: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Add habitat:");
                        await AddHabitat();
                        Pause();
                        break;

                    case "2":
                        Console.WriteLine("Show all habitats:");
                        await ShowAllHabitats();
                        Pause();
                        break;

                    case "3":
                        Console.WriteLine("Update habitat:");
                        await UpdateHabitat();
                        Pause();
                        break;

                    case "4":
                        Console.WriteLine("Delete habitat:");
                        await DeleteHabitat();
                        Pause();
                        break;

                    case "5":
                        Console.WriteLine("Search habitat:");
                        await SearchHabitat();
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

        private async Task AddHabitat()
        {
            Console.Clear();
            Console.WriteLine("===== ADD HABITAT =====");

            Console.Write("Name: ");
            string name = Console.ReadLine() ?? string.Empty;

            Console.Write("Climate: ");
            string climate = Console.ReadLine() ?? string.Empty;

            Console.Write("Vegetation: ");
            string vegetation = Console.ReadLine() ?? string.Empty;

            Habitat habitat = new Habitat
            {
                Id = Guid.NewGuid(),
                Name = name,
                Climate = climate,
                Vegetation = vegetation
            };

            var newHabitat = await _service.AddHabitatAsync(habitat);

            if (newHabitat != null)
            {
                Console.WriteLine("Habitat added successfully.");
            }
            else
            {
                Console.WriteLine("Failed to add habitat.");
            }
        }

        private async Task ShowAllHabitats()
        {
            Console.Clear();
            Console.WriteLine("===== ALL HABITATS =====");

            var habitats = await _service.GetAllHabitatsAsync();

            if (habitats.Count == 0)
            {
                Console.WriteLine("No habitats found.");
                return;
            }

            foreach (var habitat in habitats)
            {
                Console.WriteLine($"ID: {habitat.Id}");
                Console.WriteLine($"Name: {habitat.Name}");
                Console.WriteLine($"Climate: {habitat.Climate}");
                Console.WriteLine($"Vegetation: {habitat.Vegetation}");
                Console.WriteLine();
            }
        }

        private async Task UpdateHabitat()
        {
            Console.Clear();
            Console.WriteLine("===== UPDATE HABITAT =====");

            Console.Write("Enter habitat ID: ");
            string? idInput = Console.ReadLine();

            bool isValidId = Guid.TryParse(idInput, out Guid habitatId);

            if (!isValidId)
            {
                Console.WriteLine("Invalid habitat ID.");
                return;
            }

            var habitat = await _service.GetHabitatByIdAsync(habitatId);

            if (habitat == null)
            {
                Console.WriteLine("Habitat not found.");
                return;
            }

            Console.Write("Name: ");
            string name = Console.ReadLine() ?? string.Empty;

            Console.Write("Climate: ");
            string climate = Console.ReadLine() ?? string.Empty;

            Console.Write("Vegetation: ");
            string vegetation = Console.ReadLine() ?? string.Empty;

            bool isUpdated = await _service.UpdateHabitatAsync(habitatId, new Habitat
            {
                Id = habitat.Id,
                Name = name,
                Climate = climate,
                Vegetation = vegetation
            });

            if (isUpdated)
            {
                Console.WriteLine("Habitat updated successfully.");
            }
            else
            {
                Console.WriteLine("Failed to update habitat.");
            }
        }

        private async Task DeleteHabitat()
        {
            Console.Clear();
            Console.WriteLine("===== DELETE HABITAT =====");

            Console.Write("Enter habitat ID: ");
            string? idInput = Console.ReadLine();

            bool isValidId = Guid.TryParse(idInput, out Guid habitatId);

            if (!isValidId)
            {
                Console.WriteLine("Invalid habitat ID.");
                return;
            }

            bool isDeleted = await _service.DeleteHabitatAsync(habitatId);

            if (isDeleted)
            {
                Console.WriteLine("Habitat deleted successfully.");
            }
            else
            {
                Console.WriteLine("Failed to delete habitat.");
            }
        }

        private async Task SearchHabitat()
        {
            Console.Clear();
            Console.WriteLine("===== SEARCH HABITAT =====");

            Console.Write("Enter habitat ID: ");
            string? idInput = Console.ReadLine();

            bool isValidId = Guid.TryParse(idInput, out Guid habitatId);

            if (!isValidId)
            {
                Console.WriteLine("Invalid habitat ID.");
                return;
            }

            var habitat = await _service.GetHabitatByIdAsync(habitatId);

            if (habitat == null)
            {
                Console.WriteLine("Habitat not found.");
                return;
            }

            Console.WriteLine($"ID: {habitat.Id}");
            Console.WriteLine($"Name: {habitat.Name}");
            Console.WriteLine($"Climate: {habitat.Climate}");
            Console.WriteLine($"Vegetation: {habitat.Vegetation}");
        }

        private static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}