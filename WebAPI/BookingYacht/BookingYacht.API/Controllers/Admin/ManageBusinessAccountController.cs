﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.ViewModels;
using BookingYacht.Business.SearchModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookingYacht.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageBusinessAccountController : ControllerBase
    {
        private readonly IManageBusinessAccountService _manageBusinessAccountService;

        public ManageBusinessAccountController(IManageBusinessAccountService manageBusinessAccountService)
        {
            _manageBusinessAccountService = manageBusinessAccountService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var businesses = await _manageBusinessAccountService.GetBusinesses();
            return Ok(businesses);
        }

        // GET api/<ManageBusinessAccountController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var member = await _manageBusinessAccountService.GetBusiness(id);
           
            return Ok(member);
        }

        // GET api/<ManageBusinessAccountController>/5
        [HttpGet("Paging/{page}")]
        public async Task<IActionResult> Get(int page)
        {
            var member = await _manageBusinessAccountService.SearchBusinessed(page: page);

            return Ok(member);
        }

        // POST api/<ManageBusinessAccountController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BusinessViewModel model)
        {
            var id = await _manageBusinessAccountService.AddBusiness(model);
            return Ok(id);
        }

        // POST api/<ManageBusinessAccountController>
        [HttpPost("search")]
        public async Task<IActionResult> Post([FromBody] BusinessSearchModel model)
        {
            var businesses = await _manageBusinessAccountService.SearchBusinessed(model);
            return Ok(businesses);
        }

        [HttpPost("search/{page}")]
        public async Task<IActionResult> Post(int page ,[FromBody] BusinessSearchModel model)
        {
            var businesses = await _manageBusinessAccountService.SearchBusinessed(model, page);
            return Ok(businesses);
        }

        // PUT api/<ManageBusinessAccountController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] BusinessViewModel model)
        {
            await _manageBusinessAccountService.UpdateBusiness(id, model);
            return Ok();
        }

        // DELETE api/<ManageBusinessAccountController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _manageBusinessAccountService.DeleteBusiness(id);
            return Ok();
        }
    }
}
