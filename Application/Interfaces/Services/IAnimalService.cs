using Domain.Entities;


namespace Application.Interfaces.Services
{
    public interface IAnimalService
    {
        Task<List<Animal>> GetAllAnimalsAsync();
        Task<Animal?> GetAnimalByIdAsync(Guid id);
        Task<Animal> AddAnimalAsync(Animal animal);
        Task<bool> UpdateAnimalAsync(Animal animal);
        Task<bool> DeleteAnimalAsync(Guid id); 
    }
}
