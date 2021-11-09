using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;

namespace BookingYacht.Business.Interfaces.Admin
{
    public interface IVehicleTypeService
    {
        Task<List<VehicleTypeViewModel>> SearchVehicleTypes(VehicleTypeSearchModel model = null);
        Task<VehicleTypeViewModel> GetVehicleType(Guid id);
        Task<Guid> AddVehicleType(VehicleTypeViewModel model);
        Task<bool> UpdateVehicleType(Guid id, VehicleTypeViewModel model);
        Task<bool> DeleteVehicleType(Guid id);
    }
}