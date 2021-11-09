using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookingYacht.API.Controllers.Admin
{
    [Route(AdminRoute)]
    [ApiController]
    [Authorize]
    [ApiExplorerSettings(GroupName = Role)]
    public class AccountsController : BaseAdminController
    {
        private readonly IAdminService _service;

        public AccountsController(IAdminService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] AdminSearchModel model)
        {
            var admin = await _service.SearchAdmins(model);
            return Success(admin);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var admin = await _service.GetAdmin(id);

            return Success(admin);
        }

        /*
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AdminViewModel model)
        {
            var id = await _service.AddAdmin(model);
            return Success(id);
        }
        */

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] AdminViewModel model)
        {
            await _service.UpdateAdmin(id, model);
            return Success();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAdmin(id);
            return Success();
        }
    }
}