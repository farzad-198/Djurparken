using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IAnimalRepository
    {
        Task<List<Animal>> GetAllAnimalsAsync();
        Task<Animal?> GetAnimalByIdAsync(Guid id);
        Task  AddAnimalAsync(Animal animal);
        Task DeleteAnimalAsync(Guid id);
        Task UpdateAnimalAsync(Animal animal);
        
    }
}
