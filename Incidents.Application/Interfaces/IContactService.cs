using Incidents.Domain.Entities;

namespace Incidents.Application.Interfaces
{
    public interface IContactService
    {
        Task<Contact> GetByEmailAsync(string email);
        Task<Contact> CreateOrUpdateAsync(string firstName, string lastName, string email, Account account);
        Task<Contact> UpdateAsync(string email, string firstName, string lastName, string newEmail, string accountName);
        Task DeleteAsync(Contact contact);
    }
}
