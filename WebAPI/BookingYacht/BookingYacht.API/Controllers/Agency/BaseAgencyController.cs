namespace BookingYacht.API.Controllers.Agency
{
    public class BaseAgencyController : BaseController
    {
        protected const string Role = "Agency";
        protected const string AgencyRoute = "api/" + Version + "/" + Role + "/[controller]";
    }
}