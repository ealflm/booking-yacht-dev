using System;
using System.Threading.Tasks;
using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.SearchModels;
using Microsoft.AspNetCore.Mvc;

namespace BookingYacht.API.Controllers.Agency
{
    [Route(ApiVer2Route)]
    [ApiController]
    public class TicketController : BaseApiVer2Controller
    {
        
        private readonly ITicketService _service;

        public TicketController(ITicketService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] TicketSearchModel model)
        {
            var tickets = await _service.SearchTicketsNavigation(model);
            return Success(tickets);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var ticket = await _service.GetTicketNavigation(id);
            return Success(ticket);
        }
    }
}