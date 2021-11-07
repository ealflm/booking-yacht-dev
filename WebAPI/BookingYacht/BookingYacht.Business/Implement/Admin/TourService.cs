using System;
using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using BookingYacht.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using BookingYacht.Data.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingYacht.Business.Enum;

namespace BookingYacht.Business.Implement.Admin
{
    public class TourService : BaseService, ITourService
    {

        public TourService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public async Task<Guid> AddTour(TourViewModel model)
        {
            var tour = new Tour()
            {
                Id = model.Id,
                Title= model.Title,
                Descriptions= model.Descriptions,
                Status = model.Status,
                ImageLink= model.ImageLink
            };
            tour.Status = (int)Status.ENABLE;
            await _unitOfWork.TourRepository.Add(tour);
            await _unitOfWork.SaveChangesAsync();
            return tour.Id;
        }

        public async Task DeleteTour(Guid id)
        {
            var tour = await _unitOfWork.TourRepository.Query()
                .Where(x => x.Id.Equals(id))
                .Select(x => new Tour()
                {
                    Id = x.Id,
                    Title=x.Title,
                    Descriptions= x.Descriptions,
                    Status = x.Status,
                    ImageLink= x.ImageLink
                }).FirstOrDefaultAsync();
            tour.Status = (int)Status.DISABLE;
            _unitOfWork.TourRepository.Update(tour);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<TourViewModel> GetTour(Guid id)
        {
            var tour = await _unitOfWork.TourRepository.Query()
                .Where(x => x.Id.Equals(id))
                .Select(x => new TourViewModel()
                {
                    Id = x.Id,
                    Title= x.Title,
                    Descriptions= x.Descriptions,
                    Status = x.Status,
                    ImageLink= x.ImageLink,
                    DestinationTours= _unitOfWork.DestinationTourRepository.Query()
                .Where(y =>  y.IdTour.Equals(x.Id))
                .Select(y => new DestinationTourViewModel()
                {
                    Id = y.Id,
                    IdDestination = y.IdDestination,
                    IdTour = y.IdTour,
                    Order = y.Order,
                    Destination= _unitOfWork.DestinationRepository.Query()
                .Where(z => z.Id.Equals(y.IdDestination))
               .FirstOrDefault()
        })
                .OrderBy(x => x.Order)
                .ToList()
        }).FirstOrDefaultAsync();
            foreach(DestinationTourViewModel destinationTour in tour.DestinationTours)
            {
                destinationTour.PlaceType = _unitOfWork.PlaceTypeRepository.GetById(destinationTour.Destination.IdPlaceType).Result.Name;
            }
            return tour;
        }

        public async Task<List<TourViewModel>> SearchTours(TourSearchModel model = null)
        {
            if (model == null)
            {
                model = new TourSearchModel();
            }
            var tours = await _unitOfWork.TourRepository.Query()
                .Where(x => model.Title == null | x.Title.Contains(model.Title) || x.Descriptions.Contains(model.Descriptions))
                .Where(x => model.Status == Status.ALL | x.Status == (int)model.Status)
                .Select(x => new TourViewModel()
                {
                    Id = x.Id,
                    Title= x.Title,
                    Descriptions= x.Descriptions,
                    Status = x.Status,
                    ImageLink= x.ImageLink
                })
                .OrderBy(x => x.Title)
                .Skip(model.AmountItem * ((model.Page != 0) ? (model.Page - 1) : model.Page))

                .Take((model.Page != 0) ? model.AmountItem : _unitOfWork.TourRepository.Query()
                    .Count())
                .ToListAsync();
            return tours;
        }

        public async Task UpdateTour(Guid id, TourViewModel model)
        {
            var tour = new Tour()
            {
                Id = id,
                Title= model.Title,
                Descriptions= model.Descriptions,
                Status = model.Status,
                ImageLink= model.ImageLink
            };
            _unitOfWork.TourRepository.Update(tour);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
