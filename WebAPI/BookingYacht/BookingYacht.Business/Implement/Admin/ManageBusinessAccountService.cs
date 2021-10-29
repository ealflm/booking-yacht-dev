﻿using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using BookingYacht.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingYacht.Business.Enum;
using BookingYacht.Business.PaymentModels;
using BookingYacht.Data.Models;

namespace BookingYacht.Business.Implement.Admin
{
    public class ManageBusinessAccountService : BaseService, IManageBusinessAccountService
    {
        public ManageBusinessAccountService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<Guid> AddBusiness(BusinessViewModel model)
        {
            var business = new Data.Models.Business()
            {
                Id = model.Id,
                Name = model.Name,
                Address = model.Address,
                EmailAddress = model.EmailAddress,
                PhoneNumber = model.PhoneNumber,
                Status = model.Status,
                VnpTmnCode= "104S9O6F",
                VnpHashSecret= "WAIHCILWTDOAERGSTKMUYIRDGOCROIHW"
            };
            business.Status = (int)Status.ENABLE;
            await _unitOfWork.BusinessRepository.Add(business);
            await _unitOfWork.SaveChangesAsync();
            return business.Id;
        }

        public async Task DeleteBusiness(Guid id)
        {
            var business = await _unitOfWork.BusinessRepository.Query()
                .Where(x => x.Id.Equals(id))
                .Select(x => new Data.Models.Business()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    EmailAddress = x.EmailAddress,
                    PhoneNumber = x.PhoneNumber,
                    Status = x.Status,
                    VnpTmnCode = x.VnpTmnCode,
                    VnpHashSecret = x.VnpHashSecret
                }).FirstOrDefaultAsync();
            business.Status =(int) Status.DISABLE;
            _unitOfWork.BusinessRepository.Update(business);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<BusinessViewModel> GetBusiness(Guid id)
        {
            var business = await _unitOfWork.BusinessRepository.Query()
                .Where(x=> x.Id.Equals(id))
                .Select(x => new BusinessViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    EmailAddress = x.EmailAddress,
                    PhoneNumber = x.PhoneNumber,
                    Status = x.Status,
                    VnpTmnCode= x.VnpTmnCode,
                    VnpHashSecret=x.VnpHashSecret
                }).FirstOrDefaultAsync();
            return business;
        }

        public async Task<List<BusinessViewModel>> SearchBusinesses(BusinessSearchModel model=null)
        {
            if(model== null)
            {
                model = new BusinessSearchModel();
            }
            var businesses = await _unitOfWork.BusinessRepository.Query()
                .Where(x => model.Name == null | x.Name.Contains(model.Name))
                .Where(x => model.PhoneNumber == null | x.PhoneNumber.Contains(model.PhoneNumber))
                .Where(x => model.Address == null | x.Address.Contains(model.Address))
                .Where(x => model.EmailAddress == null | x.EmailAddress.Contains(model.EmailAddress))
                .Where(x => model.Status==Status.ALL|x.Status == (int)model.Status)
                .Select(x => new BusinessViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    EmailAddress = x.EmailAddress,
                    PhoneNumber = x.PhoneNumber,
                    Status = x.Status
                })
                .OrderBy(x => x.Name)
                .Skip(model.AmountItem * ((model.Page!=0)?(model.Page-1):model.Page))
                .Take((model.Page!=0)? model.AmountItem: _unitOfWork.BusinessRepository.Query().Count())
                .ToListAsync();
            return businesses;
        }

        public async Task UpdateBusiness(Guid id, BusinessViewModel model)
        {
            var business = new Data.Models.Business()
            {
                Id = id,
                Name = model.Name,
                Address = model.Address,
                EmailAddress = model.EmailAddress,
                PhoneNumber = model.PhoneNumber,
                Status = model.Status,
                VnpTmnCode= "104S9O6F",
                VnpHashSecret= "WAIHCILWTDOAERGSTKMUYIRDGOCROIHW"
            };
            _unitOfWork.BusinessRepository.Update(business);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<BusinessPaymentModel>> GetPayment(PaymentSearchModel model)
        {
            var businesses = await _unitOfWork.BusinessRepository.Query()
                .Select(x => new BusinessPaymentModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    EmailAddress = x.EmailAddress,
                    PhoneNumber = x.PhoneNumber,
                    Status = x.Status,
                    VnpHashSecret = x.VnpHashSecret,
                    VnpTmnCode = x.VnpTmnCode,
                    BusinessTours = _unitOfWork.BusinessTourRepository.Query()
                    .Where(y => y.IdBusiness.Equals(x.Id))
                    .Select(z => new BusinessTourPaymentModel()
                    {
                        Id=z.Id,
                        IdBusiness=z.IdBusiness,
                        IdTour=z.IdTour,
                        Status=z.Status,
                        Tour= _unitOfWork.TourRepository.Query()
                        .Where(t=>t.Id.Equals(z.IdTour))
                        .Select(t=> new Tour() 
                        {
                            Id=t.Id, 
                            Descriptions=t.Descriptions,
                            Title=t.Title, 
                            ImageLink= t.ImageLink, 
                            Status=t.Status 
                        }).FirstOrDefault(),
                        Trips= _unitOfWork.TripRepository.Query()
                .Where(t=> t.IdBusinessTour.Equals(z.Id))
                .Where(t=> t.Time.Year.Equals(model.DateTime.Year) && t.Time.Month.Equals(model.DateTime.Month))
                .Select(t => new TripPaymentModel()
                {
                    Id = t.Id,
                    Time = t.Time,
                    IdBusinessTour = t.IdBusinessTour,
                    IdVehicle = t.IdVehicle,
                    Status = t.Status,
                    AmountTicket = t.AmountTicket,
                    Tickets = _unitOfWork.TicketRepository.Query()
                .Where(v=> v.IdTrip.Equals(t.Id))
                .Select(v => new Ticket
                {
                    Id = v.Id,
                    Price = v.Price,
                    IdOrder = v.IdOrder,
                    IdTicketType = v.IdTicketType,
                    IdTrip = v.IdTrip,
                    NameCustomer = v.NameCustomer,
                    Phone = v.Phone,
                    Status = v.Status
                }).ToList()
        })
                .OrderBy(t => t.Time)
                .ToList()
        }).OrderBy(z => z.IdTour).ToList()
                })
                .OrderBy(x => x.Name)
                .Skip(model.AmountItem * ((model.Page != 0) ? (model.Page - 1) : model.Page))
                .Take((model.Page != 0) ? model.AmountItem : _unitOfWork.BusinessRepository.Query().Count())
                .ToListAsync();
            foreach(BusinessPaymentModel business in businesses)
            {
                double businessTotalPrice = 0;
                foreach(BusinessTourPaymentModel businessTour in business.BusinessTours)
                {
                    double businessTourTotalPrice = 0;
                    foreach(TripPaymentModel trip in businessTour.Trips)
                    {
                        double tripTotalPrice = 0;
                        foreach(Ticket ticket in trip.Tickets)
                        {
                            tripTotalPrice += ticket.Price;
                        }
                        trip.TotalPrice = tripTotalPrice;
                        businessTourTotalPrice += trip.TotalPrice;
                    }
                    businessTour.TotalPrice = businessTourTotalPrice;
                    businessTotalPrice += businessTour.TotalPrice;
                }
                business.TotalPrice = businessTotalPrice;
            }
            return businesses;
        }

    }
}
