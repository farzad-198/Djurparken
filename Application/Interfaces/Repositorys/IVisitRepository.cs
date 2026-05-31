using Domain.Entities;

namespace Application.Interfaces.Repositorys
{
    public interface IVisitRepository
    {
        Task<List<Visit>> GetAllVisitsAsync();
        Task<Visit?> GetVisitByIdAsync(Guid id);
        Task AddVisitAsync(Visit visit);
        Task DeleteVisitAsync(Guid id);
        Task UpdateVisitAsync(Visit visit);
    }
}
