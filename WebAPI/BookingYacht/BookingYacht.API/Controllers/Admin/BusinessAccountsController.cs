using Microsoft.AspNetCore.Mvc;
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
    public class BusinessAccountsController : ControllerBase
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
