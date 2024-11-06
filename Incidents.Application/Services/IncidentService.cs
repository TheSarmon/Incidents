using Incidents.Application.Interfaces;
using Incidents.Domain.Entities;
using Incidents.Infrastructure.Repositories;

namespace Incidents.Application.Services
{
    public class IncidentService : IIncidentService
    {
        private readonly IIncidentRepository _incidentRepository;

        public IncidentService(IIncidentRepository incidentRepository)
        {
            _incidentRepository = incidentRepository;
        }

        public async Task<Incident> CreateAsync(string incidentName, string description, Account account)
        {
            var incident = new Incident
            {
                IncidentName = incidentName,
                Description = description,
                AccountId = account.Id
            };

            await _incidentRepository.AddAsync(incident);
            return incident;
        }
    }
}
