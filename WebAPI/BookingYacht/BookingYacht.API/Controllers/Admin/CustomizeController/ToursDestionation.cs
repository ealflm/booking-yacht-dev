using System;
using System.Threading.Tasks;
using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.Interfaces.Admin.CustomizeServices;
using BookingYacht.Business.SearchModels.CustomizeSearchModels;
using BookingYacht.Business.ViewModels.CustomModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingYacht.API.Controllers.Admin.CustomizeController
{
    [Route(AdminRoute)]
    [ApiController]
    [Authorize]
    [ApiExplorerSettings(GroupName = Role)]
    public class ToursDestinationController: BaseAdminController
    {
        private readonly ITourDestinationService _service;

        public ToursDestinationController(ITourDestinationService service)
        {
            _service = service;
        }

        
        //GET api/Tour/id/Destinations
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetDestination(Guid id)
        {
            var dtl = await _service.GetDestinationByTour(id);
            return dtl == null ? Fail("Not Found") : Success(dtl);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TourDestinationListSearchModel model)
        {
            var res = await _service.UpdateDestinationByTour(model);
            return res ? Success() : Fail("Doesnt Have Model Or Destination");
        }
        
    }
}