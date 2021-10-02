using System;
using System.Threading.Tasks;
using BookingYacht.Business.Interfaces;
using BookingYacht.Business.Interfaces.Agency;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookingYacht.API.Controllers.Agency
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgenciesController : ControllerBase
    {
        private readonly IAgencyService _service;

        public AgenciesController(IAgencyService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] AgencySearchModel model)
        {
            var agencies = await _service.SearchAgencies(model);
            return Ok(agencies);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var agencies = await _service.GetAgency(id);
           
            return Ok(agencies);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AgencyViewModels model)
        {
            var id = await _service.AddAgency(model);
            return Ok(id);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] AgencyViewModels model)
        {
            await _service.UpdateAgency(id, model);
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAgency(id);
            return Ok();
        }
    }
}