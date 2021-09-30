using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookingYacht.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PlaceTypesController : ControllerBase
    {
        private readonly IPlaceTypeService _placeTypeService;

        public PlaceTypesController(IPlaceTypeService placeTypeService)
        {
            _placeTypeService = placeTypeService;
        }


        // GET: api/<PlaceTypesController>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PlaceTypeSearchModel model)
        {
            var placeType = await _placeTypeService.SearchPlaceTypes(model);
            return Ok(placeType);
        }

        // GET api/<PlaceTypesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var placType = await _placeTypeService.GetPlaceType(id);

            return Ok(placType);
        }

        // POST api/<PlaceTypesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PlaceTypeViewModel model)
        {
            var id = await _placeTypeService.AddPlaceType(model);
            return Ok(id);
        }

        // PUT api/<PlaceTypesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] PlaceTypeViewModel model)
        {
            await _placeTypeService.UpdatePlaceType(id, model);
            return Ok();
        }

        // DELETE api/<PlaceTypesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _placeTypeService.DeletePlaceType(id);
            return Ok();
        }
    }
}
