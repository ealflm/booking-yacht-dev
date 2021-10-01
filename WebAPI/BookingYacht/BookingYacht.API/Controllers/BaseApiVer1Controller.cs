using BookingYacht.API.Utilities.Response;
using Microsoft.AspNetCore.Mvc;

namespace BookingYacht.API.Controllers
{
    public class BaseApiVer1Controller : ControllerBase
    {
        protected const string ApiVer1Route = "api/v1/[controller]";

        [NonAction]
        public virtual OkObjectResult Success(object value)
        {
            var model = new SuccessModel()
            {
                Data = value
            };
            return Ok(model);
        }

        [NonAction]
        public virtual OkObjectResult Fail(string value)
        {
            var model = new ErrorModel()
            {
                Error = value
            };
            return Ok(model);
        }

    }
}
