using Application.Interfaces.Repositorys;
using Application.Interfaces.Services;
using Domain.Entities;


namespace Application.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly IAnimalRepository _repo;
        public AnimalService(IAnimalRepository repo)
        {
            _repo = repo;
        }
        public async Task<Animal> AddAnimalAsync(Animal animal)
        {
            await _repo.AddAnimalAsync(animal);
            return animal;
        }

        public async Task<bool> DeleteAnimalAsync(Guid id)
        {
            var animal = await _repo.GetAnimalByIdAsync(id);
            if (animal == null) 
            return false;

            await _repo.DeleteAnimalAsync(animal.Id);
            return true;
        }

        public async Task<List<Animal>> GetAllAnimalsAsync()
        {
            return await _repo.GetAllAnimalsAsync();
        }

        public async Task<Animal?> GetAnimalByIdAsync(Guid id)
        {
            return await _repo.GetAnimalByIdAsync(id);
        }

        public async Task<bool> UpdateAnimalAsync(Guid animalId, Animal animal)
        {
          var animalToUpdate = await _repo.GetAnimalByIdAsync(animal.Id);
            if (animalToUpdate == null)
                return false;
            animalToUpdate.Name = animal.Name;
            animalToUpdate.Species = animal.Species;
            animalToUpdate.BirthDate = animal.BirthDate;
            await _repo.UpdateAnimalAsync(animalToUpdate);
            return true;

        }
    }
}
