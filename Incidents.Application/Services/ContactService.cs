using Incidents.Application.Interfaces;
using Incidents.Domain.Entities;
using Incidents.Infrastructure.Repositories;

namespace Incidents.Application.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IAccountRepository _accountRepository;

        public ContactService(IContactRepository contactRepository, IAccountRepository accountRepository)
        {
            _contactRepository = contactRepository;
            _accountRepository = accountRepository;
        }

        public async Task<Contact> GetByEmailAsync(string email)
        {
            return await _contactRepository.GetByEmailAsync(email);
        }

        public async Task<Contact> CreateOrUpdateAsync(string firstName, string lastName, string email, Account account)
        {
            var contact = await _contactRepository.GetByEmailAsync(email);

            if (contact == null)
            {
                contact = new Contact
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    AccountId = account.Id
                };
                await _contactRepository.AddAsync(contact);
            }
            else
            {
                contact.FirstName = firstName;
                contact.LastName = lastName;
                contact.AccountId = account.Id;
                await _contactRepository.UpdateAsync(contact);
            }

            return contact;
        }

        public async Task<Contact> UpdateAsync(string email, string firstName, string lastName, string newEmail, string accountName)
        {
            var contact = await _contactRepository.GetByEmailAsync(email);
            if (contact == null)
                return null;

            var account = await _accountRepository.GetByNameAsync(accountName);
            if (account == null)
                return null;

            contact.Email = newEmail;
            contact.FirstName = firstName;
            contact.LastName = lastName;
            contact.AccountId = account.Id;

            await _contactRepository.UpdateAsync(contact);

            return contact;
        }

        public async Task DeleteAsync(Contact contact)
        {
            await _contactRepository.DeleteAsync(contact);
        }
    }
}