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
    internal static partial class Mapper
    {
        public static async Task<AgencyViewModels> CreateEntity(Agency x)
        {
            return await Task.Run(() => new AgencyViewModels
            {
                Id = x.Id,
                Address = x.Address,
                EmailAddress = x.EmailAddress,
                Name = x.Name,
                PhoneNumber = x.PhoneNumber,
                Status = x.Status,
            });
        }
        public static async Task<Agency> CreateNewEntity(AgencyViewModels model)
        {
            return await Task.Run(() =>
                new Agency
                {
                    Address = model.Address,
                    EmailAddress = model.EmailAddress,
                    Name = model.Name,
                    PhoneNumber = model.PhoneNumber,
                    Status = (int) Status.ENABLE,
                }
            );
        }
        
        public static async Task<Agency> ModelToEntity(Guid id, AgencyViewModels model)
        {
            return await Task.Run(() =>
                new Agency
                {
                    Id = id,
                    Address = model.Address,
                    EmailAddress = model.EmailAddress,
                    Name = model.Name,
                    PhoneNumber = model.PhoneNumber,
                    Status = model.Status ?? 0
                }
            );
        }

        
    }
    public class AgencyService : BaseService, IAgencyService
    {
        private const int Count = (int) CountElement.AtLeast;
        public AgencyService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async  Task<List<AgencyViewModels>> SearchAgencies(AgencySearchModel model = null)
        {
            model ??= new AgencySearchModel();
            var agency = await _unitOfWork.AgencyRepository.Query()
                .Where(x => model.Id == null | x.Id.Equals(model.Id))
                .Where(x => model.Address == null | x.Address.Contains(model.Address))
                .Where(x => model.Name == null | x.Name.Contains(model.Name))
                .Where(x => model.PhoneNumber == null | x.PhoneNumber.Contains(model.PhoneNumber))
                .Where(x => model.EmailAddress == null | x.EmailAddress.Contains(model.EmailAddress))
                .Where(x => model.Status == null | x.Status == model.Status)
                .OrderBy(x => x.Name)
                .Skip(Count * (model.Paging != 0 ? model.Paging - 1 : 0))
                .Take(model.Paging != 0 ? Count : _unitOfWork.AgencyRepository.Query().Count())
                .Select(x => Mapper.CreateEntity(x).Result)
                .ToListAsync();
            return agency;
        }

        public async Task<AgencyViewModels> GetAgency(Guid id)
        {
            var agency = await _unitOfWork.AgencyRepository.Query()
                .Where(x => x.Id.Equals(id))
                .Select(x => Mapper.CreateEntity(x).Result)
                .FirstOrDefaultAsync();
            return agency;
        }

        public async Task<Guid> AddAgency(AgencyViewModels model)
        {
            var newEntity = Mapper.CreateNewEntity(model);
            var viewModel = _unitOfWork.AgencyRepository.Query().Add(newEntity.Result);
            await _unitOfWork.SaveChangesAsync();
            return viewModel.Entity.Id;
        }

        public async Task UpdateAgency(Guid id, AgencyViewModels model)
        {
            var entity = Mapper.ModelToEntity(id, model);
            _unitOfWork.AgencyRepository.Update(entity.Result);
            await _unitOfWork.SaveChangesAsync();
        }
        
        public async Task<bool> DeleteAgency(Guid id)
        {
            var first = await _unitOfWork.AgencyRepository.Query()
                .Where(x => x.Id.Equals(id))
                .FirstOrDefaultAsync();
            if (first is null) return false;
            
            first.Status = (int) Status.DISABLE;
            await _unitOfWork.SaveChangesAsync();
            
            return first.Status == 2;
        }
    }

   
}