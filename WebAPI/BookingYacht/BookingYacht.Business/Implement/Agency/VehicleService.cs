using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingYacht.Business.Interfaces;
using BookingYacht.Business.Interfaces.Agency;
using BookingYacht.Business.ViewModels;
using BookingYacht.Data.Interfaces;
using BookingYacht.Data.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BookingYacht.Business.Implement.Agency
{
    internal static class Mapper
    {
        public static Vehicle MappingToVehicle(VehicleModel model)
        {
            return new Vehicle()
            {
                Name = model.Name,
                Descriptions = model.Description,
                Id = model.Id,
                Seat = model.Seat,
                Status = model.Status
            };
        }

        public static VehicleModel MappingToModel(Vehicle vehicle)
        {
            return new VehicleModel()
            {
                Name = vehicle.Name,
                Description = vehicle.Descriptions,
                Id = vehicle.Id,
                Seat = vehicle.Seat,
                Status = vehicle.Status
            };
        }
        
    }
    
    public class VehicleService : BaseService, IVehicleService
    {
        public VehicleService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        
        public async Task<List<VehicleModel>> GetVehicle()
        {
            var sql = $"SELECT Id, Name, Seat, IdVehicleType, IdBusiness, Descriptions, Status FROM Vehicle WHERE Status = @status";
            var vehicles = await _unitOfWork.VehicleRepository.Query()
                .FromSqlRaw(sql, new SqlParameter("@status", 1))
                .ToListAsync();
            var vehicleModels = vehicles.Select(Mapper.MappingToModel).ToList();
            return vehicleModels;
            
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