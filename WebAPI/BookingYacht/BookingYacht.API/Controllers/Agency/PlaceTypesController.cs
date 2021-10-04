using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookingYacht.API.Controllers.Agency
{
    [Route(ApiVer2Route)]
    [ApiController]
    [Authorize]
    public class PlaceTypesController : BaseApiVer2Controller
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
            return Success(placeType);
        }

        // GET api/<PlaceTypesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var placType = await _placeTypeService.GetPlaceType(id);

            return Success(placType);
        }

    }
}
