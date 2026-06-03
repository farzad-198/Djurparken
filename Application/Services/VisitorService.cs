using Application.Interfaces.Repositorys;
using Application.Interfaces.Services;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class VisitorService : IVisitorService
    {
        private readonly IVisitorRepository _repo;
        public VisitorService(IVisitorRepository repo)
        {
            _repo = repo;
        }

        public async Task<Visitor> AddVisitorAsync(Visitor visitor)
        {
            await _repo.AddVisitorAsync(visitor);
            return visitor;
        }

        public async Task<bool> DeleteVisitorAsync(Guid id)
        {
            var visitor = await _repo.GetVisitorByIdAsync(id);
            if (visitor == null)
                return false;

            await _repo.DeleteVisitorAsync(visitor.Id);
            return true;
        }

        public async Task<List<Visitor>> GetAllVisitorsAsync()
        {
            return await _repo.GetAllVisitorsAsync();
        }

        public async Task<Visitor?> GetVisitorByIdAsync(Guid id)
        {
            return await _repo.GetVisitorByIdAsync(id);
        }

        public async Task<bool> UpdateVisitorAsync(Guid id, Visitor visitor)
        {
            var visitorToUpdate = await _repo.GetVisitorByIdAsync(id);
            if (visitorToUpdate == null)
                return false;
            visitorToUpdate.FullName = visitor.FullName;
            visitorToUpdate.Age = visitor.Age;
            visitorToUpdate.PhoneNumber = visitor.PhoneNumber;
            await _repo.UpdateVisitorAsync(visitorToUpdate);
            return true;
        }
    }
}

        

    
