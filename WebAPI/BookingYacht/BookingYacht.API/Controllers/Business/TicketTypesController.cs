using BookingYacht.Business.Interfaces.Business;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookingYacht.API.Controllers.Business
{
    [Route(BusinessRoute)]
    [ApiController]
    [Authorize]
    public class TicketTypesController : BaseBusinessController
    {
        private readonly ITicketTypeService _tickettypeService;

        public TicketTypesController(ITicketTypeService tickettypeService)
        {
            _tickettypeService = tickettypeService;
        }



        // GET: api/<TicketTypesController>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] TicketTypeSearchModel model)
        {
            var ticketTypes = await _tickettypeService.SearchTicketTypes(model);
            return Success(ticketTypes);
        }

        // GET api/<TicketTypesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var ticketType  = await _tickettypeService.GetTicketType(id);
            return Success(ticketType);
        }

        // POST api/<TicketTypesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TicketTypeViewModel model)
        {
            var id = await _tickettypeService.AddTicketType(model);
            return Success(id);
        }

        // PUT api/<TicketTypesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] TicketTypeViewModel model)
        {
            await _tickettypeService.UpdateTicketType(id, model);
            return Success();
        }

        // DELETE api/<TicketTypesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _tickettypeService.DeleteTicketType(id);
            return Success();
        }
    }
}
