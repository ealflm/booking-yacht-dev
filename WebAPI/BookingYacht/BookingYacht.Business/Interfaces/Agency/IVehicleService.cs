using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;

namespace BookingYacht.Business.Interfaces.Agency
{
    public interface IVehicleService
    {
        Task<List<VehicleViewModel>> SearchVehicles(VehicleSearchModel model=null);
        Task<VehicleViewModel> GetVehicle(Guid id);
        Task<Guid> AddVehicle(VehicleViewModel model);
        Task<bool> UpdateVehicle(Guid id, VehicleViewModel model);
        Task<bool> DeleteVehicle(Guid id);
    }
}