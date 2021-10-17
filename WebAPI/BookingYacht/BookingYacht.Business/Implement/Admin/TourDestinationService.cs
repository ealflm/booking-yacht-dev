using System;
using System.Linq;
using System.Threading.Tasks;
using BookingYacht.Business.Enum;
using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.ViewModels;
using BookingYacht.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookingYacht.Business.Implement.Admin
{
    public class TourDestinationService : BaseService, ITourDestination
    {
        
        public TourDestinationService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        
        public async Task<TourDestinationListViewModel> GetDestinationByTour(Guid id)
        {
            //TODO GET TOUR HAS GUID IN-COME
            var result = await _unitOfWork.TourRepository
                .Query()
                .Where(x => x.Id.Equals(id))
                .FirstOrDefaultAsync();

            //TODO RETURN NULL IF TOUR IS NULL
            if (result == null) return null;
            
            //TODO NEW TOUR-DESTINATION-LIST.CLASS
            var tdl = new TourDestinationListViewModel() {IdTour = result.Id, Status = (Status) result.Status};
            
            //TODO GET LIST ID-DESTINATION:GUID TOUR-DESTINATION BY ID TOUR
            var listDt = await _unitOfWork.DestinationTourRepository
                .Query().Where(x => x.IdTour.Equals(tdl.IdTour))
                .Select(x => x.IdDestination)
                .ToListAsync();
            
            //TODO GET DESTINATION FROM LIST<ID-DESTINATION-TOUR>
            foreach (var guid in listDt)
            {
                var destination = await _unitOfWork.DestinationRepository.Query()
                    .Where(x => x.Id.Equals(guid))
                    .Select(x => new DestinyViewModel()
                    {
                        Id = x.Id,
                        Address = x.Address,
                        IdPlaceType = x.IdPlaceType,
                        Name = x.Name,
                        Status = x.Status
                    })
                    .FirstAsync();
                if (destination != null)
                {
                    tdl.Destination.Add(destination);
                }
            }

            return tdl;
        }

    }
}