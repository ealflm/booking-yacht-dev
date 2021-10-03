using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using System;
using System.Threading.Tasks;

namespace BookingYacht.Business.Interfaces.Admin
{
    public interface IAdminService
    {
        Task<MessageResult> Login(LoginSearchModel model);
        Task<MessageResult> OpenLogin(OpenLoginSearchModel model);
        Task<Guid?> Register(RegisterSearchModel model);
    }
}
