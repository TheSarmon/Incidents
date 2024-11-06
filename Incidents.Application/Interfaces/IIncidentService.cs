using Incidents.Domain.Entities;

namespace Incidents.Application.Interfaces
{
    public interface IIncidentService
    {
        Task<Incident> CreateAsync(string incidentName, string description, Account account);
    }
}
