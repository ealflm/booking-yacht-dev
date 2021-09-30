using BookingYacht.Business.SearchModels;
using System.Threading.Tasks;

namespace BookingYacht.Business.Interfaces.Admin
{
    public interface IAdminService
    {
        Task<string> Login(LoginSearchModel model);
        Task<bool> Register(RegisterSearchModel model);
    }
}
