using System.Threading.Tasks;
using BookingYacht.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookingYacht.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        public VehicleController(IVehicleService service)
        {
            _agencyService = service;
        }

        private readonly IVehicleService _agencyService;
        
        [HttpGet()]
        public async Task<IActionResult> GetListAgency()
        {
            var vehicles = await _agencyService.GetVehicle();
            return Ok(vehicles);
        }
    }
}