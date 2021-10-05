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
        private const int Count = (int) CountElement.AtLeast;
        public VehicleService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<List<VehicleViewModel>> SearchVehicles(VehicleSearchModel model = null)
        {
            model ??= new VehicleSearchModel();
            var searchVal = await _unitOfWork.VehicleRepository.Query()
                .Where(x => model.Id == null | x.Id.Equals(model.Id))
                .Where(x => model.Name == null | x.Name.Equals(model.Name))
                .Where(x => model.Descriptions == null | x.Descriptions.Equals(model.Descriptions))
                .Where(x => model.Seat == null | x.Seat == model.Seat)
                .Where(x => model.IdBusiness == null | x.IdBusiness.Equals(model.IdBusiness))
                .Where(x => model.IdVehicleType == null | x.IdVehicleType.Equals(model.IdVehicleType))
                .Where(x => model.Status == null | x.Status == model.Status)
                .OrderBy(x => x.Seat)
                .Skip(Count * (model.Paging != 0 ? model.Paging - 1 : 0))
                .Take(model.Paging != 0 ? Count : _unitOfWork.VehicleRepository.Query().Count())
                .Select(x => new VehicleViewModel()
                {
                    Id = x.Id,
                    Descriptions = x.Descriptions,
                    IdBusiness = x.IdBusiness,
                    IdVehicleType = x.IdVehicleType,
                    Name = x.Name,
                    Seat = x.Seat,
                    Status = x.Status
                })
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
                    Descriptions = x.Descriptions,
                    IdBusiness = x.IdBusiness,
                    IdVehicleType = x.IdVehicleType,
                    Name = x.Name,
                    Seat = x.Seat,
                    Status = x.Status
                }).FirstOrDefaultAsync();
            return value;
        }

        public async Task<Guid> AddVehicle(VehicleViewModel model)
        {
            var vehicle = _unitOfWork.VehicleRepository.Query()
                .Add(new Vehicle()
                {
                    Id = model.Id,
                    Descriptions = model.Descriptions,
                    IdBusiness = model.IdBusiness,
                    IdVehicleType = model.IdVehicleType,
                    Name = model.Name,
                    Seat = model.Seat,
                    Status = (int) Status.ALL
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
                    Descriptions = model.Descriptions,
                    IdBusiness = model.IdBusiness,
                    IdVehicleType = model.IdVehicleType,
                    Name = model.Name,
                    Seat = model.Seat,
                    Status = model.Status
                });
            if (vehicle.Entity == null) return false;

            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteVehicle(Guid id)
        {
            var vehicle = await _unitOfWork.VehicleRepository.Query()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            
            if (vehicle == null) return false;
            
            vehicle.Status = (int)Status.DISABLE;
            _unitOfWork.VehicleRepository.Update(vehicle);
            await _unitOfWork.SaveChangesAsync();

            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}