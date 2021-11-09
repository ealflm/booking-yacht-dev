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
            _agencyService = service;
        }

        private readonly IVehicleService _agencyService;

        [HttpGet()]
        public async Task<IActionResult> Search([FromQuery] VehicleSearchModel model)
        {
            var vehicles = await _agencyService.SearchVehiclesNavigation(model);
            return Success(vehicles);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var vehicles = await _agencyService.GetVehicleNavigation(id);
            return Success(vehicles);
        }

        [HttpGet("counting")]
        public async Task<IActionResult> Count()
        {
            return Success(await _agencyService.Count());
        }
    }
}