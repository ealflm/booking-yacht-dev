using System;
using System.Threading.Tasks;
using BookingYacht.API.Controllers.Admin;
using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookingYacht.API.Controllers.Agency
{

    [Route(AgencyRoute)]
    [ApiController]
    [Authorize]
    [ApiExplorerSettings(GroupName = Role)]
    public class TicketTypesController : BaseAgencyController
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
            return Success(ticketType);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var ticketType = await _ticketTypeService.GetTicketType(id);

            return Success(ticketType);
        }

      
    }
}
