namespace BookingYacht.API.Controllers.Business
{
    public class BaseBusinessController : BaseController
    {
        protected const string Role = "Business";
        protected const string BusinessRoute = "api/" + Version + "/" + Role + "/[controller]";
    }
}
