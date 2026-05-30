using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
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
