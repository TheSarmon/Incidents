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
            var incident = await _incidentService.CreateAsync(incidentDto.AccountName, incidentDto.IncidentDescription);
            if (incident == null)
                return NotFound("Account not found.");

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
