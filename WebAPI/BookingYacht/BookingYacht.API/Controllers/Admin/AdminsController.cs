using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.SearchModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookingYacht.API.Controllers.Admin
{
    [Route(AdminRoute)]
    [ApiController]
    public class AdminsController : BaseAdminController
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

            if (result.Data == null)
            {
                return Fail(result.Message);
            }

            return Success(result.Data);
        }

        [HttpPost("open-login")]
        public async Task<IActionResult> OpenLogin([FromBody] OpenLoginSearchModel model)
        {
            var result = await _adminService.OpenLogin(model);

            if (result.Data == null)
            {
                return Fail(result.Message);
            }

            return Success(result.Data);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterSearchModel model)
        {
            var result = await _adminService.Register(model);

            if (result == null)
            {
                return Fail("This email address has already been registered");
            }
            return Success(result);
        }

    }
}
