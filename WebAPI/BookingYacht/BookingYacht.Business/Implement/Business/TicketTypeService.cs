﻿using System;
using BookingYacht.Business.Interfaces.Business;
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

namespace BookingYacht.Business.Implement.Business
{
    public class TicketTypeService : BaseService, ITicketTypeService
    {
        public TicketTypeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public async Task<Guid> AddTicketType(TicketTypeViewModel model)
        {
            var ticketType = new TicketType()
            {
                Id = model.Id,
                Price = model.Price,
                CommissionFeePercentage = model.CommissionFeePercentage,
                ServiceFeePercentage = model.ServiceFeePercentage,
                IdBusinessTour = model.IdBusinessTour,
                Status = model.Status
            };
            ticketType.Status = (int)Status.WAITING;
            await _unitOfWork.TicketTypeRepository.Add(ticketType);
            await _unitOfWork.SaveChangesAsync();
            return ticketType.Id;
        }

        public async Task DeleteTicketType(Guid id)
        {
            var ticketType = await _unitOfWork.TicketTypeRepository.Query()
                .Where(x => x.Id.Equals(id))
                .Select(x => new TicketType()
                {
                    Id = x.Id,
                    Price = x.Price,
                    ServiceFeePercentage = x.ServiceFeePercentage,
                    IdBusinessTour = x.IdBusinessTour,
                    Status = x.Status
                }).FirstOrDefaultAsync();
            ticketType.Status = (int)Status.DISABLE;
            _unitOfWork.TicketTypeRepository.Update(ticketType);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<TicketTypeViewModel> GetTicketType(Guid id)
        {
            var ticketType = await _unitOfWork.TicketTypeRepository.Query()
                .Where(x => x.Id.Equals(id))
                .Select(x => new TicketTypeViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    CommissionFeePercentage = x.CommissionFeePercentage,
                    ServiceFeePercentage = x.ServiceFeePercentage,
                    IdBusinessTour = x.IdBusinessTour,
                    Status = x.Status
                }).FirstOrDefaultAsync();
            return ticketType;
        }

        public async Task<List<TicketTypeViewModel>> SearchTicketTypes(TicketTypeSearchModel model = null)
        {
            if (model == null)
            {
                model = new TicketTypeSearchModel();
            }
            var ticketType = await _unitOfWork.TicketTypeRepository.Query()
                .Where(x => model.Price == null | x.Price==model.Price)
                .Where(x => model.CommissionFeePercentage == null | x.CommissionFeePercentage == model.CommissionFeePercentage)
                .Where(x => model.ServiceFeePercentage == null | x.ServiceFeePercentage==model.ServiceFeePercentage)
                .Where(x => model.IdBusinessTour == null | x.IdBusinessTour.Equals(model.IdBusinessTour))
                .Where(x => model.Status == Status.ALL | x.Status == (int)model.Status)
                .Select(x => new TicketTypeViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    CommissionFeePercentage = x.CommissionFeePercentage,
                    ServiceFeePercentage = x.ServiceFeePercentage,
                    IdBusinessTour = x.IdBusinessTour,
                    Status = x.Status
                })
                .OrderBy(x => x.Id)
                .Skip(model.AmountItem * ((model.Page != 0) ? (model.Page - 1) : model.Page))
                .Take((model.Page != 0) ? model.AmountItem : _unitOfWork.BusinessRepository.Query().Count())
                .ToListAsync();
            return ticketType;
        }

        public async Task UpdateTicketType(Guid id, TicketTypeViewModel model)
        {
            var ticketType = new TicketType()
            {
                Id = id,
                Name = model.Name,
                Price = model.Price,
                ServiceFeePercentage = model.ServiceFeePercentage,
                IdBusinessTour = model.IdBusinessTour,
                Status = model.Status
            };
            _unitOfWork.TicketTypeRepository.Update(ticketType);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
