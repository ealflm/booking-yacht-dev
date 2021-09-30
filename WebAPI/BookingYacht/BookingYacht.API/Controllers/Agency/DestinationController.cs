using System.Threading.Tasks;
using BookingYacht.Business.Interfaces.Agency;
using Microsoft.AspNetCore.Mvc;


namespace BookingYacht.API.Controllers.Agency
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class DestinationController : ControllerBase
    {
        private readonly IDestinationService _service;

        public DestinationController(IDestinationService service)
        {
            _service = service;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetDestinations()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{search}")]
        public async Task<IActionResult> SearchDestinations(string search)
        {
            var list = _service.SearchByAddress(search);
            return Ok(list);
        }
        
    }
}