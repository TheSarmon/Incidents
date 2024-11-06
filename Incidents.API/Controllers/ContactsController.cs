using Microsoft.AspNetCore.Mvc;
using Incidents.Application.Interfaces;
using Incidents.Application.DTO;

namespace Incidents.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IAccountService _accountService;

        public ContactsController(IContactService contactService, IAccountService accountService)
        {
            _contactService = contactService;
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate([FromBody] ContactDto contactDto, [FromQuery] string accountName)
        {
            var account = await _accountService.GetByNameAsync(accountName);
            if (account == null)
                return NotFound("Account not found.");

            var contact = await _contactService.CreateOrUpdateAsync(contactDto.FirstName, contactDto.LastName, contactDto.Email, account);
            return Ok(contact);
        }
    }
}
