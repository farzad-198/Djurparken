using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class HabitatRepository : IHabitatRepository
    {
        private readonly ZooDbContext _context;
        public HabitatRepository(ZooDbContext context)
        {
            _context = context;
        }

        public async Task AddHabitatAsync(Habitat habitat)
        {
            await _context.Habitats.AddAsync(habitat);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteHabitatAsync(Guid id)
        {
            var habitat = await _context.Habitats.FindAsync(id);
            if (habitat != null)
            {
                _context.Habitats.Remove(habitat);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Habitat>> GetAllHabitatsAsync()
        {
            return await _context.Habitats
                .Include(h => h.Animals)
                .ToListAsync();
        }

        public async Task<Habitat?> GetHabitatByIdAsync(Guid id)
        {
            return await _context.Habitats
                .Include(h => h.Animals)
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task UpdateHabitatAsync(Habitat habitat)
        {
            _context.Habitats.Update(habitat);
            await _context.SaveChangesAsync();
        }
    }
}

 