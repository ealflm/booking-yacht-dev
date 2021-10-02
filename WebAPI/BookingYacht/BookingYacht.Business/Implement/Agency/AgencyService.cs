using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingYacht.Business.Enum;
using BookingYacht.Business.Interfaces.Agency;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using BookingYacht.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookingYacht.Business.Implement.Agency
{
    public class AgencyService : BaseService, IAgencyService
    {
        public AgencyService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async  Task<List<AgencyViewModels>> SearchAgencies(AgencySearchModel model = null)
        {
            model ??= new AgencySearchModel();
            var agency = await _unitOfWork.AgencyRepository.Query()
                .Where(x => model.Id == null | x.Id.Equals(model.Id))
                .Where(x => model.Address == null | x.Address.Equals(model.Address))
                .Where(x => model.Name == null | x.Name.Equals(model.Name))
                .Where(x => model.Phone == null | x.Phone.Equals(model.Phone))
                .Where(x => model.EmailAddress == null | x.EmailAddress.Equals(model.EmailAddress))
                .Where(x => model.Status == null | x.Status == model.Status)
                .Where(x => model.Token == null | x.Token.Equals(model.Token))
                .Select(x => new AgencyViewModels
                {
                    Id = x.Id,
                    Address = x.Address,
                    EmailAddress = x.EmailAddress,
                    Name = x.Name,
                    Phone = x.Phone,
                    Status = x.Status,
                    Token = x.Token
                })
                .ToListAsync();
            return agency;
        }

        public async Task<AgencyViewModels> GetAgency(Guid id)
        {
            var agency = await _unitOfWork.AgencyRepository.Query()
                .Where(x => x.Id.Equals(id))
                .Select(x => new AgencyViewModels
                {
                    Id = x.Id,
                    Address = x.Address,
                    EmailAddress = x.EmailAddress,
                    Name = x.Name,
                    Phone = x.Phone,
                    Status = x.Status,
                    Token = x.Token
                })
                .FirstOrDefaultAsync();
            return agency;
        }

        public async Task<Guid> AddAgency(AgencyViewModels model)
        {
            var agency = new Data.Models.Agency()
            {
                Address = model.Address,
                EmailAddress = model.EmailAddress,
                Name = model.Name,
                Phone = model.Phone,
                Status = (int) Status.ENABLE,
                Token = model.Token
            };
            await _unitOfWork.AgencyRepository.Add(agency);
            await _unitOfWork.SaveChangesAsync();
            return agency.Id;
        }

        public async Task UpdateAgency(Guid id, AgencyViewModels model)
        {
            if (model.Status != null)
            {
                var agency = new Data.Models.Agency()
                {
                    Id = id,
                    Address = model.Address,
                    EmailAddress = model.EmailAddress,
                    Name = model.Name,
                    Phone = model.Phone,
                    Status = (int) model.Status
                };
                _unitOfWork.AgencyRepository.Update(agency);
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAgency(Guid id)
        {
            var first = await _unitOfWork.AgencyRepository.Query()
                .Where(x => x.Id.Equals(id))
                .FirstOrDefaultAsync();
            if (first != null)
            {
                first.Status = (int)Status.DISABLE;
            }

            await _unitOfWork.SaveChangesAsync();
        }
    }

   
}