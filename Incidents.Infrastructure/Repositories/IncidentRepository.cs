using Incidents.Domain.Entities;
using Incidents.Infrastructure.Data;

namespace Incidents.Infrastructure.Repositories
{
    public interface IIncidentRepository
    {
        Task AddAsync(Incident incident);
    }

    public class IncidentRepository : IIncidentRepository
    {
        private readonly AppDbContext _context;

        public IncidentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Incident incident)
        {
            await _context.Incidents.AddAsync(incident);
            await _context.SaveChangesAsync();
        }
    }
}