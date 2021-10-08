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
    public class DestinationService : BaseService, IDestinationService
    {
        private const int Count = (int) CountElement.AtLeast;
        
        public async Task<Guid> AddDestiny(DestinyViewModel model)
        {
            var destiny = new Data.Models.Destination
            {
                Id = model.Id,
                Name = model.Name,
                Address = model.Address,
                IdPlaceType = model.IdPlaceType,
                Status = (int) Status.ENABLE
            };
            await _unitOfWork.DestinationRepository.Add(destiny);
            await _unitOfWork.SaveChangesAsync();
            return destiny.Id;
        }

        public async Task DeleteDestiny(Guid id)
        {
            var destiny = await _unitOfWork.DestinationRepository.Query()
                .Where(x => x.Id.Equals(id))
                .Select(x => new Data.Models.Destination()
                {
                    Id = x.Id,
                    Address = x.Address,
                    Status = x.Status,
                    IdPlaceType =  x.IdPlaceType
                }).FirstOrDefaultAsync();
            destiny.Status =(int) Status.DISABLE;
            _unitOfWork.DestinationRepository.Update(destiny);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<DestinyViewModel> GetDestiny(Guid id)
        {
            var destiny = await _unitOfWork.DestinationRepository.Query()
                .Where(x=> x.Id.Equals(id))
                .Select(x => new DestinyViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    Status = x.Status,
                    IdPlaceType = x.IdPlaceType
                }).FirstOrDefaultAsync();
            return destiny;
        }

        public async Task<Destination> GetDestinyNavigation(Guid id)
        {
            var destiny = await _unitOfWork.Context().Destinations
                .Include(x => x.IdPlaceTypeNavigation)
                .Where(x => x.Id.Equals(id))
                .FirstOrDefaultAsync();
            return destiny;
        }

        public async Task<List<DestinyViewModel>> SearchDestinies(DestinySearchModel model=null)
        {
            model ??= new DestinySearchModel();
            var destiny = await _unitOfWork.DestinationRepository.Query()
                .Where(x => model.Address == null | x.Address.Contains(model.Address))
                .Where(x => model.Name == null | x.Name.Contains(model.Name))
                .Where(x => model.Status == null |x.Status == model.Status)
                .Where(x => model.IdPlaceType == null | x.IdPlaceType == model.IdPlaceType)
                .OrderBy(x => x.Address)
                .Skip(model.AmountItem * (model.Page != 0 ? model.Page - 1 : 0))
                .Take(model.Page != 0 ? model.AmountItem : _unitOfWork.DestinationRepository.Query().Count())
                .Select(x => new DestinyViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    Status = x.Status,
                    IdPlaceType = x.IdPlaceType
                })
                .OrderBy(x => x.Address)
                .ToListAsync();
            return destiny;
        }

        public async Task<List<Destination>> SearchDestiniesNavigation(DestinySearchModel model = null)
        {
            model ??= new DestinySearchModel();
            var destiny = await _unitOfWork.Context().Destinations
                .Include(x => x.IdPlaceTypeNavigation)
                .Where(x => model.Address == null | x.Address.Contains(model.Address))
                .Where(x => model.Name == null | x.Name.Contains(model.Name))
                .Where(x => model.Status == null | x.Status == model.Status)
                .Where(x => model.IdPlaceType == null | x.IdPlaceType == model.IdPlaceType)
                .OrderBy(x => x.Address)
                .Skip(model.AmountItem * (model.Page != 0 ? model.Page - 1 : 0))
                .Take(model.Page != 0 ? model.AmountItem : _unitOfWork.DestinationRepository.Query().Count())
                .OrderBy(x => x.Address)
                .ToListAsync();
            return destiny;
        }

        public async Task UpdateDestiny(Guid id, DestinyViewModel model)
        {
            var destiny = new Data.Models.Destination()
            {
                Id = id,
                Name = model.Name,
                Address = model.Address,
                Status = model.Status,
                IdPlaceType = model.IdPlaceType
            };
            _unitOfWork.DestinationRepository.Update(destiny);
            await _unitOfWork.SaveChangesAsync();
        }

        public DestinationService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}