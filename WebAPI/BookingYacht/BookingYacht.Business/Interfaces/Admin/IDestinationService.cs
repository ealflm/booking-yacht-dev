using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using BookingYacht.Data.Models;

namespace BookingYacht.Business.Interfaces.Admin
{
    public interface IDestinationService
    {
        Task<List<DestinyViewModel>> SearchDestinies(DestinySearchModel model = null);
        Task<List<Destination>> SearchDestiniesNavigation(DestinySearchModel model = null);
        Task<DestinyViewModel> GetDestiny(Guid id);
        Task<Destination> GetDestinyNavigation(Guid id);
        Task<Guid> AddDestiny(DestinyViewModel model);
        Task UpdateDestiny(Guid id, DestinyViewModel model);
        Task DeleteDestiny(Guid id);
    }
}