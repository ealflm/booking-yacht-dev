using System;
using System.Threading.Tasks;
using BookingYacht.API.Controllers.Agency;
using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingYacht.API.Controllers.Business
{
    [Route(BusinessRoute)]
    [ApiController]
    [Authorize]
    [ApiExplorerSettings(GroupName = Role)]
    public class VehiclesController : BaseBusinessController
    {
        public VehiclesController(IVehicleService service)
        {
            _service = service;
        }

        private readonly IVehicleService _service;
        
        [HttpGet()]
        public async Task<IActionResult> Search([FromQuery] VehicleSearchModel model)
        {
            var vehicles = await _service.SearchVehicles(model);
            return Success(vehicles);
        }  
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var vehicles = await _service.GetVehicle(id);
            return Success(vehicles);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] VehicleViewModel model)
        {
            var vehicle = await _service.AddVehicle(model);
            return Success(vehicle);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, 
                                                [FromBody] VehicleViewModel model)
        {
            await _service.UpdateVehicle(id, model);
            return Success();
        }
        
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Update(Guid id)
        {
            await _service.DeleteVehicle(id);
            return Success();
        }

    }
}