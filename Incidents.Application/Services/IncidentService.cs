using Incidents.Application.Interfaces;
using Incidents.Domain.Entities;
using Incidents.Infrastructure.Repositories;

namespace Incidents.Application.Services
{
    public class IncidentService : IIncidentService
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly IAccountRepository _accountRepository;

        public IncidentService(IIncidentRepository incidentRepository, IAccountRepository accountRepository)
        {
            _incidentRepository = incidentRepository;
            _accountRepository = accountRepository;
        }

        public async Task<Incident> GetByIncidentNameAsync(string incidentName)
        {
            return await _incidentRepository.GetByIncidentNameAsync(incidentName);
        }

        public async Task<Incident> CreateAsync(string accountName, string description)
        {
            var account = await _accountRepository.GetByNameAsync(accountName);
            if (account == null)
            {
                return null;
            }

            var incident = new Incident
            {
                IncidentName = Guid.NewGuid().ToString(),
                Description = description,
                AccountId = account.Id
            };

            await _incidentRepository.AddAsync(incident);

            return incident;
        }

        public async Task UpdateAsync(Incident incident)
        {
            await _incidentRepository.UpdateAsync(incident);
        }

        public async Task DeleteAsync(Incident incident)
        {
            await _incidentRepository.DeleteAsync(incident);
        }
    }
}
