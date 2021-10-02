﻿using System;
using System.Threading.Tasks;
using BookingYacht.Business.Interfaces;
using BookingYacht.Business.Interfaces.Agency;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookingYacht.API.Controllers.Agency
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        public VehicleController(IVehicleService service)
        {
            _agencyService = service;
        }

        private readonly IVehicleService _agencyService;
        
        [HttpGet()]
        public async Task<IActionResult> Search([FromQuery] VehicleSearchModel model)
        {
            var vehicles = await _agencyService.SearchVehicles(model);
            return Ok(vehicles);
        }  
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var vehicles = await _agencyService.GetVehicle(id);
            return Ok(vehicles);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] VehicleViewModel model)
        {
            var vehicle = await _agencyService.AddVehicle(model);
            return Ok(vehicle);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, 
                                                [FromBody] VehicleViewModel model)
        {
            await _agencyService.UpdateVehicle(id, model);
            return Ok();
        }
        
        [HttpDelete]
        public async Task<IActionResult> Update([FromQuery] Guid id)
        {
            await _agencyService.DeleteVehicle(id);
            return Ok();
        }
        
    }
}