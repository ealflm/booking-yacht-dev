using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingYacht.Business.Enum;
using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using BookingYacht.Data.Interfaces;
using BookingYacht.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingYacht.Business.Implement.Admin
{

    public class VehicleService : BaseService, IVehicleService
    {
        public VehicleService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<List<VehicleViewModel>> SearchVehicles(VehicleSearchModel model = null)
        {
            model ??= new VehicleSearchModel();
            var searchVal = await _unitOfWork.VehicleRepository.Query()
                .Where(x => model.Name == null | x.Name.Equals(model.Name))
                .Where(x => model.RegistrationNumber == null | x.RegistrationNumber.Equals(model.RegistrationNumber))
                .Where(x => model.WhereProduction == null | x.WhereProduction.Equals(model.WhereProduction))
                .Where(x => model.YearOfManufacture == null | x.YearOfManufacture==model.YearOfManufacture)
                .Where(x => model.Descriptions == null | x.Descriptions.Equals(model.Descriptions))
                .Where(x => model.Seat == null | x.Seat == model.Seat)
                .Where(x => model.IdBusiness == null | x.IdBusiness.Equals(model.IdBusiness))
                .Where(x => model.IdVehicleType == null | x.IdVehicleType.Equals(model.IdVehicleType))
                .Where(x => model.Status == null | x.Status == model.Status)
                .OrderBy(x => x.Seat)
                .Skip(model.AmountItem * (model.Page != 0 ? model.Page - 1 : 0))
                .Take(model.Page != 0 ? model.AmountItem : _unitOfWork.VehicleRepository.Query().Count())
                .Select(x => new VehicleViewModel()
                {
                    Id = x.Id,
                    RegistrationNumber=x.RegistrationNumber,
                    WhereProduction=x.WhereProduction,
                    YearOfManufacture=x.YearOfManufacture,
                    Descriptions = x.Descriptions,
                    IdBusiness = x.IdBusiness,
                    IdVehicleType = x.IdVehicleType,
                    Name = x.Name,
                    Seat = x.Seat,
                    Status = x.Status,
                    ImageLink= x.ImageLink
                })
                .OrderBy(x => x.Seat)
                .ToListAsync();
            return searchVal;
        }

        public async Task<List<Vehicle>> SearchVehiclesNavigation(VehicleSearchModel model = null)
        {
            model ??= new VehicleSearchModel();
            var searchVal = await _unitOfWork.Context().Vehicles
                .Include(x => x.IdVehicleTypeNavigation)
                // .Include(x => x.IdBusinessNavigation)
                .Where(x => model.Name == null | x.Name.Contains(model.Name))
                .Where(x => model.RegistrationNumber == null | x.RegistrationNumber.Equals(model.RegistrationNumber))
                .Where(x => model.WhereProduction == null | x.WhereProduction.Equals(model.WhereProduction))
                .Where(x => model.YearOfManufacture == null | x.YearOfManufacture == model.YearOfManufacture)
                .Where(x => model.Descriptions == null | x.Descriptions.Equals(model.Descriptions))
                .Where(x => model.Seat == null | x.Seat == model.Seat)
                .Where(x => model.IdBusiness == null | x.IdBusiness.Equals(model.IdBusiness))
                .Where(x => model.IdVehicleType == null | x.IdVehicleType.Equals(model.IdVehicleType))
                .Where(x => model.Status == null | x.Status == model.Status)
                .OrderBy(x => x.Seat)
                .Skip(model.AmountItem * (model.Page != 0 ? model.Page - 1 : 0))
                .Take(model.Page != 0 ? model.AmountItem : _unitOfWork.VehicleRepository.Query().Count())
                .OrderBy(x => x.Seat)
                .ToListAsync();
            return searchVal;
        }

        public async Task<VehicleViewModel> GetVehicle(Guid id)
        {
            var value = await _unitOfWork.VehicleRepository.Query()
                .Where(x => x.Id.Equals(id))
                .Select(x => new VehicleViewModel()
                {
                    Id = x.Id,
                    RegistrationNumber = x.RegistrationNumber,
                    WhereProduction = x.WhereProduction,
                    YearOfManufacture = x.YearOfManufacture,
                    Descriptions = x.Descriptions,
                    IdBusiness = x.IdBusiness,
                    IdVehicleType = x.IdVehicleType,
                    Name = x.Name,
                    Seat = x.Seat,
                    Status = x.Status,
                    ImageLink= x.ImageLink
                }).FirstOrDefaultAsync();
            return value;
        } 
        public async Task<Vehicle> GetVehicleNavigation(Guid id)
        {
            var value = await _unitOfWork.Context().Vehicles
                .Include(x => x.IdVehicleTypeNavigation)
                .Where(x => x.Id.Equals(id))
                .FirstOrDefaultAsync();
            return value;
        }

        public async Task<Guid> AddVehicle(VehicleViewModel model)
        {
            var vehicle = _unitOfWork.VehicleRepository.Query()
                .Add(new Vehicle()
                {
                    Id = model.Id,
                    RegistrationNumber = model.RegistrationNumber,
                    WhereProduction = model.WhereProduction,
                    YearOfManufacture = model.YearOfManufacture,
                    Descriptions = model.Descriptions,
                    IdBusiness = model.IdBusiness,
                    IdVehicleType = model.IdVehicleType,
                    Name = model.Name,
                    Seat = model.Seat,
                    Status = (int) Status.ENABLE,
                    ImageLink= model.ImageLink
                });
            await _unitOfWork.SaveChangesAsync();
            return vehicle.Entity.Id;
        }

        public async Task<bool> UpdateVehicle(Guid id, VehicleViewModel model)
        {
            var vehicle = _unitOfWork.VehicleRepository.Query()
                .Update(new Vehicle()
                {
                    Id = id,
                    RegistrationNumber = model.RegistrationNumber,
                    WhereProduction = model.WhereProduction,
                    YearOfManufacture = model.YearOfManufacture,
                    Descriptions = model.Descriptions,
                    IdBusiness = model.IdBusiness,
                    IdVehicleType = model.IdVehicleType,
                    Name = model.Name,
                    Seat = model.Seat,
                    Status = model.Status,
                    ImageLink= model.ImageLink
                });
            if (vehicle.Entity == null) return false;

            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteVehicle(Guid id)
        {
            var vehicle = _unitOfWork.VehicleRepository.GetById(id).Result;
            
            if (vehicle == null) return false;
            
            vehicle.Status = (int)Status.DISABLE;
            _unitOfWork.VehicleRepository.Update(vehicle);
            await _unitOfWork.SaveChangesAsync();

            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<int> Count()
        {
            return await _unitOfWork.Context().Vehicles.CountAsync();
        }
    }
}