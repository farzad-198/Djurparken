using Domain.Entities;


namespace Application.Interfaces.Repositorys
{
    public interface IAnimalRepository
    {
        Task<List<Animal>> GetAllAnimalsAsync();
        Task<Animal?> GetAnimalByIdAsync(Guid id);
        Task  AddAnimalAsync(Animal animal);
        Task DeleteAnimalAsync(Guid id);
        Task UpdateAnimalAsync(Animal animal);
    
    }
}
