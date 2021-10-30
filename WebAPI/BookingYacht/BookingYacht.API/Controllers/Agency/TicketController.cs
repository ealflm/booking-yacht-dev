using System;
using System.Threading.Tasks;
using BookingYacht.Business.Enum;
using BookingYacht.Business.InsertModels;
using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingYacht.API.Controllers.Agency
{
    [Route(AgencyRoute)]
    [ApiController]
    [Authorize]
    [ApiExplorerSettings(GroupName = Role)]
    public class TicketController : BaseAgencyController
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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TicketsInsertModel model)
        {
            int j=0;
            int sum=model.AmountTickets[j];
            for(int i=0; i< model.CustomerNames.Count; i++)
            {
                if (i >= sum && j < model.IdTicketTypes.Count)
                {
                    j++;
                    sum += model.AmountTickets[j];
                }
                TicketViewModel ticket = new TicketViewModel();
                ticket.IdOrder = model.IdOrder;
                ticket.IdTrip = model.IdTrip;
                ticket.NameCustomer = model.CustomerNames[i];
                ticket.Phone = model.Phones[i];
                ticket.IdTicketType = model.IdTicketTypes[j];
                await _service.AddTicket(ticket);
            }
            return Success();
        }
    }
}