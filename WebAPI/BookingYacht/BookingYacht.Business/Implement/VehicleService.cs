using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingYacht.Business.Interfaces;
using BookingYacht.Business.ViewModels;

namespace BookingYacht.Business.Implement
{
    class VehicleService : IVehicle
    {
        public Task<List<VehicleModel>> GetVehicle()
        {
            throw new NotImplementedException();
        }

        public Task<List<VehicleModel>> SearchVehicle(string model)
        {
            throw new NotImplementedException();
        }

        public Task<List<VehicleModel>> SearchAgenciesString(string search)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddVehicle(VehicleModel model)
        {
            throw new NotImplementedException();
        }

        public Task<VehicleModel> UpdateVehicle(Guid id, VehicleModel model)
        {
            throw new NotImplementedException();
        }

        public Task<VehicleModel> UpdateVehicleDisable(string id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteVehicle(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}