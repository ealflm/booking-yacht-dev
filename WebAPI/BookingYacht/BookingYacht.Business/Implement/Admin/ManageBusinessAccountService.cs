using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using BookingYacht.Data.Interfaces;
using BookingYacht.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingYacht.Business.Implement.Admin
{
    public class ManageBusinessAccountService : BaseService, IManageBusinessAccountService
    {
        public ManageBusinessAccountService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<Guid> AddBusiness(BusinessViewModel model)
        {
            var business = new Data.Models.Business()
            {
                Id = model.Id,
                Name = model.Name,
                Address = model.Address,
                EmailAddress = model.EmailAddress,
                Phone = model.Phone,
                Status = model.Status
            };
            await _unitOfWork.BusinessRepository.Add(business);
            await _unitOfWork.SaveChangesAsync();
            return business.Id;
        }

        public async Task DeleteBusiness(Guid id)
        {
            var business = await _unitOfWork.BusinessRepository.Query()
                .Where(x => x.Id.Equals(id))
                .Select(x => new Data.Models.Business()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    EmailAddress = x.EmailAddress,
                    Phone = x.Phone,
                    Status = x.Status
                }).FirstOrDefaultAsync();
            business.Status =(int) Status.DISABLE;
            _unitOfWork.BusinessRepository.Update(business);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<BusinessViewModel> GetBusiness(Guid id)
        {
            var business = await _unitOfWork.BusinessRepository.Query()
                .Where(x=> x.Id.Equals(id))
                .Select(x => new BusinessViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    EmailAddress = x.EmailAddress,
                    Phone = x.Phone,
                    Status = x.Status
                }).FirstOrDefaultAsync();
            return business;
        }

        public async Task<List<BusinessViewModel>> GetBusinesses()
        {
            var businesses = await _unitOfWork.BusinessRepository.Query().Select(x => new BusinessViewModel()
            {
                Id = x.Id, Name= x.Name, Address= x.Address, EmailAddress=x.EmailAddress, Phone= x.Phone, Status= x.Status
            }).ToListAsync();
            return businesses;
        }

        public async Task<List<BusinessViewModel>> SearchBusinessed(BusinessSearchModel model=null)
        {
            if(model== null)
            {
                model = new BusinessSearchModel();
            }
            var businesses = await _unitOfWork.BusinessRepository.Query()
                .Where(x => model.Name == null | x.Name.Contains(model.Name))
                .Where(x => model.Phone == null | x.Phone.Contains(model.Phone))
                .Where(x => model.Address == null | x.Address.Contains(model.Address))
                .Where(x => model.EmailAddress == null | x.EmailAddress.Contains(model.EmailAddress))
                .Where(x => model.Status==Status.ALL|x.Status == (int)model.Status)
                .Select(x => new BusinessViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    EmailAddress = x.EmailAddress,
                    Phone = x.Phone,
                    Status = x.Status
                })
                .Skip(model.AmountItem * ((model.Page!=0)?(model.Page-1):model.Page))
                .Take((model.Page!=0)? model.AmountItem: _unitOfWork.BusinessRepository.Query().Count())
                .OrderBy(x=> x.Name).ToListAsync();
            return businesses;
        }

        public async Task UpdateBusiness(Guid id, BusinessViewModel model)
        {
            var business = new Data.Models.Business()
            {
                Id = id,
                Name = model.Name,
                Address = model.Address,
                EmailAddress = model.EmailAddress,
                Phone = model.Phone,
                Status = model.Status
            };
            _unitOfWork.BusinessRepository.Update(business);
            await _unitOfWork.SaveChangesAsync();
        }
        
      
    }
}
