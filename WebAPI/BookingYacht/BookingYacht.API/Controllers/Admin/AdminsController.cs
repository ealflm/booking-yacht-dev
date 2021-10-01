using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.SearchModels;
using FirebaseAdmin;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookingYacht.API.Controllers.Admin
{
    [Route(ApiVer1Route)]
    [ApiController]
    public class AdminsController : BaseApiVer1Controller
    {
        private readonly IAdminService _adminService;

        public AdminsController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginSearchModel model)
        {
            var result = await _adminService.Login(model);

            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterSearchModel model)
        {
            var result = await _adminService.Register(model);

            if (result == false)
            {
                return NotFound();
            }
            return Ok(result);
        }

    }
}
