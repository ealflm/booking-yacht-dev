using System;
using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.SearchModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BookingYacht.Business.NotificationUtils.NotificationModel;

namespace BookingYacht.API.Controllers.Business
{
    [Route("api/" + Version + "/" + Role)]
    [ApiController]
    [ApiExplorerSettings(GroupName = " " + Role)]
    public class AuthorizationsController : BaseBusinessController
    {
        private readonly IManageBusinessAccountService _adminService;

        public AuthorizationsController(IManageBusinessAccountService adminService)
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

        [HttpPost("registrationToken")]
        public async Task<IActionResult> GetRegistrationToken(
            [FromBody] RegistrationTokenModel model)
        {
            var result = await _adminService.SaveRegistrationToken(model);
            return result ? Success() : Fail("Token or id is invalid!!!");
        }
    }
}