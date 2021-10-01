using System;
using BookingYacht.Business.Interfaces.Admin;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingYacht.Business.ViewModels;
using BookingYacht.Business.SearchModels;
using BookingYacht.Data.Interfaces;
using BookingYacht.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingYacht.Business.Implement.Admin
{
    public class PlaceTypeService : BaseService, IPlaceTypeService
    {

        public PlaceTypeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public async Task<Guid> AddPlaceType(PlaceTypeViewModel model)
        {
            var placeType = new PlaceType()
            {
                Id = model.Id,
                Name = model.Name,
                Status = model.Status
            };
            placeType.Status = (int)Status.ENABLE;
            await _unitOfWork.PlaceTypeRepository.Add(placeType);
            await _unitOfWork.SaveChangesAsync();
            return placeType.Id;
        }

        public async Task DeletePlaceType(Guid id)
        {
            var placeType = await _unitOfWork.PlaceTypeRepository.Query()
                .Where(x => x.Id.Equals(id))
                .Select(x => new PlaceType()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Status = x.Status
                }).FirstOrDefaultAsync();
            placeType.Status = (int)Status.DISABLE;
            _unitOfWork.PlaceTypeRepository.Update(placeType);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<PlaceTypeViewModel> GetPlaceType(Guid id)
        {
            var placeType = await _unitOfWork.PlaceTypeRepository.Query()
                .Where(x => x.Id.Equals(id))
                .Select(x => new PlaceTypeViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Status = x.Status
                }).FirstOrDefaultAsync();
            return placeType;
        }

        public async Task<List<PlaceTypeViewModel>> SearchPlaceTypes(PlaceTypeSearchModel model = null)
        {
            if (model == null)
            {
                model = new PlaceTypeSearchModel();
            }
            var placeType = await _unitOfWork.PlaceTypeRepository.Query()
                .Where(x => model.Name == null | x.Name.Contains(model.Name))
                .Where(x => model.Status == Status.ALL | x.Status == (int)model.Status)
                .Select(x => new PlaceTypeViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Status = x.Status
                })
                .OrderBy(x => x.Name)
                .Skip(model.AmountItem * ((model.Page != 0) ? (model.Page - 1) : model.Page))
                .Take((model.Page != 0) ? model.AmountItem : _unitOfWork.PlaceTypeRepository.Query().Count())
                .ToListAsync();
            return placeType;
        }

        public async Task UpdatePlaceType(Guid id, PlaceTypeViewModel model)
        {
            var placeType = new PlaceType()
            {
                Id = id,
                Name = model.Name,
                Status = model.Status
            };
            placeType.Status = (int)Status.ENABLE;
            _unitOfWork.PlaceTypeRepository.Update(placeType);
            await _unitOfWork.SaveChangesAsync();
        }

    }
}
