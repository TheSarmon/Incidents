using Microsoft.AspNetCore.Mvc;
using Incidents.Application.Interfaces;
using Incidents.Application.DTO;

namespace Incidents.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var account = await _accountService.GetByNameAsync(name);
            if (account == null)
                return NotFound();

            return Ok(new AccountDto { AccountName = account.Name });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AccountDto accountDto)
        {
            var existingAccount = await _accountService.GetByNameAsync(accountDto.AccountName);
            if (existingAccount != null)
                return Conflict("Account with the same name already exists.");

            var account = await _accountService.CreateAsync(accountDto.AccountName);
            return CreatedAtAction(nameof(GetByName), new { name = account.Name }, account);
        }

        [HttpPut("{name}")]
        public async Task<IActionResult> Update(string name, [FromBody] AccountDto accountDto)
        {
            var account = await _accountService.GetByNameAsync(name);
            if (account == null)
                return NotFound("Account not found.");

            account.Name = accountDto.AccountName;
            await _accountService.UpdateAsync(account);

            return Ok(new AccountDto { AccountName = account.Name });
        }

        [HttpDelete("{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            var account = await _accountService.GetByNameAsync(name);
            if (account == null)
                return NotFound();

            await _accountService.DeleteAsync(account);
            return NoContent();
        }
    }
}