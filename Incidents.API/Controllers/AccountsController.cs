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

            return Ok(new AccountDto { Name = account.Name });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AccountDto accountDto)
        {
            var existingAccount = await _accountService.GetByNameAsync(accountDto.Name);
            if (existingAccount != null)
                return Conflict("Account with the same name already exists.");

            var account = await _accountService.CreateAsync(accountDto.Name);
            return CreatedAtAction(nameof(GetByName), new { name = account.Name }, account);
        }
    }
}