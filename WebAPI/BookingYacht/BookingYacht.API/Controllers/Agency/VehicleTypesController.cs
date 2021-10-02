using System;
using System.Threading.Tasks;
using System.Xml;
using BookingYacht.Business.Interfaces.Agency;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

namespace BookingYacht.API.Controllers.Agency
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleTypesController : ControllerBase
    {
        private readonly IVehicleTypeService _service;
        public VehicleTypesController(IVehicleTypeService service)
        {
            this._service = service;
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] VehicleTypeSearchModel search)
        {
            var list = await _service.SearchVehicleTypes(search);
            return Ok(list);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var model = await _service.GetVehicleType(id);
            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VehicleTypeViewModel model)
        {
            var res = await _service.AddVehicleType(model);
            return Ok(res);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] VehicleTypeViewModel model)
        {
            var res = await _service.UpdateVehicleType(id, model);
            return Ok();
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var res = await _service.DeleteVehicleType(id);
            return Ok();
        }
        
    }
}