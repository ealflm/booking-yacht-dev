using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingYacht.Model;
using BookingYacht.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookingYacht.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceTypeController : ControllerBase
    {
        private IPlaceTypeRepository placeTypeRepository;

        public PlaceTypeController()
        {
            this.placeTypeRepository = new PlaceTypeRepository(new BookYachtContext());
        }


        // GET api/<PlaceTypeController>/5
        [HttpGet("{token}")]
        public IEnumerable<PlaceType> Get(string token)
        {
            return placeTypeRepository.GetPlaceTypes();
        }

        // POST api/<PlaceTypeController>
        [HttpPost("{token}")]
        public void Post(string token,[FromBody] PlaceType placeType)
        {
            placeTypeRepository.InsertPlaceType(placeType);
            placeTypeRepository.Save();
        }

        // PUT api/<PlaceTypeController>/5
        [HttpPut("{token}")]
        public void Put(string token, [FromBody] PlaceType placeType)
        {
            placeTypeRepository.UpdatePlaceType(placeType);
            placeTypeRepository.Save();
        }

        // DELETE api/<PlaceTypeController>/5
        [HttpDelete("{id}/{token}")]
        public void Delete(Guid id, string token)
        {
            placeTypeRepository.DeletePlaceType(id);
            placeTypeRepository.Save();
        }
    }
}
