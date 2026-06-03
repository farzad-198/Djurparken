using Application.Interfaces.Repositorys;
using Application.Interfaces.Services;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class HabitatService : IHabitatService
    {
        private readonly IHabitatRepository _repo;
        public HabitatService(IHabitatRepository repo)
        {
            _repo = repo;
        }

        public async Task<Habitat> AddHabitatAsync(Habitat habitat)
        {
            await _repo.AddHabitatAsync(habitat);
            return habitat;
        }

        public async Task<bool> DeleteHabitatAsync(Guid id)
        {
            var habitat = await _repo.GetHabitatByIdAsync(id);
            if (habitat == null)
                return false;

            await _repo.DeleteHabitatAsync(habitat.Id);
            return true;
        }

        public async Task<List<Habitat>> GetAllHabitatsAsync()
        {
            return await _repo.GetAllHabitatsAsync();

        }

        public async Task<Habitat?> GetHabitatByIdAsync(Guid id)
        {
            return await _repo.GetHabitatByIdAsync(id);
        }

        public async Task<bool> UpdateHabitatAsync(Guid id, Habitat habitat)
        {
            var habitatToUpdate = await _repo.GetHabitatByIdAsync(id);
            if (habitatToUpdate == null)
                return false;
            habitatToUpdate.Name = habitat.Name;
            habitatToUpdate.Vegetation = habitat.Vegetation;
            habitatToUpdate.Climate = habitat.Climate;
            await _repo.UpdateHabitatAsync(habitatToUpdate);
            return true;
        }
    }
}

       


