using System;
using System.Threading.Tasks;
using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookingYacht.API.Controllers.Admin
{
    [Route(AdminRoute)]
    [ApiController]
    public class BusinessTourController : BaseAdminController
    {
        private readonly IBusinessTourService _service;

        public BusinessTourController(IBusinessTourService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] BusinessTourSearchModel model)
        {
            var result = await _service.SearchAgenciesNavigation(model);
            return Success(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _service.GetBusinessTour(id);
            return Success(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BusinessTourViewModel model)
        {
            var id = await _service.AddBusinessTour(model);
            return Success(id);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] BusinessTourViewModel model)
        {
            await _service.UpdateBusinessTour(id, model);
            return Success();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteBusinessTour(id);
            return result ? Success() : Fail("The Business Tour not Exist!!!");
        }
        
    }
}