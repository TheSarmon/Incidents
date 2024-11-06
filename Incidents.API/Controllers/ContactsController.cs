using Microsoft.AspNetCore.Mvc;
using Incidents.Application.Interfaces;
using Incidents.Application.DTO;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> CreateOrUpdate([FromBody] ContactDto contactDto)
        {
            var account = await _accountService.GetByNameAsync(contactDto.AccountName);
            if (account == null)
                return NotFound("Account not found.");

            var contact = await _contactService.CreateOrUpdateAsync(contactDto.ContactFirstName, contactDto.ContactLastName, contactDto.ContactEmail, account);
            return Ok(contact);
        }

        [HttpPut("{email}")]
        public async Task<IActionResult> Edit(string email, [FromBody] ContactDto contactDto)
        {
            var contact = await _contactService.GetByEmailAsync(email);
            if (contact == null)
                return NotFound("Contact not found.");

            contact.FirstName = contactDto.ContactFirstName;
            contact.LastName = contactDto.ContactLastName;
            contact.Email = contactDto.ContactEmail;

            var account = await _accountService.GetByNameAsync(contactDto.AccountName);
            if (account == null)
                return NotFound("Account for contact not found.");

            contact.AccountId = account.Id;

            await _contactService.UpdateAsync(contact);

            return Ok(contact);
        }

        [HttpDelete("{email}")]
        public async Task<IActionResult> Delete(string email)
        {
            var contact = await _contactService.GetByEmailAsync(email);
            if (contact == null)
                return NotFound();

            await _contactService.DeleteAsync(contact);
            return NoContent();
        }
    }
}
