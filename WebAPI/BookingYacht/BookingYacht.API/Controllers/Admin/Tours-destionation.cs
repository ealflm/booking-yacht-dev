using System;
using System.Threading.Tasks;
using BookingYacht.Business.Interfaces.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingYacht.API.Controllers.Admin
{
    [Route(AdminRoute)]
    [ApiController]
    [Authorize]
    [ApiExplorerSettings(GroupName = Role)]
    public class ToursDestination: BaseAdminController
    {
        private readonly ITourDestination _service;

        public ToursDestination(ITourDestination service)
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
    }
}