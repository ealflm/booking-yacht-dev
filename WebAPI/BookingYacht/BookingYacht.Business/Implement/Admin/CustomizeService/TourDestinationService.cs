using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using BookingYacht.Business.Enum;
using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.Interfaces.Admin.CustomizeServices;
using BookingYacht.Business.SearchModels.CustomizeSearchModels;
using BookingYacht.Business.ViewModels.CustomModels;
using BookingYacht.Data.Interfaces;
using BookingYacht.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;

namespace BookingYacht.Business.Implement.Admin.CustomizeService
{
    public class TourDestinationService : BaseService, ITourDestinationService
    {
        public TourDestinationService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<TourDestinationListViewModel> GetDestinationByTour(Guid idTour)
        {
            var list = await _unitOfWork.Context().DestinationTours
                .Where(x => x.IdTour.Equals(idTour))
                .Join(_unitOfWork.Context().Destinations,
                    x => x.IdDestination,
                    y => y.Id,
                    (x, y) => new TourDestinationViewModel()
                    {
                        Id = y.Id,
                        Address = y.Address,
                        Location = y.Location,
                        Name = y.Name,
                        Status = y.Status,
                        Order = x.Order
                    })
                .OrderBy(tour => tour.Order)
                .ToListAsync();
            var result = new TourDestinationListViewModel()
            {
                IdTour =  idTour,
                Destination =  list
            };
            return result;
        }

        
        public async Task<bool> UpdateDestinationByTour(TourDestinationListSearchModel model = null)
        {
            if (model?.Destinations == null)
            {
                return false;
            }
            await DeleteTourDestinationList(model);

            var res = await AddNewDestinationList(model);

            return res;
        }

        /**
         * add new Tour Destination by model SearchModel [idTour, List<idDestination>]
         */
        private async Task<bool> AddNewDestinationList(TourDestinationListSearchModel model)
        {
            var pos = 0;
            foreach (var idDestinations in model.Destinations)
            {
                var des = await _unitOfWork.DestinationRepository.GetById(idDestinations);
                if (des == null)
                {
                    return false;
                }

                await _unitOfWork.DestinationTourRepository.Add(new DestinationTour()
                {
                    IdTour = model.IdTour,
                    IdDestination = idDestinations,
                    Order = pos
                });
                await _unitOfWork.SaveChangesAsync();
                pos++;
            }

            return true;
        }
        /**
         * Delete ALl Destination Tour has model.idTour
         * Else (list[contain idTour] == null) return 
         */
        private async Task DeleteTourDestinationList(TourDestinationListSearchModel model)
        {
            var list = await _unitOfWork.DestinationTourRepository.Query()
                .Where(x => x.IdTour.Equals(model.IdTour))
                .Select(x => x.Id)
                .ToListAsync();
            if (list != null)
            {
                foreach (var idDestinationTour in list)
                {
                    await _unitOfWork.DestinationTourRepository.Remove(idDestinationTour);
                    await _unitOfWork.SaveChangesAsync();
                }
            }

        }
    }
}