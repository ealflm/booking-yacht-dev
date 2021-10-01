using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingYacht.Business.ViewModels;

namespace BookingYacht.Business.Interfaces.Agency
{
    public interface IVehicleService
    {
        Task<List<VehicleModel>> GetVehicle();
        
        Task<List<VehicleModel>> SearchVehicle(string model);
        public Task<List<VehicleModel>> SearchAgenciesString(string search);
        
        Task<bool> AddVehicle(VehicleModel model);
        
        Task<VehicleModel> UpdateVehicle(Guid id, VehicleModel model);

        Task<VehicleModel> UpdateVehicleDisable(string id);
        
        Task DeleteVehicle(Guid id);
    }
}