using System;
using System.Threading.Tasks;
using BookingYacht.Business.SearchModels.CustomizeSearchModels;
using BookingYacht.Business.ViewModels.CustomModels;

namespace BookingYacht.Business.Interfaces.Admin.CustomizeServices
{
    public interface ITourDestinationService
    {
        Task<TourDestinationListViewModel> GetDestinationByTour(Guid id);
        Task<bool> UpdateDestinationByTour(TourDestinationListSearchModel model = null);
        // Task<TourDestinationListViewModel> DeleteDestinationByTour(Guid id);
    }
}