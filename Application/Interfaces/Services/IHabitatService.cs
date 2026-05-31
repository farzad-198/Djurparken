using Domain.Entities;


namespace Application.Interfaces.Services
{
    public interface IHabitatService
    {
        Task<List<Habitat>> GetAllHabitatsAsync();
        Task<Habitat?> GetHabitatByIdAsync(Guid id);
        Task<Habitat> AddHabitatAsync(Habitat habitat);
        Task<bool> UpdateHabitatAsync(Habitat habitat);
        Task<bool> DeleteHabitatAsync(Guid id); 
    }
}
