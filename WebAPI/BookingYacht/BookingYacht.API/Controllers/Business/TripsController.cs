using BookingYacht.Business.Interfaces.Business;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingYacht.API.Extensions;
using BookingYacht.Data.Models;
using Microsoft.Extensions.Caching.Distributed;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookingYacht.API.Controllers.Business
{
    [Route(BusinessRoute)]
    [ApiController]
    [Authorize]
    [ApiExplorerSettings(GroupName = Role)]
    public class TripsController : BaseBusinessController
    {
        private readonly ITripService _tripService;
        private readonly IDistributedCache _cache;
        private const string Trip = "Trip_";

        public TripsController(ITripService tripService, IDistributedCache cache)
        {
            _tripService = tripService;
            _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] TripSearchModel model)
        {
            var trips = await _tripService.SearchTrip(model);
            foreach (var trip in trips)
            {
                await _cache.SetRecordAsync(Trip + trip.Id, trip);
            }

            return Success(trips);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var trip = await _cache.GetRecordAsync<TripViewModel>(Trip + id);
            if (trip is null)
            {
                trip = await _tripService.GetTrip(id);
                await _cache.SetRecordAsync(Trip + id, trip);
            }

            return Success(trip);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TripViewModel model)
        {
            var id = await _tripService.AddTrip(model);
            await _cache.SetRecordAsync(Trip + id, model);
            return Success(id);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] TripViewModel model)
        {
            await _tripService.UpdateTrip(id, model);
            await _cache.SetRecordAsync(Trip + id, model);
            return Success();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _tripService.DeleteTrip(id);
            return Success();
        }
    }
}