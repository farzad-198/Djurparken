using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Services
{
    public interface IVisitService
    {
        Task<List<Visit>> GetAllVisitsAsync();
        Task<Visit?> GetVisitByIdAsync(Guid id);
        Task<Visit> AddVisitAsync(Visit visit);
        Task<bool> UpdateVisitAsync(Visit visit);
        Task<bool> DeleteVisitAsync(Guid id);
    }
}
