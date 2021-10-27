using System;
using System.Threading.Tasks;
using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using BookingYacht.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingYacht.API.Controllers.Business
{
    [Route(BusinessRoute)]
    [ApiController]
    [Authorize]
    [ApiExplorerSettings(GroupName = Role)]
    public class BusinessToursController : BaseBusinessController
    {
        private readonly IBusinessTourService _service;

        public BusinessToursController(IBusinessTourService service)
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
            var result = await _service.GetBusinessTourNavigation(id);
            return Success(result);
        }

        
    }
}