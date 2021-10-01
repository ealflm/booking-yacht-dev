﻿using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookingYacht.API.Controllers.Admin
{
    [Route(ApiVer1Route)]
    [ApiController]
    [Authorize]
    public class BusinessAccountsController : BaseApiVer1Controller
    {
        private readonly IManageBusinessAccountService _manageBusinessAccountService;

        public BusinessAccountsController(IManageBusinessAccountService manageBusinessAccountService)
        {
            _manageBusinessAccountService = manageBusinessAccountService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] BusinessSearchModel model)
        {
            var businesses = await _manageBusinessAccountService.SearchBusinessed(model);
            return Ok(businesses);
        }

        // GET api/<ManageBusinessAccountController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var business = await _manageBusinessAccountService.GetBusiness(id);
           
            return Ok(business);
        }

        // POST api/<ManageBusinessAccountController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BusinessViewModel model)
        {
            var id = await _manageBusinessAccountService.AddBusiness(model);
            return Ok(id);
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
