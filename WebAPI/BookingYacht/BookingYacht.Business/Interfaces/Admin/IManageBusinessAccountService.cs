using BookingYacht.Business.PaymentModels;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingYacht.Business.NotificationUtils.NotificationModel;

namespace BookingYacht.Business.Interfaces.Admin
{
    public interface IManageBusinessAccountService
    {

        #region Authorization

        Task<MessageResult> Login(LoginSearchModel model);
        Task<MessageResult> OpenLogin(OpenLoginSearchModel model);
        Task<Guid?> Register(RegisterSearchModel model);

        #endregion
        Task<List<BusinessViewModel>> SearchBusinesses(BusinessSearchModel model=null);
        Task<BusinessViewModel> GetBusiness(Guid id);
        Task<Guid> AddBusiness(BusinessViewModel model);
        Task UpdateBusiness(Guid id, BusinessViewModel model);
        Task DeleteBusiness(Guid id);
        Task<List<BusinessPaymentModel>> GetPayment(PaymentSearchModel model);
        Task<BusinessPaymentModel> GetPaymentById(Guid id,PaymentSearchModel model);

        Task<bool> SaveRegistrationToken(RegistrationTokenModel model);
    }
}
