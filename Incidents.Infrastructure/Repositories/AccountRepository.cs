using Microsoft.EntityFrameworkCore;
using Incidents.Domain.Entities;
using Incidents.Infrastructure.Data;

namespace Incidents.Infrastructure.Repositories
{
    public interface IAccountRepository
    {
        Task<Account> GetByNameAsync(string name);
        Task AddAsync(Account account);
    }

    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;

        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Account> GetByNameAsync(string name)
        {
            return await _context.Accounts
                .Include(a => a.Contacts)
                .Include(a => a.Incidents)
                .FirstOrDefaultAsync(a => a.Name == name);
        }

        public async Task AddAsync(Account account)
        {
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }
    }
}