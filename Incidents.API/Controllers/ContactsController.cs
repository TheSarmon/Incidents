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
        public async Task<IActionResult> Update(string email, [FromBody] ContactDto contactDto)
        {
            var updatedContact = await _contactService.UpdateAsync(email, 
                contactDto.ContactFirstName, 
                contactDto.ContactLastName, 
                contactDto.ContactEmail, 
                contactDto.AccountName
            );
            if (updatedContact == null)
                return NotFound("Contact or account not found.");

            return Ok(updatedContact);
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
