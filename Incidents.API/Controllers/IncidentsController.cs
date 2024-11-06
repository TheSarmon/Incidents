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
        public async Task<IActionResult> Create([FromBody] IncidentDto incidentDto, [FromQuery] string accountName, [FromQuery] string contactEmail)
        {
            var account = await _accountService.GetByNameAsync(accountName);
            if (account == null)
                return NotFound("Account not found.");

            var contact = await _contactService.GetByEmailAsync(contactEmail);
            if (contact == null || contact.AccountId != account.Id)
            {
                return NotFound("Contact not found or not linked to the specified account.");
            }

            var incident = await _incidentService.CreateAsync(incidentDto.IncidentName, incidentDto.Description, account);
            return Ok(incident);
        }
    }
}
