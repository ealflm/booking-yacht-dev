using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;

namespace BookingYacht.Business.Interfaces.Admin
{
    public interface IAgencyService : IBaseService
    {
        #region Authorization

        Task<MessageResult> Login(LoginSearchModel model);
        Task<MessageResult> OpenLogin(OpenLoginSearchModel model);
        Task<Guid?> Register(RegisterSearchModel model);

        #endregion

        Task<List<AgencyViewModels>> SearchAgencies(AgencySearchModel model = null);
        Task<AgencyViewModels> GetAgency(Guid id);
        Task<Guid> AddAgency(AgencyViewModels model);
        Task UpdateAgency(Guid id, AgencyViewModels model);
        Task<bool> DeleteAgency(Guid id);
        Task<int> Count();
    }
}