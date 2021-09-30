using BookingYacht.Business.Interfaces;
using BookingYacht.Business.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookingYacht.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetMembers()
        {
            var members = await _memberService.GetMembers();
            return Ok(members);
        }

        [HttpGet("{Code}")]
        public async Task<IActionResult> GetMember(string code)
        {
            var member = await _memberService.GetMember(code);
            if (member == null)
            {
                return NotFound();
            }
            return Ok(member);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddMember([FromBody] MemberViewModel model)
        {
            var id = await _memberService.AddMember(model);
            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember([FromBody] MemberViewModel model, [FromRoute] Guid id)
        {
            await _memberService.UpdateMember(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember([FromRoute] Guid id)
        {
            await _memberService.DeleteMember(id);
            return Ok();
        }

    }
}
