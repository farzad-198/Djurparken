using Application.Services;
using Domain.Entities;


namespace Presentation.Menus
{

    public class AnimalMenu
    {

        private readonly AnimalService _Service;

        public AnimalMenu(AnimalService service)
        {
            _Service = service;
        }
        public async Task StartAnimalMenu()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();

                Console.WriteLine("===== ANIMAL MENU =====");
                Console.WriteLine("1. Add animal");
                Console.WriteLine("2. Show all animals");
                Console.WriteLine("3. Update animal");
                Console.WriteLine("4. Delete animal");
                Console.WriteLine("5. Search animals");
                Console.WriteLine("0. Back to main menu");
                Console.Write("Choose an option: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Add animal:");
                        await AddAnimal();
                        Pause();
                        break;

                    case "2":
                        Console.WriteLine("Show all animals: ");
                        await ShowAllAnimals();
                        Pause();
                        break;

                    case "3":
                        Console.WriteLine("Update animal:");
                        await UpdateAnimal();
                        Pause();
                        break;

                    case "4":
                        Console.WriteLine("Delete animal:");
                        await DeleteAnimal();
                        Pause();
                        break;

                    case "5":
                        Console.WriteLine(" Search animals :");
                        await SearchAnimals();
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
        private static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }


        private async Task AddAnimal()
        {
            Console.Clear();
            Console.WriteLine("===== ADD ANIMAL =====");

            Console.Write("Name: ");
            string name = Console.ReadLine() ?? string.Empty;

            Console.Write("Species: ");
            string species = Console.ReadLine() ?? string.Empty;

            Console.Write("Gender: ");
            string gender = Console.ReadLine() ?? string.Empty;

            Console.Write("Status: ");
            string status = Console.ReadLine() ?? string.Empty;

            Console.Write("Birth date (yyyy-mm-dd): ");
            DateTime birthDate = DateTime.Parse(Console.ReadLine() ?? DateTime.Now.ToString("yyyy-MM-dd"));

            Console.WriteLine();
            Console.Write("Enter habitat ID: ");
            string? habitatInput = Console.ReadLine();

            bool isValidHabitatId = Guid.TryParse(habitatInput, out Guid habitatId);

            if (!isValidHabitatId)
            {
                Console.WriteLine("Invalid habitat ID.");
                Pause();
                return;
            }

            Animal animal = new Animal
            {
                Id = Guid.NewGuid(),
                Name = name,
                Species = species,
                Gender = gender,
                Status = status,
                BirthDate = birthDate,
                HabitatId = habitatId
            };
            var newAnimal = await _Service.AddAnimalAsync(animal);
            if (newAnimal != null)
            {
                Console.WriteLine("Animal added successfully.");
            }
            else
            {
                Console.WriteLine("Failed to add animal.");
            }
            Console.WriteLine();

        }
        private async Task ShowAllAnimals()
        {
            Console.Clear();
            Console.WriteLine("===== ALL ANIMALS =====");
            var animals = await _Service.GetAllAnimalsAsync();
            if (animals.Count == 0)
            {
                Console.WriteLine("No animals found.");
                return;
            }
            foreach (var animal in animals)
            {
                Console.WriteLine($"ID: {animal.Id}");
                Console.WriteLine($"Name: {animal.Name}");
                Console.WriteLine($"Species: {animal.Species}");
                Console.WriteLine($"Gender: {animal.Gender}");
                Console.WriteLine($"Status: {animal.Status}");
                Console.WriteLine($"Birth Date: {animal.BirthDate}");
                Console.WriteLine($"Habitat ID: {animal.HabitatId}");
                Console.WriteLine();
            }
        }
        private async Task UpdateAnimal()
        {
            Console.Clear();
            Console.WriteLine("===== UPDATE ANIMAL =====");
            Console.Write("Enter animal ID: ");
            string? idInput = Console.ReadLine();
            bool isValidId = Guid.TryParse(idInput, out Guid animalId);
            if (!isValidId)
            {
                Console.WriteLine("Invalid animal ID.");
                Pause();
                return;
            }
            var animal = await _Service.GetAnimalByIdAsync(animalId);
            if (animal == null)
            {
                Console.WriteLine("Animal not found.");
                Pause();
                return;
            }
            Console.Write("Name: ");
            string name = Console.ReadLine() ?? string.Empty;

            Console.Write("Species: ");
            string species = Console.ReadLine() ?? string.Empty;

            Console.Write("Gender: ");
            string gender = Console.ReadLine() ?? string.Empty;

            Console.Write("Status: ");
            string status = Console.ReadLine() ?? string.Empty;

            Console.Write("Birth date (yyyy-mm-dd): ");
            DateTime birthDate = DateTime.Parse(Console.ReadLine() ?? DateTime.Now.ToString("yyyy-MM-dd"));
            bool isUpdated = await _Service.UpdateAnimalAsync(animalId, new Animal
            {
                Id = animal.Id,
                Name = name,
                Species = species,
                Gender = gender,
                Status = status,
                BirthDate = birthDate,
                HabitatId = animal.HabitatId
            });
            if (isUpdated)
            {
                Console.WriteLine("Animal updated successfully.");
            }
            else
            {
                Console.WriteLine("Failed to update animal.");
            }
        }
        private async Task DeleteAnimal()
        {
            Console.Clear();
            Console.WriteLine("===== DELETE ANIMAL =====");
            Console.Write("Enter animal ID: ");
            string? idInput = Console.ReadLine();
            bool isValidId = Guid.TryParse(idInput, out Guid animalId);
            if (!isValidId)
            {
                Console.WriteLine("Invalid animal ID.");
                Pause();
                return;
            }
            bool isDeleted = await _Service.DeleteAnimalAsync(animalId);
            if (isDeleted)
            {
                Console.WriteLine("Animal deleted successfully.");
            }
            else
            {
                Console.WriteLine("Failed to delete animal.");
            }
        }
        private async Task SearchAnimals()
        {
            Console.Clear();
            Console.WriteLine("===== SEARCH ANIMALS =====");
            Console.Write("Enter search Id: ");
            string? idInput = Console.ReadLine();
            bool isValidId = Guid.TryParse(idInput, out Guid animalId);
            if (!isValidId)
            {
                Console.WriteLine("Invalid animal ID.");
                Pause();
                return;
            }
            var animal = await _Service.GetAnimalByIdAsync(animalId);
            if (animal == null)
            {
                Console.WriteLine("Animal not found.");
                Pause();
                return;
            }
            Console.WriteLine($"ID: {animal.Id}");
            Console.WriteLine($"Name: {animal.Name}");
            Console.WriteLine($"Species: {animal.Species}");
            Console.WriteLine($"Gender: {animal.Gender}");
            Console.WriteLine($"Status: {animal.Status}");
            Console.WriteLine($"Birth Date: {animal.BirthDate}");
            Console.WriteLine($"Habitat ID: {animal.HabitatId}");
            Console.WriteLine();

        }
    }
}
        
