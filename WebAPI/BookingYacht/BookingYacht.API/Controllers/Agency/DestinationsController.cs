using System;
using System.Threading.Tasks;
using BookingYacht.Business.Interfaces.Agency;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace BookingYacht.API.Controllers.Agency
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class DestinationsController : ControllerBase
    {
        private readonly IDestinationService _service;

        public DestinationsController(IDestinationService manageBusinessAccountService)
        {
            _service = manageBusinessAccountService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] DestinySearchModel model)
        {
            var businesses = await _service.SearchDestinies(model);
            return businesses.Count > 0 ? Ok(businesses) : NotFound();
        }

        // GET api/<ManageBusinessAccountController>/5
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var business = await _service.GetDestiny(id);
           
            return Ok(business);
        }

        // POST api/<ManageBusinessAccountController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DestinyViewModel model)
        {
            var id = await _service.AddDestiny(model);
            return Ok(id);
        }



        // PUT api/<ManageBusinessAccountController>/5
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] DestinyViewModel model)
        {
            await _service.UpdateDestiny(id, model);
            return Ok();
        }

        // DELETE api/<ManageBusinessAccountController>/5
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteDestiny(id);
            return Ok();
        }
        
    }
}