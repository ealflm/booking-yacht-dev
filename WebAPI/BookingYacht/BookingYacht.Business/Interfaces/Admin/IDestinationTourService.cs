using System;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingYacht.Business.Interfaces.Admin
{
    public interface IDestinationTourService
    {
        Task<List<DestinationTourViewModel>> SearchDestinationTours(DestinationTourSearchModel model = null);
        Task<List<DestinationTourViewModel>> SearchDestinationToursNavigation(DestinationTourSearchModel model = null);
        Task<DestinationTourViewModel> GetDestinationTour(Guid id);
        Task<DestinationTourViewModel> GetDestinationTourNavigation(Guid id);
        Task<Guid> AddDestinationTour(DestinationTourViewModel model);
        Task UpdateDestinationTour(Guid id, DestinationTourViewModel model);
        Task DeleteDestinationTour(Guid id);
    }
}
