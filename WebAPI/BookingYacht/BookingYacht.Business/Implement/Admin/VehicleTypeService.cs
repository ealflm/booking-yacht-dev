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
    public class VehicleTypeService : BaseService, IVehicleTypeService
    {
        private const int Count = (int) CountElement.AtLeast;
        
        public VehicleTypeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<List<VehicleTypeViewModel>> SearchVehicleTypes(VehicleTypeSearchModel model = null)
        {
            model ??= new VehicleTypeSearchModel();
            var list = await _unitOfWork.VehicleTypeRepository.Query()
                .Where(x => model.Name == null | x.Name.Equals(model.Name))
                .Where(x => model.Status == null | x.Status == model.Status)
                .OrderBy(x => x.Name)
                .Skip(model.AmountItem * (model.Page != 0 ? model.Page - 1 : 0))
                .Take(model.Page != 0 ? model.AmountItem : _unitOfWork.VehicleTypeRepository.Query().Count())
                .Select(x => new VehicleTypeViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Status = x.Status
                }).ToListAsync();
            return list;
        }

        public async Task<VehicleTypeViewModel> GetVehicleType(Guid id)
        {
            var value = await _unitOfWork.VehicleTypeRepository.Query()
                .Where(x => x.Id.Equals(id))
                .Select(x => new VehicleTypeViewModel()
                {
                    Id = id,
                    Name = x.Name,
                    Status = x.Status
                }).FirstOrDefaultAsync();
            return value;
        }

        public async Task<Guid> AddVehicleType(VehicleTypeViewModel model)
        {
            var type = new VehicleType()
            {
                Id = model.Id,
                Name = model.Name,
                Status = (int) Status.ENABLE
            };
            var add =  _unitOfWork.VehicleTypeRepository.Query().Add(type);
            await _unitOfWork.SaveChangesAsync();
            return add.Entity.Id;
        }

        public async Task<bool> UpdateVehicleType(Guid id, VehicleTypeViewModel model)
        {
            var entity = _unitOfWork.VehicleTypeRepository.GetById(id).Result;
            
            if (entity == null) return false;
            
            entity.Name = model.Name;
            entity.Status = model.Status;
            _unitOfWork.VehicleTypeRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return true;

        }

     

        public async Task<bool> DeleteVehicleType(Guid id)
        {
            var entityById = _unitOfWork.VehicleTypeRepository.GetById(id).Result;
            if (entityById == null) return false;
            entityById.Status = (int)Status.DISABLE;
            _unitOfWork.VehicleTypeRepository.Update(entityById);
            await _unitOfWork.SaveChangesAsync();
            return true;

        }
    }
}