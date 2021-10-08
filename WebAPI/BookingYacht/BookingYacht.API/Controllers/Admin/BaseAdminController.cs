namespace BookingYacht.API.Controllers.Admin
{
    public class BaseAdminController : BaseController
    {
        protected const string Role = "Admin";
        protected const string AdminRoute = "api/" + Version + "/" + Role + "/[controller]";
    }
}
