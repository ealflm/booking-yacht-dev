using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingYacht.Business.Interfaces.Agency
{
    public interface IAdminService
    {
        #region Authorization

        Task<MessageResult> Login(LoginSearchModel model);
        Task<MessageResult> OpenLogin(OpenLoginSearchModel model);
        Task<Guid?> Register(RegisterSearchModel model);

        #endregion

        Task<AdminViewModel> GetAdmin(Guid id);
        Task<List<AdminViewModel>> SearchAdmins(AdminSearchModel model = null);
        Task<Guid> AddAdmin(AdminViewModel model);
        Task DeleteAdmin(Guid id);
        Task UpdateAdmin(Guid id, AdminViewModel model);
    }
}
