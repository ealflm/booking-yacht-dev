using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using BookingYacht.Data.Models;

namespace BookingYacht.Business.Interfaces.Admin
{
    public interface IVehicleService
    {
        Task<List<VehicleViewModel>> SearchVehicles(VehicleSearchModel model = null);
        Task<List<Vehicle>> SearchVehiclesNavigation(VehicleSearchModel model = null);
        Task<VehicleViewModel> GetVehicle(Guid id);
        Task<Vehicle> GetVehicleNavigation(Guid id);
        Task<Guid> AddVehicle(VehicleViewModel model);
        Task<bool> UpdateVehicle(Guid id, VehicleViewModel model);
        Task<bool> DeleteVehicle(Guid id);
        Task<int> Count();
    }
}