using System;
using System.Threading.Tasks;
using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.SearchModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingYacht.API.Controllers.Admin
{
    [Route(AdminRoute)]
    [ApiController]
    [Authorize]
    [ApiExplorerSettings(GroupName = Role)]
    public class VehiclesController : BaseAdminController
    {
        public VehiclesController(IVehicleService service)
        {
            _service = service;
        }

        private readonly IVehicleService _service;
        
        [HttpGet()]
        public async Task<IActionResult> Search([FromQuery] VehicleSearchModel model)
        {
            var vehicles = await _service.SearchVehiclesNavigation(model);
            return Success(vehicles);
        }  
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var vehicles = await _service.GetVehicleNavigation(id);
            return Success(vehicles);
        }
        
        [HttpDelete("total")]
        public async Task<IActionResult> Count()
        {
            return Success(await _service.Count());
        }
    }
}