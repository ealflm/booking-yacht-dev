using System;
using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using BookingYacht.Data.Interfaces;
using BookingYacht.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingYacht.Business.Implement.Admin
{
    public class DestinationTourService : BaseService, IDestinationTourService
    {
        public DestinationTourService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public async Task<Guid> AddDestinationTour(DestinationTourViewModel model)
        {
            var destinationTour = new DestinationTour()
            {
                Id = model.Id,
                IdPier= model.IdPier,
                IdTour= model.IdTour,
                Status = model.Status
            };
            destinationTour.Status = (int)Status.ENABLE;
            await _unitOfWork.DestinationTourRepository.Add(destinationTour);
            await _unitOfWork.SaveChangesAsync();
            return destinationTour.Id;
        }

        public async Task DeleteDestinationTour(Guid id)
        {
            var destinationTour = await _unitOfWork.DestinationTourRepository.Query()
                .Where(x => x.Id.Equals(id))
                .Select(x => new DestinationTour()
                {
                    Id = x.Id,
                    IdPier = x.IdPier,
                    IdTour = x.IdTour,
                    Status = x.Status
                }).FirstOrDefaultAsync();
            destinationTour.Status = (int)Status.DISABLE;
            _unitOfWork.DestinationTourRepository.Update(destinationTour);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<DestinationTourViewModel> GetDestinationTour(Guid id)
        {
            var destinationTour = await _unitOfWork.DestinationTourRepository.Query()
                .Where(x => x.Id.Equals(id))
                .Select(x => new DestinationTourViewModel()
                {
                    Id = x.Id,
                    IdPier = x.IdPier,
                    IdTour = x.IdTour,
                    Status = x.Status
                }).FirstOrDefaultAsync();
            return destinationTour;
        }

        public async Task<List<DestinationTourViewModel>> SearchDestinationTours(DestinationTourSearchModel model = null)
        {
            if (model == null)
            {
                model = new DestinationTourSearchModel();
            }
            var destinationTour = await _unitOfWork.DestinationTourRepository.Query()
                .Where(x => model.IdPier == null | x.IdPier.Equals(model.IdPier))
                .Where(x => model.IdTour == null | x.IdTour.Equals(model.IdTour))
                .Where(x => model.Status == Status.ALL | x.Status == (int)model.Status)
                .Select(x => new DestinationTourViewModel()
                {
                    Id = x.Id,
                    IdPier = x.IdPier,
                    IdTour = x.IdTour,
                    Status = x.Status
                })
                .OrderBy(x => x.Id)
                .Skip(model.AmountItem * ((model.Page != 0) ? (model.Page - 1) : model.Page))
                .Take((model.Page != 0) ? model.AmountItem : _unitOfWork.DestinationTourRepository.Query().Count())
                .ToListAsync();
            return destinationTour;
        }

        public async Task UpdateDestinationTour(Guid id, DestinationTourViewModel model)
        {
            var destinationTour = new DestinationTour()
            {
                Id = id,
                IdPier = model.IdPier,
                IdTour = model.IdTour,
                Status = model.Status
            };
            _unitOfWork.DestinationTourRepository.Update(destinationTour);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
