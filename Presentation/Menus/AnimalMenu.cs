using Application.Services;
using Domain.Entities;

namespace Presentation.Menus
{
    public class AnimalMenu
    {
        private readonly AnimalService _animalService;
        private readonly HabitatService _habitatService;

        public AnimalMenu(AnimalService animalService, HabitatService habitatService)
        {
            _animalService = animalService;
            _habitatService = habitatService;
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
                Console.WriteLine("0. Back to main menu");
                Console.Write("Choose an option: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await AddAnimal();
                        Pause();
                        break;

                    case "2":
                        await ShowAllAnimals();
                        Pause();
                        break;

                    case "3":
                        await UpdateAnimal();
                        Pause();
                        break;

                    case "4":
                        await DeleteAnimal();
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

        private async Task<Animal?> SelectAnimalFromNumber()
        {
            var animals = await _animalService.GetAllAnimalsAsync();

            if (animals.Count == 0)
            {
                Console.WriteLine("No animals found.");
                return null;
            }

            Console.WriteLine("Choose an animal:");
            Console.WriteLine();

            for (int i = 0; i < animals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {animals[i].Name} - {animals[i].Species} - {animals[i].Status}");
            }

            Console.WriteLine();
            Console.Write("Enter number: ");

            string? input = Console.ReadLine();

            bool isValidNumber = int.TryParse(input, out int selectedNumber);

            if (!isValidNumber || selectedNumber < 1 || selectedNumber > animals.Count)
            {
                Console.WriteLine("Invalid number.");
                return null;
            }

            return animals[selectedNumber - 1];
        }

        private async Task<Habitat?> SelectHabitatFromNumber()
        {
            var habitats = await _habitatService.GetAllHabitatsAsync();

            if (habitats.Count == 0)
            {
                Console.WriteLine("No habitats found.");
                Console.WriteLine("You must add a habitat before adding an animal.");
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

            Console.Write("Enter habitat number: ");
            string? input = Console.ReadLine();

            bool isValidNumber = int.TryParse(input, out int selectedNumber);

            if (!isValidNumber || selectedNumber < 1 || selectedNumber > habitats.Count)
            {
                Console.WriteLine("Invalid habitat number.");
                return null;
            }

            return habitats[selectedNumber - 1];
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
            string? birthDateInput = Console.ReadLine();

            bool isValidBirthDate = DateTime.TryParse(birthDateInput, out DateTime birthDate);

            if (!isValidBirthDate)
            {
                Console.WriteLine("Invalid birth date.");
                return;
            }

            Console.WriteLine();
            Habitat? selectedHabitat = await SelectHabitatFromNumber();

            if (selectedHabitat == null)
            {
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
                HabitatId = selectedHabitat.Id
            };

            var newAnimal = await _animalService.AddAnimalAsync(animal);

            if (newAnimal != null)
            {
                Console.WriteLine("Animal added successfully.");
                Console.WriteLine($"Habitat: {selectedHabitat.Name}");
            }
            else
            {
                Console.WriteLine("Failed to add animal.");
            }
        }

        private async Task ShowAllAnimals()
        {
            Console.Clear();
            Console.WriteLine("===== ALL ANIMALS =====");

            var animals = await _animalService.GetAllAnimalsAsync();

            if (animals.Count == 0)
            {
                Console.WriteLine("No animals found.");
                return;
            }

            for (int i = 0; i < animals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {animals[i].Name}");
                Console.WriteLine($"Species: {animals[i].Species}");
                Console.WriteLine($"Gender: {animals[i].Gender}");
                Console.WriteLine($"Status: {animals[i].Status}");
                Console.WriteLine($"Birth Date: {animals[i].BirthDate:yyyy-MM-dd}");
                Console.WriteLine();
            }
        }

        private async Task UpdateAnimal()
        {
            Console.Clear();
            Console.WriteLine("===== UPDATE ANIMAL =====");

            Animal? selectedAnimal = await SelectAnimalFromNumber();

            if (selectedAnimal == null)
            {
                return;
            }

            Console.WriteLine();
            Console.WriteLine($"Selected animal: {selectedAnimal.Name} - {selectedAnimal.Species}");
            Console.WriteLine();

            Console.Write("New name: ");
            string name = Console.ReadLine() ?? string.Empty;

            Console.Write("New species: ");
            string species = Console.ReadLine() ?? string.Empty;

            Console.Write("New gender: ");
            string gender = Console.ReadLine() ?? string.Empty;

            Console.Write("New status: ");
            string status = Console.ReadLine() ?? string.Empty;

            Console.Write("New birth date (yyyy-mm-dd): ");
            string? birthDateInput = Console.ReadLine();

            bool isValidBirthDate = DateTime.TryParse(birthDateInput, out DateTime birthDate);

            if (!isValidBirthDate)
            {
                Console.WriteLine("Invalid birth date.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Choose new habitat:");
            Habitat? selectedHabitat = await SelectHabitatFromNumber();

            if (selectedHabitat == null)
            {
                return;
            }

            Animal updatedAnimal = new Animal
            {
                Id = selectedAnimal.Id,
                Name = name,
                Species = species,
                Gender = gender,
                Status = status,
                BirthDate = birthDate,
                HabitatId = selectedHabitat.Id
            };

            bool isUpdated = await _animalService.UpdateAnimalAsync(selectedAnimal.Id, updatedAnimal);

            if (isUpdated)
            {
                Console.WriteLine("Animal updated successfully.");
                Console.WriteLine($"New habitat: {selectedHabitat.Name}");
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

            Animal? selectedAnimal = await SelectAnimalFromNumber();

            if (selectedAnimal == null)
            {
                return;
            }

            Console.WriteLine();
            Console.WriteLine($"Selected animal: {selectedAnimal.Name} - {selectedAnimal.Species}");
            Console.Write("Are you sure you want to delete this animal? (y/n): ");

            string? confirmation = Console.ReadLine();

            if (confirmation?.ToLower() != "y")
            {
                Console.WriteLine("Delete cancelled.");
                return;
            }

            bool isDeleted = await _animalService.DeleteAnimalAsync(selectedAnimal.Id);

            if (isDeleted)
            {
                Console.WriteLine("Animal deleted successfully.");
            }
            else
            {
                Console.WriteLine("Failed to delete animal.");
            }
        }

       
    }
}