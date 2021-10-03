using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;

namespace BookingYacht.Business.Interfaces.Admin
{
    public interface IAgencyService : IBaseService 
    {
        Task<List<AgencyViewModels>> SearchAgencies(AgencySearchModel model=null);
        Task<AgencyViewModels> GetAgency(Guid id);
        Task<Guid> AddAgency(AgencyViewModels model);
        Task UpdateAgency(Guid id, AgencyViewModels model);
        Task DeleteAgency(Guid id);
    }
}