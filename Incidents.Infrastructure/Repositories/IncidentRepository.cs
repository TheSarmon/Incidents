using Incidents.Domain.Entities;
using Incidents.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Incidents.Infrastructure.Repositories
{
    public interface IIncidentRepository
    {
        Task<Incident> GetByIncidentNameAsync(string incidentName);
        Task AddAsync(Incident incident);

        Task UpdateAsync(Incident incident);
        Task DeleteAsync(Incident incident);
    }

    public class IncidentRepository : IIncidentRepository
    {
        private readonly AppDbContext _context;

        public IncidentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Incident> GetByIncidentNameAsync(string incidentName)
        {
            return await _context.Incidents.FirstOrDefaultAsync(i => i.IncidentName == incidentName);
        }

        public async Task AddAsync(Incident incident)
        {
            await _context.Incidents.AddAsync(incident);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Incident incident)
        {
            _context.Incidents.Update(incident);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Incident incident)
        {
            _context.Incidents.Remove(incident);
            await _context.SaveChangesAsync();
        }
    }
}