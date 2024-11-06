using Incidents.Domain.Entities;

namespace Incidents.Application.Interfaces
{
    public interface IAccountService
    {
        Task<Account> GetByNameAsync(string name);
        Task<Account> CreateAsync(string name);
        Task UpdateAsync(Account account);
        Task DeleteAsync(Account account);
    }
}
