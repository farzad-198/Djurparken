using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;



namespace Infrastructure.Repositories
{
    public class VisitRepository : IVisitRepository
    {
        private readonly ZooDbContext _context;
        public VisitRepository(ZooDbContext context)
        {
            _context = context;
        }

        public async Task AddVisitAsync(Visit visit)
        {
            await _context.Visits.AddAsync(visit);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVisitAsync(Guid id)
        {
          Visit? visit = _context.Visits.Find(id);
            if (visit != null)
            {
                _context.Visits.Remove(visit);
                await _context.SaveChangesAsync();
            }
            
        }

        public async Task<List<Visit>> GetAllVisitsAsync()
        {
            return await _context.Visits
                .Include(v => v.Visitor)
                .ToListAsync();
        }

        public async Task<Visit?> GetVisitByIdAsync(Guid id)
        {
          return await _context.Visits
                .Include(v => v.Visitor)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task UpdateVisitAsync(Visit visit)
        {
            _context.Visits.Update(visit);
            await _context.SaveChangesAsync();
        }
    }
}
