using Application.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly ZooDbContext _context;
        public AnimalRepository(ZooDbContext context)
        {
            _context = context;
        }       

      

      

        public async Task AddAnimalAsync(Animal animal)
        {
            _context.Animals.Add(animal);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAnimalAsync(Guid id)
        {
            Animal? animal = await _context.Animals.FindAsync(id);
            if (animal != null)
            {
                _context.Animals.Remove(animal);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<Animal>> GetAllAnimalsAsync()
        {
            return await _context.Animals
                .Include(a => a.Habitat)
                .ToListAsync();
        }
        public async Task<Animal?> GetAnimalByIdAsync(Guid id)
        {
            return await _context.Animals
                .Include(a => a.Habitat)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task UpdateAnimalAsync(Animal animal)
        {
            _context.Animals.Update(animal);
            await _context.SaveChangesAsync();
        }
    }
}
