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
                Console.WriteLine("0. Back to main menu");
                Console.Write("Choose an option: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await AddHabitat();
                        Pause();
                        break;

                    case "2":
                        await ShowAllHabitats();
                        Pause();
                        break;

                    case "3":
                        await UpdateHabitat();
                        Pause();
                        break;

                    case "4":
                        await DeleteHabitat();
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

        private async Task<Habitat?> SelectHabitatFromNumber()
        {
            var habitats = await _service.GetAllHabitatsAsync();

            if (habitats.Count == 0)
            {
                Console.WriteLine("No habitats found.");
                return null;
            }

            Console.WriteLine("Choose a habitat:");
            Console.WriteLine();

            for (int i = 0; i < habitats.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {habitats[i].Name}");
                Console.WriteLine($"   Climate: {habitats[i].Climate}");
                Console.WriteLine($"   Vegetation: {habitats[i].Vegetation}");
                Console.WriteLine();
            }

            Console.Write("Enter number: ");
            string? input = Console.ReadLine();

            bool isValidNumber = int.TryParse(input, out int selectedNumber);

            if (!isValidNumber || selectedNumber < 1 || selectedNumber > habitats.Count)
            {
                Console.WriteLine("Invalid number.");
                return null;
            }

            return habitats[selectedNumber - 1];
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

            for (int i = 0; i < habitats.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {habitats[i].Name}");
                Console.WriteLine($"Climate: {habitats[i].Climate}");
                Console.WriteLine($"Vegetation: {habitats[i].Vegetation}");
                Console.WriteLine();
            }
        }

        private async Task UpdateHabitat()
        {
            Console.Clear();
            Console.WriteLine("===== UPDATE HABITAT =====");

            Habitat? selectedHabitat = await SelectHabitatFromNumber();

            if (selectedHabitat == null)
            {
                return;
            }

            Console.WriteLine();
            Console.WriteLine($"Selected habitat: {selectedHabitat.Name}");
            Console.WriteLine();

            Console.Write("New name: ");
            string name = Console.ReadLine() ?? string.Empty;

            Console.Write("New climate: ");
            string climate = Console.ReadLine() ?? string.Empty;

            Console.Write("New vegetation: ");
            string vegetation = Console.ReadLine() ?? string.Empty;

            Habitat updatedHabitat = new Habitat
            {
                Id = selectedHabitat.Id,
                Name = name,
                Climate = climate,
                Vegetation = vegetation
            };

            bool isUpdated = await _service.UpdateHabitatAsync(selectedHabitat.Id, updatedHabitat);

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

            Habitat? selectedHabitat = await SelectHabitatFromNumber();

            if (selectedHabitat == null)
            {
                return;
            }

            Console.WriteLine();
            Console.WriteLine($"Selected habitat: {selectedHabitat.Name}");
            Console.Write("Are you sure you want to delete this habitat? (y/n): ");

            string? confirmation = Console.ReadLine();

            if (confirmation?.ToLower() != "y")
            {
                Console.WriteLine("Delete cancelled.");
                return;
            }

            bool isDeleted = await _service.DeleteHabitatAsync(selectedHabitat.Id);

            if (isDeleted)
            {
                Console.WriteLine("Habitat deleted successfully.");
            }
            else
            {
                Console.WriteLine("Failed to delete habitat.");
            }
        }

        private static void PrintHabitat(Habitat habitat)
        {
            Console.WriteLine($"Name: {habitat.Name}");
            Console.WriteLine($"Climate: {habitat.Climate}");
            Console.WriteLine($"Vegetation: {habitat.Vegetation}");
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