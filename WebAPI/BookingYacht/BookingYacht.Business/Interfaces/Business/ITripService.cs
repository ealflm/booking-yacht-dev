using System;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingYacht.Business.Interfaces.Business
{
    public interface ITripService
    {
        Task<List<TripViewModel>> SearchTrip(TripSearchModel model = null);
        Task<TripViewModel> GetTrip(Guid id);
        Task<Guid> AddTrip(TripViewModel model);
        Task UpdateTrip(Guid id, TripViewModel model);
        Task DeleteTrip(Guid id);
    }
}
