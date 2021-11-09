using BookingYacht.Business;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookingYacht.API.Controllers.Admin
{
    [Route(AdminRoute)]
    [ApiController]
    [Authorize]
    [ApiExplorerSettings(GroupName = Role)]
    public class TicketTypesController : BaseAdminController
    {
        private readonly BookingYacht.Business.Interfaces.Business.ITicketTypeService _ticketTypeService;
        private readonly BookingYacht.Business.Interfaces.Admin.ITicketTypeService _ticketTypeServiceAdmin;
        public TicketTypesController(BookingYacht.Business.Interfaces.Business.ITicketTypeService ticketTypeService, BookingYacht.Business.Interfaces.Admin.ITicketTypeService ticketTypeServiceAdmin)
        {
            _ticketTypeService = ticketTypeService;
            _ticketTypeServiceAdmin = ticketTypeServiceAdmin;
        }


        // GET: api/<TicketTypesController>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] TicketTypeSearchModel model)
        {
            var ticketType = await _ticketTypeService.SearchTicketTypes(model);
            return Success(ticketType);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var ticketType = await _ticketTypeServiceAdmin.GetNavigation(id);

            return Success(ticketType);
        }

        // PUT api/<TicketTypesController>/5
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] TicketTypeViewModel model)
        {
            //Set -> Update
            await _ticketTypeServiceAdmin.Set(id, model);
            return Success();
        }
    }
}