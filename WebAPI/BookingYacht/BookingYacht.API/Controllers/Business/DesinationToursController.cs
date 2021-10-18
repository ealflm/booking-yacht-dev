﻿using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using BookingYacht.Business.SearchModels.CustomizeSearchModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookingYacht.API.Controllers.Business
{
    [Route(BusinessRoute)]
    [ApiController]
    [Authorize]
    [ApiExplorerSettings(GroupName = Role)]
    public class DesinationToursController : BaseBusinessController
    {

        private readonly IDestinationTourService _destinationTourService;

        public DesinationToursController(IDestinationTourService destinationTourService)
        {
            _destinationTourService = destinationTourService;
        }



        // GET: api/<DesinationTourController>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] DestinationTourSearchModel model)
        {
            var destinationTours = await _destinationTourService.SearchDestinationTours(model);
            return Success(destinationTours);
        }

        // GET api/<DesinationTourController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var destinationTour = await _destinationTourService.GetDestinationTour(id);
            return Success(destinationTour);
        }

    }
}
