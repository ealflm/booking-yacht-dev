using System;
using System.Threading.Tasks;
using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookingYacht.API.Controllers.Admin
{
    [Route(ApiVer1Route)]
    [ApiController]
    public class TicketController : BaseApiVer1Controller
    {
        
        private readonly ITicketService _service;

        public TicketController(ITicketService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] TicketSearchModel model)
        {
            var tickets = await _service.SearchTickets(model);
            return Success(tickets);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var ticket = await _service.GetTicket(id);
           
            return Success(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TicketViewModel model)
        {
            var id = await _service.AddTicket(model);
            return Success(id);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] TicketViewModel model)
        {
            await _service.UpdateTicket(id, model);
            return Success();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteTicket(id);
            return Success();
        }
    }
}