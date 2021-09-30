using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookingYacht.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminsController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AdminViewModel model)
        {
            var result = await _adminService.Login(model);

            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }

            return Ok(result);
        }
    }
}
