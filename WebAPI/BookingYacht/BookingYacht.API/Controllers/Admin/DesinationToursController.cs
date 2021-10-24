using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using BookingYacht.Business.InsertModels;
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
    public class DesinationToursController : BaseAdminController
    {

        private readonly IDestinationTourService _destinationTourService;

        public DesinationToursController(IDestinationTourService destinationTourService)
        {
            _destinationTourService = destinationTourService;
        }



        // GET: api/<DesinationTourController>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] DestinationTourSearchModel model)
        {
            var destinationTours = await _destinationTourService.SearchDestinationToursNavigation(model);
            return Success(destinationTours);
        }

        // GET api/<DesinationTourController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var destinationTour = await _destinationTourService.GetDestinationTourNavigation(id);
            return Success(destinationTour);
        }

        // POST api/<DesinationTourController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DestinationTourInsertModel model)
        {
            for(int i=0; i<model.IdDestinationList.Count; i++)
            {
                DestinationTourViewModel destinationTour = new DestinationTourViewModel();
                destinationTour.IdTour = model.IdTour;
                destinationTour.IdDestination = model.IdDestinationList[i];
                destinationTour.Order = i + 1;
                await _destinationTourService.AddDestinationTour(destinationTour);
            }
            return Success();
        }

        // PUT api/<DesinationTourController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] DestinationTourViewModel model)
        {
            await _destinationTourService.UpdateDestinationTour(id, model);
            return Success();
        }

        // DELETE api/<DesinationTourController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _destinationTourService.DeleteDestinationTour(id);
            return Success();
        }
    }
}
