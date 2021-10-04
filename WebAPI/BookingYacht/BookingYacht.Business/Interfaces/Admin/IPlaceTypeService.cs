using BookingYacht.Business.ViewModels;
using BookingYacht.Business.SearchModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingYacht.Business.Interfaces.Admin
{
    public interface IPlaceTypeService
    {
        Task<List<PlaceTypeViewModel>> SearchPlaceTypes(PlaceTypeSearchModel model = null);
        Task<PlaceTypeViewModel> GetPlaceType(Guid id);
        Task<Guid> AddPlaceType(PlaceTypeViewModel model);
        Task UpdatePlaceType(Guid id, PlaceTypeViewModel model);
        Task DeletePlaceType(Guid id);
    }   
}
