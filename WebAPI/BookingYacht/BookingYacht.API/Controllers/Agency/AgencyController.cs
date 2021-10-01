using System;
using System.Threading.Tasks;
using BookingYacht.Business.Interfaces;
using BookingYacht.Business.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookingYacht.API.Controllers.Agency
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgencyController : ControllerBase
    {
        public AgencyController(IAgencyService agencyService)
        {
            _agencyService = agencyService;
        }

        private readonly IAgencyService _agencyService;
        
        //TODO GET ALL
        [HttpGet("")]
        public async Task<IActionResult> GetAgencies()
        {
            var agencies = await _agencyService.GetAgency();
            return Ok(agencies);
        }
        //TODO SEARCH BY STRING
        [HttpGet("search")]
        public async Task<IActionResult> SearchAgencies(string search)
        {
            return Ok(await _agencyService.SearchAgenciesString(search));
        }

        
        //TODO UPDATE AGENCY
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAgency([FromBody] AgencyModel model, [FromRoute] Guid id)
        {
            
            var updateAgency = await _agencyService.UpdateAgency(id, model);
            if (updateAgency != null)
            {
                return Ok(updateAgency);
            }

            return NotFound();
        }
          
        //TODO UPDATE AGENCY
        [HttpPost("{id:guid}")]
        public async Task<IActionResult> AddAgency([FromBody] AgencyModel model, [FromRoute] Guid id)
        {
            var updateAgency = await _agencyService.AddAgency(id, model);
            return Ok(updateAgency);
        }
        
    }
}