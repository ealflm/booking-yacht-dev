namespace BookingYacht.API.Controllers.Admin
{
    public class BaseAdminController : BaseController
    {
        protected const string AdminRoute = "api/" + Version + "/admin/[controller]";
    }
}
