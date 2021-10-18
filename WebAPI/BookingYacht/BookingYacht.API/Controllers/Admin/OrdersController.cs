using System;
using System.Threading.Tasks;
using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.CompilerServices;

namespace BookingYacht.API.Controllers.Admin
{
    [Route(AdminRoute)]
    [ApiController]
    [Authorize]
    [ApiExplorerSettings(GroupName = Role)]
    public class OrdersController: BaseAdminController
    {
        private readonly IOrdersService _service;

        public OrdersController(IOrdersService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] OrdersSearchModel model)
        {
            var order = await _service.SearchNavigation(model);
            return order != null ? Success(order) : Fail("The Order's not exist");
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var order = await _service.GetNavigation(id);
            return order != null ? Success(order) : Fail("The Order's not exist");
        }
        
        [HttpGet("total")]
        public async Task<IActionResult> Count()
        {
            var result = await _service.Count();
            return Success(result);
        }
    }
}