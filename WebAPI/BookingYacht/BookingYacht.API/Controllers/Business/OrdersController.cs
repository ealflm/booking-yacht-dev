using System;
using System.Threading.Tasks;
using BookingYacht.API.Extensions;
using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using BookingYacht.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace BookingYacht.API.Controllers.Business
{
    [Route(BusinessRoute)]
    [ApiController]
    [Authorize]
    [ApiExplorerSettings(GroupName = Role)]
    public class OrdersController : BaseBusinessController
    {
        private readonly IOrdersService _service;

        private readonly IDistributedCache _cache;
        private const string Order = "Order_";

        public OrdersController(IOrdersService service, IDistributedCache cache)
        {
            _service = service;
            _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] OrdersSearchModel model)
        {
            var order = await _service.Search(model);
            foreach (var view in order)
            {
                await _cache.SetRecordAsync(Order + view.Id, view);
            }
            return Success(order);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var navigation = Order + "_Nav" + id;
            var order = await _cache.GetRecordAsync<Order>(navigation);
            if (order is null)
            {
                order = await _service.GetNavigation(id);
                await _cache.SetRecordAsync(navigation, order);
            }
            return order != null ? Success(order) : Fail("The Order's not exist");
        }


        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] OrdersViewModel model)
        {
            var update = await _service.Update(id, model);
            await _cache.SetRecordAsync(Order + id, model);
            return update ? Success() : Fail("The Order's not exist");
        }


        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var delete = await _service.Delete(id);
            return delete ? Success() : Fail("The Order's not exist");
        }
    }
}