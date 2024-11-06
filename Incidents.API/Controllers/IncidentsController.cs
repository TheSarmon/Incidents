using Microsoft.AspNetCore.Mvc;
using Incidents.Application.Interfaces;
using Incidents.Application.DTO;

namespace Incidents.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncidentsController : ControllerBase
    {
        private readonly IIncidentService _incidentService;
        private readonly IAccountService _accountService;
        private readonly IContactService _contactService;

        public IncidentsController(IIncidentService incidentService, IAccountService accountService, IContactService contactService)
        {
            _incidentService = incidentService;
            _accountService = accountService;
            _contactService = contactService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateIncident([FromBody] IncidentDto incidentDto)
        {
            var account = await _accountService.GetByNameAsync(incidentDto.AccountName);
            if (account == null)
                return NotFound("Account not found.");

            var contact = await _contactService.GetByEmailAsync(incidentDto.ContactEmail);
            if (contact == null)
            {
                contact = await _contactService.CreateOrUpdateAsync(
                    incidentDto.ContactFirstName,
                    incidentDto.ContactLastName,
                    incidentDto.ContactEmail,
                    account);
            }
            else
            {
                contact.FirstName = incidentDto.ContactFirstName;
                contact.LastName = incidentDto.ContactLastName;

                if (contact.AccountId != account.Id)
                {
                    contact.AccountId = account.Id;
                    await _contactService.CreateOrUpdateAsync(contact.FirstName, contact.LastName, contact.Email, account);
                }
            }

            var incident = await _incidentService.CreateAsync(
                incidentName: Guid.NewGuid().ToString(),
                description: incidentDto.IncidentDescription,
                account: account);

            return Ok(incident);
        }

        [HttpPut("{incidentName}")]
        public async Task<IActionResult> Update(string incidentName, [FromBody] IncidentDto incidentDto)
        {
            var incident = await _incidentService.GetByIncidentNameAsync(incidentName);
            if (incident == null)
                return NotFound("Incident not found.");

            incident.Description = incidentDto.IncidentDescription;
            await _incidentService.UpdateAsync(incident);

            return Ok(incident);
        }

        [HttpDelete("{incidentName}")]
        public async Task<IActionResult> Delete(string incidentName)
        {
            var incident = await _incidentService.GetByIncidentNameAsync(incidentName);
            if (incident == null)
                return NotFound();

            await _incidentService.DeleteAsync(incident);
            return NoContent();
        }
    }
}
