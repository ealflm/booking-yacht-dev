using System;
using System.Linq;
using System.Threading.Tasks;
using BookingYacht.API.Extensions;
using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using BookingYacht.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace BookingYacht.API.Controllers.Admin
{
    
    [Route(AdminRoute)]
    [ApiController]
    [Authorize]
    [ApiExplorerSettings(GroupName = Role)]
    public class DestinationsController : BaseAdminController
    {
        private readonly IDestinationService _service;
        // private readonly IDistributedCache _cache;
        // private const string Des = "Des_";

        public DestinationsController(IDestinationService service
        //     ,IDistributedCache cache)
        // {
        ){
            _service = service;
            // _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] DestinySearchModel model)
        {
            var destinies = await _service.SearchDestiniesNavigation(model);
            // foreach (var des in destinies)
            // {
            //     await _cache.SetRecordAsync(Des + des.Id, des);
            // }
            return Success(destinies);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {

            // var destinies = await _cache.GetRecordAsync<Destination>(Des + id);
            // if (destinies is null)
            // {
            var  destinies = await _service.GetDestinyNavigation(id);
                // await _cache.SetRecordAsync(Des + id, destinies);
            // }
            return Success(destinies);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DestinyViewModel model)
        {
            var id = await _service.AddDestiny(model);
            // await _cache.SetRecordAsync(Des + id, model);
            return Success(id);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] DestinyViewModel model)
        {
            await _service.UpdateDestiny(id, model);
            // await _cache.SetRecordAsync(Des + id, model);
            return Success();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            
            await _service.DeleteDestiny(id);
            return Success();
        }
        
    }
}