using System;
using System.Threading.Tasks;
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
    public class VehicleTypesController : BaseBusinessController
    {
        private readonly IVehicleTypeService _service;
        public VehicleTypesController(IVehicleTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] VehicleTypeSearchModel search)
        {
            var list = await _service.SearchVehicleTypes(search);
            return Success(list);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var model = await _service.GetVehicleType(id);
            return Success(model);
        }
        
    }
}