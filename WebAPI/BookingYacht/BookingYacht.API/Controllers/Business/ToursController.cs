using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookingYacht.API.Controllers.Business
{
    [Route(BusinessRoute)]
    [ApiController]
    [Authorize]
    public class ToursController : BaseBusinessController
    {
        private readonly ITourService _tourService;

        public ToursController(ITourService tourService)
        {
            _tourService = tourService;
        }


        // GET: api/<ToursController>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] TourSearchModel model)
        {
            var tours = await _tourService.SearchTours(model);
            return Success(tours);
        }

        // GET api/<ToursController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var tour = await _tourService.GetTour(id);
            return Success(tour);
        }

    }
}
