namespace BookingYacht.API.Controllers.Business
{
    public class BaseBusinessController : BaseController
    {
        protected const string BusinessRoute = "api/" + Version + "/business/[controller]";
    }
}
