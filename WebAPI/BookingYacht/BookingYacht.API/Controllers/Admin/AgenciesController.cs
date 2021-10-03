using System;
using System.Threading.Tasks;
using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingYacht.API.Controllers.Admin
{
    [Route(ApiVer2Route)]
    [ApiController]
    [Authorize]
    public class AgenciesController : BaseApiVer2Controller
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
            return agencies == null ? Fail("Not found") : Success(agencies);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var agencies = await _service.GetAgency(id);
           
            return agencies == null ? Fail("Not Found") : Success(agencies);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AgencyViewModels model)
        {
            var id = await _service.AddAgency(model);
            return Success(id) ;
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