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
    public class VehicleController : BaseBusinessController
    {
        public VehicleController(IVehicleService service)
        {
            _agencyService = service;
        }

        private readonly IVehicleService _agencyService;
        
        [HttpGet()]
        public async Task<IActionResult> Search([FromQuery] VehicleSearchModel model)
        {
            var vehicles = await _agencyService.SearchVehicles(model);
            return Success(vehicles);
        }  
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var vehicles = await _agencyService.GetVehicle(id);
            return Success(vehicles);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] VehicleViewModel model)
        {
            var vehicle = await _agencyService.AddVehicle(model);
            return Success(vehicle);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, 
                                                [FromBody] VehicleViewModel model)
        {
            await _agencyService.UpdateVehicle(id, model);
            return Success();
        }
        
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Update(Guid id)
        {
            await _agencyService.DeleteVehicle(id);
            return Success();
        }
        
    }
}