using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingYacht.Business.ViewModels;

namespace BookingYacht.Business.Interfaces
{
    public interface IAgencyService : IBaseService 
    {
        Task<List<AgencyModel>> GetAgency();
        
        Task<List<AgencyModel>> SearchAgency(string model);
        public Task<List<AgencyModel>> SearchAgenciesString(string search);
        
        Task<bool> AddAgency(Guid id, AgencyModel model);
        
        Task<AgencyModel> UpdateAgency(Guid id, AgencyModel model);

        Task<AgencyModel> UpdateAgencyDisable(string id);
        
        Task DeleteAgency(Guid id);
    }
}