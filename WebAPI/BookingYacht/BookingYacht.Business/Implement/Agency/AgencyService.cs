using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BookingYacht.Business.Interfaces;
using BookingYacht.Business.ViewModels;
using BookingYacht.Data.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BookingYacht.Business.Implement.Agency
{
    public class AgencyService : BaseService, IAgencyService
    {
        public AgencyService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        private static Expression<Func<Data.Models.Agency, AgencyModel>> ToAgencyModel()
        {
            return x => new AgencyModel
            {
                Id = x.Id,
                Address = x.Address,
                EmailAddress =  x.EmailAddress,
                Name = x.Name,
                Phone = x.Phone,
                Status = x.Status
            };
        }
        
        private static void ToAgency(AgencyModel model, Data.Models.Agency agency)
        {
            agency.Name = model.Name;
            agency.Address = model.Address;
            agency.EmailAddress = model.EmailAddress;
            agency.Phone = model.Phone;
            agency.Status = model.Status ?? 0;
        }

        
        
        //Get List Agency
        public async Task<List<AgencyModel>> GetAgency()
        {
            var agencies = await _unitOfWork.AgencyRepository
                .Query()
                .Select(ToAgencyModel()).ToListAsync();
            Console.WriteLine("I'm here");
            agencies.ForEach(x => Console.Write(x.Name));
            return agencies;
        }

        public async Task<List<AgencyModel>> SearchAgenciesString(string search)
        { 
            var sql = $"SELECT Id, Name, Address, EmailAddress, Phone, Status FROM Agency Where CONCAT(Name, Address, EmailAddress) Like @search";
            var agencies = await _unitOfWork.AgencyRepository
                .Query().FromSqlRaw(sql, new SqlParameter("@search", "%" + search + "%"))
                .Select(ToAgencyModel()).ToListAsync();
            return agencies;
        }

        public async Task<AgencyModel> UpdateAgency(Guid id, AgencyModel model)
        {
            var agency = _unitOfWork.AgencyRepository.GetById(id).Result;
            if (agency == null)
            {
                return null;
            }
            ToAgency(model, agency);
            _unitOfWork.AgencyRepository.Update(agency);
            await _unitOfWork.SaveChangesAsync();
            return model;
        }
        
        public async Task<bool> AddAgency(Guid id, AgencyModel model)
        {
            var byId = await _unitOfWork.AgencyRepository.GetById(id);
            if (byId == null)
            {
                
                var temp = new Data.Models.Agency()
                {
                    Name = model.Name,
                    Address = model.Address,
                    EmailAddress = model.EmailAddress,
                    Phone = model.Phone,
                    Status = model.Status ?? 0,
                };  
                Console.WriteLine($"Not null {temp}");
                await _unitOfWork.AgencyRepository.Add(temp);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }

            Console.WriteLine($"null {byId}");
            return false;
        }
        
        //todo replace delete 
        public Task<AgencyModel> UpdateAgencyDisable(string id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAgency(Guid id)
        {
            throw new NotImplementedException();
        }
        
        public Task<List<AgencyModel>> SearchAgency(string model)
        {
            throw new NotImplementedException();
        }

        public Task<List<AgencyModel>> SearchAgency(AgencyModel model)
        {
            throw new NotImplementedException();
        }

    }

   
}