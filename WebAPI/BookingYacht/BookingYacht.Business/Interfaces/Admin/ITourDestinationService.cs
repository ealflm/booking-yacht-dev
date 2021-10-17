using System;
using System.Threading.Tasks;
using BookingYacht.Business.ViewModels;

namespace BookingYacht.Business.Interfaces.Admin
{
    public interface ITourDestination
    {
        Task<TourDestinationListViewModel> GetDestinationByTour(Guid id);
    }
}