using Incidents.Domain.Entities;

namespace Incidents.Application.Interfaces
{
    public interface IContactService
    {
        Task<Contact> GetByEmailAsync(string email);
        Task<Contact> CreateOrUpdateAsync(string firstName, string lastName, string email, Account account);
    }
}
