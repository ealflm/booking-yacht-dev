using BookingYacht.Business.ViewModels;
using System.Threading.Tasks;

namespace BookingYacht.Business.Interfaces.Admin
{
    public interface IAdminService
    {
        Task<string> Login(AdminViewModel model);
    }
}
