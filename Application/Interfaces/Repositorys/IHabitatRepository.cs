using Domain.Entities;


namespace Application.Interfaces.Repositorys
{
    public interface IHabitatRepository
    {
        Task<List<Habitat>> GetAllHabitatsAsync();
        Task<Habitat?> GetHabitatByIdAsync(Guid id);
        Task AddHabitatAsync(Habitat habitat);
        Task DeleteHabitatAsync(Guid id);
        Task UpdateHabitatAsync(Habitat habitat);
    }
}
