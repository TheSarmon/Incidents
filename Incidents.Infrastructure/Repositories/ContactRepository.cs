using Microsoft.EntityFrameworkCore;
using Incidents.Domain.Entities;
using Incidents.Infrastructure.Data;

namespace Incidents.Infrastructure.Repositories
{
    public interface IContactRepository
    {
        Task<Contact> GetByEmailAsync(string email);
        Task AddAsync(Contact contact);
        Task UpdateAsync(Contact contact);
    }

    public class ContactRepository : IContactRepository
    {
        private readonly AppDbContext _context;

        public ContactRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Contact> GetByEmailAsync(string email)
        {
            return await _context.Contacts.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task AddAsync(Contact contact)
        {
            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Contact contact)
        {
            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync();
        }
    }
}