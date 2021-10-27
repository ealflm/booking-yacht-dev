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
using BookingYacht.Business.Enum;

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
                IdDestination = model.IdDestination,
                IdTour = model.IdTour,
                Order = model.Order
            };
            await _unitOfWork.DestinationTourRepository.Add(destinationTour);
            await _unitOfWork.SaveChangesAsync();
            return destinationTour.Id;
        }

        public async Task DeleteDestinationTour(Guid id)
        {
            var destinationTour = await  _unitOfWork.DestinationTourRepository.Query()
                .Where(x => x.Id.Equals(id))
                .Select(x => new DestinationTourViewModel()
                {
                    Id = x.Id,
                    IdDestination = x.IdDestination,
                    IdTour = x.IdTour,
                    Order = x.Order
                }).FirstOrDefaultAsync();
            var destinationTours = await _unitOfWork.DestinationTourRepository.Query()
                .Where(x => x.IdTour.Equals(destinationTour.IdTour))
                .OrderBy(x => x.Id)
                .ToListAsync();
            foreach(DestinationTour destinationTourItem in destinationTours)
            {
                if (destinationTourItem.Order > destinationTour.Order)
                {
                    destinationTourItem.Order--;
                    _unitOfWork.DestinationTourRepository.Update(destinationTourItem);
                }
            }
            await _unitOfWork.DestinationTourRepository.Remove(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<DestinationTourViewModel> GetDestinationTour(Guid id)
        {
            var destinationTour = await _unitOfWork.DestinationTourRepository.Query()
                .Where(x => x.Id.Equals(id))
                .Select(x => new DestinationTourViewModel()
                {
                    Id = x.Id,
                    IdDestination = x.IdDestination,
                    IdTour = x.IdTour,
                    Order=x.Order
                }).FirstOrDefaultAsync();
            return destinationTour;
        }

        public async Task<DestinationTour> GetDestinationTourNavigation(Guid id)
        {
            var destinationTour = await _unitOfWork.Context().DestinationTours
                .Include(x => x.IdDestinationNavigation)
                .Include(x => x.IdTourNavigation)
                .Where(x => x.Id.Equals(id))
                .FirstOrDefaultAsync();
            return destinationTour;
        }



        public async Task<List<DestinationTourViewModel>> SearchDestinationTours(DestinationTourSearchModel model = null)
        {
            if (model == null)
            {
                model = new DestinationTourSearchModel();
            }
            var destinationTour = await _unitOfWork.DestinationTourRepository.Query()
                .Where(x => model.IdDestination == null | x.IdDestination.Equals(model.IdDestination))
                .Where(x => model.IdTour == null | x.IdTour.Equals(model.IdTour))
                .Where(x => model.Order == 0 | x.Order == model.Order)
                .Select(x => new DestinationTourViewModel()
                {
                    Id = x.Id,
                    IdDestination = x.IdDestination,
                    IdTour = x.IdTour,
                    Order= x.Order
                })
                .OrderBy(x => x.Order)
                .Skip(model.AmountItem * ((model.Page != 0) ? (model.Page - 1) : model.Page))
                .Take((model.Page != 0) ? model.AmountItem : _unitOfWork.DestinationTourRepository.Query().Count())
                .ToListAsync();
            return destinationTour;
        }

        public async Task<List<DestinationTour>> SearchDestinationToursNavigation(DestinationTourSearchModel model = null)
        {
            if (model == null)
            {
                model = new DestinationTourSearchModel();
            }
            var destinationTour = await _unitOfWork.Context().DestinationTours
                .Include(x => x.IdDestinationNavigation)
                .Include(x => x.IdTourNavigation)
                .Where(x => model.IdDestination == null | x.IdDestination.Equals(model.IdDestination))
                .Where(x => model.IdTour == null | x.IdTour.Equals(model.IdTour))
                .Where(x => model.Order == 0 | x.Order == model.Order)
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
                IdDestination = model.IdDestination,
                IdTour = model.IdTour,
                Order = model.Order
            };
            _unitOfWork.DestinationTourRepository.Update(destinationTour);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
