using BookingYacht.Business.Interfaces.Agency;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookingYacht.API.Controllers.Agency
{
    [Route(ApiVer2Route)]
    [ApiController]
    public class VehicleController : BaseApiVer2Controller
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