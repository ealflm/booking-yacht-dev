using System;
using System.Threading.Tasks;
using BookingYacht.API.Controllers.Admin;
using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BookingYacht.API.Controllers.Agency
{
    
    [Route(ApiVer2Route)]
    [ApiController]
    [Authorize]
    public class DestinationsController : BaseApiVer2Controller
    {
        private readonly IDestinationService _service;

        public DestinationsController(IDestinationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] DestinySearchModel model)
        {
            var businesses = await _service.SearchDestinies(model);
            return Success(businesses);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var business = await _service.GetDestiny(id);
           
            return Success(business);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DestinyViewModel model)
        {
            var id = await _service.AddDestiny(model);
            return Success(id);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] DestinyViewModel model)
        {
            await _service.UpdateDestiny(id, model);
            return Success();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteDestiny(id);
            return Success();
        }
        
    }
}