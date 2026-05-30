using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class VisitorRepository : IVisitorRepository
    {
        private readonly ZooDbContext _context;

        public VisitorRepository(ZooDbContext context)
        {
            _context = context;
        }

        public async Task AddVisitorAsync(Visitor visitor)
        {
            await _context.Visitors.AddAsync(visitor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVisitorAsync(Guid id)
        {
            Visitor? visitor = await _context.Visitors.FindAsync(id);
            if (visitor != null)
            {
                _context.Visitors.Remove(visitor);
                await _context.SaveChangesAsync();
            }
            
        }

        public async Task<List<Visitor>> GetAllVisitorsAsync()
        {
          return await _context.Visitors
                .Include(v => v.Visits)
                .ToListAsync();
        }

        public async Task<Visitor?> GetVisitorByIdAsync(Guid id)
        {
            return await _context.Visitors
                .Include(v => v.Visits)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task UpdateVisitorAsync(Visitor visitor)
        {
            _context.Visitors.Update(visitor);
            await _context.SaveChangesAsync();
        }
    }
}
