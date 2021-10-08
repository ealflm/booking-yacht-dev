using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;

namespace BookingYacht.Business.Interfaces.Admin
{
    public interface IBusinessTourService
    {
        Task<List<BusinessTourViewModel>> SearchAgenciesNavigation(BusinessTourSearchModel model=null);
        Task<BusinessTourViewModel> GetBusinessTour(Guid id);
        Task<Guid> AddBusinessTour(BusinessTourViewModel model);
        Task<bool> UpdateBusinessTour(Guid id, BusinessTourViewModel model);
        Task<bool> DeleteBusinessTour(Guid id);
    }
}