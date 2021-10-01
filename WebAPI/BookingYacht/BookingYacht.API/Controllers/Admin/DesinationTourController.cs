﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookingYacht.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DesinationTourController : ControllerBase
    {

        private readonly IDestinationTourService _destinationTourService;

        public DesinationTourController(IDestinationTourService destinationTourService)
        {
            _destinationTourService = destinationTourService;
        }



        // GET: api/<DesinationTourController>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] DestinationTourSearchModel model)
        {
            var destinationTours = await _destinationTourService.SearchDestinationTours(model);
            return Ok(destinationTours);
        }

        // GET api/<DesinationTourController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var destinationTour = await _destinationTourService.GetDestinationTour(id);
            return Ok(destinationTour);
        }

        // POST api/<DesinationTourController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DestinationTourViewModel model)
        {
            var id = await _destinationTourService.AddDestinationTour(model);
            return Ok(id);
        }

        // PUT api/<DesinationTourController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] DestinationTourViewModel model)
        {
            await _destinationTourService.UpdateDestinationTour(id, model);
            return Ok();
        }

        // DELETE api/<DesinationTourController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _destinationTourService.DeleteDestinationTour(id);
            return Ok();
        }
    }
}
