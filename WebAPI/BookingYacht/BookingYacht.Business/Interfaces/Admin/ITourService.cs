using System;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingYacht.Business.Interfaces.Admin
{
    public interface ITourService
    {
        Task<List<TourViewModel>> SearchTours(TourSearchModel model = null);
        Task<TourViewModel> GetTour(Guid id);
        Task<Guid> AddTour(TourViewModel model);
        Task UpdateTour(Guid id, TourViewModel model);
        Task DeleteTour(Guid id);
    }
}
