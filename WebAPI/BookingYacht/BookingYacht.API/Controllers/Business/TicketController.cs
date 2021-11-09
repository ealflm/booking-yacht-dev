using System;
using System.Threading.Tasks;
using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingYacht.API.Controllers.Business
{
    [Route(BusinessRoute)]
    [ApiController]
    [Authorize]
    [ApiExplorerSettings(GroupName = Role)]
    public class TicketController : BaseBusinessController
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

        [HttpPost("qr")]
        public async Task<IActionResult> Post([FromBody] QRViewModel model)
        {
            var ticket = await _service.CheckQRString(model.QR);
            if (ticket != null)
            {
                return Success(ticket);
            }

            return Fail("Ticket does not exist");
        }
    }
}