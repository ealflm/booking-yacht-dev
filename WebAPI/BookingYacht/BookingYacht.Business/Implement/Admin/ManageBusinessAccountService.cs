using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using BookingYacht.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingYacht.Business.Enum;

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
                PhoneNumber = model.PhoneNumber,
                Status = model.Status,
                VnpTmnCode= "104S9O6F",
                VnpHashSecret= "WAIHCILWTDOAERGSTKMUYIRDGOCROIHW"
            };
            business.Status = (int)Status.ENABLE;
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
                    PhoneNumber = x.PhoneNumber,
                    Status = x.Status,
                    VnpTmnCode = x.VnpTmnCode,
                    VnpHashSecret = x.VnpHashSecret
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
                    PhoneNumber = x.PhoneNumber,
                    Status = x.Status,
                    VnpTmnCode= x.VnpTmnCode,
                    VnpHashSecret=x.VnpHashSecret
                }).FirstOrDefaultAsync();
            return business;
        }

        public async Task<List<BusinessViewModel>> SearchBusinesses(BusinessSearchModel model=null)
        {
            if(model== null)
            {
                model = new BusinessSearchModel();
            }
            var businesses = await _unitOfWork.BusinessRepository.Query()
                .Where(x => model.Name == null | x.Name.Contains(model.Name))
                .Where(x => model.PhoneNumber == null | x.PhoneNumber.Contains(model.PhoneNumber))
                .Where(x => model.Address == null | x.Address.Contains(model.Address))
                .Where(x => model.EmailAddress == null | x.EmailAddress.Contains(model.EmailAddress))
                .Where(x => model.Status==Status.ALL|x.Status == (int)model.Status)
                .Select(x => new BusinessViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    EmailAddress = x.EmailAddress,
                    PhoneNumber = x.PhoneNumber,
                    Status = x.Status
                })
                .OrderBy(x => x.Name)
                .Skip(model.AmountItem * ((model.Page!=0)?(model.Page-1):model.Page))
                .Take((model.Page!=0)? model.AmountItem: _unitOfWork.BusinessRepository.Query().Count())
                .ToListAsync();
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
                PhoneNumber = model.PhoneNumber,
                Status = model.Status,
                VnpTmnCode= "104S9O6F",
                VnpHashSecret= "WAIHCILWTDOAERGSTKMUYIRDGOCROIHW"
            };
            _unitOfWork.BusinessRepository.Update(business);
            await _unitOfWork.SaveChangesAsync();
        }
        
      
    }
}
