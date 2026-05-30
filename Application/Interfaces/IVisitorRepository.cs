using Domain.Entities;

namespace Application.Interfaces
{
    public interface IVisitorRepository
    {
        Task<List<Visitor>> GetAllVisitorsAsync();
        Task<Visitor?> GetVisitorByIdAsync(Guid id);
        Task AddVisitorAsync(Visitor visitor);
        Task UpdateVisitorAsync(Visitor visitor);
        Task DeleteVisitorAsync(Guid id);
    }
}
