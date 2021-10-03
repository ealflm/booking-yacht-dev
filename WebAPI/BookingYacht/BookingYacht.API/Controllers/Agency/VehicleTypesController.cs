using BookingYacht.Business.Interfaces.Agency;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookingYacht.API.Controllers.Agency
{
    [Route(ApiVer2Route)]
    [ApiController]
    [Authorize]
    public class VehicleTypesController : BaseApiVer2Controller
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VehicleTypeViewModel model)
        {
            var res = await _service.AddVehicleType(model);
            return Success(res);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] VehicleTypeViewModel model)
        {
            var res = await _service.UpdateVehicleType(id, model);
            return Success();
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var res = await _service.DeleteVehicleType(id);
            return Success();
        }
        
    }
}