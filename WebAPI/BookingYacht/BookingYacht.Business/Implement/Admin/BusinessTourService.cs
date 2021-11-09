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
using BookingYacht.Business.Implement.Business;
using Microsoft.EntityFrameworkCore;

namespace BookingYacht.Business.Implement.Admin
{
    public class BusinessTourService : BaseService, IBusinessTourService
    {
        public BusinessTourService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<List<BusinessTourViewModel>> SearchAgenciesNavigation(BusinessTourSearchModel model = null)
        {
            model ??= new BusinessTourSearchModel();
            var list = await _unitOfWork.Context().BusinessTours
                .Include(x => x.IdTourNavigation)
                .Include(x => x.IdBusinessNavigation)
                .Where(x => model.IdBusiness == null | x.IdBusiness.Equals(model.IdBusiness))
                .Where(x => model.IdTour == null | x.IdTour.Equals(model.IdTour))
                .Where(x => model.Status == null | x.Status == (int)model.Status)
                .Select(x => new BusinessTourViewModel()
                {
                    id = x.Id,
                    idBusiness = x.IdBusiness,
                    idTour = x.IdTour,
                    Status = x.Status,
                    Trips = _unitOfWork.TripRepository.Query()
                        .Where(y => y.IdBusinessTour.Equals(x.Id))
                        .Where(y => model.Time == null | y.Time.Date.Equals(model.Time))
                        .OrderBy(y => y.Time)
                        .Select(y => new TripViewModel()
                        {
                            Id = y.Id,
                            AmountTicket = y.AmountTicket,
                            IdVehicleNavigation = _unitOfWork.VehicleRepository.Query()
                                .Where(z => z.Id.Equals(y.IdVehicle)).FirstOrDefault(),
                            Time = y.Time,
                            Status = y.Status,
                            Orders = _unitOfWork.OrderRepository.Query().Where(z => z.IdTrip.Equals(y.Id)).ToList()
                        })
                        .ToList(),
                    TicketTypes = _unitOfWork.TicketTypeRepository.Query()
                        .Where(y => y.IdBusinessTour.Equals(x.Id))
                        .OrderBy(y => y.Id)
                        .ToList(),
                    IdTourNavigation = _unitOfWork.TourRepository.Query()
                        .Where(y => y.Id.Equals(x.IdTour))
                        .FirstOrDefault(),
                    IdBusinessNavigation = _unitOfWork.BusinessRepository.Query()
                        .Where(y => y.Id.Equals(x.IdBusiness))
                        .FirstOrDefault()
                })
                .OrderBy(x => x.Status)
                .Skip(model.AmountItem * (model.Page != 0 ? model.Page - 1 : 0))
                .Take(model.Page != 0 ? model.AmountItem : _unitOfWork.BusinessTourRepository.Query().Count())
                .ToListAsync();
            for (int i = 0; i < list.Count;)
            {
                if (list[i].IdTourNavigation.Status == (int)Status.DISABLE)
                {
                    list.RemoveAt(i);
                }
                else
                {
                    foreach (TripViewModel trip in list[i].Trips)
                    {
                        trip.IdVehicleNavigation.IdBusinessNavigation = null;
                    }

                    i++;
                }
            }

            return list;
        }


        public async Task<List<BusinessTourViewModel>> SearchBusinessTourForAgency(BusinessTourSearchModel model = null)
        {
            model ??= new BusinessTourSearchModel();
            var list = await _unitOfWork.Context().BusinessTours
                .Where(x => model.IdBusiness == null | x.IdBusiness.Equals(model.IdBusiness))
                .Where(x => model.IdTour == null | x.IdTour.Equals(model.IdTour))
                .Where(x => model.Status == null | x.Status == (int)model.Status)
                .Select(x => new BusinessTourViewModel()
                {
                    id = x.Id,
                    idBusiness = x.IdBusiness,
                    idTour = x.IdTour,
                    Status = x.Status,
                    Trips = _unitOfWork.TripRepository.Query()
                        .Where(y => y.IdBusinessTour.Equals(x.Id))
                        .Where(y => y.Time.Date.CompareTo(DateTime.Now.Date) > 0)
                        .OrderBy(y => y.Time)
                        .Select(y => new TripViewModel()
                        {
                            Id = y.Id,
                            AmountTicket = y.AmountTicket,
                            IdVehicleNavigation = _unitOfWork.VehicleRepository.Query()
                                .Include(z => z.IdVehicleTypeNavigation).Where(z => z.Id.Equals(y.IdVehicle))
                                .FirstOrDefault(),
                            Time = y.Time,
                            Status = y.Status,
                        })
                        .ToList(),
                    TicketTypes = _unitOfWork.TicketTypeRepository.Query()
                        .Where(y => y.IdBusinessTour.Equals(x.Id))
                        .OrderBy(y => y.Id)
                        .ToList(),
                    IdTourNavigation = _unitOfWork.TourRepository.Query()
                        .Where(y => y.Id.Equals(x.IdTour))
                        .FirstOrDefault(),
                    IdBusinessNavigation = _unitOfWork.BusinessRepository.Query()
                        .Where(y => y.Id.Equals(x.IdBusiness))
                        .FirstOrDefault()
                })
                .OrderBy(x => x.Status)
                .Skip(model.AmountItem * (model.Page != 0 ? model.Page - 1 : 0))
                .Take(model.Page != 0 ? model.AmountItem : _unitOfWork.BusinessTourRepository.Query().Count())
                .ToListAsync();
            for (int i = 0; i < list.Count;)
            {
                if ((model.Query == null || list[i].IdTourNavigation.Title.Contains(model.Query) ||
                     list[i].IdTourNavigation.Descriptions.Contains(model.Query)) && list[i].Trips.Count > 0)
                {
                    foreach (TripViewModel trip in list[i].Trips)
                    {
                        trip.IdVehicleNavigation.IdBusinessNavigation = null;
                    }

                    i++;
                }
                else
                {
                    list.RemoveAt(i);
                }
            }

            return list;
        }


        public async Task<BusinessTour> GetBusinessTourNavigation(Guid id)
        {
            return await _unitOfWork.Context().BusinessTours
                .Include(x => x.IdBusinessNavigation)
                .Include(x => x.IdTourNavigation)
                .Where(x => x.Id.Equals(id))
                .FirstOrDefaultAsync();
        }

        public async Task<Guid> AddBusinessTour(BusinessTourViewModel model)
        {
            var businessTour = _unitOfWork.BusinessTourRepository.Query().Add(new BusinessTour()
            {
                Id = model.id,
                IdBusiness = model.idBusiness,
                IdTour = model.idTour,
                Status = model.Status
            });
            await _unitOfWork.SaveChangesAsync();
            return businessTour.Entity.Id;
        }

        public async Task<bool> UpdateBusinessTour(Guid id, BusinessTourViewModel model)
        {
            var result = await _unitOfWork.BusinessTourRepository.Query()
                .Where(x => x.Id.Equals(id))
                .FirstOrDefaultAsync();
            if (result == null)
            {
                return false;
            }

            result.Status = model.Status;
            result.IdBusiness = model.idBusiness;
            result.IdTour = model.idTour;
            _unitOfWork.BusinessTourRepository.Update(result);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBusinessTour(Guid id)
        {
            var result = await _unitOfWork.BusinessTourRepository.Query()
                .Where(x => x.Id.Equals(id))
                .FirstOrDefaultAsync();
            if (result == null)
            {
                return false;
            }

            result.Status = (int)Status.DISABLE;
            _unitOfWork.BusinessTourRepository.Update(result);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}