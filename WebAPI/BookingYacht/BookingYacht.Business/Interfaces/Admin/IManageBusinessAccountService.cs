using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingYacht.Business.Interfaces.Admin
{
    public interface IManageBusinessAccountService
    {
        Task<List<BusinessViewModel>> SearchBusinesses(BusinessSearchModel model=null);
        Task<BusinessViewModel> GetBusiness(Guid id);
        Task<Guid> AddBusiness(BusinessViewModel model);
        Task UpdateBusiness(Guid id, BusinessViewModel model);
        Task DeleteBusiness(Guid id);
    }
}
