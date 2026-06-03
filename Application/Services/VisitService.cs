using Application.Interfaces.Repositorys;
using Application.Interfaces.Services;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class VisitService : IVisitService
    {
        private readonly IVisitRepository _repo;
        public VisitService(IVisitRepository repo)
        {
            _repo = repo;
        }

        public async Task<Visit> AddVisitAsync(Visit visit)
        {
            await _repo.AddVisitAsync(visit);
            return visit;
        }

        public async Task<bool> DeleteVisitAsync(Guid id)
        {
            var visit = await _repo.GetVisitByIdAsync(id);
            if (visit == null)
                return false;

            await _repo.DeleteVisitAsync(visit.Id);
            return true;
        }

        public async Task<List<Visit>> GetAllVisitsAsync()
        {
            return await _repo.GetAllVisitsAsync();
        }

        public async Task<Visit?> GetVisitByIdAsync(Guid id)
        {
            return await _repo.GetVisitByIdAsync(id);
        }

        public async Task<bool> UpdateVisitAsync(Guid id, Visit visit)
        {
            var visitToUpdate = await _repo.GetVisitByIdAsync(id);
            if (visitToUpdate == null)
                return false;
            visitToUpdate.VisitorId = visit.VisitorId;
            visitToUpdate.VisitDate = visit.VisitDate;
            visitToUpdate.HasPaidTicket = visit.HasPaidTicket;
            await _repo.UpdateVisitAsync(visitToUpdate);
            return true;
        }
    }
}
