using BookingYacht.Business.Interfaces.Agency;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookingYacht.API.Controllers.Agency
{
    [Route(ApiVer1Route)]
    [ApiController]
    [Authorize]
    public class AgenciesController : BaseApiVer1Controller
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
            return Success(agencies);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var agencies = await _service.GetAgency(id);
           
            return Success(agencies);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AgencyViewModels model)
        {
            var id = await _service.AddAgency(model);
            return Success(id);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] AgencyViewModels model)
        {
            await _service.UpdateAgency(id, model);
            return Success();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAgency(id);
            return Success();
        }
    }
}