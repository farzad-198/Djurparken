using Domain.Entities;


namespace Application.Interfaces.Services
{
    public interface IVisitorService
    {
        Task<List<Visitor>> GetAllVisitorsAsync();
        Task<Visitor?> GetVisitorByIdAsync(Guid id);
        Task<Visitor> AddVisitorAsync(Visitor visitor);
        Task<bool> UpdateVisitorAsync(Guid id, Visitor visitor);
        Task<bool> DeleteVisitorAsync(Guid id);
    }
}
