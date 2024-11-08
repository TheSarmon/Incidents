﻿using Incidents.Application.Interfaces;
using Incidents.Domain.Entities;
using Incidents.Infrastructure.Repositories;

namespace Incidents.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Account> GetByNameAsync(string name)
        {
            return await _accountRepository.GetByNameAsync(name);
        }

        public async Task<Account> CreateAsync(string name)
        {
            var existingAccount = await _accountRepository.GetByNameAsync(name);
            if (existingAccount != null)
            {
                return null;
            }

            var account = new Account { Name = name };
            await _accountRepository.AddAsync(account);
            return account;
        }

        public async Task UpdateAsync(Account account)
        {
            await _accountRepository.UpdateAsync(account);
        }

        public async Task DeleteAsync(Account account)
        {
            await _accountRepository.DeleteAsync(account);
        }
    }
}