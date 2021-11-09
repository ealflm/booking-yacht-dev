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

namespace BookingYacht.API.Controllers.Agency
{
    [Route(AgencyRoute)]
    [ApiController]
    [Authorize]
    [ApiExplorerSettings(GroupName = Role)]
    public class BusinessToursController : BaseAgencyController
    {
        private readonly IBusinessTourService _service;
        private readonly IDistributedCache _cache;
        private const string BusinessTour = "BTour_nav_";
        public BusinessToursController(IBusinessTourService service, IDistributedCache cache)
        {
            _service = service;
            _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] BusinessTourSearchModel model)
        {
            var result = await _service.SearchBusinessTourForAgency(model);
            result.ForEach(x => _cache?.SetRecordAsync(BusinessTour + x.id, x));
            return Success(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _cache.GetRecordAsync<BusinessTour>(BusinessTour + id);
            if (result is null)
            {
                result = await _service.GetBusinessTourNavigation(id);
            }
            return Success(result);
        }
    }
}