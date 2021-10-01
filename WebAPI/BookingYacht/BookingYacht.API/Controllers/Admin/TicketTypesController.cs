using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.ViewModels;
using BookingYacht.Business.SearchModels;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookingYacht.API.Controllers.Admin
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketTypesController : ControllerBase
    {
        private readonly ITicketTypeService _ticketTypeService;

        public TicketTypesController(ITicketTypeService ticketTypeService)
        {
            _ticketTypeService = ticketTypeService;
        }


        // GET: api/<TicketTypesController>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] TicketTypeSearchModel model)
        {
            var ticketType = await _ticketTypeService.SearchTicketTypes(model);
            return Ok(ticketType);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var ticketType = await _ticketTypeService.GetTicketType(id);

            return Ok(ticketType);
        }

        // PUT api/<TicketTypesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] TicketTypeViewModel model)
        {
            await _ticketTypeService.SetStatusTicketType(id, model);
            return Ok();
        }
    }
}
