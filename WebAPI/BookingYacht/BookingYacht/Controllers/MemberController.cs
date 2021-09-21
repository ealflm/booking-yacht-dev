using BookingYacht.Models;
using BookingYacht.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookingYacht.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly MemberRepository repository;

        public MemberController()
        {
            repository = new MemberRepository();
        }

        [HttpGet]
        public ActionResult<Member> GetMembers()
        {
            var members = repository.GetMembers();
            return Ok(members);
        }

        [HttpGet("{Code}")]
        public ActionResult<Member> GetMember(string Code)
        {
            var member = repository.GetMember(Code);
            if (member is null)
            {
                return NotFound();
            }
            return Ok(member);
        }

        [HttpGet("Vip")]
        public ActionResult<string> GetVip()
        {
            return Ok("Tested Again!");
        }
    }
}
