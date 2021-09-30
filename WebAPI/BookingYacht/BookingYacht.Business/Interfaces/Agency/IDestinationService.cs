using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingYacht.Business.ViewModels;
using BookingYacht.Data.Models;

namespace BookingYacht.Business.Interfaces.Agency
{
    public interface IDestinationService
    {
        public Task<List<DestinationModel>> GetAll();
        
        public Task<List<Destination>> SearchByAddress(string search);
        
        public Task<List<DestinationModel>> GetDestinationByPlaceType (Guid placetype);
        
        public Task<bool> DisableDestination(Guid guid);

        public Task<bool> AddDestination(Guid id, Guid placetype, DestinationModel model);


    }
}