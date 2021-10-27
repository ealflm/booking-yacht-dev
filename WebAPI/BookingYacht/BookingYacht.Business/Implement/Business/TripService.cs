using BookingYacht.Business.Enum;
using BookingYacht.Business.Interfaces.Business;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using BookingYacht.Data.Interfaces;
using BookingYacht.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingYacht.Business.Implement.Business
{
    public class TripService : BaseService, ITripService
    {
        public TripService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<Guid> AddTrip(TripViewModel model)
        {

            var businessTour = await _unitOfWork.BusinessTourRepository.Query()
                .Where(x => x.IdBusiness.Equals(model.IdBusiness))
                .Where(x => x.IdTour.Equals(model.IdTour))
                .FirstOrDefaultAsync();
            await _unitOfWork.SaveChangesAsync();

            var trip = new Trip()
            {
                Id = model.Id,
                Time= model.Time ,
                IdBusinessTour= businessTour.Id,
                IdVehicle= model.IdVehicle,
                Status = model.Status
            };
            trip.Status = (int)Status.ENABLE;
            await _unitOfWork.TripRepository.Add(trip);
            await _unitOfWork.SaveChangesAsync();
            return trip.Id;
        }

        public async Task DeleteTrip(Guid id)
        {
            var trip = await _unitOfWork.TripRepository.Query()
                .Where(x => x.Id.Equals(id))
                .Select(x => new Trip()
                {
                    Id = x.Id,
                    Time = x.Time,
                    IdBusinessTour = x.IdBusinessTour,
                    IdVehicle = x.IdVehicle,
                    Status = x.Status
                }).FirstOrDefaultAsync();
            trip.Status = (int)Status.DISABLE;
            _unitOfWork.TripRepository.Update(trip);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<TripViewModel> GetTrip(Guid id)
        {
            var trip = await _unitOfWork.TripRepository.Query()
                .Where(x => x.Id.Equals(id))
                .Select(x => new TripViewModel()
                {
                    Id = x.Id,
                    Time = x.Time,
                    IdBusinessTour = x.IdBusinessTour,
                    IdVehicle = x.IdVehicle,
                    Status = x.Status
                }).FirstOrDefaultAsync();
            return trip;
        }

        public async Task<List<TripViewModel>> SearchTrip(TripSearchModel model = null)
        {
            if (model == null)
            {
                model = new TripSearchModel();
            }
            var trips = await _unitOfWork.TripRepository.Query()
                .Where(x => model.IdBusinessTour == null | x.IdBusinessTour.Equals(model.IdBusinessTour))
                .Where(x => model.IdVehicle == null | x.IdVehicle.Equals(model.IdVehicle))
                .Where(x => model.Status == Status.ALL | x.Status == (int)model.Status)
                .Select(x => new TripViewModel()
                {
                    Id = x.Id,
                    Time = x.Time,
                    IdBusinessTour = x.IdBusinessTour,
                    IdVehicle = x.IdVehicle,
                    Status = x.Status
                })
                .OrderBy(x => x.Time)
                .Skip(model.AmountItem * ((model.Page != 0) ? (model.Page - 1) : model.Page))
                .Take((model.Page != 0) ? model.AmountItem : _unitOfWork.TripRepository.Query().Count())
                .ToListAsync();
            return trips;
        }

        public async Task UpdateTrip(Guid id, TripViewModel model)
        {
            var trip = new Trip()
            {
                Id = id,
                Time = model.Time,
                IdBusinessTour = model.IdBusinessTour,
                IdVehicle = model.IdVehicle,
                Status = model.Status
            };
            _unitOfWork.TripRepository.Update(trip);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
