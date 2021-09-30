using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingYacht.Business.Interfaces.Agency;
using BookingYacht.Business.ViewModels;
using BookingYacht.Data.Interfaces;
using BookingYacht.Data.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BookingYacht.Business.Implement.Agency
{

    class Mapper
    {
        public static DestinationModel MappingToModel(Destination obj)
        {
            return new DestinationModel()
            {
                Address = obj.Address,
                Status = obj.Status
            };
        }

        public static Destination MappingToObject(DestinationModel model)
        {
            return new Destination()
            {
                Address = model.Address,
                Status = model.Status
            };
        }

        public static Destination GetDestinies(Destination des)
        {
            return new Destination()
            {
                Id = des.Id,
                Address = des.Address,
                Status = des.Status
            };
        }
    }
    
    public class DestionationService: BaseService, IDestinationService
    {
        public DestionationService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<List<DestinationModel>> GetAll()
        {
            var destination = await _unitOfWork.DestinationRepository
                .Query()
                .ToListAsync();
            return destination.Select(Mapper.MappingToModel).ToList();
        }

        public async Task<List<Destination>> SearchByAddress(string search)
        {
            var sql = "SELECT Id, Address, Status FROM Destination Where Address Like @search";
            var destinies = await _unitOfWork.DestinationRepository.Query()
                .FromSqlRaw(sql, new SqlParameter("@search", "%" + search + "%"))
                .Select(x => Mapper.GetDestinies(x))
                .ToListAsync();
            // return destinies.Select(Mapper.MappingToModel).ToList();
            return destinies;
        }

        public async Task<List<DestinationModel>> GetDestinationByPlaceType(Guid placetype)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DisableDestination(Guid guid)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddDestination(Guid id, Guid idTypePlace,DestinationModel model)
        {
            throw new NotImplementedException();
        }
    }
}